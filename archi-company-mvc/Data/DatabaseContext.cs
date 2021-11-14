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

        public DbSet<Equipment> Equipment { get; set; }
        
        public DbSet<Room> Room { get; set; }

        public DbSet<EquipmentType> EquipmentType { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<Patient> Patient { get; set; }

        public DbSet<Caregiver> Caregiver { get; set; }

        public DbSet<Secretary> Secretary { get; set; }

        public DbSet<HealthFile> HealthFile { get; set; }

        public DbSet<User> Users { get; set; }
    }
