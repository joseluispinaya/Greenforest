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
    }
}