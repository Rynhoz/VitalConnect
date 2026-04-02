using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Models;

namespace VitalConnect_API.Data

{
    public class VitalConnectDbContext : DbContext
    {
        public VitalConnectDbContext(DbContextOptions<VitalConnectDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario>Usuarios { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Asistente> Asistentes { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<FichaAtencion> FichasAtencion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }

    }
}
