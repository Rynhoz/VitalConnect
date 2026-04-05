using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;
using WEB_VitalConnectApi.Models;
using VitalConnect_API.Models;
using System.Collections.Generic;

namespace WEB_VitalConnectApi.Pages.Profesional.Medicamento
{
    public class MedicamentoModel : PageModel
    {
        private readonly ApiService _api;

        public List<VitalConnect_API.Models.Medicamento> Medicamentos { get; set; } = new();
        public int TotalMedicamentos { get; set; }
        public int Activos { get; set; }
        public int Inactivos { get; set; }

        public MedicamentoModel(ApiService api)
        {
            _api = api;
        }

        public async Task OnGet()
        {
            var resultado = await _api.GetListAsync<VitalConnect_API.Models.Medicamento>("api/Medicamento");

            if (resultado != null)
            {
                Medicamentos = resultado;

                TotalMedicamentos = Medicamentos.Count;
                Activos = Medicamentos.Count(m => m.Estado);
                Inactivos = Medicamentos.Count(m => !m.Estado);
            }
        }
    }

    public class MedicamentoViewModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
