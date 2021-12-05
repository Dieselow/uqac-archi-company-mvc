using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using archi_company_mvc.Models;

namespace archi_company_mvc.ViewComponents
{
    public class SearchBar : ViewComponent
    {

        public SearchBar()
        {
        }

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