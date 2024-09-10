using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class NProducto
    {
        #region "PATRON SINGLETON"
        private static NProducto instancia = null;
        private NProducto() { }
        public static NProducto GetInstance()
        {
            if (instancia == null)
            {
                instancia = new NProducto();
            }
            return instancia;
        }
        #endregion

        public Respuesta<bool> RegistrarProductoNuevo(EProducto producto)
        {
            return DProducto.GetInstance().RegistrarProductoNuevo(producto);
        }

        public Respuesta<bool> ActualizarProductoNuevo(EProducto producto)
        {
            return DProducto.GetInstance().ActualizarProductoNuevo(producto);
        }

        public bool RegistrarProducto(EProducto producto)
        {
            return DProducto.GetInstance().RegistrarProducto(producto);
        }
        public bool ActualizarProducto(EProducto producto)
        {
            return DProducto.GetInstance().ActualizarProducto(producto);
        }
        public List<EProducto> ObtenerProductos()
        {
            return DProducto.GetInstance().ObtenerProductos();
        }
        public List<EProducto> ObtenerProductosFil(string buscar)
        {
            return DProducto.GetInstance().ObtenerProductosFil(buscar);
        }
    }
}
