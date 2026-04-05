namespace WEB_VitalConnectApi.Models
{
    public class Medicamento
    {
        public int MedicamentoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}
