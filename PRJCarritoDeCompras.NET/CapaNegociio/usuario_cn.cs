using PRJCarritoDeCompras.NET.Logica;
using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.CapaNegociio
{
    public class usuario_cn
       
    {
        private usuario_logica objcapaDato=new usuario_logica();
        public List<usuario> listar()
        {
            return objcapaDato.listar();
        }

        public int registrar(usuario obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres))
            {
                mensaje = "el nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                mensaje = "el apellido del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "el correo del usuario no puede ser vacio";

            }
            if (string.IsNullOrEmpty(mensaje))
            {



                string clave = cn_recursos.generarclave();

                string asunto = "Creacion de Cuenta";
                string mensaje_correo = "<h3>Felicidades  Su cuenta fue creada correctamente </h3></br><p>Su contraseña para acceder a la Aplicacion es  : !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = cn_recursos.enviarcorreo(obj.correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.clave = cn_recursos.convertirsha256(clave);
                    return objcapaDato.registrar(obj, out mensaje);
                }
                else
                {
                    mensaje = "no se puede enviar el correo";
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }

        public bool editar(usuario obj, out string mensaje)
        {
            mensaje = String.Empty;
            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres))
            {
                mensaje = "el nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                mensaje = "el apellido del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "el correo del usuario no puede ser vacio";

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

        public bool eliminar(int id, out string mensaje)
        {
            return objcapaDato.eliminar(id, out mensaje);
        }
    }
}