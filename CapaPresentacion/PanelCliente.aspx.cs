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
        public static Respuesta<bool> ApagarApi(bool Vale)
        {
            //Configuracion.Ousuario = null;
            AjusteApi.Vale = Vale;

            return new Respuesta<bool>() { Estado = true };

        }

        [WebMethod]
        public static Respuesta<bool> ApagarApiobt()
        {
            //Configuracion.Ousuario = null;
            //AjusteApi.Vale = Vale;
            bool re = AjusteApi.Vale;

            return new Respuesta<bool>() { Estado = re };

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
                bool re = AjusteApi.Vale;

                if (!re)
                {
                    return new Respuesta<bool> { Estado = false, Valor = "El servidor esta Apagado" };
                }
                if (!ValidarRuc(oCliente.Ruc))
                {
                    return new Respuesta<bool> { Estado = false, Valor = "Ya existe el RUC" };
                }

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

        private static bool ValidarRuc(string ruc)
        {
            try
            {
                Respuesta<List<ECliente>> Lista = NCliente.GetInstance().ObtenerClientesEn();
                var listafi = Lista.Data;
                var item = listafi.FirstOrDefault(x => x.Ruc == ruc);

                if (item != null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
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

        [WebMethod]
        public static Respuesta<bool> ValidarClave(int IdUsuario, string claveActual)
        {
            try
            {
                var utilidades = Utilidadesj.GetInstance();

                if (IdUsuario <= 0)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "No se encro al Usuario Intente mas tarde" };
                }
                var listaUsuarios = NUsuario.GetInstance().ObtenerUsuarios();
                var item = listaUsuarios.FirstOrDefault(x => x.IdUsuario == IdUsuario);

                if (item == null)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Usuario No Tiene Nivel de Acceso" };
                }
                if (!utilidades.VerificarClave(item.Clave, claveActual))
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Su Contraseña es Incorrecta" };
                }
                return new Respuesta<bool>() { Estado = true, Valor = "Nivel de Acceso Aprobado" };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }
    }
}