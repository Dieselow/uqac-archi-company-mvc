using System;
using System.Collections.Generic;
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
    public class HealthFilesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public HealthFilesController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HealthFiles
        [Authorize(Roles = "Caregiver")]
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.HealthFile.ToListAsync());
        }

        // GET: HealthFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthFile = await _context.HealthFile
                .FirstOrDefaultAsync(m => m.Id == id);
          /**  var currentCaregiver = _userManager.GetUserAsync(HttpContext.User);
            if (currentCaregiver.Id.ToString() == healthFile.Patient.PrimaryDoctorId)
            {
                
            }**/
            if (healthFile == null)
            {
                return NotFound();
            }

            return View(healthFile);
        }

        // GET: HealthFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Medications,ChronicConditions,EmergencyContact")] HealthFile healthFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(healthFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(healthFile);
        }

        // GET: HealthFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthFile = await _context.HealthFile.FindAsync(id);
            if (healthFile == null)
            {
                return NotFound();
            }
            return View(healthFile);
        }

        // POST: HealthFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Medications,ChronicConditions,EmergencyContact")] HealthFile healthFile)
        {
            if (id != healthFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(healthFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthFileExists(healthFile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }//int patientID=healthFile.Patient.Id; -- non fonctionnel.
                //return Redirect(Url.Action("Details", "Patients")+"/"+patientID); -- pour revenir directement sur les détails d'un patient.
                return Redirect(Url.Action("Index", "Patients"));
            }
            return View(healthFile);
        }

        // GET: HealthFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthFile = await _context.HealthFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthFile == null)
            {
                return NotFound();
            }

            return View(healthFile);
        }

        // POST: HealthFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthFile = await _context.HealthFile.FindAsync(id);
            _context.HealthFile.Remove(healthFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthFileExists(int id)
        {
            return _context.HealthFile.Any(e => e.Id == id);
        }
    }
}
