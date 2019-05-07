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
    public class ActionsController : Controller
    {
        private readonly BolsaDeValoresContext _context;

        public ActionsController(BolsaDeValoresContext context)
        {
            _context = context;
        }

        // GET: Actions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actions.ToListAsync());
            // Listar ações com status à venda
        }

        // GET: Actions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actions = await _context.Actions
                .FirstOrDefaultAsync(m => m.id == id);
            if (actions == null)
            {
                return NotFound();
            }

            return View(actions);
        }

        // GET: Actions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,owner,category,quantity,priceQuant,quantMinSell,status")] Actions actions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actions);
        }

        // GET: Actions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actions = await _context.Actions.FindAsync(id);
            if (actions == null)
            {
                return NotFound();
            }
            return View(actions);
        }

        // POST: Actions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,owner,category,quantity,priceQuant,quantMinSell,status")] Actions actions)
        {
            if (id != actions.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionsExists(actions.id))
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
            return View(actions);
        }

        // GET: Actions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actions = await _context.Actions
                .FirstOrDefaultAsync(m => m.id == id);
            if (actions == null)
            {
                return NotFound();
            }

            return View(actions);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actions = await _context.Actions.FindAsync(id);
            _context.Actions.Remove(actions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActionsExists(int id)
        {
            return _context.Actions.Any(e => e.id == id);
        }

        public async Task<IActionResult> BuyAction (int? idAction)
        {
            Actions action = _context.Actions.FirstOrDefault(i => i.id == idAction);
            return View(action);
        }

        //public async Task<IActionResult> BuyAction(int? idAction, int? quantSell)
        //{
        //    Actions action = _context.Actions.First(i => i.id == idAction);
        //    int quantMinSell = action.quantMinSell;
        //    return View(action);
        //}
    }
}
