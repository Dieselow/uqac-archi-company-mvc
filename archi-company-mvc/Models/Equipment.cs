using System;
using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Installation Date")]
        [Searchable]
        public DateTime InstallationDate { get; set; }

        public int EquipmentTypeId {get; set;}

        [Display(Name="Name")]
        [Searchable(true)]
        public virtual EquipmentType EquipmentType {get; set;}

        public int RoomId { get; set; }

        [Searchable]
        public virtual Room Room { get; set; }
        public string GetController()
        {
            return "Equipments";
        }
    }
}