using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class AsistenteModel : PageModel
    {
        private readonly ApiService _apiService;

        public AsistenteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<AsistenteDto> Asistentes { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? FiltroTurno { get; set; }

        [TempData]
        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(FiltroTurno))
            {
                var porTurno = await _apiService.GetListAsync<AsistenteDto>($"api/Asistente/turno/{FiltroTurno}");
                Asistentes = porTurno ?? new List<AsistenteDto>();

                if (!Asistentes.Any())
                {
                    Mensaje = "No se encontraron asistentes para ese turno.";
                }

                return;
            }

            var lista = await _apiService.GetListAsync<AsistenteDto>("api/Asistente");
            Asistentes = lista ?? new List<AsistenteDto>();

            if (!Asistentes.Any())
            {
                Mensaje = "No hay asistentes registrados.";
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var ok = await _apiService.DeleteAsync($"api/Asistente/{id}");
            Mensaje = ok ? "Asistente eliminado correctamente." : "No se pudo eliminar el asistente.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync(int id)
        {
            var ok = await _apiService.PatchAsync($"api/Asistente/{id}/cambiar-estado");
            Mensaje = ok ? "Estado del asistente cambiado correctamente." : "No se pudo cambiar el estado.";
            return RedirectToPage();
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