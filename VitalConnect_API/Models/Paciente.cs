using System.Text.Json.Serialization;

namespace VitalConnect_API.Models
{
    public class Paciente : Usuario
    { 
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Cita> Citas { get; set; } = new ();

        
    }
}
