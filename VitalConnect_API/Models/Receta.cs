using System.Text.Json.Serialization;

namespace VitalConnect_API.Models
{
    public class Receta
    {
        public int RecetaId { get; set; }
        public string? Observaciones { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;

        public int IdFicha { get; set; }
        [JsonIgnore]
        public FichaAtencion? FichaAtencion { get; set; }

        public List<DetalleReceta> DetallesReceta { get; set; } = new ();


    }
}
