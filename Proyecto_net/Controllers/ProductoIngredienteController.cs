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
    public class ProductoIngredienteController : Controller
    {
        private readonly proyecto_netContext _context;

        public ProductoIngredienteController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: ProductoIngrediente
        public async Task<IActionResult> Index()
        {
            var proyecto_netContext = _context.ProductoIngrediente.Include(p => p.IdingredienteNavigation).Include(p => p.IdproductoNavigation);
            return View(await proyecto_netContext.ToListAsync());
        }

        // GET: ProductoIngrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoIngrediente = await _context.ProductoIngrediente
                .Include(p => p.IdingredienteNavigation)
                .Include(p => p.IdproductoNavigation)
                .FirstOrDefaultAsync(m => m.Idingrediente == id);
            if (productoIngrediente == null)
            {
                return NotFound();
            }

            return View(productoIngrediente);
        }

        // GET: ProductoIngrediente/Create
        public IActionResult Create()
        {
            ViewData["Idingrediente"] = new SelectList(_context.Ingrediente, "Idingrediente", "Idingrediente");
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto");
            return View();
        }

        // POST: ProductoIngrediente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idingrediente,Idproducto")] ProductoIngrediente productoIngrediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoIngrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idingrediente"] = new SelectList(_context.Ingrediente, "Idingrediente", "Idingrediente", productoIngrediente.Idingrediente);
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto", productoIngrediente.Idproducto);
            return View(productoIngrediente);
        }

        // GET: ProductoIngrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoIngrediente = await _context.ProductoIngrediente.FindAsync(id);
            if (productoIngrediente == null)
            {
                return NotFound();
            }
            ViewData["Idingrediente"] = new SelectList(_context.Ingrediente, "Idingrediente", "Idingrediente", productoIngrediente.Idingrediente);
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto", productoIngrediente.Idproducto);
            return View(productoIngrediente);
        }

        // POST: ProductoIngrediente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idingrediente,Idproducto")] ProductoIngrediente productoIngrediente)
        {
            if (id != productoIngrediente.Idingrediente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoIngrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoIngredienteExists(productoIngrediente.Idingrediente))
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
            ViewData["Idingrediente"] = new SelectList(_context.Ingrediente, "Idingrediente", "Idingrediente", productoIngrediente.Idingrediente);
            ViewData["Idproducto"] = new SelectList(_context.Producto, "Idproducto", "Idproducto", productoIngrediente.Idproducto);
            return View(productoIngrediente);
        }

        // GET: ProductoIngrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoIngrediente = await _context.ProductoIngrediente
                .Include(p => p.IdingredienteNavigation)
                .Include(p => p.IdproductoNavigation)
                .FirstOrDefaultAsync(m => m.Idingrediente == id);
            if (productoIngrediente == null)
            {
                return NotFound();
            }

            return View(productoIngrediente);
        }

        // POST: ProductoIngrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoIngrediente = await _context.ProductoIngrediente.FindAsync(id);
            _context.ProductoIngrediente.Remove(productoIngrediente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoIngredienteExists(int id)
        {
            return _context.ProductoIngrediente.Any(e => e.Idingrediente == id);
        }
    }
}
