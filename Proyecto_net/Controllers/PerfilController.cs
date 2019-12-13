using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_net.Models;

namespace Proyecto_net.Controllers
{
    public class PerfilController : Controller
    {

        private readonly proyecto_netContext _context;

        public PerfilController(proyecto_netContext context)
        {
            _context = context;
        }
        // GET: Perfil
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
                HttpContext.Response.Cookies.Delete("user_nombre");
                HttpContext.Response.Cookies.Append("user_email", usuario.Correo);
                HttpContext.Response.Cookies.Append("user_nombre", usuario.Nombre);
                return RedirectToAction(nameof(Index));

            }
            return View(usuario);
        }
    }
}