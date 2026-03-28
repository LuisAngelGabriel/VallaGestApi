using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VallaGestApi.DAL;
using VallaGestApi.Models;

namespace VallaGestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarritoController(AppDbContext context) => _context = context;

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetCarrito(int usuarioId)
        {
            return await _context.CarritoItems.Include(c => c.Valla)
                .Where(c => c.UsuarioId == usuarioId)
                .Select(c => new {
                    c.CarritoItemId,
                    c.VallaId,
                    NombreValla = c.Valla!.Nombre,
                    Precio = c.Valla.PrecioMensual,
                    ImagenUrl = c.Valla.ImagenUrl
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarritoItem item)
        {
            _context.CarritoItems.Add(item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CarritoItems.FindAsync(id);
            if (item == null) return NotFound();
            _context.CarritoItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
