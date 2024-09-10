using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EProducto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public int ValorCodigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public float PrecioUnidadVenta { get; set; }
        public bool Activo { get; set; }

        public string ImageFulP => string.IsNullOrEmpty(Imagen)
            ? $"/ImagePro/prodsinim.jpg"
            : Imagen;

        public string TotalCadena => $"{PrecioUnidadVenta:F2} /Bs";
    }
}
