using Microsoft.AspNetCore.Mvc;
using VallaGestApi.DAL;
using VallaGestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace VallaGestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VallasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VallasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valla>>> GetVallas()
        {
            return await _context.Vallas.Include(v => v.Categoria).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Valla>> PostValla(Valla valla)
        {
            _context.Vallas.Add(valla);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVallas), new { id = valla.VallaId }, valla);
        }
    }
}
