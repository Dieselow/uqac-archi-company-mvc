using System.ComponentModel.DataAnnotations;

namespace archi_company_mvc.Models
{
    public class Patient : User
    {
        public string? PrimaryDoctorId { get; set; }
        
        [Display(Name = "Primary Doctor")]
        public virtual Caregiver? PrimaryDoctor { get; set; }
        
        public int? HealthFileId { get; set; }

        [Display(Name = "Health File")]
        public virtual HealthFile HealthFile { get; set; }

        public override string GetController()
        {
            return "Patients";
        }

        public override string GetDefaultAction()
        {
            return "Details";
        }
        public override string ToString()
        {
            return $"{this.LastName}-{this.FirstName}";
        }

    }
}