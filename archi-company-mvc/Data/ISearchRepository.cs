using System;
using System.Collections.Generic;
using archi_company_mvc.Models;

namespace archi_company_mvc.Data
{
    public interface ISearchRepository: IDisposable
    {
        List<Entity> searchEntities(String search);
    }
}