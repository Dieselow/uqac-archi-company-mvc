using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace archi_company_mvc.Models
{
    [Table("AspNetUsers")]
    public class User: IdentityUser
    {
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Display(Name="Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        public string Password { get; set; }

        public string Address { get; set; }

        public virtual string GetDefaultAction()
        {
            return "Index";
        }

        public virtual string GetController()
        {
            return GetType().Name;
        }
    }
}