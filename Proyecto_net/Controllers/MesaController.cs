using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_net.Models;

namespace Proyecto_net.Controllers
{
    public class MesaController : Controller
    {
        private readonly proyecto_netContext _context;

        public MesaController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: Mesa
        public async Task<IActionResult> Index()
        {
            var proyecto_netContext = _context.Mesa.Include(m => m.IdlocalNavigation);
            return View(await proyecto_netContext.ToListAsync());
        }

        // GET: Mesa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesa
                .Include(m => m.IdlocalNavigation)
                .FirstOrDefaultAsync(m => m.Idmesa == id);
            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // GET: Mesa/Create
        public IActionResult Create()
        {
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal");
            return View();
        }

        // POST: Mesa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idmesa,Idlocal,Capacidad,Notas")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", mesa.Idlocal);
            return View(mesa);
        }

        // GET: Mesa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesa.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", mesa.Idlocal);
            return View(mesa);
        }

        // POST: Mesa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idmesa,Idlocal,Capacidad,Notas")] Mesa mesa)
        {
            if (id != mesa.Idmesa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesaExists(mesa.Idmesa))
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
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", mesa.Idlocal);
            return View(mesa);
        }

        // GET: Mesa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesa
                .Include(m => m.IdlocalNavigation)
                .FirstOrDefaultAsync(m => m.Idmesa == id);
            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // POST: Mesa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mesa = await _context.Mesa.FindAsync(id);
            _context.Mesa.Remove(mesa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesaExists(int id)
        {
            return _context.Mesa.Any(e => e.Idmesa == id);
        }
    }
}
