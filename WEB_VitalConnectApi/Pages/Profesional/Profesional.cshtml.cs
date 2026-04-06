using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VitalConnect_API.Models;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class ProfesionalModel : PageModel
    {
        

        private readonly ApiService _api;

        public int CitasHoy { get; set; }
        public int PacientesSemana { get; set; }

        public ProfesionalModel(ApiService api)
        {
            _api = api;
        }

        public async Task OnGet()
        {
            var citas = await _api.GetAsync<List<Cita>>("Cita");
            
            if(citas == null)
            {
                CitasHoy = 0;
                PacientesSemana = 0;
                return;
            }

            CitasHoy = citas.Count(c => c.Fecha.Date == DateTime.Today);

            PacientesSemana = citas.Count(c =>
                c.Fecha >= DateTime.Today.AddDays(-7));
        }
    }
}
