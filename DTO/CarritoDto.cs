namespace VallaGestApi.DTO
{
    public class CarritoItemDto
    {
        public int CarritoItemId { get; set; }
        public int VallaId { get; set; }
        public string NombreValla { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
