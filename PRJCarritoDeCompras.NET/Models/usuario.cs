using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.Models
{
    public class usuario
    {
        public int idusuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public bool activo { get; set; }

    }
}