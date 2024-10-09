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
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        [HttpGet]
        [Route("lista")]
        public IHttpActionResult GetListausuarios()
        {
            try
            {
                List<EUsuario> Lista = NUsuario.GetInstance().ObtenerUsuarios();
                return Ok(Lista);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}