using Newtonsoft.Json;

namespace archi_company_mvc.Controllers
{
    public class EntityAutocompleteResponse
    {
        public string Id;
        public string EntityUrl;
        public string Name;

        public EntityAutocompleteResponse(string id, string entityUrl, string name)
        {
            Id = id;
            EntityUrl = entityUrl;
            Name = name;
        }
    }
}