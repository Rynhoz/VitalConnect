using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_VitalConnectApi.Pages
{
    public class RecetasModel : PageModel
    {
        public List<RecetaDto> Recetas { get; set; }

        public void OnGet()
        {
            Recetas = new List<RecetaDto>
            {
                new RecetaDto
                {
                    Medicamento = "Paracetamol",
                    Dosis = "500 mg",
                    Frecuencia = "Cada 8 horas",
                    Dias = 5
                },
                new RecetaDto
                {
                    Medicamento = "Ibuprofeno",
                    Dosis = "400 mg",
                    Frecuencia = "Cada 12 horas",
                    Dias = 3
                }
            };
        }
    }

    public class RecetaDto
    {
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public int Dias { get; set; }
    }
}