using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Models;

namespace VitalConnect_API.Data
{
    public class VT_DbContext : DbContext
    {
        public VT_DbContext(DbContextOptions<VT_DbContext> options) : base(options) { }


        public DbSet<Usuario> Usuarios { get; set; }

        //Para Profesionales
        public DbSet<Profesional> Profesionales { get; set; }

        //Para Asistentes
        public DbSet<Asistente> Asistentes { get; set; }

        //Para pacientes
        public DbSet<Paciente> Pacientes { get; set; }

        //Para Medicamentos
        public DbSet<Medicamento> Medicamentos { get; set; }

        //Para Fichas de atencion
        public DbSet<FichaAtencion> FichaAtencion { get; set; }

        //Para Citas
        public DbSet<Cita> Citas { get; set; }

        //Para Recetas
        public DbSet<Receta> Recetas { get; set; }

        public DbSet<DetalleReceta> DetalleReceta { get; set; }

        //Para la existencia de las 3 tablas de Paciente, Profesional y Asistente

        // OnModelCreating es el método donde se le dan instrucciones extras a EF Core
        // sobre cosas que no puede inferir automáticamente desde las clases.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Por defecto, EF Core mete todas las clases hijas (Profesional, Paciente, Asistente)
            // en UNA SOLA tabla llamada "Usuarios" con una columna "Discriminator" para distinguirlas.
            // Esto se llama TPH (Table Per Hierarchy).

            // Con .ToTable() le decimos a EF que use TPT (Table Per Type),
            // es decir, que genere una tabla separada para cada clase hija.
            // Cada tabla hija solo tendrá sus propios campos, y su ID actuará
            // como PK y FK hacia la tabla Usuarios al mismo tiempo.
            modelBuilder.Entity<Profesional>().ToTable("Profesionales");
            modelBuilder.Entity<Paciente>().ToTable("Pacientes");
            modelBuilder.Entity<Asistente>().ToTable("Asistentes");


        }
    }
}
