using System.ComponentModel.DataAnnotations;

namespace VallaGestApi.Models
{
    public class CarritoItem
    {
        [Key]
        public int CarritoItemId { get; set; }
        public int UsuarioId { get; set; }
        public int VallaId { get; set; }
        public virtual Valla? Valla { get; set; }
    }
}
