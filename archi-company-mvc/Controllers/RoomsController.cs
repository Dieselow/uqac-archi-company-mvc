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
    public class RoomsController : Controller
    {
        private readonly DatabaseContext _context;

        public RoomsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string searchString, string searchProperty)
        {   
            var rooms = from m in _context.Room
                            select m;

            ViewData["Attributes"] = SelectListUtils.CreatePropertiesSelectListForType("Room", String.IsNullOrEmpty(searchProperty) ? "" : searchProperty);
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(searchProperty))
            {
                rooms = SelectListUtils.DynamicWhere(rooms, searchProperty, searchString);
                ViewBag.Search = searchString;
            } else {
                ViewBag.Search = "";
            }

            return View(await rooms.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.Include(room => room.Equipments)
                            .ThenInclude(Equipment => Equipment.EquipmentType)
                            .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create(string backLink)
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                await _context.Entities.AddAsync(new Entity(room.Id.ToString(), "Room", room));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == id.ToString());
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                    entity.setEntitySearchTags(room);
                    _context.Entities.Update(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Room.FindAsync(id);
            var entity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == id.ToString());
            _context.Entities.Remove(entity);
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}
