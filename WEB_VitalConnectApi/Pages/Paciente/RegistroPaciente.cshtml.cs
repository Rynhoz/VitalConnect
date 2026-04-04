//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace WEB_VitalConnectApi.Pages
//{
//    public class RegistroPacienteModel : PageModel
//    {
//        [BindProperty]
//        public string NombreCompleto { get; set; }

//        [BindProperty]
//        public string CI { get; set; }

//        [BindProperty]
//        public string Telefono { get; set; }

//        [BindProperty]
//        public string Genero { get; set; }

//        [BindProperty]
//        public string Direccion { get; set; }

//        [BindProperty]
//        public DateTime FechaNacimiento { get; set; }

//        public IActionResult OnPost()
//        {
//            TempData["NombreCompleto"] = NombreCompleto;
//            TempData["CI"] = CI;
//            TempData["Telefono"] = Telefono;
//            TempData["Genero"] = Genero;
//            TempData["Direccion"] = Direccion;
//            TempData["FechaNacimiento"] = FechaNacimiento.ToShortDateString();

//            return RedirectToPage("/Paciente/Paciente");
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class RegistroPacienteModel : PageModel
    {
        private readonly ApiService _apiService;

        public RegistroPacienteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public string NombreCompleto { get; set; }

        [BindProperty]
        public string CI { get; set; }

        [BindProperty]
        public string Telefono { get; set; }

        [BindProperty]
        public string Genero { get; set; }

        [BindProperty]
        public string Direccion { get; set; }

        [BindProperty]
        public DateTime FechaNacimiento { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var paciente = new
            {
                NombreCompleto = NombreCompleto,
                CI = CI,
                Telefono = Telefono,
                Genero = Genero,
                Direccion = Direccion,
                FechaNacimiento = FechaNacimiento
            };

            var guardado = await _apiService.PostAsync("api/Paciente", paciente);

            if (guardado)
            {
                return RedirectToPage("/Paciente/Paciente");
            }

            Mensaje = "No se pudo registrar el paciente";
            return Page();
        }
    }
}