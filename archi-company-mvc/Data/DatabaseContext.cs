using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<archi_company_mvc.Models.Equipment> Equipment { get; set; }
        
        public DbSet<archi_company_mvc.Models.Room> Room { get; set; }

        public DbSet<archi_company_mvc.Models.EquipmentType> EquipmentType { get; set; }

        public DbSet<archi_company_mvc.Models.Ticket> Ticket { get; set; }

        public DbSet<archi_company_mvc.Models.Patient> Patient { get; set; }

        public DbSet<archi_company_mvc.Models.Caregiver> Caregiver { get; set; }

        public DbSet<archi_company_mvc.Models.Secretary> Secretary { get; set; }

        public DbSet<archi_company_mvc.Models.HealthFile> HealthFile { get; set; }
    }
