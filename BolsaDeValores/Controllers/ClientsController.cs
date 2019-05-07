using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BolsaDeValores.Models;

namespace BolsaDeValores.Controllers
{
    public class ClientsController : Controller
    {
        private readonly BolsaDeValoresContext _context;

        public ClientsController(BolsaDeValoresContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clients == null)
            {
                return NotFound();
            }

            return View(clients);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clients);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password")] Clients clients)
        {
            if (id != clients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(clients.Id))
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
            return View(clients);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clients == null)
            {
                return NotFound();
            }

            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clients = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(clients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        // GET: Actions/MyActions
        public async Task<IActionResult> MyActions(int id)
        {
            return View(await _context.Actions.Where(m => m.IdOwner == Program.currentUser).ToListAsync());
        }

        
        // GET: Actions/SellAction/5
        public async Task<IActionResult> SellAction(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actions = await _context.Actions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actions == null)
            {
                return NotFound();
            }

            ViewData["Id"] = actions.Id;
            ViewData["Nome"] = actions.IdCategory;
            ViewData["Quantidade"] = actions.Quantity;
            return View();            
        }

        // POST: Actions/SellAction/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SellAction(int? id, int QuantSell, int PriceUnit)
        {
            var action = await _context.Actions
                    .FirstOrDefaultAsync(m => m.Id == id);

            if (QuantSell <= action.Quantity && QuantSell != 0 && PriceUnit.GetType() == typeof(int))
            {                
                action.Status = true;
                action.QuantMinSell = QuantSell;
                action.PriceQuant = PriceUnit;
                _context.Update(action);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(MyActions));
            }
            else
            {
                ViewData["Id"] = action.Id;
                ViewData["Nome"] = action.IdCategory;
                ViewData["Quantidade"] = action.Quantity;
                ViewData["Erro"] = "Digite um valor válido";
                return View();
            }           
        }

        // POST: Actions/CancelSellAction/5        
        public async Task<IActionResult> CancelSellAction(int? id)
        {
            var action = await _context.Actions
                   .FirstOrDefaultAsync(m => m.Id == id);
            action.Status = false;
            action.QuantMinSell = 0;
            action.PriceQuant = 0;
            _context.Update(action);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyActions));
        }
    }
}
