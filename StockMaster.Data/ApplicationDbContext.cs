using Microsoft.EntityFrameworkCore;
using StockMaster.Entidades;

namespace StockMaster.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                
                // Le decimos que 'nombre' es la llave primaria (como en tu foto)
                entity.HasKey(e => e.Nombre);

                // Mapeo exacto a tus columnas de Supabase
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Precio).HasColumnName("precio");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.Categoria).HasColumnName("categoria");
            });
        }
    }
}
