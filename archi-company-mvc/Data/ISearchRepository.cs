using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using archi_company_mvc.Controllers;
using archi_company_mvc.Models;

namespace archi_company_mvc.Data
{
    public interface ISearchRepository: IDisposable
    {
        Task<List<Entity>> searchEntities(String search);
        Task<List<EntityAutocompleteResponse>> getAutocompleteEntities(String search);
    }
}