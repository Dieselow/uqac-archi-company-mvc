using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace archi_company_mvc.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Searchable(true)]
        public string Name {get; set;}

        [DataType(DataType.Date)]
        [Searchable]
        public DateTime RequestDate { get; set; }

        [NotMapped]
        public ICollection<int> ConsumablesIds { get; set; }

        [Display(Name="Consumable")]
        public virtual ICollection<Consumable> Consumables {get; set;}

    }
}