using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.CapaNegociio
{
    public class carrito_cn
    {
        private carrito_logica objcapaDato =new carrito_logica();

        public bool existecarrito(int idcliente, int idproducto)
        {
            return objcapaDato.existecarrito(idcliente, idproducto);
        }

        public bool operacioncarrito(int idcliente, int idproducto, bool sumar, out string mensaje)
        {
            return objcapaDato.operacioncarrito(idcliente, idproducto, sumar,out mensaje);
        }

        public int cantidadcarrito(int idcliente)
        {
            return objcapaDato.cantidadcarrito(idcliente);
        }


        public List<carrito> listarproductos(int idcliente)
        {
            return objcapaDato.listarproductos(idcliente);
        }
        public bool eliminarcarrito(int idcliente, int idproducto)
        {
            return objcapaDato.eliminarcarrito(idcliente, idproducto);
        }
    }
}