using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_net.Models;

namespace Proyecto_net.Controllers
{
    public class LoginController : Controller
    {
        private readonly proyecto_netContext _context;
        public LoginController(proyecto_netContext context)
        {
            _context = context;
        }

        public IActionResult comprobarEmailExistente(string email)
        {
            if (email == null) { return NotFound(); }
            var correo = _context.Usuario.Where(c => c.Correo.Equals(email)).Select(c => c.Nombre).SingleOrDefault();
            if (correo == null) { 
            correo = _context.Local.Where(c => c.Correo.Equals(email)).Select(c => c.Nombre).SingleOrDefault();
            }
            return Json(correo);
        }


        public IActionResult login(string email, string password)
        { 
            if (email == null || password == null) { return NotFound(); }
            var usuarioID = _context.Usuario.Where(c => c.Correo.Equals(email) & c.Pw.Equals(password)).SingleOrDefault();
            if (usuarioID != null) { 
                HttpContext.Response.Cookies.Append("user_id", usuarioID.Idusuario.ToString());
                HttpContext.Response.Cookies.Append("user_email", usuarioID.Correo);
                HttpContext.Response.Cookies.Append("user_nombre", usuarioID.Nombre);
                HttpContext.Response.Cookies.Append("user_type", "usuario");
                return Json(usuarioID.Idusuario);
            }
            var localID = _context.Local.Where(c => c.Correo.Equals(email) & c.Pw.Equals(password)).SingleOrDefault();
            if (localID != null)
            {
                HttpContext.Response.Cookies.Append("user_id", localID.Idlocal.ToString());
                HttpContext.Response.Cookies.Append("user_email", localID.Correo);
                HttpContext.Response.Cookies.Append("user_nombre", localID.Nombre);
                HttpContext.Response.Cookies.Append("user_type", "local");
                return Json(localID.Idlocal);
            }
            return Json(null);
        }


        public async Task<IActionResult> crearCuentaUsuario(string nombre, string apellido, string email, string password, string telefono)
        {
            if (nombre == null || apellido == null || email == null || password == null || telefono == null) { return NotFound(); }
            Usuario cuentaUsuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Correo = email,
                Pw = password,
                Telefono = telefono
            };
            _context.Add(cuentaUsuario);
            await _context.SaveChangesAsync();
            return Json(true);
        }

        public IActionResult borrarCookies()
        {
            HttpContext.Response.Cookies.Delete("user_email");
            HttpContext.Response.Cookies.Delete("user_id");
            HttpContext.Response.Cookies.Delete("user_type");
            HttpContext.Response.Cookies.Delete("user_nombre");
            return Json(true);
        }





        public async Task<IActionResult> U()
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
        public async Task<IActionResult> U(int id, [Bind("Idusuario,Nombre,Apellido,Correo,Pw,FechaNacimiento,Telefono,FotoPerfil")] Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                HttpContext.Response.Cookies.Delete("user_email");
                HttpContext.Response.Cookies.Append("user_email", usuario.Correo);
                return RedirectToAction(nameof(U));

            }
            return View(usuario);
        }

        public IActionResult L()
        {
            string rol = HttpContext.Request.Cookies["user_type"];
            if (rol != "local") { return Redirect("~/"); }
            int localid;
            if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out localid)) { };
            ViewBag.local = _context.Local.Where(c => c.Idlocal.Equals(localid)).FirstOrDefault();
            ViewBag.menu = _context.Producto.Where(c => c.Idlocal.Equals(localid)).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> L(string nombreForm, string passwordForm, string correoForm, string telefonoForm, string direccionForm, string cpostalForm)
        {
            int localid;
            if (!Int32.TryParse(HttpContext.Request.Cookies["user_id"], out localid)) { };
            Local cuentaLocal = new Local
            {
                Idlocal = localid,
                Nombre = nombreForm,
                Correo = correoForm,
                Pw = passwordForm,
                Telefono = telefonoForm,
                Direccion = direccionForm,
                CPostal = cpostalForm
            };

                _context.Update(cuentaLocal);
                await _context.SaveChangesAsync();
                HttpContext.Response.Cookies.Delete("user_email");
                HttpContext.Response.Cookies.Append("user_email", cuentaLocal.Correo);
            ViewBag.local = cuentaLocal;
            return RedirectToAction(nameof(L));

        }

    }
}