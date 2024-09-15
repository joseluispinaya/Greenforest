using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class VentaRepoFecha
    {
        public int IdVenta { get; set; }
        public string Codigo { get; set; }
        public string FechaRegistro { get; set; }
        public DateTime VFechaRegistro { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public float TotalCosto { get; set; }
        public string TotalRepo => $"{TotalCosto:F2} /USD";
    }
}
