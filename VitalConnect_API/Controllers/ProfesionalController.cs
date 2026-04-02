using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VitalConnect_API.Models;
using VitalConnect_API.Data;

namespace VitalConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalController : ControllerBase
    {
        private readonly VT_DbContext _context;

        public ProfesionalController(VT_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Profesionales.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Profesional profesional)
        {
            if (profesional == null)
                return BadRequest("Los datos del profesional son obligatorios.");

            if (string.IsNullOrWhiteSpace(profesional.NombreCompleto))
                return BadRequest("El nombre completo es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.Telefono))
                return BadRequest("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.CI))
                return BadRequest("El CI es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.MatriculaProfesional))
                return BadRequest("La matrícula profesional es obligatoria.");

            if (string.IsNullOrWhiteSpace(profesional.Especialidad))
                return BadRequest("La especialidad es obligatoria.");

            var existeCI = await _context.Profesionales
                .AnyAsync(x => x.CI == profesional.CI);

            if (existeCI)
                return BadRequest("Ya existe un profesional con ese CI.");

            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();

            return Ok(profesional);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Profesional profesional)
        {
            var data = await _context.Profesionales.FindAsync(id);

            if (data == null)
                return NotFound("No se encontró el profesional.");

            if (string.IsNullOrWhiteSpace(profesional.NombreCompleto))
                return BadRequest("El nombre completo es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.Telefono))
                return BadRequest("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.CI))
                return BadRequest("El CI es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.MatriculaProfesional))
                return BadRequest("La matrícula profesional es obligatoria.");

            if (string.IsNullOrWhiteSpace(profesional.Especialidad))
                return BadRequest("La especialidad es obligatoria.");

            data.NombreCompleto = profesional.NombreCompleto;
            data.Telefono = profesional.Telefono;
            data.CI = profesional.CI;
            data.Estado = profesional.Estado;
            data.MatriculaProfesional = profesional.MatriculaProfesional;
            data.Especialidad = profesional.Especialidad;

            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Profesionales.FindAsync(id);
            if (data == null) return NotFound();
            _context.Profesionales.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}