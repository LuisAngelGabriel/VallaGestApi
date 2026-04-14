namespace VallaGestApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using VallaGestApi.DAL;
    using VallaGestApi.DTO.VallaGestApi.DTOs;

    [Route("api/[controller]")]
    [ApiController]
    public class VallasOcupadasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VallasOcupadasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VallaOcupadaDto>>> GetOcupadas()
        {
            var ocupadas = await _context.Vallas
                .Where(v => v.EstaOcupada)
                .Select(v => new VallaOcupadaDto
                {
                    VallaId = v.VallaId,
                    NombreValla = v.Nombre,
                    Precio = v.PrecioMensual,

                    Cliente = _context.OrdenDetalles
                        .Where(od => od.VallaId == v.VallaId && od.Orden.Estado == Models.EstadoOrden.Completado)
                        .OrderByDescending(od => od.OrdenId)
                        .Select(od => od.Orden.Usuario.Nombre)
                        .FirstOrDefault() ?? "Sin asignar",

                    FechaAlquiler = _context.OrdenDetalles
                        .Where(od => od.VallaId == v.VallaId)
                        .OrderByDescending(od => od.OrdenId)
                        .Select(od => od.Orden.FechaOrden.ToString("dd/MM/yyyy"))
                        .FirstOrDefault(),

                    MesesAlquilados = _context.OrdenDetalles
                        .Where(od => od.VallaId == v.VallaId)
                        .OrderByDescending(od => od.OrdenId)
                        .Select(od => od.Meses)
                        .FirstOrDefault(),

                    FechaVencimiento = _context.OrdenDetalles
                        .Where(od => od.VallaId == v.VallaId)
                        .OrderByDescending(od => od.OrdenId)
                        .Select(od => od.Orden.FechaOrden.AddMonths(od.Meses).ToString("dd/MM/yyyy"))
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(ocupadas);
        }
    }
}
