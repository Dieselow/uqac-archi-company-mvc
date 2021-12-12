using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using archi_company_mvc.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace archi_company_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("http://localhost:5003/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
       
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Search(string term)
        {
            var request = HttpContext.Request;
            var _baseURL = $"{request.Scheme}://{request.Host}";
            List<EntityAutocompleteResponse> responses = new List<EntityAutocompleteResponse>();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/Search", new SearchRequest(term,_baseURL));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"[", "[").Replace("]\"", "]").Replace("\\", ""); 
                responses = JsonConvert.DeserializeObject<List<EntityAutocompleteResponse>>(content);
            }

            
            return Json(new { data = responses });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}