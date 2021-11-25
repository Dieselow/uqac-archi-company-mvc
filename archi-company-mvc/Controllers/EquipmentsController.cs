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
    public class EquipmentsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public EquipmentsController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var equipments = from m in _context.Equipment
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                equipments = equipments.Where(s => s.EquipmentType.Name.Contains(searchString));
                ViewBag.Search = searchString;
            } else {
                ViewBag.Search = "";
            }

            return View(await equipments.Include(e => e.EquipmentType)
                                        .Include(e => e.Room)
                                        .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.Include(e => e.EquipmentType)
                                                    .Include(e => e.Room)
                                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        public IActionResult Create()
        {
            ViewBag.Rooms = new SelectList(_context.Room.ToList(), "Id", "Name");
            ViewBag.EquipmentTypes = new SelectList(_context.EquipmentType.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InstallationDate,RoomId,EquipmentTypeId")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            ViewBag.Rooms = new SelectList(_context.Room.ToList(), "Id", "Name");
            ViewBag.EquipmentTypes = new SelectList(_context.EquipmentType.ToList(), "Id", "Name");
            return View(equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InstallationDate,RoomId,EquipmentTypeId")] Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id))
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
            return View(equipment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.Include(e => e.EquipmentType)
                                                    .Include(e => e.Room)
                                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }
    }
}
