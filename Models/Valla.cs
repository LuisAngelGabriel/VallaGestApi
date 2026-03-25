using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VallaGestApi.Models
{
    public class Valla
    {
        [Key]
        public int VallaId { get; set; }

        [Required(ErrorMessage = "El nombre de la valla es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioMensual { get; set; }

        public string? ImagenUrl { get; set; }

        public bool EstaOcupada { get; set; } = false;

        // Relación con Categoría
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }
    }
}
