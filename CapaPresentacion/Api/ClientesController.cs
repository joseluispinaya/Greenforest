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
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        [HttpGet]
        [Route("lista")]
        public IHttpActionResult GetLista()
        {
            try
            {
                Respuesta<List<ECliente>> Lista = NCliente.GetInstance().ObtenerClientes();
                return Ok(Lista);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet]
        [Route("lisdesen")]
        public IHttpActionResult GetListaDesen()
        {
            try
            {
                Respuesta<List<ECliente>> Lista = NCliente.GetInstance().ObtenerClientesEn();
                return Ok(Lista);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult PostCliente(ClienteDTO clienteDTO)
        {
            try
            {
                // Llamada al método ValidarClienteDTO
                if (!ValidarClienteDTO(clienteDTO, out string mensajeError))
                {
                    return BadRequest(mensajeError); // Retorna el mensaje de error específico
                }
                if (!ValidarRuc(clienteDTO.Ruc))
                {
                    return BadRequest("El RUC ya existe.");
                }

                if (RegistrarCliente(clienteDTO))
                {
                    Respuesta<List<ECliente>> Lista = NCliente.GetInstance().ObtenerClientesEn();
                    var listafi = Lista.Data;
                    var item = listafi.FirstOrDefault(x => x.Ruc == clienteDTO.Ruc);
                    if (item == null)
                    {
                        return BadRequest("Ocurrio un error.");
                    }
                    return Ok(item);
                }
                else
                {
                    return BadRequest("Ocurrio un error.");
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        private bool ValidarRuc(string ruc)
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

        private bool ValidarClienteDTO(ClienteDTO clienteDTO, out string mensajeError)
        {
            if (clienteDTO == null)
            {
                mensajeError = "Debe ingresar datos.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(clienteDTO.Ruc))
            {
                mensajeError = "El campo RUC es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(clienteDTO.RazonSocial))
            {
                mensajeError = "El campo Razón Social es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(clienteDTO.Direccion))
            {
                mensajeError = "El campo Dirección es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(clienteDTO.Telefono))
            {
                mensajeError = "El campo Teléfono es obligatorio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(clienteDTO.Correo))
            {
                mensajeError = "El campo Correo es obligatorio.";
                return false;
            }

            mensajeError = string.Empty; // Si no hay errores
            return true;
        }
        private bool RegistrarCliente(ClienteDTO request)
        {
            ECliente obj = new ECliente
            {
                Ruc = EncryptacionH.Encrypt(request.Ruc),
                RazonSocial = EncryptacionH.Encrypt(request.RazonSocial),
                Direccion = EncryptacionH.Encrypt(request.Direccion),
                Telefono = EncryptacionH.Encrypt(request.Telefono),
                Correo = EncryptacionH.Encrypt(request.Correo)
            };
            Respuesta<bool> respuesta = NCliente.GetInstance().RegistrarCliente(obj);
            var esta = respuesta.Estado;
            return esta;
        }
    }
}