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
    public class UsuarioController : Controller
    {
        private readonly proyecto_netContext _context;

        public UsuarioController(proyecto_netContext context)
        {
            _context = context;
        }

        // GET: Usuario


        public async Task<IActionResult> Index()
        {
            string rol = HttpContext.Request.Cookies["user_type"];
            if (rol != "usuario") { return Redirect("~/"); }
            int usuarioid;
            if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out usuarioid)) { };

            var usuario = await _context.Usuario.FindAsync(usuarioid);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, [Bind("Idusuario,Nombre,Apellido,Correo,Pw,FechaNacimiento,Telefono,FotoPerfil")] Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                HttpContext.Response.Cookies.Delete("user_email");
                HttpContext.Response.Cookies.Append("user_email", usuario.Correo);
                return RedirectToAction(nameof(Index));

            }
            return View(usuario);
        }
    }
}
