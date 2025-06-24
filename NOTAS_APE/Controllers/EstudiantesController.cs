using Microsoft.AspNetCore.Mvc;
using NOTAS_APE.Models;
using NOTAS_APE.Services;

namespace NOTAS_APE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly EstudianteService _estudianteService;

        public EstudiantesController(EstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        // GET: api/Estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> Get()
        {
            var estudiantes = await _estudianteService.GetEstudiantesAsync();
            if (estudiantes == null || !estudiantes.Any())
            {
                return NotFound("No se encontraron estudiantes.");
            }
            return Ok(estudiantes);
        }

        // GET: api/Estudiantes/{cedula}
        [HttpGet("{cedula}")]
        public async Task<ActionResult<Estudiante>> GetByCedula(string cedula)
        {
            var estudiante = await _estudianteService.GetEstudianteByCedulaAsync(cedula);
            if (estudiante == null)
            {
                return NotFound($"Estudiante con cédula {cedula} no encontrado.");
            }
            return Ok(estudiante);
        }

        // GET: api/Estudiantes/filtrar?cedula=...&apellido=...
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<Estudiante>>> Filtrar([FromQuery] string? cedula, [FromQuery] string? apellido)
        {
            if (string.IsNullOrEmpty(cedula) && string.IsNullOrEmpty(apellido))
            {
                return BadRequest("Debe enviar 'cedula' o 'apellido' como query param");
            }

            var estudiantes = await _estudianteService.FiltrarEstudiantesAsync(cedula, apellido);

            if (!estudiantes.Any())
            {
                return NotFound("No se encontraron estudiantes.");
            }

            return Ok(estudiantes);
        }

        // POST: api/Estudiantes
        [HttpPost]
        public async Task<ActionResult<Estudiante>> Post([FromBody] Estudiante estudiante)
        {
            try
            {
                await _estudianteService.AddEstudianteAsync(estudiante);
                return CreatedAtAction(nameof(GetByCedula), new { cedula = estudiante.Cedula }, estudiante);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Estudiantes/{cedula}
        [HttpPut("{cedula}")]
        public async Task<IActionResult> Put(string cedula, [FromBody] Estudiante estudiante)
        {
            if (cedula != estudiante.Cedula)
            {
                return BadRequest("La cédula no coincide.");
            }

            try
            {
                await _estudianteService.UpdateEstudianteAsync(estudiante);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Estudiantes/{cedula}
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> Delete(string cedula)
        {
            try
            {
                await _estudianteService.DeleteEstudianteAsync(cedula);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}