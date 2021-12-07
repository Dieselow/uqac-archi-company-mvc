using System.Linq;
using System.Threading.Tasks;
using archi_company_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using archi_company_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace archi_company_mvc.Controllers
{
    [Authorize(Roles = "Admin,Secretary")]
    public class TicketsController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticket.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.Include(m => m.Consumables).ThenInclude(c => c.ConsumableType).FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {

            var request = from x in _context.Consumable where x.TicketId == null select new {x.Id, x.Quantity,  x.ConsumableType.Name, x.ConsumableType.Brand, FinalName = x.ConsumableType.Brand + " " + x.ConsumableType.Name + " : " + x.Quantity}; 
            ViewData["ConsumableList"] = new MultiSelectList(request, "Id","FinalName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestDate,EquipmentTypeId,ConsumablesIds,Name")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                _context.Entities.Add(new Entity(ticket.Id.ToString(), "Ticket", ticket,ticket.GetController(),ticket.GetType().Name + ": "+ticket.Name));
                await _context.SaveChangesAsync();
                foreach(var consumableId in ticket.ConsumablesIds){
                    Consumable toUpdate = await _context.Consumable.FindAsync(consumableId);
                    var entityToUpdate =
                        await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == consumableId.ToString());
                    toUpdate.TicketId = ticket.Id;
                    entityToUpdate.setEntitySearchTags(toUpdate);
                    _context.Update(toUpdate);
                    _context.Entities.Update(entityToUpdate);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.Include(m => m.Consumables).ThenInclude(c => c.ConsumableType).FirstOrDefaultAsync(m => m.Id == id);
             
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestDate,EquipmentTypeId,Name")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == id.ToString());
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    entity.setEntitySearchTags(ticket);
                    _context.Entities.Update(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.Include(m => m.Consumables).ThenInclude(c => c.ConsumableType).FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.Include(m => m.Consumables).FirstOrDefaultAsync(m => m.Id == id);
            var entity = await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == id.ToString());
            _context.Entities.Remove(entity);
            foreach(var consumable in ticket.Consumables){
                    consumable.TicketId = null;
                    var consumableEntity =
                        await _context.Entities.FirstOrDefaultAsync(e => e.EntityId == consumable.Id.ToString());
                    consumableEntity.setEntitySearchTags(consumable);
                    _context.Update(consumable);
                    _context.Entities.Update(consumableEntity);
            }
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
