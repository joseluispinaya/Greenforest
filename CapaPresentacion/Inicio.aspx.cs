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

        //sin usar por el momento
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
    }
}