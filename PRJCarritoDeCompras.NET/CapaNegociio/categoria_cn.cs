using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.CapaNegociio
{
    public class categoria_cn
    {
        private categoria_logica  objcapaDato = new categoria_logica();

        public List<categoria> listar()
        {
            return objcapaDato.listar();
        }

        public int registrar(categoria obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "el descripcion de  la categoria no puede ser vacio";
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

        public bool editar(categoria obj, out string mensaje)
        {
            mensaje = String.Empty;
            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "el descripcion de  la categoria no puede ser vacio";
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
        public bool eliminar(int id)
        {
            return objcapaDato.Eliminar(id);
        }
    }
}