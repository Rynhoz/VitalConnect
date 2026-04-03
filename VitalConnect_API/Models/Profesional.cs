using System.Text.Json.Serialization;

namespace VitalConnect_API.Models
{
    public class Profesional : Usuario
    {
       
        public string MatriculaProfesional { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Cita> Citas { get; set; } = new ();
    }
}
