namespace VitalConnect_API.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string CI { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
        public bool Estado { get; set; }= true;

    }
}
