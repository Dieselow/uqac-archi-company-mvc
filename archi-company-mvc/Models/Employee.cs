using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Employee : User
    {
        private float _salary;
        private string _workSchedule;
        private DateTime _employmentDate;
        public float Salary
        {
            get
            {
                return this._salary;
            }
            set
            {
                this._salary = value;
            }
        }

        [Display(Name = "Work Schedule")]
        public string WorkSchedule
        {
            get
            {
                return this._workSchedule;
            }
            set
            {
                this._workSchedule = value;
            }
        }

        [Display(Name="Employment Date")]
        [DataType(DataType.Date)]
        public DateTime EmploymentDate
        {
            get
            {
                return this._employmentDate;
            }
            set
            {
                this._employmentDate = value;
            }
        }

    }
}