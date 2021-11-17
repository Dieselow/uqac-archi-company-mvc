using System.Threading.Tasks;
using archi_company_mvc.Constants;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;

namespace archi_company_mvc.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Secretary.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Caregiver.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Patient.ToString()));
        }
        
        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Products");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
        }
    }
}