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
    public class ImagenController : Controller
    {
        private readonly proyecto_netContext _context;

        public ImagenController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: Imagen
        public async Task<IActionResult> Index()
        {
            var proyecto_netContext = _context.Imagen.Include(i => i.IdlocalNavigation);
            return View(await proyecto_netContext.ToListAsync());
        }

        // GET: Imagen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagen
                .Include(i => i.IdlocalNavigation)
                .FirstOrDefaultAsync(m => m.Idimagen == id);
            if (imagen == null)
            {
                return NotFound();
            }

            return View(imagen);
        }

        // GET: Imagen/Create
        public IActionResult Create()
        {
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal");
            return View();
        }

        // POST: Imagen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idimagen,Idlocal,Url")] Imagen imagen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", imagen.Idlocal);
            return View(imagen);
        }

        // GET: Imagen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagen.FindAsync(id);
            if (imagen == null)
            {
                return NotFound();
            }
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", imagen.Idlocal);
            return View(imagen);
        }

        // POST: Imagen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idimagen,Idlocal,Url")] Imagen imagen)
        {
            if (id != imagen.Idimagen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagenExists(imagen.Idimagen))
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
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", imagen.Idlocal);
            return View(imagen);
        }

        // GET: Imagen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagen = await _context.Imagen
                .Include(i => i.IdlocalNavigation)
                .FirstOrDefaultAsync(m => m.Idimagen == id);
            if (imagen == null)
            {
                return NotFound();
            }

            return View(imagen);
        }

        // POST: Imagen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagen = await _context.Imagen.FindAsync(id);
            _context.Imagen.Remove(imagen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagenExists(int id)
        {
            return _context.Imagen.Any(e => e.Idimagen == id);
        }
    }
}
