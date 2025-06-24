using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NOTAS_APE.DTOs;
using NOTAS_APE.Models;
using NOTAS_APE.Services;

namespace NOTAS_APE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotasController : ControllerBase
    {
        private readonly NotaService _service;

        public NotasController(NotaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaDTO>>> Get()
        {
            var notas = await _service.GetNotasDTOAsync();
            return Ok(notas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<NotaDTO>> GetById(int id)
        {
            var nota = await _service.GetNotaByIdAsync(id);
            if (nota == null) return NotFound();
            return Ok(nota);
        }


        [HttpPost]
        public async Task<ActionResult<NotaDTO>> CrearNota([FromBody] NotaCreateDTO dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Datos inválidos" });

            try
            {
                var notaCreada = await _service.CrearNotaAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = notaCreada.Id }, notaCreada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al crear nota: {ex.Message}" });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarNota(int id, [FromBody] NotaUpdateDTO dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Datos inválidos" });

            try
            {
                var actualizado = await _service.ActualizarNotaAsync(id, dto);
                if (!actualizado)
                    return NotFound(new { error = $"No se encontró la nota con id {id}" });

                return Ok(new { mensaje = "Nota actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al actualizar nota: {ex.Message}" });
            }
        }




    }
}
