using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    [Display(Name = "Equipment Type")]  
    public class EquipmentType
    {
        [Key]
        public int Id { get; set; }

        [Searchable(true)]
        public string Name { get; set; }
    }
}