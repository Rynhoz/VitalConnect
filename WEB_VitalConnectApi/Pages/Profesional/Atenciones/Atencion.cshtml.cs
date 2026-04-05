using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;
using WEB_VitalConnectApi.Models;

namespace WEB_VitalConnectApi.Pages
{
    public class AtencionModel : PageModel
    {
        private readonly ApiService _api;

        public List<FichaAtencion> Lista { get; set; }

        public AtencionModel(ApiService api)
        {
            _api = api;
        }

        public async Task OnGet()
        {
            var fichas = await _api.GetAsync<List<FichaAtencion>>("FichaAtencion");

            Lista = fichas
                .Where(f => f.FechaAtencion.Date == DateTime.Today)
                .ToList();
        }
    }
}
