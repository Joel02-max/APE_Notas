using Microsoft.AspNetCore.Mvc;
using NOTAS_APE.Data;
using NOTAS_APE.Models;
using Microsoft.EntityFrameworkCore;

namespace NOTAS_APE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CursosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> Get()
        {
            return await _context.Cursos.ToListAsync();
        }
    }
}
