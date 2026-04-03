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
        public async Task<IActionResult> GetProfesionales()
        {
            var profesionales = await _context.Profesionales.ToListAsync();
            return Ok(profesionales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesional>> GetProfesional(int id)
        {
            var profesional = await _context.Profesionales.Include(p => p.Citas)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (profesional is null)
            {
                return NotFound("Profesional no encontrado");
            }

            return Ok(profesional);
        }

        [HttpPost]
        public async Task<ActionResult<Profesional>> CreateProfesional(Profesional profesional)
        {
            if (string.IsNullOrWhiteSpace(profesional.NombreCompleto))
                return BadRequest("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.Telefono))
                return BadRequest("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.CI))
                return BadRequest("El CI es obligatorio.");

            if (string.IsNullOrWhiteSpace(profesional.MatriculaProfesional))
                return BadRequest("La matrícula profesional es obligatoria.");

            if (string.IsNullOrWhiteSpace(profesional.Especialidad))
                return BadRequest("La especialidad es obligatoria.");

            var CIexistente = await _context.Profesionales.AnyAsync(p => p.CI == profesional.CI);

            if (CIexistente)
            {
                return BadRequest("El CI ya está registrado");
            }

            profesional.Rol = "Profesional";
            profesional.Estado = true;

            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesional), new { id = profesional.ID }, profesional);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Profesional>> UpdateProfesional(int id, Profesional profesional)
        {
            var Objprofesional = await _context.Profesionales.FirstOrDefaultAsync(p => p.ID == id);

            if (Objprofesional is null)
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

            Objprofesional.NombreCompleto = profesional.NombreCompleto;
            Objprofesional.Telefono = profesional.Telefono;
            Objprofesional.CI = profesional.CI;
            Objprofesional.Estado = profesional.Estado;
            Objprofesional.MatriculaProfesional = profesional.MatriculaProfesional;
            Objprofesional.Especialidad = profesional.Especialidad;

            await _context.SaveChangesAsync();

            return Ok(Objprofesional);
        }


        [HttpPatch("{id}/cambiar-estado")]
        public async Task<IActionResult> CambiarEstadoProfesional(int id)
        {
            var profesional = await _context.Profesionales.FirstOrDefaultAsync(p => p.ID == id);

            if (profesional == null)
            {
                return NotFound("El profesional no fue encontrado");
            }

            profesional.Estado = !profesional.Estado;

            await _context.SaveChangesAsync();

            return Ok(profesional);
        }

        [HttpGet("especialidad")]
        public async Task<ActionResult<List<Profesional>>> GetPorEspecialidad(string especialidad)
        {
            if (string.IsNullOrWhiteSpace(especialidad))
            {
                return BadRequest("La especialidad es obligatoria");
            }

            var profesionales = await _context.Profesionales.Where(p => p.Estado && p.Especialidad.Contains(especialidad))
                .ToListAsync();

            if (!profesionales.Any())
            {
                return NotFound("No se encontraron profesionales con esa especialidad");
            }

            return Ok(profesionales);
        }
    }
}