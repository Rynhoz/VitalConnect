using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages.Paciente
{
    public class PacienteModel : PageModel
    {
        private readonly ApiService _apiService;

        public PacienteModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public PacienteDto Paciente { get; set; } = new();
        public ProximaCitaDto? ProximaCita { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var paciente = await _apiService.GetAsync<PacienteDto>($"api/Paciente/{id}");
            if (paciente == null)
                return RedirectToPage("/Error");

            Paciente = paciente;

            var citas = await _apiService.GetListAsync<CitaDto>("api/Cita") ?? new List<CitaDto>();
            var profesionales = await _apiService.GetListAsync<ProfesionalDto>("api/Profesional") ?? new List<ProfesionalDto>();

            var siguiente = citas
                .Where(c => c.IdPaciente == id && c.Fecha.Date >= DateTime.Today && c.Estado)
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.Hora)
                .FirstOrDefault();

            if (siguiente != null)
            {
                var profesional = profesionales.FirstOrDefault(p => p.IdUsuario == siguiente.IdProfesional);

                ProximaCita = new ProximaCitaDto
                {
                    Fecha = siguiente.Fecha,
                    Hora = siguiente.Hora,
                    EstadoCita = siguiente.EstadoCita,
                    NombreProfesional = profesional?.NombreCompleto ?? "No disponible",
                    Especialidad = profesional?.Especialidad ?? "No disponible"
                };
            }

            return Page();
        }

        public class PacienteDto
        {
            public int IdUsuario { get; set; }
            public string NombreCompleto { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public string CI { get; set; } = string.Empty;
            public bool Estado { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Genero { get; set; } = string.Empty;
            public string Direccion { get; set; } = string.Empty;
            public string AlergiasAntecedentes { get; set; } = string.Empty;
        }

        public class CitaDto
        {
            public int IdCita { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; } = string.Empty;
            public string EstadoCita { get; set; } = string.Empty;
            public bool Estado { get; set; }
            public int IdPaciente { get; set; }
            public int IdProfesional { get; set; }
            public int IdAsistente { get; set; }
        }

        public class ProfesionalDto
        {
            public int IdUsuario { get; set; }
            public string NombreCompleto { get; set; } = string.Empty;
            public string Especialidad { get; set; } = string.Empty;
        }

        public class ProximaCitaDto
        {
            public DateTime Fecha { get; set; }
            public string Hora { get; set; } = string.Empty;
            public string EstadoCita { get; set; } = string.Empty;
            public string NombreProfesional { get; set; } = string.Empty;
            public string Especialidad { get; set; } = string.Empty;
        }
    }
}