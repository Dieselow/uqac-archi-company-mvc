using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace archi_company_mvc.Controllers
{
    [Authorize(Roles = "Admin,Secretary")]
    public class CaregiversController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;
        public CaregiversController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Caregiver.ToListAsync());
        }

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

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                return View(await _context.Caregiver.FindAsync(user.Id));
            }

            var caregiver = await _context.Caregiver.FindAsync(id);
            if (caregiver == null)
            {
                return NotFound();
            }
            return View(caregiver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LicenceNumber,Salary,WorkSchedule,EmploymentDate,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Caregiver caregiver)
        {
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
