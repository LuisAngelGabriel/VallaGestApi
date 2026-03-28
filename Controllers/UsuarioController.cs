using Microsoft.AspNetCore.Mvc;
using VallaGestApi.DAL;
using VallaGestApi.DTO;
using VallaGestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace VallaGestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context) => _context = context;

        [HttpPost("Registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistroDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("El correo ya existe");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = dto.Rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(new UsuarioDto { UsuarioId = usuario.UsuarioId, Nombre = usuario.Nombre, Email = usuario.Email, Rol = usuario.Rol });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                return Unauthorized();

            return Ok(new UsuarioDto { UsuarioId = usuario.UsuarioId, Nombre = usuario.Nombre, Email = usuario.Email, Rol = usuario.Rol });
        }
    }
}
