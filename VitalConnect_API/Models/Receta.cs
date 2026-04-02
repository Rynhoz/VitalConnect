namespace VitalConnect_API.Models
{
    public class Receta
    {
        public int IdReceta { get; set; }
        public string Observaciones { get; set; } = string.Empty;

        public int IdFicha { get; set; }
        public FichaAtencion? FichaAtencion { get; set; }

        public List<DetalleReceta> DetallesReceta { get; set; } = new ();


    }
}
