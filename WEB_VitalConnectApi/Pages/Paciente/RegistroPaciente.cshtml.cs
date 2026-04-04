using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages.Paciente
{
    public class RegistroPacienteModel : PageModel
    {
        private readonly ApiService _apiService;

        public RegistroPacienteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public PacienteInputModel Paciente { get; set; } = new();

        public string Mensaje { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Paciente.Estado = true;

            var response = await _apiService.PostAsync("api/Paciente", Paciente);

            if (!response.IsSuccessStatusCode)
            {
                Mensaje = "No se pudo registrar el paciente.";
                return Page();
            }

            Mensaje = "Paciente registrado correctamente.";
            ModelState.Clear();
            Paciente = new PacienteInputModel();
            return Page();
        }

        public class PacienteInputModel
        {
            [Required]
            [Display(Name = "Nombre completo")]
            public string NombreCompleto { get; set; } = string.Empty;

            [Required]
            [Display(Name = "CI")]
            public string CI { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Teléfono")]
            public string Telefono { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Fecha de nacimiento")]
            public DateTime FechaNacimiento { get; set; }

            [Required]
            [Display(Name = "Género")]
            public string Genero { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Dirección")]
            public string Direccion { get; set; } = string.Empty;

            [Display(Name = "Alergias o antecedentes")]
            public string AlergiasAntecedentes { get; set; } = string.Empty;

            public bool Estado { get; set; }
        }
    }
}