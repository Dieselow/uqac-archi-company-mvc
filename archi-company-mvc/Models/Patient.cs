using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class Patient : User
    {
        private int? _primaryDoctorId;
        private Caregiver _primaryDoctor;
        private int? _healthFileId;
        private HealthFile _healthFile;

        public int? PrimaryDoctorId
        {
            get
            {
                return this._primaryDoctorId;
            }
            set
            {
                this._primaryDoctorId = value;
            }
        }

        public virtual Caregiver PrimaryDoctor
        {
            get
            {
                return this._primaryDoctor;
            }
            set
            {
                this._primaryDoctor = value;
            }
        }
        
        public int? HealthFileId
        {
            get
            {
                return this._healthFileId;
            }
            set
            {
                this._healthFileId = value;
            }
        }
        
        public virtual HealthFile HealthFile
        {
            get
            {
                return this._healthFile;
            }
            set
            {
                this._healthFile = value;
            }
        }

    }
}