using System.Text.Json.Serialization;

namespace WEB_VitalConnectApi.Models
{
    public class DetalleReceta
    {
        public int DetalleRecetaId { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public int IdReceta { get; set; }
        public int IdMedicamento { get; set; }
        
    }
}
