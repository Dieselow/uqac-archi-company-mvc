using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace archi_company_mvc.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly DatabaseContext _context;

        public EquipmentsController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Secretary")]
        // GET: Equipments
        public async Task<IActionResult> Index(string searchString, string searchProperty)
        {
            var equipments = from m in _context.Equipment
                            select m;
   
            ViewData["Attributes"] = SelectListUtils.CreatePropertiesSelectListForType("Equipment", String.IsNullOrEmpty(searchProperty) ? "" : searchProperty);
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchProperty))
            {
                equipments = SelectListUtils.DynamicWhere(equipments, searchProperty, searchString);
                ViewBag.Search = searchString;
            } else {
                ViewBag.Search = "";
            }

            return View(await equipments.Include(e => e.EquipmentType)
                                        .Include(e => e.Room)
                                        .ToListAsync());
        }

        // GET: Equipments/Details/5
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

        // GET: Equipments/Create
        public IActionResult Create()
        {
            ViewBag.Rooms = new SelectList(_context.Room.ToList(), "Id", "Name");
            ViewBag.EquipmentTypes = new SelectList(_context.EquipmentType.ToList(), "Id", "Name");
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InstallationDate,RoomId,EquipmentTypeId")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                _context.Entities.Add(new Entity(equipment.Id.ToString(), "Equipment", equipment,equipment.GetController()));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipments/Edit/5
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

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    var entity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == equipment.Id.ToString());
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                    entity.setEntitySearchTags(equipment);
                    _context.Entities.Update(entity);
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

        // GET: Equipments/Delete/5
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

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            var entity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == id.ToString());
            _context.Equipment.Remove(equipment);
            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }
    }
}
