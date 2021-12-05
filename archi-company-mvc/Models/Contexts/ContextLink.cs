namespace archi_company_mvc.Models
{
    public class ContextLink
    {
        public string ControllerName { get; set;}

        public string ActionName { get; set;}
        
        public string Title { get; set; }

        public ContextLink(string controllerName, string actionName,string title) {
            ControllerName = controllerName;
            ActionName = actionName;
            Title = title;
        }
        
    }
}