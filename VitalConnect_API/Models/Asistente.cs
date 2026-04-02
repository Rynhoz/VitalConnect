namespace VitalConnect_API.Models
{
    public class Asistente : Usuario
    {
        public string Turno { get; set; } = string.Empty;

        public List<Cita> Citas { get; set; } = new ();
    }
}
