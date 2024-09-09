using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NUsuario
    {
        #region "PATRON SINGLETON"
        private static NUsuario daoEmpleado = null;
        private NUsuario() { }
        public static NUsuario GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NUsuario();
            }
            return daoEmpleado;
        }
        #endregion

        public bool RegistrarUsuario(EUsuario oUsuario)
        {
            return DUsuario.GetInstance().RegistrarUsuario(oUsuario);
        }

        public bool ActualizarUsuario(EUsuario oUsuario)
        {
            return DUsuario.GetInstance().ActualizarUsuario(oUsuario);
        }

        public List<EUsuario> ObtenerUsuarios()
        {
            return DUsuario.GetInstance().ObtenerUsuariosZ();
        }

        public bool ActualizarToken(int IdUsu, string token)
        {
            return DUsuario.GetInstance().ActualizarToken(IdUsu, token);
        }

        public string ObtenerToken(int IdUsu)
        {
            return DUsuario.GetInstance().ObtenerToken(IdUsu);
        }

        public EUsuario LoginUsuarioWeb(string Usuario, string Clave)
        {
            return DUsuario.GetInstance().LoginUsuarioWeb(Usuario, Clave);
        }
    }
}
