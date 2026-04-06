using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class CrearProfesionalModel : PageModel
    {
        private readonly ApiService _apiService;

        public CrearProfesionalModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public ProfesionalFormModel Formulario { get; set; } = new()
        {
            Estado = true
        };

        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var profesional = await _apiService.GetAsync<ProfesionalDto>($"api/Profesional/{id.Value}");

                if (profesional != null)
                {
                    Formulario = new ProfesionalFormModel
                    {
                        ID = profesional.ID,
                        NombreCompleto = profesional.NombreCompleto,
                        CI = profesional.CI,
                        MatriculaProfesional = profesional.MatriculaProfesional,
                        Especialidad = profesional.Especialidad,
                        Estado = profesional.Estado
                    };
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Formulario.NombreCompleto) ||
                string.IsNullOrWhiteSpace(Formulario.CI) ||
                string.IsNullOrWhiteSpace(Formulario.MatriculaProfesional) ||
                string.IsNullOrWhiteSpace(Formulario.Especialidad) ||
                string.IsNullOrWhiteSpace(Formulario.Telefono))
            {
                Mensaje = "Completa todos los campos obligatorios.";
                return Page();
            }

            var modeloApi = new VitalConnect_API.Models.Profesional
            {
                NombreCompleto = Formulario.NombreCompleto,
                CI = Formulario.CI,
                MatriculaProfesional = Formulario.MatriculaProfesional,
                Especialidad = Formulario.Especialidad,
                Telefono = Formulario.Telefono,  
                Rol = "Profesional",             
                Estado = Formulario.Estado
            };

            bool ok;

            if (Formulario.ID > 0)
            {
                ok = await _apiService.PutAsync($"api/Profesional/{Formulario.ID}", modeloApi);
            }
            else
            {
                ok = await _apiService.PostAsync("api/Profesional", modeloApi);
            }

            if (!ok)
            {
                Mensaje = "No se pudo registrar el médico. Revisa la consola de la API.";
                return Page();
            }

            return RedirectToPage("/Lista");
        }

        public class ProfesionalFormModel
        {
            public int ID { get; set; }
            public string NombreCompleto { get; set; } = string.Empty;
            public string CI { get; set; } = string.Empty;
            public string MatriculaProfesional { get; set; } = string.Empty;
            public string Especialidad { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty; 
            public bool Estado { get; set; } = true;
        }

        public class ProfesionalDto
        {
            public int ID { get; set; }
            public string NombreCompleto { get; set; } = string.Empty;
            public string CI { get; set; } = string.Empty;
            public string MatriculaProfesional { get; set; } = string.Empty;
            public string Especialidad { get; set; } = string.Empty;
            public bool Estado { get; set; }
        }
    }
}