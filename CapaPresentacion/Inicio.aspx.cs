using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Respuesta<EUsuario> ObtenerDatos()
        {
            try
            {
                if (Configuracion.Ousuario == null)
                {
                    return new Respuesta<EUsuario>() { Estado = false };
                }

                var usuario = Configuracion.Ousuario;
                var tok = NUsuario.GetInstance().ObtenerToken(usuario.IdUsuario);

                return new Respuesta<EUsuario>() { Estado = true, Data = usuario, Valor = tok };
            }
            catch (Exception ex)
            {
                return new Respuesta<EUsuario> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
                //throw;
            }
        }
        //sin usar por el momento
        [WebMethod]
        public static Respuesta<EUsuario> ObtenerDatosOri()
        {
            try
            {
                if (Configuracion.Ousuario == null)
                {
                    return new Respuesta<EUsuario>() { Estado = false };
                }

                var usuario = Configuracion.Ousuario;
                var tok = NUsuario.GetInstance().ObtenerToken(usuario.IdUsuario);

                return new Respuesta<EUsuario>() { Estado = true, Data = usuario, Valor = tok };
            }
            catch (Exception ex)
            {
                return new Respuesta<EUsuario> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
                //throw;
            }
        }

        
        [WebMethod]
        public static Respuesta<string> ObtenerToken(int IdUsu)
        {
            try
            {
                var tokenSesion = NUsuario.GetInstance().ObtenerToken(IdUsu);
                return new Respuesta<string>() { Estado = true, Valor = tokenSesion };
            }
            catch (Exception)
            {
                return new Respuesta<string>() { Estado = false };
            }
        }

        [WebMethod]
        public static Respuesta<bool> CerrarSesion()
        {
            //Configuracion.Ousuario = null;

            return new Respuesta<bool>() { Estado = true };

        }

        [WebMethod]
        public static Respuesta<EReporteVentaProd> ObtenerTotalesLabel()
        {
            List<EVenta> oListaVentare = NVenta.GetInstance().ObtenerListaVentaa();
            List<EReporteVentaProd> oListaVentTo = NVenta.GetInstance().ReportePorProductoN();
            float totalVenta = oListaVentTo.Sum(item => item.MontoTotal);

            List<EProducto> ListaPro = NProducto.GetInstance().ObtenerProductos();
            Respuesta<List<ECliente>> ListaCli = NCliente.GetInstance().ObtenerClientesEn();
            var lis = ListaCli.Data;
            EReporteVentaProd obj = new EReporteVentaProd
            {
                //totalVenta
                NombreProducto = oListaVentare.Count.ToString(),
                //totalIngresos
                Codigo = totalVenta.ToString(),
                //totalProductos
                Imagen = ListaPro.Count.ToString(),
                //totalClientes
                Descripcion = lis.Count.ToString()
            };
            if (obj != null)
            {
                return new Respuesta<EReporteVentaProd>() { Estado = true, Data = obj };
            }
            else
            {
                return new Respuesta<EReporteVentaProd>() { Estado = false, Data = null };
            }
        }

        [WebMethod]
        public static Respuesta<List<EReporteVentaProd>> DetalleVentaDash()
        {

            try
            {

                List<EReporteVentaProd> oListaVentare = NVenta.GetInstance().ReportePorProductoN();
                return new Respuesta<List<EReporteVentaProd>>() { Estado = true, Data = oListaVentare };
            }
            catch (Exception ex)
            {
                // Manejar errores de conversión de fecha
                return new Respuesta<List<EReporteVentaProd>>() { Estado = false, Valor = "Error al obtener la lista: " + ex.Message, Data = null };
            }
        }

        [WebMethod]
        public static Respuesta<List<EVenta>> ObtenerListaVenta()
        {
            DateTime FecIni = new DateTime(2024, 09, 16); 
            //DateTime FecIni = DateTime.Now.Date;
            DateTime Fechfin = DateTime.Now.Date;
            FecIni = FecIni.AddDays(-4);  // Restar 4 días desde la fecha actual
                                          // Obtener la lista completa
            List<EVenta> oListaVentare = NVenta.GetInstance().ObtenerListaVentaa();

            // Filtrar la lista por el rango de fechas usando solo la parte de la fecha
            List<EVenta> oListaVentaFiltrada = oListaVentare
                .Where(venta => venta.VFechaRegistro.Date >= FecIni && venta.VFechaRegistro.Date <= Fechfin)
                .ToList();
            var listc = from d in oListaVentaFiltrada
                        group d by d.FechaRegistro into totals
                        select new
                        {
                            fechas = totals.Key,
                            Cantidad = totals.Count()
                        };
            List<EVenta> oListaTabla = new List<EVenta>();
            foreach (var item in listc)
            {
                EVenta obj = new EVenta();
                obj.FechaRegistro = item.fechas;
                obj.IdVenta = item.Cantidad;

                oListaTabla.Add(obj);
            }
            if (oListaTabla != null)
            {
                return new Respuesta<List<EVenta>>() { Estado = true, Data = oListaTabla };
            }
            else
            {
                return new Respuesta<List<EVenta>>() { Estado = false, Data = null };
            }
        }
    }
}