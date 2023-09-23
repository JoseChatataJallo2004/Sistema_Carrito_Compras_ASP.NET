using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.Logica
{
    public class Conexion
    {
        public static string cn = ConfigurationManager.ConnectionStrings["cj"].ToString();
    }
}