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
    public class DCliente
    {
        #region "PATRON SINGLETON"
        public static DCliente _instancia = null;

        private DCliente()
        {

        }

        public static DCliente GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DCliente();
            }
            return _instancia;
        }
        #endregion

        public Respuesta<bool> RegistrarCliente(ECliente cliente)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Ruc", cliente.Ruc);
                        cmd.Parameters.AddWithValue("@RazonSocial", cliente.RazonSocial);
                        cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Correo", cliente.Correo);

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
                return new Respuesta<bool>
                {
                    Estado = respuesta,
                    Valor = respuesta ? "Se registro correctamente" : "Error al registrar ingrese otro Ruc"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<List<ECliente>> ObtenerClientes()
        {
            try
            {
                List<ECliente> rptLista = new List<ECliente>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerClientes", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ECliente()
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    Ruc = dr["Ruc"].ToString(),
                                    RazonSocial = dr["RazonSocial"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ECliente>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Valor = "Clientes obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ECliente>>()
                {
                    Estado = false,
                    Valor = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<ECliente>> ObtenerClientesEn()
        {
            try
            {
                List<ECliente> rptLista = new List<ECliente>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerClientes", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ECliente()
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    // Aplicamos el método Decrypt para desencriptar los campos encriptados
                                    Ruc = EncryptacionH.Decrypt(dr["Ruc"].ToString()),
                                    RazonSocial = EncryptacionH.Decrypt(dr["RazonSocial"].ToString()),
                                    Direccion = EncryptacionH.Decrypt(dr["Direccion"].ToString()),
                                    Telefono = EncryptacionH.Decrypt(dr["Telefono"].ToString()),
                                    Correo = EncryptacionH.Decrypt(dr["Correo"].ToString()),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ECliente>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Valor = "Clientes obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ECliente>>()
                {
                    Estado = false,
                    Valor = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
