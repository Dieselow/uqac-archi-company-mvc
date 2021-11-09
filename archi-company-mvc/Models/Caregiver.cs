using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Caregiver : Employee
    {
        private string _licenceNumber;
        private List<Patient> _patients;

        [Display(Name = "Licence Number")]
        public string LicenceNumber
        {
            get
            {
                return this._licenceNumber;
            }
            set
            {
                this._licenceNumber = value;
            }
        }
        public virtual List<Patient> Patients
        {
            get
            {
                return this._patients;
            }
            set
            {
                this._patients = value;
            }
        }

    }
}