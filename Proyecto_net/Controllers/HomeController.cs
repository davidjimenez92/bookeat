using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_net.Models;

namespace Proyecto_net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly proyecto_netContext _context;

        public HomeController(ILogger<HomeController> logger, proyecto_netContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.prueba = "Esto es una prueba";
          return View();
        }

        public IActionResult autoCompleteCiudades(string term)
        {
            if (term == null) { return NotFound(); }
            var listaDeCiudades = _context.Local.Where(c => c.Distrito.Contains(term)).Select(c => c.Distrito).ToHashSet();
            return Json(listaDeCiudades);
        }

        public IActionResult autoCompleteBusqueda(string term)
        {
            if (term == null) { return NotFound(); }
            var listadoBusqueda = _context.Local.Where(c => c.Nombre.Contains(term)).ToHashSet();
            return Json(listadoBusqueda);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
