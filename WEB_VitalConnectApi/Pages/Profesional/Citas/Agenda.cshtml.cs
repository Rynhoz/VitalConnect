using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class AgendaModel : PageModel
    {
        private readonly ApiService _apiService;

        public AgendaModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<CitaDto> Citas { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public DateTime? FiltroFecha { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? FiltroIdProfesional { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroEstadoCita { get; set; }

        public int TotalCitas { get; set; }
        public int TotalActivas { get; set; }
        public int TotalPendientes { get; set; }
        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var lista = await _apiService.GetListAsync<CitaDto>("api/Cita");

            if (lista == null || !lista.Any())
            {
                Citas = new List<CitaDto>();
                Mensaje = "No hay citas registradas en este momento.";
                return;
            }

            IEnumerable<CitaDto> query = lista;

            if (FiltroFecha.HasValue)
            {
                query = query.Where(c => c.Fecha.Date == FiltroFecha.Value.Date);
            }

            if (FiltroIdProfesional.HasValue)
            {
                query = query.Where(c => c.IdProfesional == FiltroIdProfesional.Value);
            }

            if (!string.IsNullOrWhiteSpace(FiltroEstadoCita))
            {
                query = query.Where(c => c.EstadoCita.Equals(FiltroEstadoCita, StringComparison.OrdinalIgnoreCase));
            }

            Citas = query
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.Hora)
                .ToList();

            TotalCitas = Citas.Count;
            TotalActivas = Citas.Count(c => c.Estado);
            TotalPendientes = Citas.Count(c => c.EstadoCita.Equals("Pendiente", StringComparison.OrdinalIgnoreCase));

            if (!Citas.Any())
            {
                Mensaje = "No se encontraron resultados con esos filtros.";
            }
        }

        public class CitaDto
        {
            public int CitaId { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; } = string.Empty;
            public string EstadoCita { get; set; } = string.Empty;
            public bool Estado { get; set; }
            public int IdPaciente { get; set; }
            public int IdProfesional { get; set; }
            public int? IdAsistente { get; set; }
        }
    }
}