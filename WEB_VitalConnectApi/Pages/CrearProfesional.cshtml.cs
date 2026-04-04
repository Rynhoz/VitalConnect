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
        public VitalConnect_API.Models.Profesional Profesional { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var guardado = await _apiService.PostAsync("api/Profesional", Profesional);

            if (guardado) return RedirectToPage("/Lista");

            return Page();
        }
    }
}