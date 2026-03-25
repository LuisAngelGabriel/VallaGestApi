using System.ComponentModel.DataAnnotations;

namespace VallaGestApi.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public virtual ICollection<Valla>? Vallas { get; set; }
    }
}
