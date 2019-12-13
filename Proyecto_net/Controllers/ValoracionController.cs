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
    public class ValoracionController : Controller
    {
        private readonly proyecto_netContext _context;

        public ValoracionController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: Valoracion
        public async Task<IActionResult> Index()
        {
            var proyecto_netContext = _context.Valoracion.Include(v => v.IdpedidoNavigation);
            return View(await proyecto_netContext.ToListAsync());
        }

        // GET: Valoracion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valoracion = await _context.Valoracion
                .Include(v => v.IdpedidoNavigation)
                .FirstOrDefaultAsync(m => m.Idvaloracion == id);
            if (valoracion == null)
            {
                return NotFound();
            }

            return View(valoracion);
        }

        // GET: Valoracion/Create
        public IActionResult Create()
        {
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido");
            return View();
        }

        // POST: Valoracion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idvaloracion,Idpedido,Valor,Comentario")] Valoracion valoracion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valoracion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido", valoracion.Idpedido);
            return View(valoracion);
        }

        // GET: Valoracion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valoracion = await _context.Valoracion.FindAsync(id);
            if (valoracion == null)
            {
                return NotFound();
            }
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido", valoracion.Idpedido);
            return View(valoracion);
        }

        // POST: Valoracion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idvaloracion,Idpedido,Valor,Comentario")] Valoracion valoracion)
        {
            if (id != valoracion.Idvaloracion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valoracion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValoracionExists(valoracion.Idvaloracion))
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
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido", valoracion.Idpedido);
            return View(valoracion);
        }

        // GET: Valoracion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valoracion = await _context.Valoracion
                .Include(v => v.IdpedidoNavigation)
                .FirstOrDefaultAsync(m => m.Idvaloracion == id);
            if (valoracion == null)
            {
                return NotFound();
            }

            return View(valoracion);
        }

        // POST: Valoracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valoracion = await _context.Valoracion.FindAsync(id);
            _context.Valoracion.Remove(valoracion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValoracionExists(int id)
        {
            return _context.Valoracion.Any(e => e.Idvaloracion == id);
        }
    }
}
