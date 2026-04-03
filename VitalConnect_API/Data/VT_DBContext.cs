using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Models;

namespace VitalConnect_API.Data
{
    public class VT_DbContext : DbContext
    {
        public VT_DbContext(DbContextOptions<VT_DbContext> options) : base(options) { }

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
    }
}
