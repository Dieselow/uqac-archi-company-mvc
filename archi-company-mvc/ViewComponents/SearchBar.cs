using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using archi_company_mvc.Models;

namespace archi_company_mvc.ViewComponents
{
    public class SearchBar : ViewComponent
    {

        public IViewComponentResult Invoke(IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> properties, string searchString)
        {   
            var SearchBarViewModel = new SearchBarViewModel  
            {  
                Properties = properties,  
                SearchString = searchString  
            }; 
            return View(SearchBarViewModel);
        }

    }
}