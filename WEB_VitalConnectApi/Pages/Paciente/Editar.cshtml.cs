using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages.Paciente
{
    public class EditarModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditarModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public PacienteEditModel Paciente { get; set; } = new();

        public string Mensaje { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var data = await _apiService.GetAsync<PacienteEditModel>($"api/Paciente/{id}");
            if (data == null) return RedirectToPage("/Error");

            Paciente = data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var response = await _apiService.PutAsync($"api/Paciente/{Paciente.IdUsuario}", Paciente);

            if (!response.IsSuccessStatusCode)
            {
                Mensaje = "No se pudo actualizar la información.";
                return Page();
            }

            return RedirectToPage("/Paciente/Paciente", new { id = Paciente.IdUsuario });
        }

        public class PacienteEditModel
        {
            public int IdUsuario { get; set; }

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

            public bool Estado { get; set; } = true;
        }
    }
}