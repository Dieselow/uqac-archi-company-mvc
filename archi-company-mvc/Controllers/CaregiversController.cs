using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;

namespace archi_company_mvc.Controllers
{
    public class CaregiversController : Controller
    {
        private readonly DatabaseContext _context;

        public CaregiversController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Caregivers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Caregiver.ToListAsync());
        }

        // GET: Caregivers/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiver = await _context.Caregiver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caregiver == null)
            {
                return NotFound();
            }

            return View(caregiver);
        }

        // GET: Caregivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caregivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LicenceNumber,Salary,WorkSchedule,EmploymentDate,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Caregiver caregiver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caregiver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caregiver);
        }

        // GET: Caregivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiver = await _context.Caregiver.FindAsync(id);
            if (caregiver == null)
            {
                return NotFound();
            }
            return View(caregiver);
        }

        // POST: Caregivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LicenceNumber,Salary,WorkSchedule,EmploymentDate,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Caregiver caregiver)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser.Id == caregiver.Id)
            {
                var currentCaregiver = await _context.Caregiver.FindAsync(caregiver.Id);
                currentCaregiver.FirstName = caregiver.FirstName;
                currentCaregiver.LastName = caregiver.LastName;
                currentCaregiver.Address = caregiver.Address;
                currentCaregiver.DateOfBirth = caregiver.DateOfBirth;
                currentCaregiver.PhoneNumber = caregiver.PhoneNumber;
                currentCaregiver.Salary = caregiver.Salary;
                currentCaregiver.WorkSchedule = caregiver.WorkSchedule;
                currentCaregiver.EmploymentDate = caregiver.EmploymentDate;
                currentCaregiver.LicenceNumber = caregiver.LicenceNumber;
                var result = await _userManager.UpdateAsync(currentCaregiver);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Edit));
                }
                ModelState.AddModelError(string.Empty,"Something went wront during update");
            }
            if (id != caregiver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caregiver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiverExists(caregiver.Id))
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
            return View(caregiver);
        }

        // GET: Caregivers/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiver = await _context.Caregiver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caregiver == null)
            {
                return NotFound();
            }

            return View(caregiver);
        }

        // POST: Caregivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregiver = await _context.Caregiver.FindAsync(id);
            _context.Caregiver.Remove(caregiver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaregiverExists(string id)
        {
            return _context.Caregiver.Any(e => e.Id == id);
        }
    }
}