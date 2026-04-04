using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Data;
using VitalConnect_API.Models;

namespace VitalConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly VT_DbContext _context;

        public PacienteController(VT_DbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetPacientes()
        {
            var pacientes = await _context.Pacientes.ToListAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.Include(p => p.Citas)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (paciente is null)
            {
                return NotFound("Paciente no encontrado");
            }

            return Ok(paciente);
        }


        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente(Paciente paciente)
        {
            if (string.IsNullOrWhiteSpace(paciente.NombreCompleto))
            {
                return BadRequest("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.Telefono))
            {
                return BadRequest("El teléfono es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.CI))
            {
                return BadRequest("El CI es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.Genero))
            {
                return BadRequest("El género es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.Direccion))
            {
                return BadRequest("La dirección es obligatoria");
            }

            if (paciente.FechaNacimiento == default)
            {
                return BadRequest("La fecha de nacimiento es obligatoria");
            }

            if (paciente.FechaNacimiento > DateTime.Now)
            {
                return BadRequest("La fecha de nacimiento no puede ser futura");
            }

            var CIexistente = await _context.Pacientes.AnyAsync(p => p.CI == paciente.CI);

            if (CIexistente)
            {
                return BadRequest("El CI ya está registrado");
            }

            paciente.Rol = "Paciente";
            paciente.Estado = true;

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.ID }, paciente);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Paciente>> UpdatePaciente(int id, Paciente paciente)
        {
            var pacienteObj = await _context.Pacientes.FirstOrDefaultAsync(p => p.ID == id);

            if (pacienteObj is null)
            {
                return NotFound("El paciente no fue encontrado");
            }

            if (string.IsNullOrWhiteSpace(paciente.NombreCompleto))
            {
                return BadRequest("El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.Telefono))
            {
                return BadRequest("El teléfono es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.CI))
            {
                return BadRequest("El CI es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.Genero))
            {
                return BadRequest("El género es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(paciente.Direccion))
            {
                return BadRequest("La dirección es obligatoria");
            }

            if (paciente.FechaNacimiento == default)
            {
                return BadRequest("La fecha de nacimiento es obligatoria");
            }

            if (paciente.FechaNacimiento > DateTime.Now)
            {
                return BadRequest("La fecha de nacimiento no puede ser futura");
            }

            var CIexistente = await _context.Pacientes.AnyAsync(p => p.CI == paciente.CI && p.ID != id);

            if (CIexistente)
            {
                return BadRequest("El CI ya pertenece a otro paciente");
            }

            pacienteObj.NombreCompleto = paciente.NombreCompleto;
            pacienteObj.Telefono = paciente.Telefono;
            pacienteObj.CI = paciente.CI;
            pacienteObj.Genero = paciente.Genero;
            pacienteObj.Direccion = paciente.Direccion;
            pacienteObj.FechaNacimiento = paciente.FechaNacimiento;
            pacienteObj.Estado = paciente.Estado;

            await _context.SaveChangesAsync();

            return Ok(pacienteObj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.ID == id);
            if (paciente is null)
            {
                return NotFound("El paciente no fue encontrado");
            }
            var citasPaciente = await _context.Citas.AnyAsync(c => c.IdPaciente == id);
            if (citasPaciente)
            {
                return BadRequest("No se puede eliminar un paciente con citas registradas");
            }
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/cambiar-estado")]
        public async Task<IActionResult> CambiarEstadoPaciente(int id)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.ID == id);

            if (paciente is null)
            {
                return NotFound("El paciente no fue encontrado");
            }

            var TieneCitas = await _context.Citas.AnyAsync(c => c.IdPaciente == id && c.Estado);

            if (TieneCitas && paciente.Estado)
            {
                return BadRequest("No se puede desactivar un paciente con citas activas");
            }

            paciente.Estado = !paciente.Estado;

            await _context.SaveChangesAsync();

            return Ok("Se cambio el estado del paciente correctamente");
        }


        [HttpGet("buscar/ci/{ci}")]
        public async Task<IActionResult> BuscarPorCI(string ci)
        {
            if (string.IsNullOrWhiteSpace(ci))
            {
                return BadRequest("El CI es obligatorio");
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.CI == ci && p.Estado);

            if (paciente == null)
            {
                return NotFound("Paciente no encontrado");
            }

            return Ok(paciente);
        }




    }
}