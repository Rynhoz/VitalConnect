using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;
using WEB_VitalConnectApi.Models;

namespace WEB_VitalConnectApi.Pages
{
    public class MedicamentoModel : PageModel
    {
        private readonly ApiService _api;

        public List<Medicamento> Medicamentos { get; set; } = new();
        public int TotalMedicamentos { get; set; }
        public int Activos { get; set; }
        public int Inactivos { get; set; }

        public MedicamentoModel(ApiService api)
        {
            _api = api;
        }

        public async Task OnGet()
        {
            Medicamentos = await _api.GetAsync<List<Medicamento>>("Medicamento");

            TotalMedicamentos = Medicamentos.Count;
            Activos = Medicamentos.Count(m => m.Estado);
            Inactivos = Medicamentos.Count(m => !m.Estado);
        }
    }
}
