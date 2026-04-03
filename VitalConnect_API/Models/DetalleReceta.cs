using System.Text.Json.Serialization;

namespace VitalConnect_API.Models
{
    public class DetalleReceta
    {
        public int DetalleRecetaId { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public int IdReceta { get; set; }
        [JsonIgnore]
        public Receta Receta { get; set; } = null!;
        public int IdMedicamento { get; set; }
        [JsonIgnore]
        public Medicamento Medicamento { get; set; } = null!;
    }
}
