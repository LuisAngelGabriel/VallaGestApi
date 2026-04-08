using System.ComponentModel.DataAnnotations;

namespace VallaGestApi.Models
{
    public class OrdenDetalle
    {
        [Key]
        public int OrdenDetalleId { get; set; }
        public int OrdenId { get; set; }
        public int VallaId { get; set; }
        public decimal PrecioAplicado { get; set; }
        public int Meses { get; set; }
        public virtual Valla? Valla { get; set; }
    }
}
