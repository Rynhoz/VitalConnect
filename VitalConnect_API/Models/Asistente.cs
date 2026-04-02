namespace VitalConnect_API.Models
{
    public class Asistente : Usuario
    {
        public int IdAsistente { get; set; }
        public string Turno { get; set; } = string.Empty;
    }
}
