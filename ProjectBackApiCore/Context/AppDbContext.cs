using Microsoft.EntityFrameworkCore;
using ProjectBackApiCore.Entities;

namespace ProjectBack.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductoModel> registroProductos { get; set; }
        public DbSet<ProductoModel> obtenerProductos { get; set; }

    }
}
