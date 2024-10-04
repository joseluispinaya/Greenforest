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
    public partial class PanelUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<ERol>> ObtenerRol()
        {
            try
            {
                List<ERol> Lista = NTipos.GetInstance().ObtenerRol();

                // Devuelve una respuesta exitosa, ya que Lista nunca será null
                return new Respuesta<List<ERol>>()
                {
                    Estado = true,
                    Data = Lista,
                    Valor = "Roles obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ERol>>()
                {
                    Estado = false,
                    Valor = "Error al obtener los roles: " + ex.Message,
                    Data = null
                };
            }
            //List<ERol> Lista = NTipos.GetInstance().ObtenerRol();
            ////Lista = NTipos.getInstance().ObtenerRol();

            //if (Lista != null)
            //{
            //    return new Respuesta<List<ERol>>() { Estado = true, Data = Lista };
            //}
            //else
            //{
            //    return new Respuesta<List<ERol>>() { Estado = false, Data = null };
            //}
        }

        [WebMethod]
        public static Respuesta<List<EUsuario>> ObtenerUsuario()
        {
            try
            {
                List<EUsuario> Lista = NUsuario.GetInstance().ObtenerUsuarios();
                return new Respuesta<List<EUsuario>>()
                {
                    Estado = true,
                    Data = Lista,
                    Valor = "Roles obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EUsuario>>()
                {
                    Estado = false,
                    Valor = "Error al obtener los Usuarios: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> GuardarUsua(EUsuario oUsuario, byte[] imageBytes)
        {
            try
            {
                var utilidades = Utilidadesj.GetInstance();
                var imageUrl = string.Empty;

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        string folder = "/Imagenes/";
                        //imageUrl = Utilidadesj.GetInstance().UploadPhotoA(stream, folder);
                        imageUrl = utilidades.UploadPhotoA(stream, folder);
                    }
                }
                
                EUsuario obj = new EUsuario
                {
                    Nombres = oUsuario.Nombres,
                    Apellidos = oUsuario.Apellidos,
                    Correo = oUsuario.Correo,
                    //Clave = oUsuario.Clave,
                    //Clave = EncryptacionH.Encrypt(oUsuario.Clave),
                    Clave = utilidades.GenerarHashClave(oUsuario.Clave),
                    Foto = imageUrl,
                    IdRol = oUsuario.IdRol,
                    TokenSesion = Guid.NewGuid().ToString()
                };
                bool Resul = NUsuario.GetInstance().RegistrarUsuario(obj);

                var respuesta = new Respuesta<bool>
                {
                    Estado = Resul,
                    Valor = Resul ? "Se registro correctamente" : "Error al registrar ingrese otro correo"
                };
                return respuesta;

            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }

        [WebMethod]
        public static Respuesta<bool> EditarUsuario(EUsuario oUsuario, byte[] imageBytes)
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
                item.Clave = oUsuario.Clave;
                item.Foto = imageUrl;
                item.IdRol = oUsuario.IdRol;
                item.Estado = oUsuario.Estado;

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
        public static Respuesta<bool> EditarUsuarioNo(EUsuario oUsuario, byte[] imageBytes)
        {
            try
            {
                var imageUrl = string.Empty;

                List<EUsuario> Lista = NUsuario.GetInstance().ObtenerUsuarios();
                var item = Lista.FirstOrDefault(x => x.IdUsuario == oUsuario.IdUsuario);
                if (item == null)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Ocurrio un inconveniente intente mas tarde" };
                }
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    //var stream = new MemoryStream(imageBytes);
                    //string folder = "/Imagenes/";
                    //imageUrl = Utilidadesj.GetInstance().UploadPhotoA(stream, folder);
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        string folder = "/Imagenes/";
                        imageUrl = Utilidadesj.GetInstance().UploadPhotoA(stream, folder);

                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            if (!string.IsNullOrEmpty(item.Foto))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(item.Foto));
                            }
                        }
                        else
                        {
                            imageUrl = item.Foto;
                        }
                    }
                    
                }
                else
                {
                    imageUrl = item.Foto;
                }

                item.IdUsuario = oUsuario.IdUsuario;
                item.Nombres = oUsuario.Nombres;
                item.Apellidos = oUsuario.Apellidos;
                item.Correo = oUsuario.Correo;
                item.Clave = oUsuario.Clave;
                item.Foto = imageUrl;
                item.IdRol = oUsuario.IdRol;
                item.Estado = oUsuario.Estado;


                bool Resul = NUsuario.GetInstance().ActualizarUsuario(item);

                var respuesta = new Respuesta<bool>
                {
                    Estado = Resul,
                    Valor = Resul ? "Actualizado correctamente" : "Error al actualizar el Correo ya Existe"
                };
                return respuesta;

            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }
    }
}