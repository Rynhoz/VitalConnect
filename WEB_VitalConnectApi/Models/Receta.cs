namespace WEB_VitalConnectApi.Models
{
    public class Receta
    {
        public int RecetaId { get; set; }
        public string? Observaciones { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;

        public int IdFicha { get; set; }
    }
}
