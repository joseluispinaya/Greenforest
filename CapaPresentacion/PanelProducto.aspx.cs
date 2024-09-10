using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;

namespace CapaPresentacion
{
    public partial class PanelProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<EProducto>> ObtenerProductos()
        {
            try
            {
                List<EProducto> Lista = NProducto.GetInstance().ObtenerProductos();
                return new Respuesta<List<EProducto>>()
                {
                    Estado = true,
                    Data = Lista,
                    Valor = "Productos obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EProducto>>()
                {
                    Estado = false,
                    Valor = "Error al obtener los Productos: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> GuardarProductoNu(EProducto oProducto, byte[] imageBytes)
        {
            try
            {
                var imageUrl = string.Empty;

                // Si se proporcionan bytes de la imagen, súbela y guarda la URL
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        string folder = "/ImagePro/";
                        imageUrl = Utilidadesj.GetInstance().UploadPhotoA(stream, folder);
                    }
                }

                // Crea el objeto EProducto con los datos proporcionados
                EProducto obj = new EProducto
                {
                    Nombre = oProducto.Nombre,
                    Descripcion = oProducto.Descripcion,
                    PrecioUnidadVenta = oProducto.PrecioUnidadVenta,
                    Imagen = imageUrl
                };

                // Llama al método RegistrarProductoNuevo de la capa negocio
                Respuesta<bool> respuesta = NProducto.GetInstance().RegistrarProductoNuevo(obj);

                // Retorna la respuesta recibida desde la capa negocio
                return respuesta;
            }
            catch (Exception ex)
            {
                // En caso de error, retorna una respuesta indicando el fallo
                return new Respuesta<bool> { Estado = false, Valor = "Ocurrió un error: " + ex.Message };
            }
        }

        [WebMethod]
        public static Respuesta<bool> EditarProducto(EProducto oProducto, byte[] imageBytes)
        {
            try
            {
                // Validar que el usuario es correcto
                if (oProducto == null || oProducto.IdProducto <= 0)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Datos de Producto inválidos" };
                }

                // Obtener el usuario existente
                var listaP = NProducto.GetInstance().ObtenerProductos();
                var item = listaP.FirstOrDefault(x => x.IdProducto == oProducto.IdProducto);
                if (item == null)
                {
                    return new Respuesta<bool>() { Estado = false, Valor = "Producto no encontrado" };
                }

                // Manejar la imagen, si se proporciona una nueva
                string imageUrl = item.Imagen;  // Mantener la foto actual por defecto

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        string folder = "/ImagePro/";
                        string newImageUrl = Utilidadesj.GetInstance().UploadPhotoA(stream, folder);

                        if (!string.IsNullOrEmpty(newImageUrl))
                        {
                            // Eliminar la imagen anterior si existe
                            if (!string.IsNullOrEmpty(item.Imagen))
                            {
                                string oldImagePath = HttpContext.Current.Server.MapPath(item.Imagen);
                                if (File.Exists(oldImagePath))
                                {
                                    File.Delete(oldImagePath);
                                }
                            }
                            imageUrl = newImageUrl;
                        }
                    }
                }

                // Actualizar los datos del usuario imageUrl
                item.IdProducto = oProducto.IdProducto;
                item.Nombre = oProducto.Nombre;
                item.Descripcion = oProducto.Descripcion;
                item.PrecioUnidadVenta = oProducto.PrecioUnidadVenta;
                item.Imagen = imageUrl;
                item.Activo = oProducto.Activo;

                // Llama al método ActualizarProductoNuevo de la capa negocio
                Respuesta<bool> respuesta = NProducto.GetInstance().ActualizarProductoNuevo(item);
                return respuesta;

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