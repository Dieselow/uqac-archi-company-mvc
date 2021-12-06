using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Constants;
using archi_company_mvc.Data;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;

namespace archi_company_mvc.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedSuperAdminAsync(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new User
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }
        
        public static async Task SeedUsers(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, DatabaseContext context)
        {
            var defaultPatient = new Patient
            {
                UserName = "Patient",
                Email = "patient@patient.com",
                EmailConfirmed = true,
                FirstName = "Patient",
                LastName ="Patient"
            };
            var defaultSecretary = new Secretary
            {
                UserName = "Secretary",
                Email = "secretary@secretary.com",
                EmailConfirmed = true,
                FirstName = "Secretary",
                LastName ="Secretary"
            };
            var defaultCaregiver = new Caregiver
            {
                UserName = "Caregiver",
                Email = "caregiver@caregiver.com",
                EmailConfirmed = true,
                FirstName = "Caregiver",
                LastName ="Caregiver"
            };
            await userManager.CreateAsync(defaultCaregiver, "Password1*");
            await userManager.CreateAsync(defaultSecretary, "Password1*");
            await userManager.CreateAsync(defaultPatient, "Password1*");
            await userManager.AddToRoleAsync(defaultSecretary, Roles.Secretary.ToString());
            await userManager.AddToRoleAsync(defaultCaregiver, Roles.Caregiver.ToString());
            await userManager.AddToRoleAsync(defaultPatient, Roles.Patient.ToString());
            await context.Entities.AddAsync(new Entity(defaultCaregiver.Id, "AspNetUsers", defaultCaregiver,defaultCaregiver.GetController()));
            await context.Entities.AddAsync(new Entity(defaultPatient.Id, "AspNetUsers", defaultPatient,defaultPatient.GetController()));
            await context.Entities.AddAsync(new Entity(defaultSecretary.Id, "AspNetUsers", defaultSecretary,defaultSecretary.GetController()));
            await context.SaveChangesAsync();
        }
    }
}