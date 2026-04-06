using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class ListaModel : PageModel
    {
        private readonly ApiService _apiService;

        public ListaModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<ProfesionalDto> Profesionales { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? FiltroEspecialidad { get; set; }

        [TempData]
        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var lista = await _apiService.GetListAsync<ProfesionalDto>("api/Profesional");

            if (lista != null)
            {
                if (!string.IsNullOrWhiteSpace(FiltroEspecialidad))
                {
                    Profesionales = lista.Where(p => p.Especialidad == FiltroEspecialidad).ToList();

                    if (!Profesionales.Any()) Mensaje = "No se encontraron profesionales para esa especialidad.";
                }
                else
                {
                    Profesionales = lista;
                }
            }

            if (!Profesionales.Any() && string.IsNullOrEmpty(Mensaje))
            {
                Mensaje = "No hay profesionales registrados.";
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var ok = await _apiService.DeleteAsync($"api/Profesional/{id}");
            Mensaje = ok ? "Profesional eliminado correctamente." : "No se pudo eliminar el profesional.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync(int id)
        {
            var ok = await _apiService.PatchAsync($"api/Profesional/{id}/cambiar-estado");
            Mensaje = ok ? "Estado del profesional cambiado correctamente." : "No se pudo cambiar el estado.";
            return RedirectToPage();
        }

        public class ProfesionalDto
        {
            public int ID { get; set; } 
            public string NombreCompleto { get; set; } = string.Empty;
            public string Especialidad { get; set; } = string.Empty;
            public string MatriculaProfesional { get; set; } = string.Empty;
            public string CI { get; set; } = string.Empty;
            public bool Estado { get; set; } = true;
        }
    }
}