using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NVenta
    {
        #region "PATRON SINGLETON"
        private static NVenta daoEmpleado = null;
        private NVenta() { }
        public static NVenta GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NVenta();
            }
            return daoEmpleado;
        }
        #endregion

        public int RegistrarVentaIdclie(string Detalle)
        {
            return DVenta.GetInstance().RegistrarVentaIdclie(Detalle);
        }

        public EVenta ObtenerDetalleVenta(int IdVenta)
        {
            return DVenta.GetInstance().ObtenerDetalleVenta(IdVenta);
        }

        public List<EVenta> ObtenerListaVentaa()
        {
            return DVenta.GetInstance().ObtenerListaVentaa();
        }

        public List<VentaRepoFecha> ObtenerVentaRepoFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            return DVenta.GetInstance().ObtenerVentaRepoFechas(FechaInicio, FechaFin);
        }

        public List<EReporteVentaProd> ReportePorProductoN()
        {
            return DVenta.GetInstance().ReportePorProductoN();
        }

        public List<EReporteVentaProd> ReportePorProductoFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            return DVenta.GetInstance().ReportePorProductoFechas(FechaInicio, FechaFin);
        }
    }
}
