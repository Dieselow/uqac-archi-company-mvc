using System;
using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public interface IEntity
    {
        public string tags { get; set; }
        public void setEntitySearchTags(Object o);
    }
}