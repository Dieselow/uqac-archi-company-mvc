using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using archi_company_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace archi_company_mvc.Data
{
    public class SearchRepository: ISearchRepository,IDisposable
    {
        private readonly DatabaseContext _context;

        public SearchRepository(DatabaseContext context)
        {
            _context = context;
        }
        public List<Entity> searchEntities(string search)
        {
            IList<string> tables = ListTables();
            throw new NotImplementedException();
        }
        private IList<string> ListTables()
        {
            return _context.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Distinct()
                .ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}