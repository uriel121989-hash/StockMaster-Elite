using Microsoft.EntityFrameworkCore;
using StockMaster.Entidades;
namespace StockMaster.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Producto> Productos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Producto>(entity => {
                entity.ToTable("Productos");
                entity.HasKey(e => e.Nombre);
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Precio).HasColumnName("precio");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.Categoria).HasColumnName("categoria");
            });
        }
    }
}
