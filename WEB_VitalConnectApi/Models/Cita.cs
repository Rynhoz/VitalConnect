using System.Text.Json.Serialization;
using VitalConnect_API.Models;

namespace WEB_VitalConnectApi.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = string.Empty;
        public string EstadoCita { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
        public int IdPaciente { get; set; }
        public int IdProfesional { get; set; }
        public int? IdAsistente { get; set; }
        
    }
}
