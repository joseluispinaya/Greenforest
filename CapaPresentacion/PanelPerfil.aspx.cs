using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;
using System.IO;

namespace CapaPresentacion
{
    public partial class PanelPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<bool> EditarPerfil(EUsuario oUsuario, byte[] imageBytes)
        {
            try
            {
                // Validar que el usuario es correcto
                if (oUsuario == null || oUsuario.IdUsuario <= 0)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Datos de usuario inválidos" };
                }

                // Obtener el usuario existente
                var listaUsuarios = NUsuario.GetInstance().ObtenerUsuarios();
                var item = listaUsuarios.FirstOrDefault(x => x.IdUsuario == oUsuario.IdUsuario);
                if (item == null)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Usuario no encontrado" };
                }

                // Manejar la imagen, si se proporciona una nueva
                string imageUrl = item.Foto;  // Mantener la foto actual por defecto

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        string folder = "/Imagenes/";
                        string newImageUrl = Utilidadesj.GetInstance().UploadPhotoA(stream, folder);

                        if (!string.IsNullOrEmpty(newImageUrl))
                        {
                            // Eliminar la imagen anterior si existe
                            if (!string.IsNullOrEmpty(item.Foto))
                            {
                                string oldImagePath = HttpContext.Current.Server.MapPath(item.Foto);
                                if (File.Exists(oldImagePath))
                                {
                                    File.Delete(oldImagePath);
                                }
                            }
                            imageUrl = newImageUrl;
                        }
                    }
                }

                // Actualizar los datos del usuario
                item.IdUsuario = oUsuario.IdUsuario;
                item.Nombres = oUsuario.Nombres;
                item.Apellidos = oUsuario.Apellidos;
                item.Correo = oUsuario.Correo;
                item.Foto = imageUrl;

                // Guardar cambios
                bool resultado = NUsuario.GetInstance().ActualizarUsuario(item);

                return new Respuesta<bool>
                {
                    Estado = resultado,
                    Valor = resultado ? "Usuario actualizado correctamente" : "Error al actualizar el usuario, el correo ya existe"
                };
            }
            catch (IOException ioEx)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Error al manejar la imagen: " + ioEx.Message };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }


        [WebMethod]
        public static Respuesta<bool> CambiarClaveOri(int IdUsuario, string claveActual, string claveNueva)
        {
            try
            {
                if (IdUsuario <= 0)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "No se encro al Usuario Intente mas tarde" };
                }
                var listaUsuarios = NUsuario.GetInstance().ObtenerUsuarios();
                var item = listaUsuarios.FirstOrDefault(x => x.IdUsuario == IdUsuario);

                if (item == null)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Usuario no encontrado" };
                }
                if (item.Clave != claveActual)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Contraseña Actual Incorrecta" };
                }
                item.Clave = claveNueva;
                bool resultado = NUsuario.GetInstance().ActualizarUsuario(item);
                return new Respuesta<bool>
                {
                    Estado = resultado,
                    Valor = resultado ? "Contraseña Actualizada Correctamente" : "Error al actualizar la Contraseña, intente mas tarde"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }

        [WebMethod]
        public static Respuesta<bool> CambiarClave(int IdUsuario, string claveActual, string claveNueva)
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
                    return new Respuesta<bool>() { Estado = false, Valor = "Usuario no encontrado" };
                }
                //string claveDesen = EncryptacionH.Decrypt(item.Clave);
                // Verificar si la contraseña actual ingresada coincide con la almacenada (después de generar el hash)
                if (!utilidades.VerificarClave(item.Clave, claveActual)) // Compara el hash
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Contraseña actual incorrecta" };
                }
                // Validar que la nueva clave no sea igual a la actual
                if (claveActual == claveNueva)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "La nueva contraseña no puede ser igual a la actual" };
                }
                item.Clave = utilidades.GenerarHashClave(claveNueva);
                //item.Clave = EncryptacionH.Encrypt(claveNueva);
                bool resultado = NUsuario.GetInstance().ActualizarUsuario(item);
                return new Respuesta<bool>
                {
                    Estado = resultado,
                    Valor = resultado ? "Contraseña Actualizada Correctamente" : "Error al actualizar la Contraseña, intente mas tarde"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }
        
    }
}