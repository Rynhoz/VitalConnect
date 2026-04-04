using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_VitalConnectApi.Pages
{
    public class ReservarCitaModel : PageModel
    {
        [BindProperty] public string Especialidad { get; set; }
        [BindProperty] public string Medico { get; set; }
        [BindProperty] public DateTime Fecha { get; set; }
        [BindProperty] public TimeSpan Hora { get; set; }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Paciente/Paciente");
        }
    }
}