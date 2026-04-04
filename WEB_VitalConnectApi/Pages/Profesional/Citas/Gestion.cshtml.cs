using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_VitalConnectApi.Services;

namespace WEB_VitalConnectApi.Pages
{
    public class GestionModel : PageModel
    {
        private readonly ApiService _apiService;

        public GestionModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public CitaFormModel Formulario { get; set; } = new();

        public List<CitaDto> Citas { get; set; } = new();

        [TempData]
        public string Mensaje { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await CargarCitasAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Formulario.Fecha == default ||
                string.IsNullOrWhiteSpace(Formulario.Hora) ||
                string.IsNullOrWhiteSpace(Formulario.EstadoCita) ||
                Formulario.IdPaciente <= 0 ||
                Formulario.IdProfesional <= 0)
            {
                Mensaje = "Completa correctamente los datos obligatorios.";
                await CargarCitasAsync();
                return Page();
            }

            bool ok;

            if (Formulario.CitaId > 0)
            {
                ok = await _apiService.PutAsync($"api/Cita/{Formulario.CitaId}", new
                {
                    Fecha = Formulario.Fecha,
                    Hora = Formulario.Hora,
                    EstadoCita = Formulario.EstadoCita,
                    Estado = Formulario.Estado,
                    IdPaciente = Formulario.IdPaciente,
                    IdProfesional = Formulario.IdProfesional,
                    IdAsistente = Formulario.IdAsistente
                });

                Mensaje = ok ? "Cita actualizada correctamente." : "No se pudo actualizar la cita.";
            }
            else
            {
                ok = await _apiService.PostAsync("api/Cita", new
                {
                    Fecha = Formulario.Fecha,
                    Hora = Formulario.Hora,
                    EstadoCita = Formulario.EstadoCita,
                    Estado = Formulario.Estado,
                    IdPaciente = Formulario.IdPaciente,
                    IdProfesional = Formulario.IdProfesional,
                    IdAsistente = Formulario.IdAsistente
                });

                Mensaje = ok ? "Cita registrada correctamente." : "No se pudo registrar la cita.";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditarAsync(int id)
        {
            var cita = await _apiService.GetAsync<CitaDto>($"api/Cita/{id}");

            if (cita == null)
            {
                Mensaje = "No se encontró la cita.";
                return RedirectToPage();
            }

            Formulario = new CitaFormModel
            {
                CitaId = cita.CitaId,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                EstadoCita = cita.EstadoCita,
                Estado = cita.Estado,
                IdPaciente = cita.IdPaciente,
                IdProfesional = cita.IdProfesional,
                IdAsistente = cita.IdAsistente
            };

            await CargarCitasAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var ok = await _apiService.DeleteAsync($"api/Cita/{id}");
            Mensaje = ok ? "Cita eliminada correctamente." : "No se pudo eliminar la cita.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync(int id)
        {
            var ok = await _apiService.PatchAsync($"api/Cita/{id}/cambiar-estado");
            Mensaje = ok ? "Estado de la cita cambiado correctamente." : "No se pudo cambiar el estado.";
            return RedirectToPage();
        }

        private async Task CargarCitasAsync()
        {
            Citas = await _apiService.GetListAsync<CitaDto>("api/Cita") ?? new List<CitaDto>();

            Citas = Citas
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.Hora)
                .ToList();
        }

        public class CitaFormModel
        {
            public int CitaId { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; } = string.Empty;
            public string EstadoCita { get; set; } = string.Empty;
            public bool Estado { get; set; } = true;
            public int IdPaciente { get; set; }
            public int IdProfesional { get; set; }
            public int? IdAsistente { get; set; }
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