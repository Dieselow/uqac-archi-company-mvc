namespace archi_company_mvc.Controllers
{
    public class EntityAutocompleteResponse
    {
        public string Id;
        public string ControllerName;
        public string DetailsAction;

        public EntityAutocompleteResponse(string id, string controllerName, string detailsAction)
        {
            Id = id;
            ControllerName = controllerName;
            DetailsAction = detailsAction;
        }
    }
}