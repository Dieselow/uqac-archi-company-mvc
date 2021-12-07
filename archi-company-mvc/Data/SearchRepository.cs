using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Controllers;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<List<Entity>> searchEntities(string search)
        {
            List<Entity> tables = await _context.Entities.Where(entity => entity.tags.Contains(search)).ToListAsync();
            return tables;
        }

        public async Task<List<EntityAutocompleteResponse>> GetAutocompleteEntities(string search, string baseUrl)
        {
            List<EntityAutocompleteResponse> responses = new List<EntityAutocompleteResponse>();
            List<Entity> entities = await _context.Entities.Where(entity => entity.tags.Contains(search)).Take(5)
                .ToListAsync();
            foreach (var entity in entities)
            {
             responses.Add(new EntityAutocompleteResponse(entity.EntityId, baseUrl + "/" + entity.ControllerName + "/Details/" + entity.EntityId,entity.keyEntityName));   
            }

            return responses;
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