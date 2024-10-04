using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EReporteVentaProd
    {
        public string NombreProducto { get; set; }
        public string Codigo { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public float PrecioUnidadVenta { get; set; }
        public int CantidadTotal { get; set; }
        public float MontoTotal { get; set; }
        public string ImageFulRe => string.IsNullOrEmpty(Imagen)
            ? $"/ImagePro/prodsinim.jpg"
            : Imagen;

        public string TotalCadena => $"{MontoTotal:F2} /USD";
        //public string TotalCadena => $"{MontoTotal.ToString("F2", CultureInfo.InvariantCulture)} /s";
    }
}
