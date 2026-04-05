using System.Text.Json.Serialization;

namespace WEB_VitalConnectApi.Models
{
    public class FichaAtencion
    {
        public int FichaAtencionId { get; set; }
        public DateTime FechaAtencion { get; set; }

        public string Diagnostico { get; set; } = string.Empty;
        public string Indicaciones { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;

        public int IdCita { get; set; }
        
    }
}
