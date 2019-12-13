using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_net.Models;

namespace Proyecto_net.Controllers
{
    public class BusquedaController : Controller
    {
        private readonly proyecto_netContext _context;
        public BusquedaController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: Busqueda
        public IActionResult Index(string br)
        {
            List<Local> locales = _context.Local.Where(c => c.Nombre.Contains(br) || c.Direccion.Contains(br) || c.Ciudad.Contains(br)).ToList();


            Dictionary<int, string> localVal = new Dictionary<int, string>();
            Dictionary<int, string> imagenes = new Dictionary<int, string>();
            foreach (var item in locales)
            {
               // localVal.Add(item.Idlocal, getValoracionMedia(item.Idlocal));
               // getValoracionMedia(item.Idlocal);
            }
            ViewBag.locales = _context.Local.Include(p => p.Imagen).Where(c => c.Nombre.Contains(br) || c.Direccion.Contains(br)).OrderBy(o => o.Barrio);
            //ViewBag.valoraciones = localVal;
            //ViewBag.imagenes = imagenes;
            return View();
        }

        // GET: Busqueda/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Busqueda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Busqueda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Busqueda/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Busqueda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Busqueda/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Busqueda/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private String getValoracionMedia(int idlocal)
        {
            List<Reserva> reservas = _context.Reserva.Where(c => c.Idlocal.Equals(idlocal)).ToList();
            List<Pedido> pedidos = new List<Pedido>();
            List<Valoracion> valoraciones = new List<Valoracion>();
            List<String> valoracionesMedias = new List<string>();
            double valoracionMedia = 0;
            if (reservas != null)
            {
                foreach (var item in reservas)
                {
                    pedidos.Add(_context.Pedido.Where(c => c.Idreserva.Equals(item.Idreserva)).SingleOrDefault());
                }
                if (pedidos != null)
                {
                    foreach (var item in pedidos)
                    {
                        valoraciones.Add(_context.Valoracion.Where(c => c.Idpedido.Equals(item.Idpedido)).SingleOrDefault());
                    }
                    if (valoraciones != null)
                    {
                        foreach (var item in valoraciones)
                        {
                           // valoracionMedia += item.Valor;
                        }
                        valoracionMedia /= valoraciones.Count();
                        return valoracionMedia + " " + valoraciones.Count();
                    }
                }
            }
            return null;
        }
    }
}