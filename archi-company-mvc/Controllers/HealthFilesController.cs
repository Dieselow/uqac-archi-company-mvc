using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace archi_company_mvc.Controllers
{
    public class HealthFilesController : Controller
    {
        private readonly DatabaseContext _context;

        public HealthFilesController(DatabaseContext context)
        {
            _context = context;
        }


        // GET: HealthFiles/Create
        [Authorize(Roles = "Caregiver, Admin")]
        public IActionResult Create(string patientId)
        {
            return View();
        }

        // POST: HealthFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Caregiver, Admin")]
        public async Task<IActionResult> Create([Bind("Medications,ChronicConditions,EmergencyContact")] HealthFile healthFile, string Id)
        {
            if (ModelState.IsValid)
            {
                healthFile.PatientId = Id;
                _context.Add(healthFile);
                await _context.SaveChangesAsync();
                return Redirect(Url.Action("Index", "Patients"));
            }
            return View(healthFile);
        }

        // GET: HealthFiles/Edit/5
        [Authorize(Roles = "Caregiver, Admin")]
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
        [Authorize(Roles = "Caregiver, Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Medications,ChronicConditions,EmergencyContact,PatientId")] HealthFile healthFile)
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
                //return Redirect(Url.Action("Details", "Patients")+"/"+patientID); -- pour revenir directement sur les d�tails d'un patient.
                return Redirect(Url.Action("Index", "Patients"));
            }
            return View(healthFile);
        }

        private bool HealthFileExists(int id)
        {
            return _context.HealthFile.Any(e => e.Id == id);
        }
    }
}
