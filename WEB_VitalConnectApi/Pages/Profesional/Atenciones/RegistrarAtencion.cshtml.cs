using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class RegistrarAtencionModel : PageModel
    {
        private readonly ApiService _apiService;

        public RegistrarAtencionModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public AtencionFormModel Formulario { get; set; } = new()
        {
            FechaAtencion = DateTime.Today,
            Estado = true
        };

        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var atencion = await _apiService.GetAsync<AtencionDto>($"api/Atencion/{id.Value}");

                if (atencion != null)
                {
                    Formulario = new AtencionFormModel
                    {
                        ID = atencion.ID,
                        FechaAtencion = atencion.FechaAtencion,
                        Diagnostico = atencion.Diagnostico,
                        Observaciones = atencion.Observaciones,
                        Tratamiento = atencion.Tratamiento,
                        IdCita = atencion.IdCita,
                        Estado = atencion.Estado
                    };
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Formulario.FechaAtencion == default ||
                string.IsNullOrWhiteSpace(Formulario.Diagnostico) ||
                string.IsNullOrWhiteSpace(Formulario.Observaciones) ||
                string.IsNullOrWhiteSpace(Formulario.Tratamiento) ||
                Formulario.IdCita <= 0)
            {
                Mensaje = "Completa todos los campos obligatorios.";
                return Page();
            }

            bool ok;

            if (Formulario.ID > 0)
            {
                ok = await _apiService.PutAsync($"api/Atencion/{Formulario.ID}", new
                {
                    FechaAtencion = Formulario.FechaAtencion,
                    Diagnostico = Formulario.Diagnostico,
                    Observaciones = Formulario.Observaciones,
                    Tratamiento = Formulario.Tratamiento,
                    IdCita = Formulario.IdCita,
                    Estado = Formulario.Estado
                });
            }
            else
            {
                ok = await _apiService.PostAsync("api/Atencion", new
                {
                    FechaAtencion = Formulario.FechaAtencion,
                    Diagnostico = Formulario.Diagnostico,
                    Observaciones = Formulario.Observaciones,
                    Tratamiento = Formulario.Tratamiento,
                    IdCita = Formulario.IdCita
                });
            }

            if (!ok)
            {
                Mensaje = Formulario.ID > 0
                    ? "No se pudo actualizar la atención."
                    : "No se pudo registrar la atención.";
                return Page();
            }

            return RedirectToPage("/Profesional/Atenciones/Atenciones");
        }

        public class AtencionFormModel
        {
            public int ID { get; set; }
            public DateTime FechaAtencion { get; set; }
            public string Diagnostico { get; set; } = string.Empty;
            public string Observaciones { get; set; } = string.Empty;
            public string Tratamiento { get; set; } = string.Empty;
            public int IdCita { get; set; }
            public bool Estado { get; set; }
        }

        public class AtencionDto
        {
            public int ID { get; set; }
            public DateTime FechaAtencion { get; set; }
            public string Diagnostico { get; set; } = string.Empty;
            public string Observaciones { get; set; } = string.Empty;
            public string Tratamiento { get; set; } = string.Empty;
            public int IdCita { get; set; }
            public bool Estado { get; set; }
        }
    }
}
