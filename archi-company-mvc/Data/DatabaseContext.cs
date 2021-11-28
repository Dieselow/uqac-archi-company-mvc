using System;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace archi_company_mvc.Data
{
    public class DatabaseContext : IdentityDbContext<User>
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


        public DbSet<Consumable> Consumable { get; set; }


        public DbSet<ConsumableType> ConsumableType { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
