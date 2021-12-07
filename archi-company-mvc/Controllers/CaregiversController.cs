using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace archi_company_mvc.Controllers
{
    public class CaregiversController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public CaregiversController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Caregivers
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Caregiver.ToListAsync());
        }

        // GET: Caregivers/Details/5
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> Details(string id)
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
        [Authorize(Roles = "Admin,Secretary")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caregivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> Create([Bind("LicenceNumber,Salary,WorkSchedule,EmploymentDate,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Caregiver caregiver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caregiver);
                _context.Entities.Add(new Entity(caregiver.Id, "Users", caregiver,caregiver.GetController(),  caregiver.GetType().Name + ": "+caregiver.UserName));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(caregiver);
        }

        // GET: Caregivers/Edit/5
        [Authorize(Roles = "Admin,Secretary,Caregiver")]
        public async Task<IActionResult> Edit(string id)
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

        // POST: Caregivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Secretary,Caregiver")]
        public async Task<IActionResult> Edit(string id, [Bind("LicenceNumber,Salary,WorkSchedule,EmploymentDate,Id,UserName,FirstName,LastName,DateOfBirth,Email,Password,Address,PhoneNumber")] Caregiver caregiver)
        {
            if (id != caregiver.Id)
            {
                return NotFound();
            }

            var currentCaregiver = await _context.Caregiver.FindAsync(caregiver.Id);
            var currentEntity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == currentCaregiver.Id);
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
                currentEntity.setEntitySearchTags(currentCaregiver);
                _context.Entities.Update(currentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit));
            }

            ModelState.AddModelError(string.Empty, "Something went wront during update");
            return RedirectToAction(nameof(Index));
        }

        // GET: Caregivers/Delete/5
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> Delete(string id)
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
        [Authorize(Roles = "Admin,Secretary")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregiver = await _context.Caregiver.FindAsync(id);
            var entity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == caregiver.Id);
            _context.Caregiver.Remove(caregiver);
            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaregiverExists(string id)
        {
            return _context.Caregiver.Any(e => e.Id == id);
        }
    }
}