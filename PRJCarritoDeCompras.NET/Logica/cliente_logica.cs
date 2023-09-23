using PRJTiendaPresentacion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using PRJCarritoDeCompras.NET.Logica;

namespace PRJTiendaPresentacion.Logica
{
    public class cliente_logica
    {
        public List<cliente> listar()
        {
            List<cliente> lista = new List<cliente>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select idcliente,nombres,apellidos,correo,clave,reestablecer from cliente ";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new cliente()
                            {
                                idcliente = Convert.ToInt32(dr["idcliente"]),
                                nombres = dr["nombres"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                correo = dr["correo"].ToString(),
                                clave = dr["clave"].ToString(),
                                reestablecer = Convert.ToBoolean(dr["reestablecer"])




                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<cliente>();
            }
            return lista;
        }

        public int registrar(cliente obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrar_cliente", oconexion);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@clave", obj.clave);
                    cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["@resultado"].Value);
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();





                }
            }
            catch (Exception ex)
            {

                idautogenerado = 0;
                mensaje = ex.Message;
            }
            return idautogenerado;
        }


        public bool cambiarclave(int idcliente, string nuevaclave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set clave = @nuevaclave,reestablecer=0 where idcliente=@id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idcliente);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }


        public bool reestablecerclave(int idcliente, string clave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set clave = @clave,reestablecer=1 where idcliente=@id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idcliente);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }

    }
}