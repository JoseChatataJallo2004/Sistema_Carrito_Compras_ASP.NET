using PRJTiendaPresentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.Models
{
    public class carrito
    {
        public int idcarrito { get; set; }
        public cliente ocliente { get; set; }
        public productos oproducto { get; set; }
        public int cantidad { get; set; }
    }
}