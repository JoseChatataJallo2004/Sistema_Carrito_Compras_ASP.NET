using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//referencias para enviar correo
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PRJCarritoDeCompras.NET.CapaNegociio
{
    public class cn_recursos
    {
        public static string generarclave()
        {
            //que sea nuero y clave del 0 al  6 
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }
        //encriptacion de texto en sha256
        public static string convertirsha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            //usar refenrencia de system.security.cryptography
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        //para el correo debe ser de google en tu cuenta de gmail ir a verificacion de 2 pasos y contraseñas de aplicaciones
        public static bool enviarcorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("jchatatajallo@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("jchatatajallo@gmail.com", "jfgntkopsfkhjfhp"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                };
                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            return resultado;
        }

        //foto del producto
        public static string convertirbase64(string ruta, out bool conversion)
        {
            string textobase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textobase64 = Convert.ToBase64String(bytes);
            }
            catch
            {

                conversion = false;
            }
            return textobase64;
        }

    }
}