namespace VallaGestApi.DTO
{
    namespace VallaGestApi.DTOs
    {
        public class VallaOcupadaDto
        {
            public int VallaId { get; set; }
            public string NombreValla { get; set; }
            public string Cliente { get; set; }
            public string FechaAlquiler { get; set; }
            public string FechaVencimiento { get; set; }
            public decimal Precio { get; set; }
            public int MesesAlquilados { get; set; }
        }
    }
}
