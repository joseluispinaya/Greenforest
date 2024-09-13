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
    public partial class PanelCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<ECliente>> Obtener()
        {
            try
            {
                Respuesta<List<ECliente>> Lista = NCliente.GetInstance().ObtenerClientesEn();
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ECliente>>()
                {
                    Estado = false,
                    Valor = "Error al obtener los Clientes: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<List<ECliente>> ObtenerEncriptado()
        {
            try
            {
                Respuesta<List<ECliente>> Lista = NCliente.GetInstance().ObtenerClientes();
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ECliente>>()
                {
                    Estado = false,
                    Valor = "Error al obtener los Clientes: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> GuardarPru(ECliente oCliente)
        {
            try
            {
                ECliente obj = new ECliente
                {
                    Ruc = EncryptacionH.Encrypt(oCliente.Ruc),
                    RazonSocial = EncryptacionH.Encrypt(oCliente.RazonSocial),
                    Direccion = EncryptacionH.Encrypt(oCliente.Direccion),
                    Telefono = EncryptacionH.Encrypt(oCliente.Telefono),
                    Correo = EncryptacionH.Encrypt(oCliente.Correo)
                };

                //var es = obj;
                //if (es != null)
                //{
                //    return new Respuesta<bool> { Estado = true, Valor = "Se registro correctamente" };
                //}
                //else
                //{
                //    return new Respuesta<bool> { Estado = false, Valor = "Error es nulo" };
                //}

                Respuesta<bool> respuesta = NCliente.GetInstance().RegistrarCliente(obj);

                return respuesta;

            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }
        [WebMethod]
        public static Respuesta<bool> Guardar(ECliente oCliente)
        {
            try
            {
                ECliente obj = new ECliente
                {
                    Ruc = oCliente.Ruc,
                    RazonSocial = oCliente.RazonSocial,
                    Direccion = oCliente.Direccion,
                    Telefono = oCliente.Telefono,
                    Correo = oCliente.Correo
                };

                Respuesta<bool> respuesta = NCliente.GetInstance().RegistrarCliente(obj);

                return respuesta;

            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }
    }
}