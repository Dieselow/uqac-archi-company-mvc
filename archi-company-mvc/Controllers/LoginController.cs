using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using archi_company_mvc.Helpers;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace archi_company_mvc.Controllers
{
    public class LoginController: Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email,string password)
        {
            if (ModelState.IsValid)
            {
                var fPassword = SecurityHelper.GetMd5(password);
                var user =_context.Users.FirstOrDefault(s => s.Email == email);
                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, password,true,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName:user.GetDefaultAction(),user.GetController());   
                    }
                    return RedirectToAction("Login");
                }
                ViewBag.error = "Login failed";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}