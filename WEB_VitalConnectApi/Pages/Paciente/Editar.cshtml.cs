using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_VitalConnectApi.Pages
{
    public class EditarModel : PageModel
    {
        [BindProperty] public string NombreCompleto { get; set; }
        [BindProperty] public string CI { get; set; }
        [BindProperty] public string Telefono { get; set; }
        [BindProperty] public string Genero { get; set; }
        [BindProperty] public string Direccion { get; set; }
        [BindProperty] public DateTime FechaNacimiento { get; set; }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Paciente/Paciente");
        }
    }
}