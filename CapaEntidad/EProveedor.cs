using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EProveedor
    {
        public int IdProveedor { get; set; }
        public float Precio { get; set; }
        public float Cantidad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string VFechaRegistro { get; set; }
    }
}
