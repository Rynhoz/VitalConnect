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
                .FirstOrDefaultAsync(f => f.IdFicha == id);

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

            var cita = await _context.Citas.Include(c => c.FichaAtencion).FirstOrDefaultAsync(c => c.IdCita == ficha.IdCita);

            if (cita is null)
            {
                return BadRequest("La cita no existe");
            }

            if (cita.FichaAtencion != null)
            {
                return BadRequest("La cita ya tiene una ficha de atención");
            }

            if (ficha.FechaAtencion < cita.Fecha)
            {
                return BadRequest("La fecha de atención no puede ser menor a la cita");
            }

            _context.FichaAtencion.Add(ficha);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFicha), new { id = ficha.IdFicha }, ficha);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<FichaAtencion>> UpdateFicha(int id, FichaAtencion ficha)
        {
            var fichaObj = await _context.FichaAtencion.FirstOrDefaultAsync(f => f.IdFicha == id);

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
            var ficha = await _context.FichaAtencion.FirstOrDefaultAsync(f => f.IdFicha == id);

            if (ficha is null)
            {
                return NotFound("Ficha no encontrada");
            }

            _context.FichaAtencion.Remove(ficha);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("por-cita/{idCita}")]
        public async Task<ActionResult<FichaAtencion>> GetFichaPorCita(int idCita)
        {
            var ficha = await _context.FichaAtencion.Include(f => f.Recetas).ThenInclude(r => r.DetallesReceta)
                .FirstOrDefaultAsync(f => f.IdCita == idCita);

            if (ficha is null)
            {
                return NotFound("No existe ficha para esta cita");
            }

            return Ok(ficha);
        }

    }
}
