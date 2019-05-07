using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BolsaDeValores.Models;
using Microsoft.Extensions.Logging;

namespace BolsaDeValores.Controllers
{
    public class ActionsController : Controller
    {
        private readonly BolsaDeValoresContext _context;
        private readonly ILogger _logger;

        public ActionsController(BolsaDeValoresContext context, ILogger<ActionsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Actions
        public async Task<IActionResult> Index()
        {
            var actions = await _context.Actions.Where(m => m.Status == true && m.IdOwner != Program.currentUser).ToListAsync();
            return View(actions);
        }

        // GET: Actions/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(actions);
        }

        // GET: Actions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCategory,IdOwner,Quantity,PriceQuant,QuantMinSell,Status")] Actions actions)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCategory,IdOwner,Quantity,PriceQuant,QuantMinSell,Status")] Actions actions)
        {
            if (id != actions.Id)
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
                    if (!ActionsExists(actions.Id))
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
            return _context.Actions.Any(e => e.Id == id);
        }

        // GET: Actions/BuyAction/5
        public async Task<IActionResult> BuyAction(int? id)
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
            ViewData["Categoria"] = actions.IdCategory;
            ViewData["Quantidade"] = actions.QuantMinSell;
            return View();
        }

        // POST: Actions/BuyAction/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyAction(int QuantBuy, int id)
        {
            // Encontrar a ação que será comprada
            var action = await _context.Actions
                .FirstOrDefaultAsync(m => m.Id == id);

            if (QuantBuy >= action.QuantMinSell && QuantBuy != 0 && QuantBuy <= action.Quantity)
            {
                //Novo dono da action
                var myAction = await _context.Actions
                .FirstOrDefaultAsync(m => m.IdCategory == action.IdCategory && m.IdOwner == Program.currentUser);

                if (myAction == null)
                {
                    myAction = new Actions
                    {
                        IdOwner = Program.currentUser,
                        IdCategory = action.IdCategory,
                        Quantity = QuantBuy,
                        PriceQuant = 0,
                        QuantMinSell = 0,
                        Status = false
                    };
                    _context.Add(myAction);
                }
                else
                {
                    myAction.Quantity += QuantBuy;
                    myAction.Status = false;
                    myAction.PriceQuant = 0;
                    myAction.QuantMinSell = 0;
                    _context.Actions.Update(myAction);
                }
                await _context.SaveChangesAsync();

                //Atual dono da action
                action.Quantity -= QuantBuy;
                if (action.Quantity == 0)
                {
                    _context.Actions.Remove(action);
                }
                else
                {
                    action.QuantMinSell = 1;
                    _context.Actions.Update(action);
                }
                await _context.SaveChangesAsync();

                // Retornar para a página inicial
                return RedirectToAction(nameof(Index));
                //var actions = await _context.Actions.Where(m => m.Status == true).ToListAsync();
                //return View("Index", actions);
            }
            else {
                ViewData["Id"] = action.Id;
                ViewData["Nome"] = action.IdCategory;
                ViewData["Quantidade"] = action.QuantMinSell;
                ViewData["Erro"] = "Você digitou um valor maior ou menor que a quantidade permitida.";
                return View();
            }            
        }
    }
}
