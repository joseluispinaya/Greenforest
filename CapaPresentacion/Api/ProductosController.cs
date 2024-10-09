using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion.Api
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        [HttpGet]
        [Route("lista")]
        public IHttpActionResult GetListapro()
        {
            try
            {
                List<EProducto> Lista = NProducto.GetInstance().ObtenerProductos();
                return Ok(Lista);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult PostProducto(EProducto oProducto)
        {
            try
            {
                var imageUrl = string.Empty;
                // Llamada al método ValidarClienteDTO
                if (oProducto == null)
                {
                    return BadRequest("Debe ingresar datos para el registro."); // Retorna el mensaje de error específico
                }

                EProducto obj = new EProducto
                {
                    Nombre = oProducto.Nombre,
                    Descripcion = oProducto.Descripcion,
                    PrecioUnidadVenta = oProducto.PrecioUnidadVenta,
                    Imagen = imageUrl
                };
                Respuesta<bool> res = NProducto.GetInstance().RegistrarProductoNuevo(obj);
                var respuesta = new Respuesta<bool>
                {
                    Estado = res.Estado,
                    Valor = res.Estado ? "Se registro correctamente" : "Error al registrar el producto"
                };
                return Ok(respuesta);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}