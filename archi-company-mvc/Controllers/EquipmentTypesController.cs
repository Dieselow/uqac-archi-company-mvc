using System;
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
    [Authorize(Roles = "Admin,Secretary")]
    public class EquipmentTypesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public EquipmentTypesController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EquipmentTypes
        public async Task<IActionResult> Index(string searchString, string searchProperty)
        {   
            var equipmentTypes = from m in _context.EquipmentType
                            select m;

            ViewData["Attributes"] = SelectListUtils.CreatePropertiesSelectListForType("EquipmentType", String.IsNullOrEmpty(searchProperty) ? "" : searchProperty);
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchProperty))
            {
                equipmentTypes = SelectListUtils.DynamicWhere(equipmentTypes, searchProperty, searchString);
                ViewBag.Search = searchString;
            } else {
                ViewBag.Search = "";
            }

            return View(await equipmentTypes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentType == null)
            {
                return NotFound();
            }

            return View(equipmentType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] EquipmentType equipmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentType.FindAsync(id);
            if (equipmentType == null)
            {
                return NotFound();
            }
            return View(equipmentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] EquipmentType equipmentType)
        {
            if (id != equipmentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentTypeExists(equipmentType.Id))
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
            return View(equipmentType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentType = await _context.EquipmentType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentType == null)
            {
                return NotFound();
            }

            return View(equipmentType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentType = await _context.EquipmentType.FindAsync(id);
            _context.EquipmentType.Remove(equipmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentTypeExists(int id)
        {
            return _context.EquipmentType.Any(e => e.Id == id);
        }
    }
}
