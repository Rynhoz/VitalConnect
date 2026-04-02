using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Data;
using VitalConnect_API.Models;


namespace VitalConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        private readonly VT_DbContext _context;

        public RecetaController(VT_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> Get()
        {
            return Ok(await _context.Recetas.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Receta>>> Post(Receta receta)
        {
            if (receta == null)
                return BadRequest("Los datos de la receta son obligatorios.");

            if (string.IsNullOrWhiteSpace(receta.Observaciones))
                return BadRequest("Las observaciones son obligatorias.");

            if (receta.IdFicha <= 0)
                return BadRequest("El Id de la ficha es obligatorio.");

            var fichaExiste = await _context.FichaAtencion
                .AnyAsync(x => x.IdFicha == receta.IdFicha);

            if (!fichaExiste)
                return BadRequest("La ficha de atención no existe.");

            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();

            return Ok(receta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Receta receta)
        {
            var data = await _context.Recetas.FindAsync(id);

            if (data == null)
                return NotFound("No se encontró la receta.");

            if (string.IsNullOrWhiteSpace(receta.Observaciones))
                return BadRequest("Las observaciones son obligatorias.");

            if (receta.IdFicha <= 0)
                return BadRequest("El Id de la ficha es obligatorio.");

            var fichaExiste = await _context.FichaAtencion
                .AnyAsync(x => x.IdFicha == receta.IdFicha);

            if (!fichaExiste)
                return BadRequest("La ficha de atención no existe.");

            data.Observaciones = receta.Observaciones;
            data.IdFicha = receta.IdFicha;

            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _context.Recetas.FindAsync(id);

            if (data == null)
                return NotFound("No se encontró la receta.");

            _context.Recetas.Remove(data);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

