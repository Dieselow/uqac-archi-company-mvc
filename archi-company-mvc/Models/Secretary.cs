using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Secretary : Employee
    {
        private List<Caregiver> _caregivers = new List<Caregiver>();
        private List<Patient> _patients = new List<Patient>();

        public List<Caregiver> Caregivers
        {
            get
            {
                return this._caregivers;
            }
            set
            {
                this._caregivers = value;
            }
        }
        public List<Patient> Patients
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