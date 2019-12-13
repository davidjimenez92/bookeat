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
    public class ReservaController : Controller
    {
        private readonly proyecto_netContext _context;

        public ReservaController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var proyecto_netContext = _context.Reserva.Include(r => r.IdlocalNavigation).Include(r => r.IdmesaNavigation).Include(r => r.IdusuarioNavigation);
            return View(await proyecto_netContext.ToListAsync());
        }
        public IActionResult loginReserva(string email, string password)
        {
            if (email == null || password == null) { return NotFound(); }
            var usuarioID = _context.Usuario.Where(c => c.Correo.Equals(email) & c.Pw.Equals(password)).SingleOrDefault();
            return Json(usuarioID);
        }


        public  IActionResult comprobarMesas(DateTime dia, string hora, int IdLocal)
        {
            DateTime fecha = dia.Add(TimeSpan.Parse(hora));
            List<Mesa> mesasTotales = _context.Mesa.Where(c => c.Idlocal.Equals(IdLocal)).ToList();
            List<int?> mesasOcupadas = _context.Reserva.Where(c => c.Idlocal.Equals(IdLocal) && c.Fecha.Equals(fecha)).Select(c=>c.Idmesa).ToList();
            List<Mesa> mesasDisponibles = mesasTotales.Where(w => !mesasOcupadas.Contains(w.Idmesa)).ToList();
            return Json(mesasDisponibles);
        }

        public IActionResult comprobarLogin(string email, string password)
        {
            if ( email == null && password == null) { return NotFound(); }
            int usuario = _context.Usuario.Where(c => c.Correo.Equals(email) && c.Pw.Equals(password)).Select(c => c.Idusuario).SingleOrDefault();
            return Json(usuario);
        }

        public async Task<IActionResult> crearReserva(int IdLocal, DateTime dia, string hora, string email, string password, int menu, int cantidad, string notas, int? mesa)
        {
            DateTime fecha = dia.Add(TimeSpan.Parse(hora));
            int IdUsuario;
            if (HttpContext.Request.Cookies["user_id"] != null) {
                if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out IdUsuario)) { };
            } else { IdUsuario = _context.Usuario.Where(c => c.Correo.Equals(email) && c.Pw.Equals(password)).Select(c => c.Idusuario).SingleOrDefault(); }
            if (mesa == 0) { mesa = null; }
            Reserva reservaRealizada = new Reserva
            {
                Idusuario = IdUsuario,
                Idlocal = IdLocal,
                Idmesa = mesa,
                Fecha = fecha,
                Notas = ""
            };
            _context.Add(reservaRealizada);
            await _context.SaveChangesAsync();
            
            Pedido pedidoRealizado = new Pedido
            {
                Idreserva = reservaRealizada.Idreserva,
                Cantidad = cantidad,
                Notas = ""
            };
            _context.Add(pedidoRealizado);
            await _context.SaveChangesAsync();

            ProductoPedido productoPedidoRealizado = new ProductoPedido
            {
                Idpedido = pedidoRealizado.Idpedido,
                Idproducto = menu
            };
            _context.Add(productoPedidoRealizado);
            await _context.SaveChangesAsync();

            return Json(true);
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.IdlocalNavigation)
                .Include(r => r.IdmesaNavigation)
                .Include(r => r.IdusuarioNavigation)
                .FirstOrDefaultAsync(m => m.Idreserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/Create
        public IActionResult Create()
        {
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal");
            ViewData["Idmesa"] = new SelectList(_context.Mesa, "Idmesa", "Idmesa");
            ViewData["Idusuario"] = new SelectList(_context.Usuario, "Idusuario", "Idusuario");
            return View();
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idreserva,Idusuario,Idlocal,Idmesa,Fecha,Notas")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", reserva.Idlocal);
            ViewData["Idmesa"] = new SelectList(_context.Mesa, "Idmesa", "Idmesa", reserva.Idmesa);
            ViewData["Idusuario"] = new SelectList(_context.Usuario, "Idusuario", "Idusuario", reserva.Idusuario);
            return View(reserva);
        }

        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", reserva.Idlocal);
            ViewData["Idmesa"] = new SelectList(_context.Mesa, "Idmesa", "Idmesa", reserva.Idmesa);
            ViewData["Idusuario"] = new SelectList(_context.Usuario, "Idusuario", "Idusuario", reserva.Idusuario);
            return View(reserva);
        }

        // POST: Reserva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idreserva,Idusuario,Idlocal,Idmesa,Fecha,Notas")] Reserva reserva)
        {
            if (id != reserva.Idreserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Idreserva))
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
            ViewData["Idlocal"] = new SelectList(_context.Local, "Idlocal", "Idlocal", reserva.Idlocal);
            ViewData["Idmesa"] = new SelectList(_context.Mesa, "Idmesa", "Idmesa", reserva.Idmesa);
            ViewData["Idusuario"] = new SelectList(_context.Usuario, "Idusuario", "Idusuario", reserva.Idusuario);
            return View(reserva);
        }

        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.IdlocalNavigation)
                .Include(r => r.IdmesaNavigation)
                .Include(r => r.IdusuarioNavigation)
                .FirstOrDefaultAsync(m => m.Idreserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Idreserva == id);
        }
    }
}
