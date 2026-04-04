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

        public async Task<IActionResult> GetMedicamentos()
        {
            var medicamentos = await _context.Medicamentos.ToListAsync();
            return Ok(medicamentos);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FirstOrDefaultAsync(m => m.MedicamentoId == id);
            if (medicamento is null)
            {
                return NotFound("Medicamento no encontrado");
            }
            return Ok(medicamento);
        }


        [HttpPost]
        public async Task<ActionResult> Post(Medicamento medicamento)
        {
            if (string.IsNullOrWhiteSpace(medicamento.Nombre))
                return BadRequest("El nombre del medicamento es obligatorio.");

            if (string.IsNullOrWhiteSpace(medicamento.Descripcion))
                return BadRequest("La descripción es obligatoria.");

            var existeMedicamento = await _context.Medicamentos.AnyAsync(x => x.Nombre == medicamento.Nombre);

            if (existeMedicamento)
                return BadRequest("Ya existe un medicamento con ese nombre.");

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return Ok(medicamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Medicamento>> Put(int id, Medicamento medicamento)
        {
            var medicamentoObj = await _context.Medicamentos.FirstOrDefaultAsync(m => m.MedicamentoId == id);

            if (medicamentoObj == null)
                return NotFound("No se encontró el medicamento.");

            if (string.IsNullOrWhiteSpace(medicamento.Nombre))
                return BadRequest("El nombre del medicamento es obligatorio.");

            if (string.IsNullOrWhiteSpace(medicamento.Descripcion))
                return BadRequest("La descripción es obligatoria.");

            medicamentoObj.Nombre = medicamento.Nombre;
            medicamentoObj.Descripcion = medicamento.Descripcion;
            medicamentoObj.Estado = medicamento.Estado;

            await _context.SaveChangesAsync();

            return Ok(medicamentoObj);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FirstOrDefaultAsync(m => m.MedicamentoId == id);
            if (medicamento == null)
            {
                return NotFound("El medicamento no fue encontrado");
            }
            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/cambiar-estado")]

        public async Task<IActionResult> CambiarEstadoMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FirstOrDefaultAsync(m => m.MedicamentoId == id);

            if (medicamento == null)
            {
                return NotFound("El medicamento no fue encontrado");
            }

            medicamento.Estado = !medicamento.Estado; 
            await _context.SaveChangesAsync();

            return Ok("Se cambio el estado de medicamento correctamente");
        }

        [HttpGet("buscar/{nombre}")]
        public async Task<IActionResult> BuscarMedicamentoPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("El nombre es obligatorio");
            }

            var medicamentos = await _context.Medicamentos.Where(m => m.Nombre.Contains(nombre) && m.Estado)
                .ToListAsync();

            if (!medicamentos.Any())
            {
                return NotFound("No se encontraron medicamentos");
            }

            return Ok(medicamentos);
        }


    }
}
