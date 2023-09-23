using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJTiendaPresentacion.Models
{
    public class cliente
    {
        public int idcliente { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string confirmarclave { get; set; }
        public bool reestablecer { get; set; }
    }
}