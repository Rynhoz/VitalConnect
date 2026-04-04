using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class CrearAsistenteModel : PageModel
    {
        private readonly ApiService _apiService;

        public CrearAsistenteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public VitalConnect_API.Models.Asistente Asistente { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var guardado = await _apiService.PostAsync("api/Asistente", Asistente);

            if (guardado) return RedirectToPage("/Lista");

            return Page();
        }
    }
}