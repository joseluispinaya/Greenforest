using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NProveedor
    {
        #region "PATRON SINGLETON"
        private static NProveedor instancia = null;
        private NProveedor() { }
        public static NProveedor GetInstance()
        {
            if (instancia == null)
            {
                instancia = new NProveedor();
            }
            return instancia;
        }
        #endregion

        public Respuesta<bool> RegistrarProveedor(EProveedor producto)
        {
            return DProveedor.GetInstance().RegistrarProveedor(producto);
        }

        public Respuesta<bool> EditarProveedor(EProveedor producto)
        {
            return DProveedor.GetInstance().EditarProveedor(producto);
        }

        public List<EProveedor> ObtenerProveedor()
        {
            return DProveedor.GetInstance().ObtenerProveedor();
        }
    }
}
