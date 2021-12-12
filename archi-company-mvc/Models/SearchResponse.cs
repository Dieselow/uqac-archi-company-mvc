using System.Collections.Generic;
using archi_company_mvc.Controllers;

namespace archi_company_mvc.Models
{
    public class SearchResponse
    {
        public List<EntityAutocompleteResponse> Responses;
        public int count;
    }
}