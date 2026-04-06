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
        public async Task<IActionResult> GetRecetas()
        {
            var recetas = await _context.Recetas
                .Where(r => r.Estado)
                .Include(r => r.DetallesReceta)
                .ToListAsync();

            return Ok(recetas);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceta(int id)
        {
            var receta = await _context.Recetas
                .Include(r => r.DetallesReceta)
                .ThenInclude(d => d.Medicamento)
                .FirstOrDefaultAsync(r => r.RecetaId == id);

            if (receta == null)
                return NotFound("Receta no encontrada");

            return Ok(receta);
        }


        [HttpPost]
        public async Task<IActionResult> CreateReceta(Receta receta)
        {
            
            if (string.IsNullOrWhiteSpace(receta.Observaciones))
                return BadRequest("Las observaciones son obligatorias.");

            if (receta.IdFicha <= 0)
                return BadRequest("El Id de la ficha es obligatorio.");

            var fichaExiste = await _context.FichaAtencion
                .AnyAsync(x => x.FichaAtencionId == receta.IdFicha);

            if (!fichaExiste)
                return BadRequest("La ficha de atención no existe.");

            
            if (receta.DetallesReceta == null || !receta.DetallesReceta.Any())
                return BadRequest("Debe agregar al menos un medicamento");

            foreach (var detalle in receta.DetallesReceta)
            {
                
                if (string.IsNullOrWhiteSpace(detalle.Dosis))
                    return BadRequest("La dosis es obligatoria");

                if (string.IsNullOrWhiteSpace(detalle.Frecuencia))
                    return BadRequest("La frecuencia es obligatoria");

                if (string.IsNullOrWhiteSpace(detalle.Duracion))
                    return BadRequest("La duración es obligatoria");

                if (detalle.IdMedicamento <= 0)
                    return BadRequest("El medicamento es obligatorio");

               
                var medicamentoExiste = await _context.Medicamentos
                    .AnyAsync(m => m.MedicamentoId == detalle.IdMedicamento && m.Estado);

                if (!medicamentoExiste)
                    return BadRequest($"El medicamento con ID {detalle.IdMedicamento} no existe o está inactivo");
            }

           
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReceta), new { id = receta.RecetaId}, receta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceta(int id, Receta receta)
        {
            var recetaObj = await _context.Recetas
                .Include(r => r.DetallesReceta)
                .FirstOrDefaultAsync(r => r.RecetaId == id);

            if (recetaObj == null)
                return NotFound("Receta no encontrada");

            
            if (string.IsNullOrWhiteSpace(receta.Observaciones))
                return BadRequest("Las observaciones son obligatorias");

            if (receta.IdFicha <= 0)
                return BadRequest("El Id de la ficha es obligatorio");

            var fichaExiste = await _context.FichaAtencion
                .AnyAsync(f => f.FichaAtencionId == receta.IdFicha);

            if (!fichaExiste)
                return BadRequest("La ficha no existe");

            
            if (receta.DetallesReceta == null || !receta.DetallesReceta.Any())
                return BadRequest("Debe existir al menos un detalle de receta");

            foreach (var detalle in receta.DetallesReceta)
            {
                if (string.IsNullOrWhiteSpace(detalle.Dosis))
                    return BadRequest("La dosis es obligatoria");

                if (string.IsNullOrWhiteSpace(detalle.Frecuencia))
                    return BadRequest("La frecuencia es obligatoria");

                if (string.IsNullOrWhiteSpace(detalle.Duracion))
                    return BadRequest("La duración es obligatoria");

                if (detalle.IdMedicamento <= 0)
                    return BadRequest("El medicamento es obligatorio");

                var medicamentoExiste = await _context.Medicamentos
                    .AnyAsync(m => m.MedicamentoId == detalle.IdMedicamento && m.Estado);

                if (!medicamentoExiste)
                    return BadRequest($"El medicamento con ID {detalle.IdMedicamento} no existe o está inactivo");
            }

            
            recetaObj.Observaciones = receta.Observaciones;
            recetaObj.IdFicha = receta.IdFicha;

            
            _context.DetalleReceta.RemoveRange(recetaObj.DetallesReceta);
            recetaObj.DetallesReceta = receta.DetallesReceta;

            await _context.SaveChangesAsync();

            return Ok(recetaObj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var receta = await _context.Recetas
                .Include(r => r.DetallesReceta)
                .FirstOrDefaultAsync(r => r.RecetaId == id);

            if (receta == null)
                return NotFound("Receta no encontrada");

           
            _context.DetalleReceta.RemoveRange(receta.DetallesReceta);

            
            _context.Recetas.Remove(receta);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/cambiar-estado")]
        public async Task<IActionResult> PatchEstadoReceta(int id)
        {
            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.RecetaId == id);

            if (receta == null)
                return NotFound("Receta no encontrada");

            receta.Estado = !receta.Estado;

            await _context.SaveChangesAsync();

            return Ok("Se cambio el estado de la receta correctamente");
        }

        // Obtener recetas por ficha de atención
        [HttpGet("ficha/{idFicha}")]
        public async Task<IActionResult> GetPorFicha(int idFicha)
        {
            var recetas = await _context.Recetas
                .Where(r => r.IdFicha == idFicha && r.Estado)
                .Include(r => r.DetallesReceta)
                .ThenInclude(d => d.Medicamento)
                .ToListAsync();

            if (!recetas.Any())
                return NotFound("No hay recetas para esta ficha");

            return Ok(recetas);
        }



    }
}

