using System;
using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{   [Display(Name = "Equipment Type")]  
    public class EquipmentType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}