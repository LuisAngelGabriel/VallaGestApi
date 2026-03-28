using System.ComponentModel.DataAnnotations;

namespace VallaGestApi.Models
{
    public enum MetodoPago { Tarjeta, Transferencia }
    public enum EstadoOrden { Pendiente, Completado, Rechazado }

    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaOrden { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;
        public string? ComprobanteUrl { get; set; }
        public virtual ICollection<OrdenDetalle> Detalles { get; set; } = new List<OrdenDetalle>();
    }
}
