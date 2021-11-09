using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace archi_company_mvc.Models
{
    public class User
    {
        [Key]
        private int _id;
        private string _username;
        private string _firstname;
        private string _lastname;
        private DateTime _dateOfBirth;
        private string _email;
        private string _password;
        private string _address;
        private string _phoneNumber;

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        [Display(Name = "First Name")]
        public string FirstName
        {
            get
            {
                return this._firstname;
            }
            set
            {
                this._firstname = value;
            }
        }

        [Display(Name="Last Name")]
        public string LastName
        {
            get
            {
                return this._lastname;
            }
            set
            {
                this._lastname = value;
            }
        }

        [Display(Name="Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth
        {
            get
            {
                return this._dateOfBirth;
            }
            set
            {
                this._dateOfBirth = value;
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

        [Display(Name="Phone Number")]
        public string PhoneNumber
        {
            get
            {
                return this._phoneNumber;
            }
            set
            {
                this._phoneNumber = value;
            }
        }

    }
}