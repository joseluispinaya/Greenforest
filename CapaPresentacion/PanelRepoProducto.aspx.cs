using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;
using System.Globalization;

namespace CapaPresentacion
{
    public partial class PanelRepoProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Respuesta<List<EReporteVentaProd>> ObtenerReporteFechas(string fechainicio, string fechafin)
        {
            DateTime desde, hasta;

            try
            {
                // Intenta convertir las cadenas de fecha en objetos DateTime ret dd mm yy
                desde = DateTime.ParseExact(fechainicio, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                hasta = DateTime.ParseExact(fechafin, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // Convierte las fechas al formato deseado 'yyyy/MM/dd'

                List<EReporteVentaProd> oListaVentare = NVenta.GetInstance().ReportePorProductoFechas(desde, hasta);
                return new Respuesta<List<EReporteVentaProd>>() { Estado = true, Data = oListaVentare };
            }
            catch (Exception ex)
            {
                // Manejar errores de conversión de fecha
                return new Respuesta<List<EReporteVentaProd>>() { Estado = false, Valor = "Error al obtener la lista: " + ex.Message, Data = null };
            }
        }

        [WebMethod]
        public static Respuesta<List<EReporteVentaProd>> ObtenerReporteTodo()
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
    }
}