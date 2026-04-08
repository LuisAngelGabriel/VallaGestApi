namespace VallaGestApi.DTO
{
    public class CheckoutDto
    {
        public int Metodo { get; set; } 
        public string? ComprobanteUrl { get; set; } 
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
        public int Meses { get; set; }
    }
}