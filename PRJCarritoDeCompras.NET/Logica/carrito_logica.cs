using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text;

namespace PRJCarritoDeCompras.NET.Logica
{
    public class carrito_logica
    {
        public bool existecarrito(int idcliente,int idproducto)
        {
            bool resultado = true;
           
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_existeCarrito", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = false;           
            }
            return resultado;
        }


        public bool operacioncarrito(int idcliente ,int idproducto,bool sumar,out string mensaje)
        {
            bool resultado = true;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_operacionCarrito", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.AddWithValue("@sumar", sumar);
                    cmd.Parameters.Add("@resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@resultado"].Value);
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();





                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }



        ////select count(*) from carrito where idcliente=1
        ///

        public int cantidadcarrito(int idcliente)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("select count(*) from carrito where idcliente=@idcliente", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado =Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {
                resultado = 0;
             

            }
            return resultado;
        }



        public List<carrito> listarproductos(int idcliente)
        {
            List<carrito> lista = new List<carrito>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select * from fn_obetenercarritocliente(@idcliente)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new carrito()
                            {
                                oproducto=new productos()
                                {
                                    idproducto = Convert.ToInt32(dr["idproducto"]),
                                    nombre = dr["nombre"].ToString(),                               
                                    precio = Convert.ToDecimal(dr["precio"], new CultureInfo("es-PE")),                            
                                    rutaimagen = dr["rutaimagen"].ToString(),
                                    nombreimagen = dr["nombreimagen"].ToString(),
                                    omarca=new marca() { descripcion= dr["DesMarca"].ToString() }
                                },
                                cantidad= Convert.ToInt32(dr["cantidad"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<carrito>();
            }
            return lista;
        }


        public bool eliminarcarrito(int idcliente, int idproducto)
        {
            bool resultado = true;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminarcarrito", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@resultado"].Value);
                }
            }
            catch (Exception ex)
            { 
                resultado = false;
            }
            return resultado;
        }



    }
}