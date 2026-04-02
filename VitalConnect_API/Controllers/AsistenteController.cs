using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Data;
using VitalConnect_API.Models;

namespace VitalConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenteController : ControllerBase
    {
        private readonly VT_DbContext _context;

        public AsistenteController(VT_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistente>>> GetAsistentes()
        {
            //Retornar asistentes
            return Ok(await _context.Asistentes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asistente>> GetAsistente(int id)
        {
            var asistente = await _context.Asistentes.FindAsync(id);

            if (asistente == null) return NotFound();

            return Ok(asistente);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsistente(Asistente asistente)
        {
            if (string.IsNullOrWhiteSpace(asistente.NombreCompleto)) return BadRequest("Debe Ingresar el Nombre Completo correctamente");

            if(string.IsNullOrWhiteSpace(asistente.Telefono)) return BadRequest("Debe Ingresar el Telefono correctamente");

            if (string.IsNullOrWhiteSpace(asistente.CI)) return BadRequest("Debe Ingresar el CI correctamente");

            if(string.IsNullOrWhiteSpace(asistente.Rol)) return BadRequest("Debe Ingresar el Rol correctamente");

            if (string.IsNullOrWhiteSpace(asistente.Turno)) return BadRequest("Debe Ingresar el Turno correctamente");

            _context.Asistentes.Add(asistente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof (GetAsistente), new {id = asistente.ID}, asistente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsistente(int id, Asistente asistente)
        {
            var assistant = await _context.Asistentes.FindAsync(id);

            if (asistente == null) return NotFound();

            if (string.IsNullOrWhiteSpace(asistente.NombreCompleto)) return BadRequest("Debe Ingresar el Nombre Completo correctamente");

            if (string.IsNullOrWhiteSpace(asistente.Telefono)) return BadRequest("Debe Ingresar el Telefono correctamente");

            if (string.IsNullOrWhiteSpace(asistente.CI)) return BadRequest("Debe Ingresar el CI correctamente");

            if (string.IsNullOrWhiteSpace(asistente.Rol)) return BadRequest("Debe Ingresar el Rol correctamente");

            if (string.IsNullOrWhiteSpace(asistente.Turno)) return BadRequest("Debe Ingresar el Turno correctamente");

            assistant.NombreCompleto = asistente.NombreCompleto;
            assistant.Telefono = asistente.Telefono;
            assistant.CI = asistente.CI;
            assistant.Rol = asistente.Rol;
            assistant.Turno = asistente.Turno;

            await _context.SaveChangesAsync();

            return Ok(assistant);
        }
    }
}
