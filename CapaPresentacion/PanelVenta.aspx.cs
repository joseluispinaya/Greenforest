using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;

namespace CapaPresentacion
{
    public partial class PanelVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<ECliente>> BuscarClie(string buscar)
        {
            Respuesta<List<ECliente>> Lista = NCliente.GetInstance().ObtenerClientesEn();
            var listafi = Lista.Data;
            if (listafi != null)
            {
                var listaFiltrada = listafi.Where(u => u.RazonSocial.IndexOf(buscar, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                return new Respuesta<List<ECliente>>() { Estado = true, Data = listaFiltrada };
            }
            else
            {
                return new Respuesta<List<ECliente>>() { Estado = false, Data = null };
            }
        }


        [WebMethod]
        public static Respuesta<List<EProducto>> BuscarPro(string buscar)
        {
            List<EProducto> Lista = NProducto.GetInstance().ObtenerProductosFil(buscar);
            //Lista = NUsuario.getInstance().ObtenerUsuarios();

            if (Lista != null)
            {
                return new Respuesta<List<EProducto>>() { Estado = true, Data = Lista };
            }
            else
            {
                return new Respuesta<List<EProducto>>() { Estado = false, Data = null };
            }
        }

        [WebMethod]
        public static Respuesta<int> GuardarVentaIdCliente(string xml)
        {
            try
            {
                //var llego = xml;
                int respuesta = NVenta.GetInstance().RegistrarVentaIdclie(xml);
                //int respuesta = 14;
                if (respuesta != 0)
                {
                    return new Respuesta<int>() { Estado = true, Valor = respuesta.ToString() };
                }
                else
                {
                    return new Respuesta<int>() { Estado = false, Valor = "No se pudo registrar la venta." };
                }
            }
            catch (Exception ex)
            {
                return new Respuesta<int>() { Estado = false, Valor = $"Error al registrar la venta: {ex.Message}" };
            }
        }

        [WebMethod]
        public static Respuesta<EVenta> DetalleVenta(int IdVenta)
        {
            try
            {
                EVenta oVenta = NVenta.GetInstance().ObtenerDetalleVenta(IdVenta);
                if (oVenta != null)
                {
                    return new Respuesta<EVenta>() { Estado = true, Data = oVenta };
                }
                else
                {
                    return new Respuesta<EVenta>() { Estado = false, Data = null, Valor = "No se pudo encontrar la reserva" };
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return new Respuesta<EVenta>() { Estado = false, Data = null, Valor = ex.Message };
            }
        }
    }
}