using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;

namespace archi_company_mvc.Controllers
{
    public class ConsumablesController : Controller
    {
        private readonly DatabaseContext _context;

        public ConsumablesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Consumables
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Consumable.Include(c => c.ConsumableType);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Consumables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumable
                .Include(c => c.ConsumableType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }

        // GET: Consumables/Create
        public IActionResult Create()
        {
            ViewData["ConsumableType"] = new SelectList(from x in _context.ConsumableType select new {x.Id, x.Name, x.Brand, BrandName = x.Brand + " " + x.Name}, "Id", "BrandName");
            return View();
        }

        // POST: Consumables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Treshold,ConsumableTypeId")] Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsumableTypeId"] = new SelectList(_context.Set<ConsumableType>(), "Id", "Id", consumable.ConsumableTypeId);
            return View(consumable);
        }

        // GET: Consumables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumable.FindAsync(id);
            if (consumable == null)
            {
                return NotFound();
            }
            ViewData["ConsumableType"] = new SelectList(from x in _context.ConsumableType select new {x.Id, x.Name, x.Brand, BrandName = x.Brand + " " + x.Name}, "Id", "BrandName", consumable.ConsumableTypeId);
            return View(consumable);
        }

        // POST: Consumables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,Treshold,ConsumableTypeId")] Consumable consumable)
        {
            if (id != consumable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumableExists(consumable.Id))
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
            ViewData["ConsumableTypeId"] = new SelectList(from x in _context.ConsumableType select new {x.Id, x.Name, x.Brand, BrandName = x.Brand + " " + x.Name}, "Id", "BrandName", consumable.ConsumableTypeId);
            return View(consumable);
        }

        // GET: Consumables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _context.Consumable
                .Include(c => c.ConsumableType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }

        // POST: Consumables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumable = await _context.Consumable.FindAsync(id);
            _context.Consumable.Remove(consumable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumableExists(int id)
        {
            return _context.Consumable.Any(e => e.Id == id);
        }
    }
}
