using System;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace archi_company_mvc.Controllers
{
    public class PatientsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public PatientsController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Patients
        [Authorize(Roles = "Secretary,Admin,Caregiver")]
        public async Task<IActionResult> Index(string searchString, string searchProperty)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var patients = from m in _context.Patient
                            select m;

            ViewData["Attributes"] = SelectListUtils.CreatePropertiesSelectListForType("Patient", String.IsNullOrEmpty(searchProperty) ? "" : searchProperty);
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchProperty))
            {
                patients = SelectListUtils.DynamicWhere(patients, searchProperty, searchString);
                ViewBag.Search = searchString;
            } else {
                ViewBag.Search = "";
            }

            if (user.GetType() == typeof(Caregiver))
            {   var doctorPatients = patients.Include(p => p.HealthFile).Include(p => p.PrimaryDoctor)
                    .Where(p => p.PrimaryDoctorId == user.Id);
                return View(await doctorPatients.ToListAsync());
            }

            var databaseContext = patients.Include(p => p.HealthFile).Include(p => p.PrimaryDoctor);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Patients/Details/5
        [Authorize(Roles = "Secretary,Admin,Patient,Caregiver")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.HealthFile)
                .Include(p => p.PrimaryDoctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        [Authorize(Roles = "Secretary,Admin")]
        public IActionResult Create()
        {
            var request = from x in _context.Caregiver select new {x.Id,  x.FirstName, x.LastName, FinalName = x.FirstName + " " + x.LastName}; 
            ViewData["PrimaryDoctorId"] = new SelectList(request, "Id", "FinalName");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary,Admin")]
        public async Task<IActionResult> Create(
            [Bind(
                "PrimaryDoctorId,HealthFileId,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")]
            Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var request = from x in _context.Caregiver select new {x.Id,  x.FirstName, x.LastName, FinalName = x.FirstName + " " + x.LastName}; 
            ViewData["PrimaryDoctorId"] = new SelectList(request, "Id", "FinalName", patient.PrimaryDoctorId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "Secretary,Admin,Patient,Caregiver")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                return View(await _context.Patient.FindAsync(user.Id));
            }

            var patient = await _context.Patient.Include(p => p.HealthFile).FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            var request = from x in _context.Caregiver select new {x.Id,  x.FirstName, x.LastName, FinalName = x.FirstName + " " + x.LastName}; 
            ViewData["PrimaryDoctorId"] = new SelectList(request, "Id", "FinalName", patient.PrimaryDoctorId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary,Admin,Patient,Caregiver")]
        public async Task<IActionResult> Edit(string id, [Bind("PrimaryDoctorId,HealthFileId,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            var currentPatient = await _context.Patient.FindAsync(patient.Id);
            currentPatient.FirstName = patient.FirstName;
            currentPatient.LastName = patient.LastName;
            currentPatient.Address = patient.Address;
            currentPatient.PrimaryDoctorId = patient.PrimaryDoctorId;
            currentPatient.DateOfBirth = patient.DateOfBirth;
            currentPatient.PhoneNumber = patient.PhoneNumber;
            var result = await _userManager.UpdateAsync(currentPatient);
            if (result.Succeeded)
            {
                ViewData["PrimaryDoctorId"] =
                    new SelectList(_context.Set<Caregiver>(), "Id", "Id", patient.PrimaryDoctorId);
                return RedirectToAction(nameof(Edit));
            }
            var request = from x in _context.Caregiver select new {x.Id,  x.FirstName, x.LastName, FinalName = x.FirstName + " " + x.LastName}; 
            ViewData["PrimaryDoctorId"] = new SelectList(request, "Id", "FinalName", patient.PrimaryDoctorId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "Secretary,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.HealthFile)
                .Include(p => p.PrimaryDoctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary,Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var patient = await _context.Patient.FindAsync(id);
            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(string id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}