namespace VitalConnect_API.Models
{
    public class FichaAtencion
    {
        public int IdFicha { get; set; }
        public DateTime FechaAtencion { get; set; }

        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamiento { get; set; } = string.Empty;

        public int IdCita { get; set; }
        public Cita? Cita { get; set; }
        public List<DetalleReceta> DetallesReceta { get; set; } = new ();

    }
}
