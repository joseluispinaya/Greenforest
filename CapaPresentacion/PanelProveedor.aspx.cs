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
    public partial class PanelProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<EProveedor>> ObtenerProvee()
        {
            try
            {
                List<EProveedor> Lista = NProveedor.GetInstance().ObtenerProveedor();
                return new Respuesta<List<EProveedor>>()
                {
                    Estado = true,
                    Data = Lista,
                    Valor = "Comptas obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EProveedor>>()
                {
                    Estado = false,
                    Valor = "Error al obtener los Usuarios: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> Guardar(EProveedor oProvee)
        {
            try
            {
                Respuesta<bool> respuesta = NProveedor.GetInstance().RegistrarProveedor(oProvee);

                return respuesta;

            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }

        [WebMethod]
        public static Respuesta<bool> Editar(EProveedor oProvee)
        {
            try
            {
                Respuesta<bool> respuesta = NProveedor.GetInstance().EditarProveedor(oProvee);

                return respuesta;

            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }
    }
}