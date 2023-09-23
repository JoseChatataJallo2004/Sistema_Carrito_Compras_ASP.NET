using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Text;

namespace PRJCarritoDeCompras.NET.Logica
{
    public class producto_logica
    {
        public List<productos> listar()
        {
            List<productos> lista = new List<productos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    //me ayuda en el salto de linea 
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.idproducto,p.nombre,p.descripcion,");
                    sb.AppendLine("m.idmarca,m.descripcion[DesMarca],");
                    sb.AppendLine("c.idcategoria,c.descripcion[DesCategoria],");
                    sb.AppendLine("p.precio,p.stock,p.rutaimagen,p.nombreimagen,p.activo");
                    sb.AppendLine("from producto p inner join marca m on m.idmarca=p.idmarca inner join categoria c on c.idcategoria=p.idcategoria");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new productos()
                            {
                                idproducto = Convert.ToInt32(dr["idproducto"]),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                omarca = new marca() { idmarca = Convert.ToInt32(dr["idmarca"]), descripcion = dr["DesMarca"].ToString() },
                                ocategoria = new categoria() { idcategoria = Convert.ToInt32(dr["idcategoria"]), descripcion = dr["DesCategoria"].ToString() },
                                precio = Convert.ToDecimal(dr["precio"], new CultureInfo("es-PE")),
                                stock = Convert.ToInt32(dr["stock"]),
                                rutaimagen = dr["rutaimagen"].ToString(),
                                nombreimagen = dr["nombreimagen"].ToString(),
                                activo = Convert.ToBoolean(dr["activo"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<productos>();
            }
            return lista;
        }

        public int registrar(productos obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrar_producto", oconexion);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@idmarca", obj.omarca.idmarca);
                    cmd.Parameters.AddWithValue("@idcategoria", obj.ocategoria.idcategoria);
                    cmd.Parameters.AddWithValue("@precio", obj.precio);
                    cmd.Parameters.AddWithValue("@stock", obj.stock);
                    cmd.Parameters.AddWithValue("@activo", obj.activo);
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

        public bool editar(productos obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_editar_producto", oconexion);
                    cmd.Parameters.AddWithValue("@idproducto", obj.idproducto);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@idmarca", obj.omarca.idmarca);
                    cmd.Parameters.AddWithValue("@idcategoria", obj.ocategoria.idcategoria);
                    cmd.Parameters.AddWithValue("@precio", obj.precio);
                    cmd.Parameters.AddWithValue("@stock", obj.stock);
                    cmd.Parameters.AddWithValue("@activo", obj.activo);
                    cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        public bool guardardatosimagen(productos obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string jquery = "update producto set rutaimagen =@rutaimagen,nombreimagen=@nombreimagen where idproducto=@idproducto";
                    SqlCommand cmd = new SqlCommand(jquery, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.rutaimagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.nombreimagen);
                    cmd.Parameters.AddWithValue("@idproducto", obj.idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        mensaje = "No se pudo actualizar imagen";
                    }

                }
            }
            catch (Exception ex)
            {

                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from producto where idproducto = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

    }
}