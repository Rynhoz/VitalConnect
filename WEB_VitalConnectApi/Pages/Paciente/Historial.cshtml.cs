using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_VitalConnectApi.Pages
{
    public class HistorialModel : PageModel
    {
        public List<ConsultaDto> Consultas { get; set; }

        public void OnGet()
        {
            Consultas = new List<ConsultaDto>
            {
                new ConsultaDto
                {
                    Fecha = DateTime.Now.AddMonths(-2),
                    Diagnostico = "Gripe común",
                    Observaciones = "Reposo, líquidos y control en 3 días"
                },
                new ConsultaDto
                {
                    Fecha = DateTime.Now.AddMonths(-1),
                    Diagnostico = "Dolor muscular",
                    Observaciones = "Ibuprofeno cada 8 horas"
                }
            };
        }
    }

    public class ConsultaDto
    {
        public DateTime Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Observaciones { get; set; }
    }
}