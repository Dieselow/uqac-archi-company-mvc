using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public class HealthFile
    {
        [Key]
        public int Id { get; set; }

        public string Medications { get; set; }

        [Display(Name = "Chronic Conditions")]
        public string ChronicConditions { get; set; }

        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

    }
}