namespace archi_company_mvc.Models
{
    public class SearchRequest
    {
        public string Term { get; set; }
        public string BaseUrl { get; set; }

        public SearchRequest(string term, string baseUrl)
        {
            Term = term;
            BaseUrl = baseUrl;
        }
    }
}