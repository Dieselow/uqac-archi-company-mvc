using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Employee : User
    {

        public float Salary { get; set; }

        [Display(Name = "Work Schedule")]
        public string WorkSchedule {get; set; }

        [Display(Name="Employment Date")]
        [DataType(DataType.Date)]
        public DateTime EmploymentDate { get; set; }
    }
}