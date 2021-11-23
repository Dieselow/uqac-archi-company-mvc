using Microsoft.AspNetCore.Identity;

namespace archi_company_mvc.Models
{
    public class UserEntity : IdentityUser, IEntity
    {
        private readonly string[] _userPropertiesNames = { "FirstName", "LastName", "UserName", "Email" };
        public string tags { get; set; }

        public void setEntitySearchTags()
        {
            foreach (var property in _userPropertiesNames)
            {
                var propertyValue = GetType().GetProperty(property)?.GetValue(this);
                if (propertyValue != null)
                {
                    tags += property + "=" + propertyValue + ";";
                }
            }

            tags += "Discriminator=" + GetType().Name + ";";
        }
    }
}