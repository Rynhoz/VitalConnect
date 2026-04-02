namespace VitalConnect_API.Models
{
    public class Profesional : Usuario
    {
       
        public string MatriculaProfesional { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;

        public List<Cita> Citas { get; set; } = new ();
    }
}
