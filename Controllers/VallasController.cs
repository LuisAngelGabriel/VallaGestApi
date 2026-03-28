using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VallaGestApi.DAL;
using VallaGestApi.Models;

namespace VallaGestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VallasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VallasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetVallas()
        {
            return await _context.Vallas.Include(v => v.Categoria)
                .Select(v => new {
                    v.VallaId,
                    v.Nombre,
                    v.Descripcion,
                    v.Ubicacion,
                    v.PrecioMensual,
                    v.ImagenUrl,
                    v.EstaOcupada,
                    v.CategoriaId,
                    NombreCategoria = v.Categoria != null ? v.Categoria.Nombre : "Sin Categoría"
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Valla>> PostValla(Valla valla, [FromHeader] string Rol)
        {
            if (Rol != "Admin") return Forbid();
            _context.Vallas.Add(valla);
            await _context.SaveChangesAsync();
            return Ok(valla);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutValla(int id, Valla valla, [FromHeader] string Rol)
        {
            if (Rol != "Admin") return Forbid();
            if (id != valla.VallaId) return BadRequest();
            _context.Entry(valla).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValla(int id, [FromHeader] string Rol)
        {
            if (Rol != "Admin") return Forbid();
            var valla = await _context.Vallas.FindAsync(id);
            if (valla == null) return NotFound();
            _context.Vallas.Remove(valla);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}