using NOTAS_APE.Models;
using NOTAS_APE.Repositories;

namespace NOTAS_APE.Services
{
    public class EstudianteService
    {
        private readonly IEstudianteRepository _repository;

        public EstudianteService(IEstudianteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Estudiante> GetEstudianteByCedulaAsync(string cedula)
        {
            return await _repository.GetByCedulaAsync(cedula);
        }

        public async Task<IEnumerable<Estudiante>> FiltrarEstudiantesAsync(string? cedula, string? apellido)
        {
            return await _repository.FiltrarAsync(cedula, apellido);
        }

        public async Task AddEstudianteAsync(Estudiante estudiante)
        {
            await _repository.AddAsync(estudiante);
        }

        public async Task UpdateEstudianteAsync(Estudiante estudiante)
        {
            await _repository.UpdateAsync(estudiante);
        }

        public async Task DeleteEstudianteAsync(string cedula)
        {
            await _repository.DeleteAsync(cedula);
        }
    }
}