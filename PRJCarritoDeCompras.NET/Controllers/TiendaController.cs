using PRJCarritoDeCompras.NET.CapaNegociio;
using PRJCarritoDeCompras.NET.Models;
using PRJTiendaPresentacion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRJCarritoDeCompras.NET.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idproducto=0)
        {
            productos oproducto=new productos();
            bool conversion;
            oproducto=new producto_cn().listar().Where(p=>p.idproducto==idproducto).FirstOrDefault();
            if (oproducto != null)
            {
                oproducto.base64 = cn_recursos.convertirbase64(Path.Combine(oproducto.rutaimagen, oproducto.nombreimagen), out conversion);

                oproducto.extension = Path.GetExtension(oproducto.nombreimagen);


            }
            return View(oproducto);
        }

        [HttpGet]
        public JsonResult listacategorias()
        {
            List<categoria> lista = new List<categoria>();
            lista = new categoria_cn().listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult listarproductos(int idcategoria)
        {
            List<productos> lista = new List<productos>();
            bool conversaion;
            lista = new producto_cn().listar().Select(p => new productos()
            {
                idproducto = p.idproducto,
                nombre = p.nombre,
                descripcion = p.descripcion,
                omarca = p.omarca,
                ocategoria = p.ocategoria,
                precio = p.precio,
                stock = p.stock,
                rutaimagen = p.rutaimagen,
                base64 = cn_recursos.convertirbase64(Path.Combine(p.rutaimagen, p.nombreimagen), out conversaion),
                extension = Path.GetExtension(p.nombreimagen),
                activo = p.activo
            }).Where(p => p.ocategoria.idcategoria == (idcategoria == 0 ? p.ocategoria.idcategoria : idcategoria) &&
                p.stock > 0 && p.activo == true).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;

        }


        #region metodos del carrito
        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {
            int idcliente= ((cliente)Session["Cliente"]).idcliente;
            bool existe = new carrito_cn().existecarrito(idcliente, idproducto);
            bool respuesta = false;
            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";

            }
            else
            {
                //ojo en el true era sumar pero le hemos asiganado como 0 o 1 en bool 
                respuesta=new carrito_cn().operacioncarrito(idcliente,idproducto,true,out mensaje);
            }
            return Json(new {respuesta=respuesta,mensaje=mensaje},JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult cantidadencarrito()
        {

            int idcliente = ((cliente)Session["Cliente"]).idcliente;
            int cantidad=new carrito_cn().cantidadcarrito(idcliente);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult listarproductoscarrito()
        {
            int idcliente = ((cliente)Session["Cliente"]).idcliente;
            List<carrito> olista=new List<carrito>();
            bool conversion;
            olista=new carrito_cn().listarproductos(idcliente).Select(oc=>new carrito()
            {
                oproducto=new productos()
                {
                    idproducto=oc.oproducto.idproducto,
                    nombre=oc.oproducto.nombre,
                    omarca=oc.oproducto.omarca,
                    precio=oc.oproducto.precio,
                    rutaimagen=oc.oproducto.rutaimagen,
                    base64=cn_recursos.convertirbase64( Path.Combine(oc.oproducto.rutaimagen,oc.oproducto.nombreimagen),out conversion),
                    extension=Path.GetExtension(oc.oproducto.nombreimagen)
                    
                },
                cantidad=oc.cantidad
            }).ToList();

            return Json(new {data=olista},JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult operacioncarrito(int idproducto,bool sumar)
        {
            int idcliente = ((cliente)Session["Cliente"]).idcliente;
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new carrito_cn().operacioncarrito(idcliente, idproducto, true, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult eliminarcarrito(int idproducto)
        {
            int idcliente = ((cliente)Session["Cliente"]).idcliente;
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new carrito_cn().eliminarcarrito(idcliente, idproducto);
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }


        #endregion



        [HttpPost]
        public JsonResult ObtenerDepartamento()
        {
            List<departamento> olista=new List<departamento>();
            olista=new ubicacion_cn().ObtenerDepartamento();
            return Json(new {lista=olista},JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ObtenerProvincias(string iddepartamento)
        {
            List<provincia> olista = new List<provincia>();
            olista = new ubicacion_cn().ObtenerProvincia(iddepartamento);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ObtenerDistritos(string idprovincia, string iddepartamento)
        {
            List<distrito> olista = new List<distrito>();
            olista = new ubicacion_cn().ObtenerDistrito(idprovincia, iddepartamento);
            return Json(new { lista = olista }, JsonRequestBehavior.AllowGet);

        }

    }
}