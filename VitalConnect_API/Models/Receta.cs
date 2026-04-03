using System.Text.Json.Serialization;

namespace VitalConnect_API.Models
{
    public class Receta
    {
        public int IdReceta { get; set; }
        public string? Observaciones { get; set; } = string.Empty;

        public int IdFicha { get; set; }
        [JsonIgnore]
        public FichaAtencion FichaAtencion { get; set; } = null!;

        public List<DetalleReceta> DetallesReceta { get; set; } = new ();


    }
}
