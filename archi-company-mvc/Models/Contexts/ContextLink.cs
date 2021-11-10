using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;

namespace archi_company_mvc.Models
{
    public class ContextLink
    {
        public string LinkUrl { get; set;}

        public string LinkName { get; set;}

        public ContextLink(string Url, string Name) {
            LinkUrl = Url;
            LinkName = Name;
        }
        
    }
}