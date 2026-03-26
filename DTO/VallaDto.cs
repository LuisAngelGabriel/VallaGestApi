namespace VallaGestApi.DTO
{
    public class VallaDto
    {
        public int VallaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioMensual { get; set; }
        public string? ImagenUrl { get; set; }
        public bool EstaOcupada { get; set; }
        public int CategoriaId { get; set; }
        public string? CategoriaNombre { get; set; }
    }
}
