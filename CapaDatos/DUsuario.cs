using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class DUsuario
    {
        #region "PATRON SINGLETON"
        public static DUsuario _instancia = null;

        private DUsuario()
        {

        }

        public static DUsuario GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DUsuario();
            }
            return _instancia;
        }
        #endregion

        public bool RegistrarUsuario(EUsuario oUsuario)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarUsuario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombres", oUsuario.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", oUsuario.Apellidos);
                        cmd.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                        cmd.Parameters.AddWithValue("@Clave", oUsuario.Clave);
                        cmd.Parameters.AddWithValue("@Foto", oUsuario.Foto);
                        cmd.Parameters.AddWithValue("@IdRol", oUsuario.IdRol);
                        cmd.Parameters.AddWithValue("@TokenSesion", oUsuario.TokenSesion);

                        SqlParameter outputParam = new SqlParameter("@Resultado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        respuesta = Convert.ToBoolean(outputParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar. Intente más tarde.", ex);
            }

            return respuesta;
        }

        public bool ActualizarUsuario(EUsuario oUsuario)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ModificarUsuario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdUsuario", oUsuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombres", oUsuario.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", oUsuario.Apellidos);
                        cmd.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                        cmd.Parameters.AddWithValue("@Clave", oUsuario.Clave);
                        cmd.Parameters.AddWithValue("@Foto", oUsuario.Foto);
                        cmd.Parameters.AddWithValue("@IdRol", oUsuario.IdRol);
                        cmd.Parameters.AddWithValue("@Activo", oUsuario.Estado);

                        SqlParameter outputParam = new SqlParameter("@Resultado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        respuesta = Convert.ToBoolean(outputParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Actualizar. Intente más tarde.", ex);
            }

            return respuesta;
        }

        public List<EUsuario> ObtenerUsuariosZ()
        {
            List<EUsuario> rptListaUsuario = new List<EUsuario>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerUsuario", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptListaUsuario.Add(new EUsuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Foto = dr["Foto"].ToString(),
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Orol = new ERol() { NomRol = dr["DescripcionRol"].ToString() },
                                    Estado = Convert.ToBoolean(dr["Activo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("Error al obtener los Usuarios", ex);
            }

            return rptListaUsuario;
        }

        public bool ActualizarToken(int IdUsu, string token)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ActualizaToken", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsu);
                        cmd.Parameters.AddWithValue("@TokenSesion", token);

                        SqlParameter outputParam = new SqlParameter("@Resultado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        respuesta = Convert.ToBoolean(outputParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Actualizar Token. Intente más tarde.", ex);
            }

            return respuesta;
        }

        public string ObtenerToken(int IdUsu)
        {
            var tokenSesion = string.Empty;

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ConsultaToken", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsu);
                        SqlParameter outputParam = new SqlParameter("@TokenSesion", SqlDbType.NVarChar, 60)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        tokenSesion = outputParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer token.", ex);
            }

            return tokenSesion;
        }

        public EUsuario LoginUsuarioWeb(string Usuario, string Clave)
        {
            EUsuario obj = null;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand Comando = new SqlCommand("sp_LogeoUsua", con))
                    {
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.Parameters.AddWithValue("@Correo", Usuario);
                        Comando.Parameters.AddWithValue("@Clave", Clave);

                        con.Open();
                        using (SqlDataReader dr = Comando.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                obj = new EUsuario
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Foto = dr["Foto"].ToString(),
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Estado = Convert.ToBoolean(dr["Activo"]),
                                    Orol = new ERol { NomRol = dr["Descripcion"].ToString() }
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general", ex);
            }
            return obj;
        }
    }
}
