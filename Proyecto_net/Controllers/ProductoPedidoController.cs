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
    public class ProductoPedidoController : Controller
    {
        private readonly proyecto_netContext _context;

        public ProductoPedidoController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: ProductoPedido
        public async Task<IActionResult> Index()
        {
            var proyecto_netContext = _context.ProductoPedido.Include(p => p.IdpedidoNavigation).Include(p => p.IdproductoNavigation);
            return View(await proyecto_netContext.ToListAsync());
        }

        // GET: ProductoPedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoPedido = await _context.ProductoPedido
                .Include(p => p.IdpedidoNavigation)
                .Include(p => p.IdproductoNavigation)
                .FirstOrDefaultAsync(m => m.Idpedido == id);
            if (productoPedido == null)
            {
                return NotFound();
            }

            return View(productoPedido);
        }

        // GET: ProductoPedido/Create
        public IActionResult Create()
        {
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido");
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto");
            return View();
        }

        // POST: ProductoPedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproducto,Idpedido")] ProductoPedido productoPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido", productoPedido.Idpedido);
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto", productoPedido.Idproducto);
            return View(productoPedido);
        }

        // GET: ProductoPedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoPedido = await _context.ProductoPedido.FindAsync(id);
            if (productoPedido == null)
            {
                return NotFound();
            }
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido", productoPedido.Idpedido);
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto", productoPedido.Idproducto);
            return View(productoPedido);
        }

        // POST: ProductoPedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproducto,Idpedido")] ProductoPedido productoPedido)
        {
            if (id != productoPedido.Idpedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoPedidoExists(productoPedido.Idpedido))
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
            ViewData["Idpedido"] = new SelectList(_context.Pedido, "Idpedido", "Idpedido", productoPedido.Idpedido);
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto", productoPedido.Idproducto);
            return View(productoPedido);
        }

        // GET: ProductoPedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoPedido = await _context.ProductoPedido
                .Include(p => p.IdpedidoNavigation)
                .Include(p => p.IdproductoNavigation)
                .FirstOrDefaultAsync(m => m.Idpedido == id);
            if (productoPedido == null)
            {
                return NotFound();
            }

            return View(productoPedido);
        }

        // POST: ProductoPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoPedido = await _context.ProductoPedido.FindAsync(id);
            _context.ProductoPedido.Remove(productoPedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoPedidoExists(int id)
        {
            return _context.ProductoPedido.Any(e => e.Idpedido == id);
        }
    }
}
