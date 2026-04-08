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

        [HttpPost("Checkout/{usuarioId}/{meses}")]
        public async Task<IActionResult> Post(int usuarioId, int meses, [FromBody] CheckoutDto dto)
        {
            var items = await _context.CarritoItems
                .Include(c => c.Valla)
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            if (items == null || !items.Any())
                return BadRequest("El carrito está vacío.");

            var orden = new Orden
            {
                UsuarioId = usuarioId,
                Total = items.Sum(i => (i.Valla?.PrecioMensual ?? 0)) * meses,
                MetodoPago = (MetodoPago)dto.Metodo,
                Estado = dto.Metodo == 1 ? EstadoOrden.Pendiente : EstadoOrden.Completado,
                FechaOrden = DateTime.Now,
                ComprobanteUrl = dto.ComprobanteUrl,
                Detalles = new List<OrdenDetalle>()
            };

            foreach (var item in items)
            {
                if (item.Valla != null)
                {
                    orden.Detalles.Add(new OrdenDetalle
                    {
                        VallaId = item.VallaId,
                        PrecioAplicado = item.Valla.PrecioMensual,
                        Meses = meses
                    });

                    item.Valla.EstaOcupada = true;
                    _context.Entry(item.Valla).State = EntityState.Modified;
                }
            }

            try
            {
                _context.Ordenes.Add(orden);
                _context.CarritoItems.RemoveRange(items);

                await _context.SaveChangesAsync();
                return Ok(new { orden.OrdenId, Total = orden.Total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("Historial/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetHistorial(int usuarioId)
        {
            return await _context.Ordenes
                .Include(o => o.Detalles)
                .ThenInclude(d => d.Valla)
                .Where(o => o.UsuarioId == usuarioId)
                .OrderByDescending(o => o.FechaOrden)
                .Select(o => new {
                    o.OrdenId,
                    o.FechaOrden,
                    o.Total,
                    Metodo = o.MetodoPago.ToString(),
                    Estado = o.Estado.ToString(),
                    o.ComprobanteUrl,
                    Detalles = o.Detalles.Select(d => new {
                        NombreValla = d.Valla != null ? d.Valla.Nombre : "Valla no encontrada",
                        d.PrecioAplicado,
                        d.VallaId,
                        d.Meses
                    })
                }).ToListAsync();
        }

        [HttpDelete("Cancelar/{ordenId}")]
        public async Task<IActionResult> CancelarOrden(int ordenId)
        {
            var orden = await _context.Ordenes
                .Include(o => o.Detalles)
                .FirstOrDefaultAsync(o => o.OrdenId == ordenId);

            if (orden == null) return NotFound();

            foreach (var detalle in orden.Detalles)
            {
                var valla = await _context.Vallas.FindAsync(detalle.VallaId);
                if (valla != null)
                {
                    valla.EstaOcupada = false;
                    _context.Entry(valla).State = EntityState.Modified;
                }
            }

            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}