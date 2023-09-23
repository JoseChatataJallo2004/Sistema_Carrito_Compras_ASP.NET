using PRJCarritoDeCompras.NET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PRJCarritoDeCompras.NET.Logica
{
    public class ubicacion_logica
    {
        public List<departamento> ObtenerDepartamento()
        {
            List<departamento> lista = new List<departamento>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string sql = "select * from departamento";
                    SqlCommand cmd = new SqlCommand(sql, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new departamento
                            {
                                IdDepartamento = dr["IdDepartamento"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                
                            });
                        }
                    }
                }
            }
            catch
            {

                lista = new List<departamento>();
            }
            return lista;
        }


        public List<provincia> ObtenerProvincia(string iddepartamento)
        {
            List<provincia> lista = new List<provincia>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string sql = "select * from provincia where  IdDepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(sql, oconexion);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    //cmd.CommandType=System.Data.CommandType.Text;
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new provincia
                            {
                                IdProvincia = dr["IdProvincia"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {

                lista = new List<provincia>();
            }
            return lista;
        }

        public List<distrito> ObtenerDistrito(string idprovincia,string iddepartamento)
        {
            List<distrito> lista = new List<distrito>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string sql = "select * from distrito where IdProvincia = @idprovincia   and IdDepartamento = @iddepartamento ";
                    SqlCommand cmd = new SqlCommand(sql, oconexion);                   
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);
                    cmd.CommandType = System.Data.CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new distrito
                            {
                                IdDistrito = dr["IdDistrito"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }
                    }
                }
            }
            catch
            {

                lista = new List<distrito>();
            }
            return lista;
        }
    }
}