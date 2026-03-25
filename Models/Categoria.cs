using System.ComponentModel.DataAnnotations;

namespace VallaGestApi.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        // Relación inversa para navegación
        public virtual ICollection<Valla>? Vallas { get; set; }
    }
}
