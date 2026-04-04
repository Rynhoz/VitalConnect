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

        public List<VitalConnect_API.Models.Profesional> Profesionales { get; set; } = new();
        public List<VitalConnect_API.Models.Asistente> Asistentes { get; set; } = new();

        public async Task OnGetAsync()
        {
            Profesionales = await _apiService.GetListAsync<VitalConnect_API.Models.Profesional>("api/Profesional") ?? new();
            Asistentes = await _apiService.GetListAsync<VitalConnect_API.Models.Asistente>("api/Asistente") ?? new();
        }

        public async Task<IActionResult> OnPostInactivarProfesional(string id)
        {
            await _apiService.PatchAsync($"api/Profesional/Inactivar/{id}");
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostInactivarAsistente(string id)
        {
            await _apiService.PatchAsync($"api/Asistente/Inactivar/{id}");
            return RedirectToPage();
        }
    }
}