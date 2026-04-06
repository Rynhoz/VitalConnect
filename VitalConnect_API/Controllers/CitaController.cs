using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Models;
using VitalConnect_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VitalConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        
        private readonly VT_DbContext _context;

        public CitaController(VT_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            var citas = await _context.Citas.ToListAsync();
            return Ok(citas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _context.Citas.FirstOrDefaultAsync(c => c.CitaId == id);

            if (cita == null)
                return NotFound("Cita no encontrada");

            return Ok(cita);
        }


        [HttpPost]
        public async Task<ActionResult> CreateCita(Cita cita)
        {
            if (cita == null)
                return BadRequest("Los datos de la cita son obligatorios.");

            if (cita.Fecha == default)
                return BadRequest("La fecha es obligatoria.");

            if (string.IsNullOrWhiteSpace(cita.Hora))
                return BadRequest("La hora es obligatoria.");

            if (string.IsNullOrWhiteSpace(cita.EstadoCita))
                return BadRequest("El estado de la cita es obligatorio.");

            var pacienteExiste = await _context.Pacientes
                .AnyAsync(x => x.ID == cita.IdPaciente);

            if (!pacienteExiste)
                return BadRequest("El paciente no existe.");

            var profesionalExiste = await _context.Profesionales
                .AnyAsync(x => x.ID == cita.IdProfesional);

            if (!profesionalExiste)
                return BadRequest("El profesional no existe.");


            if (cita.IdAsistente.HasValue)
            {
                var asistenteExiste = await _context.Asistentes
                    .AnyAsync(x => x.ID == cita.IdAsistente.Value);

                if (!asistenteExiste)
                    return BadRequest("El asistente no existe.");
            }

            var citaDuplicada = await _context.Citas.AnyAsync(x =>
                x.Fecha == cita.Fecha &&
                x.Hora == cita.Hora &&
                x.IdProfesional == cita.IdProfesional);

            if (citaDuplicada)
                return BadRequest("Ya existe una cita registrada en esa fecha y hora para ese profesional.");

            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCita), new { id = cita.CitaId }, cita);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Cita>> UpdateCita(int id, Cita cita)
        {
            var citaObj = await _context.Citas.FirstOrDefaultAsync(c => c.CitaId == id);

            if (citaObj == null)
                return NotFound("No se encontró la cita.");

            if (cita.Fecha == default)
                return BadRequest("La fecha es obligatoria.");

            if (string.IsNullOrWhiteSpace(cita.Hora))
                return BadRequest("La hora es obligatoria.");

            if (string.IsNullOrWhiteSpace(cita.EstadoCita))
                return BadRequest("El estado de la cita es obligatorio.");

            citaObj.Fecha = cita.Fecha;
            citaObj.Hora = cita.Hora;
            citaObj.EstadoCita = cita.EstadoCita;
            citaObj.Estado = cita.Estado;
            citaObj.IdPaciente = cita.IdPaciente;
            citaObj.IdProfesional = cita.IdProfesional;
            citaObj.IdAsistente = cita.IdAsistente;

            await _context.SaveChangesAsync();

            return Ok(citaObj);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cita>> DeleteCita(int id)
        {
            var cita = await _context.Citas.FirstOrDefaultAsync(c => c.CitaId == id);

            if (cita == null) return NotFound();

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/cambiar-estado")]
        public async Task<IActionResult> PatchEstadoCita(int id)
        {
            var cita = await _context.Citas
                .FirstOrDefaultAsync(c => c.CitaId == id);

            if (cita == null)
                return NotFound("Cita no encontrada");

            cita.Estado = !cita.Estado;

            await _context.SaveChangesAsync();

            return Ok("Se cambio el estado de la cita correctamente");
        }

        // Obtener citas por estado (pendiente, atendido, cancelada)
        [HttpGet("estado-cita/{estado}")]
        public async Task<IActionResult> GetCitasPorEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                return BadRequest("El estado es obligatorio");
            }

            var citas = await _context.Citas
                .Where(c => c.EstadoCita.ToLower() == estado.ToLower() && c.Estado)
                .ToListAsync();

            if (!citas.Any())
            {
                return NotFound("No hay citas con ese estado");
            }

            return Ok(citas);
        }


    }
}
