using NOTAS_APE.Data;
using NOTAS_APE.DTOs;
using Microsoft.EntityFrameworkCore;

namespace NOTAS_APE.Repositories
{
    public class PromedioRepository : IPromedioRepository
    {
        private readonly ApplicationDbContext _context;

        public PromedioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PromedioDTO>> GetAllPromediosConCursoAsync()
        {
            var query = from p in _context.Promedios
                        join e in _context.Estudiantes on p.CedulaEst equals e.Cedula
                        join n in _context.Notas on e.Cedula equals n.CedulaEstudiante
                        join c in _context.Cursos on n.CursoId equals c.Id
                        select new PromedioDTO
                        {
                            Cedula = e.Cedula,
                            Nombre = e.Nombre,
                            Apellido = e.Apellido,
                            Asignatura = c.Nombre,
                            Promedio = p.ValorPromedio,
                            Estado = p.ValorPromedio >= 7
                                ? "Aprobado"
                                : p.ValorPromedio >= 5
                                    ? "Suspenso"
                                    : "Reprobado"
                        };

            return await query.Distinct().ToListAsync();
        }

    }
}
