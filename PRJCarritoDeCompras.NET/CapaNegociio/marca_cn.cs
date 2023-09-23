using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.CapaNegociio
{
    public class marca_cn
    {
        private marca_logica objcapaDato = new marca_logica();


        public List<marca> listar()
        {
            return objcapaDato.listar();
        }

        public int registrar(marca obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "el descripcion de  la marca no puede ser vacio";
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

        public bool editar(marca obj, out string mensaje)
        {
            mensaje = String.Empty;
            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "el descripcion de  la marca no puede ser vacio";
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