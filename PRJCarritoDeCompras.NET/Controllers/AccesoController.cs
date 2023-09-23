using PRJCarritoDeCompras.NET.CapaNegociio;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRJCarritoDeCompras.NET.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo,string clave)
        {
            usuario ousuario=new usuario();
            ousuario=new usuario_cn().listar().Where(u=>u.correo==correo && u.clave==cn_recursos.convertirsha256(clave)).FirstOrDefault();
            if(ousuario == null)
            {
                ViewBag.error = "Correo o contraseña no son correctas";
                return View();
            }
            else
            {
                ViewBag.error = null;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}