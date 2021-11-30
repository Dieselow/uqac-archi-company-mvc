using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Searchable(true)]
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}