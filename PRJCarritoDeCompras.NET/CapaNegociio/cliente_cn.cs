using PRJCarritoDeCompras.NET.CapaNegociio;
using PRJTiendaPresentacion.Logica;
using PRJTiendaPresentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRJTiendaPresentacion.CapaNegocios
{
    public class cliente_cn
    {
        private cliente_logica objcapaDato = new cliente_logica();

        public List<cliente> listar()
        {
            return objcapaDato.listar();
        }

        public int registrar(cliente obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres))
            {
                mensaje = "el nombre del cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {
                mensaje = "el apellido del cliente no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                mensaje = "el correo del cliente no puede ser vacio";

            }


            if (string.IsNullOrEmpty(mensaje))
            {




                obj.clave = cn_recursos.convertirsha256(obj.clave);
                return objcapaDato.registrar(obj, out mensaje);


            }
            else
            {
                return 0;
            }
        }

        //panel de claves
        public bool cambiarclave(int idcliente, string nuevaclave, out string mensaje)
        {
            return objcapaDato.cambiarclave(idcliente, nuevaclave, out mensaje);
        }
        //reestablecer
        public bool reestablecerclave(int idcliente, string correo, out string mensaje)
        {
            mensaje = string.Empty;

            string nuevaclave = cn_recursos.generarclave();

            bool resultado = objcapaDato.reestablecerclave(idcliente, cn_recursos.convertirsha256(nuevaclave), out mensaje);

            if (resultado)
            {

                string asunto = "Contraseña reestablecida";
                string mensaje_correo = "<h3> Hola te habla el Ing. de Sistemas Jose Manuel   Su cuenta fue reestablecida correctamente </h3>" +
                    "</br>" +
                    "<img src='https://st5.depositphotos.com/28360198/61844/i/450/depositphotos_618440554-stock-photo-security-and-reset-password-login.jpg'> " + 
                    "<p>Su contraseña para acceder ahora es : !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);


                bool respuesta = cn_recursos.enviarcorreo(correo, asunto, mensaje_correo);
                if (respuesta)
                {
                    return true;
                }
                else
                {
                    mensaje = "No se pudo enviar al corre";
                    return false;
                }
            }
            else
            {
                mensaje = "No se pudo enviar al corre";
                return false;
            }

        }


    }
}