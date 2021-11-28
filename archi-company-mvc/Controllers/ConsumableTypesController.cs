using System;
using System.Collections.Generic;
using archi_company_mvc.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace archi_company_mvc.Controllers
{
    [Authorize(Roles = "Admin,Secretary")]
    public class ConsumableTypesController : Controller
    {
        private readonly DatabaseContext _context;

        private readonly UserManager<User> _userManager;

        public ConsumableTypesController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ConsumableTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConsumableType.ToListAsync());
        }

        // GET: ConsumableTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumableType = await _context.ConsumableType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumableType == null)
            {
                return NotFound();
            }

            return View(consumableType);
        }

        // GET: ConsumableTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsumableTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand")] ConsumableType consumableType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumableType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consumableType);
        }

        // GET: ConsumableTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumableType = await _context.ConsumableType.FindAsync(id);
            if (consumableType == null)
            {
                return NotFound();
            }
            return View(consumableType);
        }

        // POST: ConsumableTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand")] ConsumableType consumableType)
        {
            if (id != consumableType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumableType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumableTypeExists(consumableType.Id))
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
            return View(consumableType);
        }

        // GET: ConsumableTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumableType = await _context.ConsumableType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumableType == null)
            {
                return NotFound();
            }

            return View(consumableType);
        }

        // POST: ConsumableTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumableType = await _context.ConsumableType.FindAsync(id);
            _context.ConsumableType.Remove(consumableType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumableTypeExists(int id)
        {
            return _context.ConsumableType.Any(e => e.Id == id);
        }
    }
}
