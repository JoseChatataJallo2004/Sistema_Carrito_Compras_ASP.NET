using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.CapaNegociio
{
    public class producto_cn
    {
        private producto_logica objcapaDato = new producto_logica();

        public List<productos> listar()
        {
            return objcapaDato.listar();
        }

        public int registrar(productos obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "el nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "el descripcion del producto no puede ser vacio";
            }
            else if (obj.omarca.idmarca == 0)
            {
                mensaje = "debes seleccionar una marca";
            }
            else if (obj.ocategoria.idcategoria == 0)
            {
                mensaje = "debes seleccionar una categoria";
            }
            else if (obj.precio == 0)
            {
                mensaje = "debes ingresar el precio del producto";
            }
            else if (obj.stock == 0)
            {
                mensaje = "debes ingresar el stock del producto";
            }


            if (string.IsNullOrEmpty(mensaje))
            {

                return objcapaDato.registrar(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool editar(productos obj, out string mensaje)
        {
            mensaje = String.Empty;
            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "el nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "el descripcion del producto no puede ser vacio";
            }
            else if (obj.omarca.idmarca == 0)
            {
                mensaje = "debes seleccionar una marca";
            }
            else if (obj.ocategoria.idcategoria == 0)
            {
                mensaje = "debes seleccionar una categoria";
            }
            else if (obj.precio == 0)
            {
                mensaje = "debes ingresar el precio del producto";
            }
            else if (obj.stock == 0)
            {
                mensaje = "debes ingresar el stock del producto";
            }
            if (string.IsNullOrEmpty(mensaje))
            {
                return objcapaDato.editar(obj, out mensaje);

            }
            else
            {
                return false;
            }
        }

        public bool guardardatosimagen(productos obj, out string mensaje)
        {
            return objcapaDato.guardardatosimagen(obj, out mensaje);
        }
        public bool Eliminar(int id)
        {
            return objcapaDato.Eliminar(id);
        }

    }
}