using System;
using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public class Entity : IEntity
    {
        [Key]
        public string Id { get; set; }
        public string tags { get; set; }
        public string EntityId { get; set; }
        public string TableName { get; set; }
        private readonly string[] _userPropertiesNames = { "FirstName", "LastName", "UserName", "Email" };

        public Entity(string entityId, string tableName, Object entityToSaveTags)
        {
            EntityId = entityId;
            TableName = tableName;
            setEntitySearchTags(entityToSaveTags);
        }

        public void setEntitySearchTags(Object o)
        {
            if (o.GetType() == typeof(User))
            {
             computeUserTags(o);
             return;
            }
            foreach (var property in o.GetType().GetProperties())
            {
                if (property.Name == "Id")
                {
                    continue;
                }

                tags += property.Name + "=" + property.GetValue(this, null);
            }
        }

        private void computeUserTags(object user)
        {
            foreach (var property in _userPropertiesNames)
            {
                var propertyValue = user.GetType().GetProperty(property)?.GetValue(this);
                if (propertyValue != null)
                {
                    tags += property + "=" + propertyValue + ";";
                }
            }

            tags += "Discriminator=" + GetType().Name + ";";
        }
    }
    
}