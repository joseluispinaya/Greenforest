using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class DTipos
    {
        #region "PATRON SINGLETON"
        public static DTipos _instancia = null;

        private DTipos()
        {

        }

        public static DTipos GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DTipos();
            }
            return _instancia;
        }
        #endregion

        public List<ERol> ObtenerRol()
        {
            List<ERol> rptListaRol = new List<ERol>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerRoles", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptListaRol.Add(new ERol()
                                {
                                    Idrol = Convert.ToInt32(dr["IdRol"]),
                                    NomRol = dr["Descripcion"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("Error al obtener los roles", ex);
            }

            return rptListaRol;
        }
    }
}
