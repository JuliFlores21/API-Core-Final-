using API_Core_Final.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Core_Final.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Recomendacion> Recomendaciones { get; set; }
        public DbSet<DetalleRecomendacion> DetallesRecomendacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>().ToTable("Estudiante"); // Especifica el nombre exacto de la tabla
            modelBuilder.Entity<Estudiante>()
                .Property(e => e.NivelCondicionFisica)
                .HasConversion<string>();  // Guardamos el enum como string en la BD
            modelBuilder.Entity<Actividad>().ToTable("ActividadDeportiva");
            modelBuilder.Entity<Recomendacion>().ToTable("Recomendacion");
            modelBuilder.Entity<DetalleRecomendacion>().ToTable("DetalleRecomendacion");
        }
    }
}
