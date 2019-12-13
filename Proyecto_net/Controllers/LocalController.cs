using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_net.Models;

namespace Proyecto_net.Controllers
{
    public class LocalController : Controller
    {
        private readonly proyecto_netContext _context;

        public LocalController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: Local
        public async Task<IActionResult> Index()
        {
            return View(await _context.Local.ToListAsync());
        }

        // GET: Local/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Local
                .FirstOrDefaultAsync(m => m.Idlocal == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // GET: Local/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Id()
        {
            // Obtiene la url (stringIdLocal) lo transforma en (intIDLocal) y hace la consulta a la BD recogiendo el Local y la lista de menus
            string stringIdLocal = Request.Path.Value.Split('/').Last();
            int intIdLocal;
            int.TryParse(stringIdLocal, out intIdLocal);
            if (_context.Local.Where(c => c.Idlocal.Equals(intIdLocal)).SingleOrDefault() == null) { }else
            {
                ViewBag.local = _context.Local.Include(p => p.Imagen).Where(c => c.Idlocal.Equals(intIdLocal)).SingleOrDefault();
           ViewBag.menu = _context.Producto.Where(c => c.Idlocal.Equals(intIdLocal)).ToList();
            
            List<string> listadoHoras = new List<string>();
            string horarioLocal = _context.Local.Where(c => c.Idlocal.Equals(intIdLocal)).Select(c => c.Horario).SingleOrDefault();
            string horarioInicio = horarioLocal.Split(',').First();
            string horarioCierre = horarioLocal.Split(", ").Last();
            DateTime start = DateTime.ParseExact(horarioInicio, "HH:mm", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(horarioCierre, "HH:mm", CultureInfo.InvariantCulture);

            while (end > start)
            {
                listadoHoras.Add(start.ToString("HH:mm", CultureInfo.InvariantCulture));
                start = start.AddMinutes(30);
            }
            ViewBag.listadoHoras = listadoHoras;
            }
            return View();
        }

        // GET: Perfil
        public async Task<IActionResult> Modificar()
        {
            string rol = HttpContext.Request.Cookies["user_type"];
            if (rol != "local") { return Redirect("~/"); }
            int localid;
            if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out localid)) { };

            var local = await _context.Local.FindAsync(localid);
            if (local == null)
            {
                return NotFound();
            }
            ViewBag.menu = _context.Producto.Where(p => p.Idlocal.Equals(localid)).ToList();
            return View(local);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, [Bind("Idlocal,Nombre,Correo,Pw,Horario,Ciudad,Direccion,CPostal,Coordenadas,Telefono,Notas")] Local local)
        {

            if (ModelState.IsValid)
            {
                _context.Update(local);
                await _context.SaveChangesAsync();
                HttpContext.Response.Cookies.Delete("user_email");
                HttpContext.Response.Cookies.Delete("user_nombre");
                HttpContext.Response.Cookies.Append("user_email", local.Correo);
                HttpContext.Response.Cookies.Append("user_nombre", local.Correo);
                return RedirectToAction(nameof(Modificar));

            }
            return View(local);
        }

        public IActionResult Pedidos()
        {
            string rol = HttpContext.Request.Cookies["user_type"];
            if (rol != "local") { return Redirect("~/"); }
            int localid;
            if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out localid)) { };

            ViewBag.Reserva = _context.Reserva.Include(p => p.Pedido).ThenInclude(p => p.ProductoPedido).ThenInclude(p=>p.IdproductoNavigation).Include(p=>p.IdmesaNavigation).Include(p=>p.IdusuarioNavigation).Where(p => p.Idlocal.Equals(localid));//.ToList();

            return View();
        }

        public async Task<IActionResult> crearMesa(short numeroMesa, short capacidad)
        {
            string rol = HttpContext.Request.Cookies["user_type"];
            if (rol != "local") { return Redirect("~/"); }
            int localid;
            if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out localid)) { };
            Mesa mesaCrear = new Mesa
            {
                Idlocal = localid,
                numeroMesa = numeroMesa,
                Capacidad = capacidad
            };
            _context.Add(mesaCrear);
            await _context.SaveChangesAsync();
            return Json(true);
        }

        public async Task<IActionResult> crearMenu(string nombre, short precio)
        {
            string rol = HttpContext.Request.Cookies["user_type"];
            if (rol != "local") { return Redirect("~/"); }
            int localid;
            if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out localid)) { };
            Producto menuCrear = new Producto
            {
                Idlocal = localid,
                Nombre = nombre,
                Precio = precio

            };
            _context.Add(menuCrear);
            await _context.SaveChangesAsync();
            return Json(true);
        }

        // POST: Locals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idlocal,Nombre,Correo,Pw,Horario,Ciudad,Direccion,CPostal,Coordenadas,Telefono,Notas,Categoria,Distrito,Barrio")] Local local)
        {
            if (ModelState.IsValid)
            {
                _context.Add(local);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(local);
        }

        // GET: Local/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Local.FindAsync(id);
            if (local == null)
            {
                return NotFound();
            }
            return View(local);
        }

        // POST: Local/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idlocal,Nombre,Correo,Pw,Horario,Ciudad,Direccion,CPostal,Coordenadas,Telefono,Notas,Categoria,Distrito,Barrio")] Local local)
        {
            if (id != local.Idlocal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(local);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalExists(local.Idlocal))
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
            return View(local);
        }

        // GET: Local/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Local
                .FirstOrDefaultAsync(m => m.Idlocal == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // POST: Local/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var local = await _context.Local.FindAsync(id);
            _context.Local.Remove(local);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalExists(int id)
        {
            return _context.Local.Any(e => e.Idlocal == id);
        }
    }
}
