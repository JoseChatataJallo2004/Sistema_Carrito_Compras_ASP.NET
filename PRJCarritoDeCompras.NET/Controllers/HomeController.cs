using PRJCarritoDeCompras.NET.CapaNegociio;
using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRJCarritoDeCompras.NET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Usuarios()
        {
            return View();
        }
        #region usuario
        [HttpGet]
        public JsonResult listarusuarios()
        {
            List<usuario> olista=new List<usuario>();
            olista = new usuario_cn().listar();
            return Json(new {data=olista},JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult guardarusuario(usuario obj)
        {
            object resultado;
            string mensaje = string.Empty;
            if (obj.idusuario == 0)
            {
                resultado = new usuario_cn().registrar(obj, out mensaje);
            }
            else
            {
                resultado = new usuario_cn().editar(obj, out mensaje);
            }
            return Json(new {resultado = resultado,mensaje=mensaje},JsonRequestBehavior.AllowGet) ;
        }
        [HttpPost]
        public JsonResult eliminarusuarios(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new usuario_cn().eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }



        #endregion
    }
}