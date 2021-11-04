using System;
using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime InstallationDate { get; set; }

        public int EquipmentTypeId {get; set;}

        public virtual EquipmentType EquipmentType {get; set;}

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

    }
}