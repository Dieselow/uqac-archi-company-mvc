using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Ticket: Entity
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        public virtual ICollection<Consumable> Consumables {get; set;}

    }
}