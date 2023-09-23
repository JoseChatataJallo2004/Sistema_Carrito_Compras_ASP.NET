using PRJCarritoDeCompras.NET.CapaNegociio;
using PRJTiendaPresentacion.CapaNegocios;
using PRJTiendaPresentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PRJCarritoDeCompras.NET.Controllers
{
    public class AccesoTiendaController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }
        public ActionResult cambioclave()
        {
            return View();
        }
        #region cliente
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            cliente ocliente = null;
            ocliente = new cliente_cn().listar().Where(item => item.correo == correo && item.clave == cn_recursos.convertirsha256(clave)).FirstOrDefault();
            if (ocliente == null)
            {
                ViewBag.error = " correo o contraseña no son correctas";
                return View();
            }
            else
            {
                if (ocliente.reestablecer)
                {
                    TempData["idcliente"] = ocliente.idcliente;
                    return RedirectToAction("cambioclave", "AccesoTienda");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(ocliente.correo, false);

                    Session["cliente"] = ocliente;
                    ViewBag.error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }
            //return View();
        }

        [HttpPost]
        public ActionResult Registrar(cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["nombres"] = string.IsNullOrEmpty(objeto.nombres) ? "" : objeto.nombres;
            ViewData["apellidos"] = string.IsNullOrEmpty(objeto.apellidos) ? "" : objeto.apellidos;
            ViewData["correo"] = string.IsNullOrEmpty(objeto.correo) ? "" : objeto.correo;

            if (objeto.clave != objeto.confirmarclave)
            {
                ViewBag.error = "Las contraseñas no coinciden";
                return View();
            }
            resultado = new cliente_cn().registrar(objeto, out mensaje);

            if (resultado > 0)
            {
                ViewBag.error = null;
                return RedirectToAction("Index", "AccesoTienda");
            }
            else
            {
                ViewBag.error = mensaje;
                return View();
            }

        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {

            cliente ocliente = new cliente();
            ocliente = new cliente_cn().listar().Where(item => item.correo == correo).FirstOrDefault();
            if (ocliente == null)
            {
                ViewBag.error = "No se encontro un cliente relacionado a ese correo";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new cliente_cn().reestablecerclave(ocliente.idcliente, correo, out mensaje);
            if (respuesta)
            {
                ViewBag.error = null;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult cambioclave(string idcliente, string claveactual, string nuevaclave, string confirmarclave)
        {
            cliente ocliente = new cliente();
            ocliente = new cliente_cn().listar().Where(c => c.idcliente == int.Parse(idcliente)).FirstOrDefault();
            if (ocliente.clave != cn_recursos.convertirsha256(claveactual))
            {
                TempData["idcliente"] = idcliente;
                ViewData["vclave"] = "";
                ViewBag.error = "la contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["idcliente"] = idcliente;
                //vclave es para cuando las contraseñas nuevas no coinciden la contraseña actual no se borre
                ViewData["vclave"] = claveactual;
                ViewBag.error = "las contraseñas nuevas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = cn_recursos.convertirsha256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new cliente_cn().cambiarclave(int.Parse(idcliente), nuevaclave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index", "AccesoTienda");
            }
            else
            {
                TempData["idcliente"] = idcliente;
                ViewBag.error = mensaje;
                return View();
            }
        }
        #endregion
    }
}