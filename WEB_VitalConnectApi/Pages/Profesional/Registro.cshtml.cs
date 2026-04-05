using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;
using WEB_VitalConnectApi.Models;


namespace WEB_VitalConnectApi.Pages
{
    public class RegistroModel : PageModel
    {
        private readonly ApiService _api;

        [BindProperty]
        public Profesionales Profesional { get; set; }

        public RegistroModel(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnPost()
        {
            await _api.PostAsync("Profesional", Profesional);
            return RedirectToPage("Profesional");
        }
    }
}
