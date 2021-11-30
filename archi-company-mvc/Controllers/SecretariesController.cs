using System;
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
    [Authorize(Roles = "Admin,Secretary")]
    public class SecretariesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public SecretariesController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Secretaries
        public async Task<IActionResult> Index(string searchString, string searchProperty)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            
            var secretaries = from m in _context.Secretary
                            where m.Id != currentUser.Id
                            select m;
   
            ViewData["Attributes"] = SelectListUtils.CreatePropertiesSelectListForType("Secretary", String.IsNullOrEmpty(searchProperty) ? "" : searchProperty);
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchProperty))
            {
                secretaries = SelectListUtils.DynamicWhere(secretaries, searchProperty, searchString);
                ViewBag.Search = searchString;
            } else {
                ViewBag.Search = "";
            }

            return View(await secretaries.ToListAsync());
        }

        // GET: Secretaries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretary = await _context.Secretary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secretary == null)
            {
                return NotFound();
            }

            return View(secretary);
        }

        // GET: Secretaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Secretaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "Salary,WorkSchedule,EmploymentDate,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")]
            Secretary secretary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secretary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(secretary);
        }

        // GET: Secretaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                return View(await _context.Secretary.FindAsync(user.Id));
            }

            var secretary = await _context.Secretary.FindAsync(id);
            if (secretary == null)
            {
                return NotFound();
            }

            return View(secretary);
        }

        // POST: Secretaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
            [Bind(
                "Salary,WorkSchedule,EmploymentDate,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")]
            Secretary secretary)
        {
            if (id != secretary.Id)
            {
                return NotFound();
            }

            var currentSecretary = await _context.Secretary.FindAsync(secretary.Id);
            currentSecretary.FirstName = secretary.FirstName;
            currentSecretary.LastName = secretary.LastName;
            currentSecretary.Address = secretary.Address;
            currentSecretary.DateOfBirth = secretary.DateOfBirth;
            currentSecretary.PhoneNumber = secretary.PhoneNumber;
            currentSecretary.Salary = secretary.Salary;
            currentSecretary.WorkSchedule = secretary.WorkSchedule;
            currentSecretary.EmploymentDate = secretary.EmploymentDate;
            var result = await _userManager.UpdateAsync(currentSecretary);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Edit));
            }

            ModelState.AddModelError(string.Empty, "Something went wront during update");
            return View(secretary);
        }

        // GET: Secretaries/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretary = await _context.Secretary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secretary == null)
            {
                return NotFound();
            }

            return View(secretary);
        }

        // POST: Secretaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secretary = await _context.Secretary.FindAsync(id);
            _context.Secretary.Remove(secretary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecretaryExists(string id)
        {
            return _context.Secretary.Any(e => e.Id == id);
        }
    }
}