namespace CapaEntidad
{
    public class EDetalleVenta
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public float PrecioUnidad { get; set; }
        public float ImporteTotal { get; set; }
    }
}
