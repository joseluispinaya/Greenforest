using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }
        [WebMethod]
        public static Respuesta<EUsuario> Logeo(string Usuario, string Clave)
        {
            try
            {
                var tok = string.Empty;
                var obj = NUsuario.GetInstance().LoginUsuarioWeb(Usuario, Clave);

                if (obj == null)
                {
                    return new Respuesta<EUsuario>() { Estado = false };
                }
                //Configuracion.Ousuario = obj;

                var tokenSesion = Guid.NewGuid().ToString();
                bool RespuTo = NUsuario.GetInstance().ActualizarToken(obj.IdUsuario, tokenSesion);
                if (RespuTo)
                {
                    tok = NUsuario.GetInstance().ObtenerToken(obj.IdUsuario);
                }

                return new Respuesta<EUsuario>() { Estado = true, Data = obj, Valor = tok };
            }
            catch (Exception ex)
            {
                return new Respuesta<EUsuario> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
                //throw;
            }
        }
        //original sin usar
        [WebMethod]
        public static Respuesta<EUsuario> LogeoOri(string Usuario, string Clave)
        {
            try
            {
                var tok = string.Empty;
                var obj = NUsuario.GetInstance().LoginUsuarioWeb(Usuario, Clave);

                if (obj == null)
                {
                    return new Respuesta<EUsuario>() { Estado = false };
                }
                Configuracion.Ousuario = obj;

                var tokenSesion = Guid.NewGuid().ToString();
                bool RespuTo = NUsuario.GetInstance().ActualizarToken(obj.IdUsuario, tokenSesion);
                if (RespuTo)
                {
                    tok = NUsuario.GetInstance().ObtenerToken(obj.IdUsuario);
                }

                return new Respuesta<EUsuario>() { Estado = true, Data = obj, Valor = tok };
            }
            catch (Exception ex)
            {
                return new Respuesta<EUsuario> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
                //throw;
            }
        }
    }
}