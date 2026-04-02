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
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Citas.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return Ok(cita);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cita cita)
        {
            var data = await _context.Citas.FindAsync(id);
            if (data == null) return NotFound();

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
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Citas.FindAsync(id);
            if (data == null) return NotFound();

            _context.Citas.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
