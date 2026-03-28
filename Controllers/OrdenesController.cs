using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VallaGestApi.DAL;
using VallaGestApi.DTO;
using VallaGestApi.Models;

namespace VallaGestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrdenesController(AppDbContext context) => _context = context;

        [HttpPost("Checkout/{usuarioId}")]
        public async Task<IActionResult> Post(int usuarioId, [FromBody] CheckoutDto dto)
        {
            var items = await _context.CarritoItems.Include(c => c.Valla).Where(c => c.UsuarioId == usuarioId).ToListAsync();
            if (!items.Any()) return BadRequest();

            var orden = new Orden
            {
                UsuarioId = usuarioId,
                Total = items.Sum(i => i.Valla!.PrecioMensual),
                MetodoPago = dto.Metodo,
                Estado = dto.Metodo == MetodoPago.Transferencia ? EstadoOrden.Pendiente : EstadoOrden.Completado,
                FechaOrden = DateTime.Now
            };

            if (dto.Metodo == MetodoPago.Transferencia && !string.IsNullOrEmpty(dto.ReciboBase64))
                orden.ComprobanteUrl = $"uploads/{Guid.NewGuid()}{dto.Extension}";

            foreach (var i in items)
                orden.Detalles.Add(new OrdenDetalle { VallaId = i.VallaId, PrecioAplicado = i.Valla!.PrecioMensual });

            _context.Ordenes.Add(orden);
            _context.CarritoItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            return Ok(new { orden.OrdenId });
        }

        [HttpGet("Historial/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetHistorial(int usuarioId)
        {
            return await _context.Ordenes.Include(o => o.Detalles).ThenInclude(d => d.Valla)
                .Where(o => o.UsuarioId == usuarioId).OrderByDescending(o => o.FechaOrden)
                .Select(o => new {
                    o.OrdenId,
                    o.FechaOrden,
                    o.Total,
                    Metodo = o.MetodoPago.ToString(),
                    Estado = o.Estado.ToString(),
                    Detalles = o.Detalles.Select(d => new { d.Valla!.Nombre, d.PrecioAplicado })
                }).ToListAsync();
        }
    }
}
