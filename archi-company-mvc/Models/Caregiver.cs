using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Caregiver : Employee
    {

        [Display(Name = "Licence Number")]
        public string LicenceNumber {get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}