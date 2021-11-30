
namespace archi_company_mvc.Models
{
    [System.AttributeUsage(System.AttributeTargets.Property)] 
    public class SearchableAttribute : System.Attribute  
    {  
        public bool DefaultSearch;   
    
        public SearchableAttribute(bool DefaultSearch)  
        {  
            this.DefaultSearch = DefaultSearch;
        }

        public SearchableAttribute()  
        {  
            this.DefaultSearch = false;
        }
    }
}