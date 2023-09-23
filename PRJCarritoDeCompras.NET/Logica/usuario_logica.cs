using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.Logica
{
    public class usuario_logica
    {
        private static usuario_logica _instancia = null;

        public usuario_logica()
        {

        }
        public static usuario_logica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new usuario_logica();
                }

                return _instancia;
            }
        }



        public List<usuario> listar()
		{
            List<usuario> lista = new List<usuario>();
            try
            {
                using(SqlConnection oconexion=new SqlConnection(Conexion.cn))
                {
                    string sql = "select idusuario,nombres,apellidos,correo,clave,activo from usuario";
                    SqlCommand cmd=new SqlCommand(sql, oconexion);
                    oconexion.Open();
                    using(SqlDataReader dr=cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            lista.Add(new usuario
                            {
                                idusuario = Convert.ToInt32(dr["idusuario"]),
                                nombres = dr["nombres"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                correo = dr["correo"].ToString(),
                                clave = dr["clave"].ToString(),                          
                                activo = Convert.ToBoolean(dr["activo"]),
                            });
                        }
                    }
                }
            }
            catch 
            {

                lista = new List<usuario>();
            }
            return lista;
        }
       
        public int registrar(usuario obj,out string mensaje)
        {
            int idautogenerado = 0;
            mensaje=string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@clave", obj.clave);
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

        public bool editar(usuario obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_editar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("@idusuario", obj.idusuario);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
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

        public bool eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from usuario where idusuario=@id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
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


        public bool cambiarclave(int idusuario, string nuevaclave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set clave = @nuevaclave where idusuario=@id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
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



    }
}