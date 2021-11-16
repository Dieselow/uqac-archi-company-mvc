using System.Linq;
using System.Security.Cryptography;
using System.Text;
using archi_company_mvc.Data;
using archi_company_mvc.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace archi_company_mvc.Controllers
{
    public class LoginController: Controller
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email,string password)
        {
            if (ModelState.IsValid)
            {


                var fPassword = SecurityHelper.GetMd5(password);
                var data =_context.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(fPassword)).ToList();
                if(data.Capacity > 0)
                {
                    return RedirectToAction(actionName:"Index",controllerName:"Home");
                }
                ViewBag.error = "Login failed";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}