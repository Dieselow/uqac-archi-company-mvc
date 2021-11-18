using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Patient : User
    {
        public string? PrimaryDoctorId { get; set; }
        public virtual Caregiver? PrimaryDoctor { get; set; }
        
        public int? HealthFileId { get; set; }

        [Display(Name = "Health File")]
        public virtual HealthFile HealthFile { get; set; }

        public override string GetDefaultAction()
        {
            return "Details/" + Id;
        }
        public override string ToString()
        {
            return $"{this.LastName}-{this.FirstName}";
        }

    }
}