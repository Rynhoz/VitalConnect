namespace VitalConnect_API.Models
{
    public class DetalleReceta
    {
        public int IdDetalleReceta { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public int IdReceta { get; set; }
        public Receta? Receta { get; set; }
        public int IdMedicamento { get; set; }
        public Medicamento? Medicamento { get; set; }
    }
}
