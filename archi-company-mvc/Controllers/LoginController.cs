using System.Linq;
using System.Security.Cryptography;
using System.Text;
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


                var fPassword = GetMd5(password);
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
        private static string GetMd5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}