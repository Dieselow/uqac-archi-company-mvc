using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Consumable: Entity
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int Treshold {get; set; }

        public int ConsumableTypeId {get; set;}

        public virtual ConsumableType ConsumableType {get; set;}

    }
}