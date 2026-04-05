using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;
using WEB_VitalConnectApi.Models;

namespace WEB_VitalConnectApi.Pages
{
    public class AgregarModel : PageModel
    {
        private readonly ApiService _api;

        [BindProperty]
        public Medicamento Medicamento { get; set; } = new();

        public AgregarModel(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _api.PostAsync("Medicamento", Medicamento);

            return RedirectToPage("Medicamento");
        }
    }
}
