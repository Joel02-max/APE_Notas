using NOTAS_APE.Data;
using NOTAS_APE.Models;
using Microsoft.EntityFrameworkCore;
using NOTAS_APE.DTOs;

namespace NOTAS_APE.Repositories
{
    public interface IPromedioRepository
    {
        // Task<IEnumerable<Promedio>> GetAllPromediosAsync();
        Task<IEnumerable<PromedioDTO>> GetAllPromediosConCursoAsync();
    }

    
}
