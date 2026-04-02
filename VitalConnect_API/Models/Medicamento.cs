namespace VitalConnect_API.Models
{
    public class Medicamento
    {
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;

        public List<DetalleReceta> DetallesReceta { get; set; } = new ();
    }
}
