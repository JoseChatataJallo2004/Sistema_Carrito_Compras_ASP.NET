using Newtonsoft.Json;
using PRJCarritoDeCompras.NET.CapaNegociio;
using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRJCarritoDeCompras.NET.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }
        #region Categoria
        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<categoria> olista = new List<categoria>();
            olista = new categoria_cn().listar();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Guardarcategoria(categoria obj)
        {
            object resultado;
            string mensaje = string.Empty;
            if (obj.idcategoria == 0)
            {
                resultado = new categoria_cn().registrar(obj, out mensaje);
            }
            else
            {
                resultado = new categoria_cn().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;

            respuesta = new categoria_cn().eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        //////////
        ///
        #region marca
        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<marca> olista = new List<marca>();
            olista = new marca_cn().listar();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Guardarmarca(marca obj)
        {
            object resultado;
            string mensaje = string.Empty;
            if (obj.idmarca == 0)
            {
                resultado = new marca_cn().registrar(obj, out mensaje);
            }
            else
            {
                resultado = new marca_cn().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Eliminarmarca(int id)
        {
            bool respuesta = false;

            respuesta = new marca_cn().eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        //////////
        ///
        #region producto
        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<productos> olista = new List<productos>();
            olista = new producto_cn().listar();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardarproducto(string obj, HttpPostedFileBase archivoimagen)
        {
            //object resultado;
            string mensaje = string.Empty;

            bool operacion_exitosa = true;
            bool guardar_img_exito = true;

            //convertur obj a producto
            productos oproducto = new productos();
            oproducto = JsonConvert.DeserializeObject<productos>(obj);
            decimal precio;
            if (decimal.TryParse(oproducto.preciotexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oproducto.precio = precio;
            }
            else
            {
                return Json(new { operacion_exitosa = false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            }

            if (oproducto.idproducto == 0)
            {
                int idproductogenerado = new producto_cn().registrar(oproducto, out mensaje);
                if (idproductogenerado != 0)
                {
                    oproducto.idproducto = idproductogenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                operacion_exitosa = new producto_cn().editar(oproducto, out mensaje);
            }
            if (operacion_exitosa)
            {
                if (archivoimagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFoto"];
                    string extension = Path.GetExtension(archivoimagen.FileName);
                    string nombre_imga = string.Concat(oproducto.idproducto.ToString(), extension);

                    try
                    {
                        archivoimagen.SaveAs(Path.Combine(ruta_guardar, nombre_imga));
                    }
                    catch (Exception ex)
                    {

                        string msg = ex.Message;
                        guardar_img_exito = false;
                    }
                    if (guardar_img_exito)
                    {
                        oproducto.rutaimagen = ruta_guardar;
                        oproducto.nombreimagen = nombre_imga;
                        bool rpta = new producto_cn().guardardatosimagen(oproducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }

                }
            }
            return Json(new { operacion_exitosa = operacion_exitosa, idgenerado = oproducto.idproducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult imagenproducto(int id)
        {
            bool conversion;
            productos oproducto = new producto_cn().listar().Where(p => p.idproducto == id).FirstOrDefault();
            string textobase64 = cn_recursos.convertirbase64(Path.Combine(oproducto.rutaimagen, oproducto.nombreimagen), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textobase64,
                extension = Path.GetExtension(oproducto.nombreimagen)
            },
            JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;

            respuesta = new producto_cn().Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}
