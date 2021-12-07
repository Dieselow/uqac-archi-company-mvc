
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.Extensions.Logging;
using archi_company_mvc.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using Microsoft.AspNetCore.Mvc;

namespace archi_company_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ISearchRepository _searchRepository;

        public HomeController(ILogger<HomeController> logger, ISearchRepository searchRepository)
        {
            _logger = logger;
            _searchRepository = searchRepository;
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
            List<EntityAutocompleteResponse> responses = await _searchRepository.GetAutocompleteEntities(term,_baseURL);
            return Json(new { data = responses });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}