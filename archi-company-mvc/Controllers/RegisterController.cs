using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Constants;
using archi_company_mvc.Data;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace archi_company_mvc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public RegisterController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public async Task<ActionResult> RegisterPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Patient?.FirstOrDefault(s => s.Email == patient.Email ||
                s.UserName==patient.UserName);
                if (check == null)
                {
                    patient.PasswordHash = _userManager.PasswordHasher.HashPassword(patient, patient.Password);
                    await _userManager.CreateAsync(patient, patient.Password);
                    await _context.Entities.AddAsync(new Entity(patient.Id, "AspNetUsers", patient,
                        patient.GetController(), patient.GetType().Name + ": " + patient.UserName));
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(patient, Roles.Patient.ToString());
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }

                ViewBag.error = "Email or username not valid";
                return View();
            }

            return View();
        }

        //POST: Register Secretary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterSecretary(Secretary secretary)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Secretary.FirstOrDefault(s => s.Email == secretary.Email);
                if (check == null)
                {
                    secretary.PasswordHash = _userManager.PasswordHasher.HashPassword(secretary, secretary.Password);
                    await _userManager.CreateAsync(secretary, secretary.Password);
                    await _context.Entities.AddAsync(new Entity(secretary.Id, "AspNetUsers", secretary,
                        secretary.GetController(), secretary.GetType().Name + ": " + secretary.UserName));
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(secretary, Roles.Secretary.ToString());
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
        public async Task<ActionResult> RegisterCaregiver(Caregiver caregiver)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Caregiver.FirstOrDefault(s => s.Email == caregiver.Email);
                if (check == null)
                {
                    caregiver.PasswordHash = _userManager.PasswordHasher.HashPassword(caregiver, caregiver.Password);
                    await _userManager.CreateAsync(caregiver, caregiver.Password);
                    await _context.Entities.AddAsync(new Entity(caregiver.Id, "AspNetUsers", caregiver,
                        caregiver.GetController(), caregiver.GetType().Name + ": " + caregiver.UserName));
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(caregiver, Roles.Caregiver.ToString());
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