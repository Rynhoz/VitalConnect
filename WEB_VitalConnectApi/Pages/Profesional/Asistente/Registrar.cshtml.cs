using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class RegistrarModel : PageModel
    {
        private readonly ApiService _apiService;

        public RegistrarModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public AsistenteFormModel Formulario { get; set; } = new()
        {
            Rol = "Asistente",
            Estado = true
        };

        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var asistente = await _apiService.GetAsync<AsistenteDto>($"api/Asistente/{id.Value}");

                if (asistente != null)
                {
                    Formulario = new AsistenteFormModel
                    {
                        ID = asistente.ID,
                        NombreCompleto = asistente.NombreCompleto,
                        CI = asistente.CI,
                        Telefono = asistente.Telefono,
                        Rol = string.IsNullOrWhiteSpace(asistente.Rol) ? "Asistente" : asistente.Rol,
                        Estado = asistente.Estado,
                        Turno = asistente.Turno
                    };
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Formulario.NombreCompleto) ||
                string.IsNullOrWhiteSpace(Formulario.CI) ||
                string.IsNullOrWhiteSpace(Formulario.Telefono) ||
                string.IsNullOrWhiteSpace(Formulario.Turno))
            {
                Mensaje = "Completa todos los campos obligatorios.";
                return Page();
            }

            Formulario.Rol = "Asistente";

            bool ok;

            if (Formulario.ID > 0)
            {
                ok = await _apiService.PutAsync($"api/Asistente/{Formulario.ID}", new
                {
                    NombreCompleto = Formulario.NombreCompleto,
                    Telefono = Formulario.Telefono,
                    CI = Formulario.CI,
                    Rol = Formulario.Rol,
                    Estado = Formulario.Estado,
                    Turno = Formulario.Turno
                });
            }
            else
            {
                ok = await _apiService.PostAsync("api/Asistente", new
                {
                    NombreCompleto = Formulario.NombreCompleto,
                    Telefono = Formulario.Telefono,
                    CI = Formulario.CI,
                    Turno = Formulario.Turno
                });
            }

            if (!ok)
            {
                Mensaje = Formulario.ID > 0
                    ? "No se pudo actualizar el asistente."
                    : "No se pudo registrar el asistente.";
                return Page();
            }

            return RedirectToPage("/Profesional/Asistente/Asistente");
        }

        public class AsistenteFormModel
        {
            public int ID { get; set; }
            public string NombreCompleto { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public string CI { get; set; } = string.Empty;
            public string Rol { get; set; } = "Asistente";
            public bool Estado { get; set; } = true;
            public string Turno { get; set; } = string.Empty;
        }

        public class AsistenteDto
        {
            public int ID { get; set; }
            public string NombreCompleto { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public string CI { get; set; } = string.Empty;
            public string Rol { get; set; } = string.Empty;
            public bool Estado { get; set; }
            public string Turno { get; set; } = string.Empty;
        }
    }
}