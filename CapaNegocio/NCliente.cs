using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NCliente
    {
        #region "PATRON SINGLETON"
        private static NCliente daoEmpleado = null;
        private NCliente() { }
        public static NCliente GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NCliente();
            }
            return daoEmpleado;
        }
        #endregion

        public Respuesta<bool> RegistrarCliente(ECliente cliente)
        {
            return DCliente.GetInstance().RegistrarCliente(cliente);
        }

        public Respuesta<List<ECliente>> ObtenerClientes()
        {
            return DCliente.GetInstance().ObtenerClientes();
        }

        public Respuesta<List<ECliente>> ObtenerClientesEn()
        {
            return DCliente.GetInstance().ObtenerClientesEn();
        }
    }
}
