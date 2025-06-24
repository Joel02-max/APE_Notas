using NOTAS_APE.Models;

namespace NOTAS_APE.Repositories
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante> GetByCedulaAsync(string cedula);
        Task AddAsync(Estudiante estudiante);
        Task UpdateAsync(Estudiante estudiante);
        Task DeleteAsync(string cedula);
        Task<IEnumerable<Estudiante>> FiltrarAsync(string? cedula, string? apellido);
    }
}