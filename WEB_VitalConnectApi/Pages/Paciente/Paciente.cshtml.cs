using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_VitalConnectApi.Pages
{
    public class PacienteModel : PageModel
    {
        public string NombreCompleto { get; set; }
        public string CI { get; set; }
        public string Telefono { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public string FechaNacimiento { get; set; }

        public void OnGet()
        {
            NombreCompleto = TempData["NombreCompleto"]?.ToString();
            CI = TempData["CI"]?.ToString();
            Telefono = TempData["Telefono"]?.ToString();
            Genero = TempData["Genero"]?.ToString();
            Direccion = TempData["Direccion"]?.ToString();
            FechaNacimiento = TempData["FechaNacimiento"]?.ToString();
        }
    }
}