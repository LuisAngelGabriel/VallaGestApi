using VallaGestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace VallaGestApi.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Valla> Vallas { get; set; }
    }
}
