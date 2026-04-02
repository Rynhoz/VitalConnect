using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Data;
using VitalConnect_API.Models;

namespace VitalConnect_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private readonly VT_DbContext _context;

        public MedicamentoController(VT_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> Get()
        {
            return Ok(await _context.Medicamentos.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Medicamento medicamento)
        {
            if (medicamento == null)
                return BadRequest("Los datos del medicamento son obligatorios.");

            if (string.IsNullOrWhiteSpace(medicamento.Nombre))
                return BadRequest("El nombre del medicamento es obligatorio.");

            if (string.IsNullOrWhiteSpace(medicamento.Descripcion))
                return BadRequest("La descripción es obligatoria.");

            var existeMedicamento = await _context.Medicamentos
                .AnyAsync(x => x.Nombre == medicamento.Nombre);

            if (existeMedicamento)
                return BadRequest("Ya existe un medicamento con ese nombre.");

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return Ok(medicamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Medicamento medicamento)
        {
            var data = await _context.Medicamentos.FindAsync(id);

            if (data == null)
                return NotFound("No se encontró el medicamento.");

            if (string.IsNullOrWhiteSpace(medicamento.Nombre))
                return BadRequest("El nombre del medicamento es obligatorio.");

            if (string.IsNullOrWhiteSpace(medicamento.Descripcion))
                return BadRequest("La descripción es obligatoria.");

            data.Nombre = medicamento.Nombre;
            data.Descripcion = medicamento.Descripcion;
            data.Estado = medicamento.Estado;

            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _context.Medicamentos.FindAsync(id);

            if (data == null)
                return NotFound("No se encontró el medicamento.");

            _context.Medicamentos.Remove(data);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
