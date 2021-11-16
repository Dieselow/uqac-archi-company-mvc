using System.Linq;
using System.Security.Cryptography;
using System.Text;
using archi_company_mvc.Helpers;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace archi_company_mvc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DatabaseContext _context;

        public RegisterController(DatabaseContext context)
        {
            _context = context;
        }

        public ActionResult RegisterPatient()
        {
            return View();
        }

        public ActionResult RegisterSecretary()
        {
            return View();
        }

        public ActionResult RegisterCaregiver()
        {
            return View();
        }

        //POST: Register Patient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Patient.FirstOrDefault(s => s.Email == patient.Email);
                if (check == null)
                {
                    patient.Password = SecurityHelper.GetMd5(patient.Password);
                    _context.Patient.Add(patient);
                    _context.SaveChanges();
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }

                ViewBag.error = "Email already exists";
                return View();
            }

            return View();
        }

        //POST: Register Secretary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSecretary(Secretary secretary)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Secretary.FirstOrDefault(s => s.Email == secretary.Email);
                if (check == null)
                {
                    secretary.Password = SecurityHelper.GetMd5(secretary.Password);
                    _context.Secretary.Add(secretary);
                    _context.SaveChanges();
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }

                ViewBag.error = "Email already exists";
                return View();
            }

            return View();
        }

        //POST: Register Caregiver
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCaregiver(Caregiver caregiver)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Caregiver.FirstOrDefault(s => s.Email == caregiver.Email);
                if (check == null)
                {
                    caregiver.Password = SecurityHelper.GetMd5(caregiver.Password);
                    _context.Caregiver.Add(caregiver);
                    _context.SaveChanges();
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }

                ViewBag.error = "Email already exists";
                return View();
            }

            return View();
        }

        //Logout
        /** public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }**/
    }
}