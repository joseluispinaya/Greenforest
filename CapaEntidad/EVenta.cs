using System;
using System.Collections.Generic;

namespace CapaEntidad
{
    public class EVenta
    {
        public int IdVenta { get; set; }
        public string Codigo { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadTotal { get; set; }
        public float TotalCosto { get; set; }
        public string FechaRegistro { get; set; }
        public DateTime VFechaRegistro { get; set; }
        public ECliente Cliente { get; set; }
        public List<EDetalleVenta> ListaDetalleVenta { get; set; }
    }
}
