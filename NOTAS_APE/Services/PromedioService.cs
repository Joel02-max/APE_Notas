using NOTAS_APE.DTOs;
using NOTAS_APE.Repositories;
using Microsoft.EntityFrameworkCore;


namespace NOTAS_APE.Services
{
    public class PromedioService
    {
        private readonly IPromedioRepository _repository;

        public PromedioService(IPromedioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PromedioDTO>> GetPromediosDTOAsync()
        {
            return await _repository.GetAllPromediosConCursoAsync();
        }
    }
}