using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;

namespace CapaDatos
{
    public class DVenta
    {
        #region "PATRON SINGLETON"
        public static DVenta _instancia = null;

        private DVenta()
        {

        }

        public static DVenta GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DVenta();
            }
            return _instancia;
        }
        #endregion

        public int RegistrarVentaIdclie(string Detalle)
        {
            int respuesta = 0;

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarVentaIdCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Detalle", SqlDbType.Xml).Value = Detalle;
                        cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        respuesta = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                //respuesta = 0;
                throw new Exception("Error SQL al registrar la venta", sqlEx);
            }
            catch (Exception ex)
            {
                //respuesta = 0;
                throw new Exception("Error al registrar venta", ex);
            }

            return respuesta;
        }

        public EVenta ObtenerDetalleVenta(int IdVenta)
        {
            EVenta rptDetalleVenta = null;

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ObtenerDetalleVenta", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);

                        con.Open();

                        using (XmlReader dr = cmd.ExecuteXmlReader())
                        {
                            if (dr.Read())
                            {
                                XDocument doc = XDocument.Load(dr);
                                var detalleReservaElement = doc.Element("DETALLE_VENTA");

                                if (detalleReservaElement != null)
                                {
                                    rptDetalleVenta = new EVenta
                                    {
                                        Codigo = detalleReservaElement.Element("Codigo").Value,
                                        CantidadTotal = int.Parse(detalleReservaElement.Element("CantidadTotal").Value),
                                        TotalCosto = float.Parse(detalleReservaElement.Element("TotalCosto").Value, CultureInfo.InvariantCulture),
                                        FechaRegistro = detalleReservaElement.Element("FechaRegistro").Value
                                    };

                                    var detalleClienteElement = detalleReservaElement.Element("DETALLE_CLIENTE");
                                    if (detalleClienteElement != null)
                                    {
                                        rptDetalleVenta.Cliente = new ECliente
                                        {
                                            IdCliente = int.Parse(detalleClienteElement.Element("IdCliente").Value),
                                            Ruc = EncryptacionH.Decrypt(detalleClienteElement.Element("RUC").Value),
                                            RazonSocial = EncryptacionH.Decrypt(detalleClienteElement.Element("RazonSocial").Value),
                                            Correo = EncryptacionH.Decrypt(detalleClienteElement.Element("Correo").Value)
                                        };
                                    }

                                    var detalleProductoElement = detalleReservaElement.Element("DETALLE_PRODUCTO");
                                    if (detalleProductoElement != null)
                                    {
                                        rptDetalleVenta.ListaDetalleVenta = detalleProductoElement.Elements("PRODUCTO")
                                            .Select(producto => new EDetalleVenta
                                            {
                                                IdProducto = int.Parse(producto.Element("IdProducto").Value),
                                                Cantidad = int.Parse(producto.Element("Cantidad").Value),
                                                NombreProducto = producto.Element("Nombre").Value,
                                                PrecioUnidad = float.Parse(producto.Element("PrecioUnidad").Value, CultureInfo.InvariantCulture),
                                                ImporteTotal = float.Parse(producto.Element("ImporteTotal").Value, CultureInfo.InvariantCulture)
                                            }).ToList();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                // Aquí puedes registrar el error en un log, si es necesario
                rptDetalleVenta = null;
                throw new Exception("Error al encontrar detalle. Intente más tarde.", ex);
            }

            return rptDetalleVenta;
        }

        public List<EVenta> ObtenerListaVentaa()
        {
            List<EVenta> rptListaUsuario = new List<EVenta>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerListaVenta", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptListaUsuario.Add(new EVenta()
                                {
                                    IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                    Codigo = dr["Codigo"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()),
                                    Cliente = new ECliente() 
                                    {
                                        //Ruc = EncryptacionH.Decrypt(dr["RazonSocial"].ToString()),
                                        IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                        Ruc = EncryptacionH.Decrypt(dr["RUC"].ToString()),
                                        RazonSocial = EncryptacionH.Decrypt(dr["RazonSocial"].ToString())
                                    },
                                    TotalCosto = float.Parse(dr["TotalCosto"].ToString()),
                                    CantidadTotal = Convert.ToInt32(dr["CantidadTotal"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("Error al obtener las ventas", ex);
            }

            return rptListaUsuario;
        }

        public List<VentaRepoFecha> ObtenerVentaRepoFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<VentaRepoFecha> rptListaUsuario = new List<VentaRepoFecha>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_VentaReporteFechas", con))
                    {
                        comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptListaUsuario.Add(new VentaRepoFecha()
                                {
                                    IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                    Codigo = dr["Codigo"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()),
                                    Cliente = EncryptacionH.Decrypt(dr["Razonsocial"].ToString()),
                                    Producto = dr["PRODUCTO"].ToString(),
                                    TotalCosto = float.Parse(dr["totalpago"].ToString())
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("Error al obtener las reporte ventas", ex);
            }

            return rptListaUsuario;
        }

        public List<EReporteVentaProd> ReportePorProductoN()
        {
            List<EReporteVentaProd> rptListaUsuario = new List<EReporteVentaProd>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerVentasPorProducto", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptListaUsuario.Add(new EReporteVentaProd()
                                {
                                    NombreProducto = dr["Nombre"].ToString(),
                                    Codigo = dr["Codigo"].ToString(),
                                    Imagen = dr["Foto"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    PrecioUnidadVenta = float.Parse(dr["PrecioUnidadVenta"].ToString()),
                                    CantidadTotal = Convert.ToInt32(dr["CantidadTotal"]),
                                    MontoTotal = float.Parse(dr["MontoTotal"].ToString())
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("Error al obtener reporte por producto", ex);
            }

            return rptListaUsuario;
        }

        public List<EReporteVentaProd> ReportePorProductoFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<EReporteVentaProd> rptListaUsuario = new List<EReporteVentaProd>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerVentasPorProductoFech", con))
                    {
                        comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptListaUsuario.Add(new EReporteVentaProd()
                                {
                                    NombreProducto = dr["Nombre"].ToString(),
                                    Codigo = dr["Codigo"].ToString(),
                                    Imagen = dr["Foto"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    PrecioUnidadVenta = float.Parse(dr["PrecioUnidadVenta"].ToString()),
                                    CantidadTotal = Convert.ToInt32(dr["CantidadTotal"]),
                                    MontoTotal = float.Parse(dr["MontoTotal"].ToString())
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("Error al obtener reporte por producto", ex);
            }

            return rptListaUsuario;
        }
    }
}
