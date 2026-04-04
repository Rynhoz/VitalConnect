using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VitalConnect_API.Data;
using VitalConnect_API.Models;
using Microsoft.EntityFrameworkCore;

namespace VitalConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaAtencionController : ControllerBase
    {
        private readonly VT_DbContext _context;

        public FichaAtencionController(VT_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFichas()
        {
            var fichas = await _context.FichaAtencion.Include(f => f.Recetas).ToListAsync();
            return Ok(fichas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FichaAtencion>> GetFicha(int id)
        {
            var ficha = await _context.FichaAtencion.Include(f => f.Recetas).ThenInclude(r => r.DetallesReceta)
                .FirstOrDefaultAsync(f => f.FichaAtencionId == id);

            if (ficha is null)
            {
                return NotFound("Ficha no encontrada");
            }

            return Ok(ficha);
        }


        [HttpPost]
        public async Task<ActionResult<FichaAtencion>> CreateFicha(FichaAtencion ficha)
        {
            if (ficha.FechaAtencion == default)
            {
                return BadRequest("La fecha de atención es obligatoria");
            }

            if (string.IsNullOrWhiteSpace(ficha.Diagnostico))
            {
                return BadRequest("El diagnóstico es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(ficha.Indicaciones))
            {
                return BadRequest("Las indicaciones son obligatorias");
            }

            var cita = await _context.Citas.FirstOrDefaultAsync(c => c.CitaId == ficha.IdCita);
            if (cita is null)
            {
                return BadRequest("La cita no existe");
            }

            // Varificar que la ficha no exista en otra cita
            var fichaExistente = await _context.FichaAtencion.FirstOrDefaultAsync(f => f.IdCita == ficha.IdCita);
            if (fichaExistente != null)
            {
                return BadRequest("La cita ya tiene una ficha de atención");
            }


            if (ficha.FechaAtencion < cita.Fecha)
            {
                return BadRequest("La fecha de atención no puede ser menor a la cita");
            }

            _context.FichaAtencion.Add(ficha);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFicha), new { id = ficha.FichaAtencionId }, ficha);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<FichaAtencion>> UpdateFicha(int id, FichaAtencion ficha)
        {
            var fichaObj = await _context.FichaAtencion.FirstOrDefaultAsync(f => f.FichaAtencionId == id);

            if (fichaObj is null)
            {
                return NotFound("La ficha de atencion no fue encontrada");
            }

            if (string.IsNullOrWhiteSpace(ficha.Diagnostico))
            {
                return BadRequest("El diagnóstico es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(ficha.Indicaciones))
            {
                return BadRequest("Las indicaciones son obligatorias");
            }

            if (ficha.FechaAtencion == default)
            {
                return BadRequest("La fecha de atención es obligatoria");
            }

            fichaObj.Diagnostico = ficha.Diagnostico;
            fichaObj.Indicaciones = ficha.Indicaciones;
            fichaObj.FechaAtencion = ficha.FechaAtencion;

            await _context.SaveChangesAsync();

            return Ok(fichaObj);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteFicha(int id)
        {
            var ficha = await _context.FichaAtencion.FirstOrDefaultAsync(f => f.FichaAtencionId == id);
            if (ficha == null)
            {
                return NotFound("La ficha de atención no fue encontrado");
            }
            _context.FichaAtencion.Remove(ficha);
            await _context.SaveChangesAsync();
            return Ok("La ficha de atención se eliminó correctamente");
        }


        [HttpPatch("{id}/cambiar-estado")]

        public async Task<IActionResult> PatchEstadoFicha(int id)
        {
            var ficha = await _context.FichaAtencion.FirstOrDefaultAsync(f => f.FichaAtencionId == id);

            if (ficha == null)
            {
                return NotFound("La ficha de atención no fue encontrado");
            }

            ficha.Estado = !ficha.Estado;
            await _context.SaveChangesAsync();

            return Ok("Se cambio el estado de la ficha de atencion correctamente");
        }

        // Obtener fichas por fecha
        [HttpGet("fecha/{fecha}")]
        public async Task<IActionResult> GetFichasPorFecha(DateTime fecha)
        {
            var fichas = await _context.FichaAtencion
                .Where(f => f.FechaAtencion.Date == fecha.Date && f.Estado)
                .Include(f => f.Recetas)
                .ToListAsync();

            if (!fichas.Any())
            {
                return NotFound("No hay fichas en esa fecha");
            }

            return Ok(fichas);
        }

        // Obtener fichas por paciente
        [HttpGet("paciente/{idPaciente}")]
        public async Task<IActionResult> GetFichasPorPaciente(int idPaciente)
        {
            var fichas = await _context.FichaAtencion
                .Where(f => f.Cita.IdPaciente == idPaciente && f.Estado)
                .Include(f => f.Recetas)
                .ToListAsync();

            if (!fichas.Any())
            {
                return NotFound("El paciente no tiene fichas de atención");
            }

            return Ok(fichas);
        }



    }
}
