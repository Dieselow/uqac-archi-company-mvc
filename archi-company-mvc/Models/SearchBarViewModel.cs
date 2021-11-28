using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class SearchBarViewModel
    {  
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Properties { get; set; }

        public string SearchString { get; set; }  
    }
}