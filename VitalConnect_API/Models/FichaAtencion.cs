using System.Text.Json.Serialization;

namespace VitalConnect_API.Models
{
    public class FichaAtencion
    {
        public int IdFicha { get; set; }
        public DateTime FechaAtencion { get; set; }

        public string Diagnostico { get; set; } = string.Empty;
        public string Indicaciones { get; set; } = string.Empty;

        public int IdCita { get; set; }
        [JsonIgnore]
        public Cita Cita { get; set; } = null!;

        [JsonIgnore]
        public List<Receta> Recetas { get; set; } = new ();

    }
}
