using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public class Consumable
    {
        [Key]
        public int Id { get; set; }

        [Searchable]
        public int Quantity { get; set; }

        [Searchable]
        public int Treshold {get; set; }

        public int ConsumableTypeId {get; set;}

        [Display(Name="Name")]
        [Searchable(true)]
        public virtual ConsumableType ConsumableType {get; set;}

        public int? TicketId {get; set;}

        public virtual Ticket Ticket{get; set;}

    }
}