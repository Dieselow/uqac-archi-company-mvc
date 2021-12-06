namespace archi_company_mvc.Controllers
{
    public class EntityAutocompleteResponse
    {
        public string Id;
        public string EntityUrl;

        public EntityAutocompleteResponse(string id, string entityUrl)
        {
            Id = id;
            EntityUrl = entityUrl;
        }
    }
}