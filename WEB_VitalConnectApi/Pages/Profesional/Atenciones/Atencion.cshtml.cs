using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;
using WEB_VitalConnectApi.Models;

namespace WEB_VitalConnectApi.Pages
{
    public class AtencionModel : PageModel
    {
        private readonly ApiService _apiService;

        public AtencionModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<AtencionDto> ListaAtenciones { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? FiltroIdCita { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FiltroFecha { get; set; }

        [TempData]
        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var lista = await _apiService.GetListAsync<AtencionDto>("api/Atencion");
            ListaAtenciones = lista ?? new List<AtencionDto>();

            if (FiltroIdCita.HasValue)
            {
                ListaAtenciones = ListaAtenciones
                    .Where(x => x.IdCita == FiltroIdCita.Value)
                    .ToList();
            }

            if (FiltroFecha.HasValue)
            {
                ListaAtenciones = ListaAtenciones
                    .Where(x => x.FechaAtencion.Date == FiltroFecha.Value.Date)
                    .ToList();
            }

            if (!ListaAtenciones.Any())
            {
                Mensaje = "No se encontraron atenciones con esos filtros.";
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var ok = await _apiService.DeleteAsync($"api/Atencion/{id}");
            Mensaje = ok ? "Atención eliminada correctamente." : "No se pudo eliminar la atención.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync(int id)
        {
            var ok = await _apiService.PatchAsync($"api/Atencion/{id}/cambiar-estado");
            Mensaje = ok ? "Estado de la atención cambiado correctamente." : "No se pudo cambiar el estado.";
            return RedirectToPage();
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
