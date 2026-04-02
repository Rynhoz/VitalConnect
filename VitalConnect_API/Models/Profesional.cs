namespace VitalConnect_API.Models
{
    public class Profesional : Usuario
    {
        public int IdProfesional { get; set; }
        public string MatriculaProfesional { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
    }
}
