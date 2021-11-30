using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Caregiver : Employee
    {

        [Display(Name = "Licence Number")]
        [Searchable]
        public string LicenceNumber {get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public override string GetDefaultAction()
        {
            return "Index";
        }

        public override string GetController()
        {
            return "Patients";
        }
    }
}