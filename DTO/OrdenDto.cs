    using VallaGestApi.Models;

namespace VallaGestApi.DTO
{
    public class CheckoutDto
    {
        public MetodoPago Metodo { get; set; }
        public string? ReciboBase64 { get; set; }
        public string? Extension { get; set; }
    }

    public class OrdenDto
    {
        public int OrdenId { get; set; }
        public DateTime FechaOrden { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? ComprobanteUrl { get; set; }
        public List<OrdenDetalleDto> Detalles { get; set; } = new();
    }

    public class OrdenDetalleDto
    {
        public int VallaId { get; set; }
        public string NombreValla { get; set; } = string.Empty;
        public decimal PrecioAplicado { get; set; }
    }
}
