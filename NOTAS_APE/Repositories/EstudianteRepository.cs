using Microsoft.EntityFrameworkCore;
using NOTAS_APE.Data;
using NOTAS_APE.Models;

namespace NOTAS_APE.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly ApplicationDbContext _context;



        public EstudianteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante> GetByCedulaAsync(string cedula)
        {
            return await _context.Estudiantes.FirstOrDefaultAsync(e => e.Cedula == cedula);
        }

        public async Task AddAsync(Estudiante estudiante)
        {
            await _context.Estudiantes.AddAsync(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string cedula)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Cedula == cedula);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Estudiante>> FiltrarAsync(string? cedula, string? apellido)
        {
            var query = _context.Estudiantes.AsQueryable();

            if (!string.IsNullOrEmpty(cedula))
            {
                query = query.Where(e => e.Cedula.Contains(cedula));
            }

            if (!string.IsNullOrEmpty(apellido))
            {
                query = query.Where(e => e.Apellido.Contains(apellido));
            }

            return await query.ToListAsync();
        }
    }
}