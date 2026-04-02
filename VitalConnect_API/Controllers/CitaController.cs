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
        public async Task<ActionResult<IEnumerable<Cita>>> Get()
        {
            return Ok(await _context.Citas.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cita cita)
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
                .AnyAsync(x => x.IdUsuario == cita.IdPaciente);

            if (!pacienteExiste)
                return BadRequest("El paciente no existe.");

            var profesionalExiste = await _context.Profesionales
                .AnyAsync(x => x.IdUsuario == cita.IdProfesional);

            if (!profesionalExiste)
                return BadRequest("El profesional no existe.");

            var asistenteExiste = await _context.Asistentes
                .AnyAsync(x => x.IdUsuario == cita.IdAsistente);

            if (!asistenteExiste)
                return BadRequest("El asistente no existe.");

            var citaDuplicada = await _context.Citas.AnyAsync(x =>
                x.Fecha == cita.Fecha &&
                x.Hora == cita.Hora &&
                x.IdProfesional == cita.IdProfesional);

            if (citaDuplicada)
                return BadRequest("Ya existe una cita registrada en esa fecha y hora para ese profesional.");

            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            return Ok(cita);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Cita cita)
        {
            var data = await _context.Citas.FindAsync(id);

            if (data == null)
                return NotFound("No se encontró la cita.");

            if (cita.Fecha == default)
                return BadRequest("La fecha es obligatoria.");

            if (string.IsNullOrWhiteSpace(cita.Hora))
                return BadRequest("La hora es obligatoria.");

            if (string.IsNullOrWhiteSpace(cita.EstadoCita))
                return BadRequest("El estado de la cita es obligatorio.");

            data.Fecha = cita.Fecha;
            data.Hora = cita.Hora;
            data.Estado = cita.Estado;
            data.EstadoCita = cita.EstadoCita;
            data.IdPaciente = cita.IdPaciente;
            data.IdProfesional = cita.IdProfesional;
            data.IdAsistente = cita.IdAsistente;

            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _context.Citas.FindAsync(id);
            if (data == null) return NotFound();

            _context.Citas.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
