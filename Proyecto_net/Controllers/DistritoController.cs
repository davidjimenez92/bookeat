using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_net.Models;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_net.Controllers
{
    public class DistritoController : Controller
    {
        private readonly proyecto_netContext _context;

        public DistritoController(proyecto_netContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult B()
        {
            string distrito = Request.Path.Value.Split('/').Last();
            ViewBag.locales = _context.Local.Include(p => p.Imagen).Where(c => c.Distrito.Equals(distrito)).ToList();
            return View();
        }
    }
}