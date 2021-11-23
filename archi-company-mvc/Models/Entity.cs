namespace archi_company_mvc.Models
{
    public class Entity: IEntity
    {
        public string tags { get; set; }
        public void setEntitySearchTags()
        {
            foreach (var property in GetType().GetProperties())
            {
                if (property.Name == "Id")
                {
                    continue;
                }
                tags += property.Name + "=" + property.GetValue(this, null);
            }
        }
    }
}