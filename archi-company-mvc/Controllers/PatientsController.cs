using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Constants;
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
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.GetType() == typeof(Caregiver) )
            {
                var doctorPatients = _context.Patient.Include(p => p.HealthFile).Include(p => p.PrimaryDoctor).Where(p => p.PrimaryDoctorId == user.Id);
                return View(await doctorPatients.ToListAsync());
            }
            var databaseContext = _context.Patient.Include(p => p.HealthFile).Include(p => p.PrimaryDoctor);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Patients/Details/5
        [Authorize(Roles = "Secretary,Admin,Patient")]
        public async Task<IActionResult> Details(string? id)
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
            ViewData["HealthFileId"] = new SelectList(_context.Set<HealthFile>(), "Id", "ChronicConditions");
            //ViewData["HealthFileId"] = new SelectList(_context.Set<HealthFile>(), "Id", "Patient", patient.HealthFileId);
            ViewData["PrimaryDoctorId"] = new SelectList(_context.Set<Caregiver>(), "Id", "LastName");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary,Admin")]
        public async Task<IActionResult> Create([Bind("PrimaryDoctorId,HealthFileId,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HealthFileId"] = new SelectList(_context.Set<HealthFile>(), "Id", "ChronicConditions", patient.HealthFileId);
            //ViewData["HealthFileId"] = new SelectList(_context.Set<HealthFile>(), "Id", "Patient", patient.HealthFileId);
            ViewData["PrimaryDoctorId"] = new SelectList(_context.Set<Caregiver>(), "Id", "LastName", patient.PrimaryDoctorId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                return View(await _context.Patient.FindAsync(user.Id));
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["HealthFileId"] = new SelectList(_context.Set<HealthFile>(), "Id", "ChronicConditions", patient.HealthFileId);
            ViewData["PrimaryDoctorId"] = new SelectList(_context.Set<Caregiver>(), "Id", "LastName", patient.PrimaryDoctorId);
            ViewBag.PrimaryDoctorId = new SelectList(_context.Caregiver.ToList(), "Id", "LastName", patient.PrimaryDoctorId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary,Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("PrimaryDoctorId,HealthFileId,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HealthFileId"] = new SelectList(_context.Set<HealthFile>(), "Id", "Id", patient.HealthFileId);
            ViewData["PrimaryDoctorId"] = new SelectList(_context.Set<Caregiver>(), "Id", "Id", patient.PrimaryDoctorId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "Secretary,Admin")]
        public async Task<IActionResult> Delete(string? id)
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
