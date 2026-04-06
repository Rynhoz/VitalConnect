using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages.Profesional.Medicamento
{
    public class AgregarModel : PageModel
    {
        private readonly ApiService _api;

        [BindProperty]
        public VitalConnect_API.Models.Medicamento Medicamento { get; set; } = new()
        {
            Estado = true
        };

        public AgregarModel(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Medicamento.Nombre))
            {
                return Page();
            }

            var ok = await _api.PostAsync("api/Medicamento", Medicamento);

            if (ok)
            {
                return RedirectToPage("Medicamento");
            }

            ModelState.AddModelError(string.Empty, "Error al conectar con la API para guardar el medicamento.");
            return Page();
        }

        
    }
}