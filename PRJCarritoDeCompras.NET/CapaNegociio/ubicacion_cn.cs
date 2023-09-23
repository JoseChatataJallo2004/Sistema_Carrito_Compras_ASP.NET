using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.CapaNegociio
{
    public class ubicacion_cn
    {
        private ubicacion_logica objcapadato=new ubicacion_logica();


        public List<departamento> ObtenerDepartamento()
        {
            return objcapadato.ObtenerDepartamento();
        }

        public List<provincia> ObtenerProvincia(string iddepartamento)
        {
            return objcapadato.ObtenerProvincia(iddepartamento);
        }
        public List<distrito> ObtenerDistrito(string idprovincia, string iddepartamento)
        {
            return objcapadato.ObtenerDistrito(idprovincia, iddepartamento);
        }
    }
}