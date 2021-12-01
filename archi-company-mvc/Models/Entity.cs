using System;
using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
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

        public Entity(int id, string tags, string entityId, string tableName)
        {
            Id = id;
            this.tags = tags;
            EntityId = entityId;
            TableName = tableName;
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

                if (property.GetValue(o) != null)
                {
                    tags += property.Name + "=" + property.GetValue(o) + ";";   
                }
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