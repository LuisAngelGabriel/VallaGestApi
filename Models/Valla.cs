using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VallaGestApi.Models
{
    public class Valla
    {
        [Key]
        public int VallaId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string Ubicacion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioMensual { get; set; }

        public string? ImagenUrl { get; set; }

        public bool EstaOcupada { get; set; } = false;

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }
    }
}
