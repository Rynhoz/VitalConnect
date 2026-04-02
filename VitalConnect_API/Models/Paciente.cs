namespace VitalConnect_API.Models
{
    public class Paciente : Usuario
    {
        public int IdPaciente { get; set; } 
        public DateOnly FechaNacimiento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }
}
