using System.Text.Json.Serialization;

namespace VitalConnect_API.Models
{
    public class Cita
    {
        public int IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = string.Empty;

        public string EstadoCita { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
        public int IdPaciente { get; set; }
        [JsonIgnore]
        public Paciente? Paciente { get; set; }

        public int IdProfesional { get; set; }
        [JsonIgnore]
        public Profesional? Profesional { get; set; }

        public int? IdAsistente { get; set; }
        [JsonIgnore]
        public Asistente? Asistente { get; set; }

        public FichaAtencion? FichaAtencion { get; set; }
        

    }
}
