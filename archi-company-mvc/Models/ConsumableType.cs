using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class ConsumableType
    {
        [Key]
        public int Id { get; set; }

        [Searchable(true)]
        public string Name { get; set; }

        [Searchable]
        public string Brand { get; set; }

        public virtual ICollection<Consumable> Consumables { get; set;}

    }
}