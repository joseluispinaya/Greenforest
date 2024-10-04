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
                string ClaveEncri = Utilidadesj.GetInstance().GenerarHashClave(Clave);
                //string ClaveEncri = EncryptacionH.Encrypt(Clave);
                var tok = string.Empty;
                var obj = NUsuario.GetInstance().LoginUsuarioWeb(Usuario, ClaveEncri);
                //var obj = NUsuario.GetInstance().LoginUsuarioWeb(Usuario, Clave);
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

        [WebMethod]
        public static Respuesta<bool> RecuperacionCl(string correo)
        {
            try
            {
                List<EUsuario> Lista = NUsuario.GetInstance().ObtenerUsuarios();
                var item = Lista.FirstOrDefault(x => x.Correo == correo);
                if (item == null)
                {
                    return new Respuesta<bool>()
                    {
                        Estado = false,
                        Valor = "El correo ingresado no existe"
                    };
                }

                bool enviocorr = EnvioRecuperacion(item.Correo, item.Clave);

                return new Respuesta<bool>()
                {
                    Estado = enviocorr,
                    Valor = enviocorr ? "Se envio un Correo de recuperacion" : "Ocurrio un error en el envio intente mas tarde"
                };
            }
            catch (Exception)
            {
                return new Respuesta<bool>()
                {
                    Estado = false,
                    Valor = "Ocurrió un error intente mas tarde"
                };
            }
        }
        private static bool EnvioRecuperacion(string correo, string clave)
        {
            try
            {
                return Utilidadesj.GetInstance().Recuperacion(correo, clave);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}