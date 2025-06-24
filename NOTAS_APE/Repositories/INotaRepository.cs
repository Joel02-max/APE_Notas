using NOTAS_APE.Data;
using NOTAS_APE.Models;
using Microsoft.EntityFrameworkCore;

namespace NOTAS_APE.Repositories
{
    public interface INotaRepository
    {
        Task<IEnumerable<Nota>> GetAllNotasAsync();
        Task<Nota> GetNotaByIdAsync(int id);
        Task<Nota> CrearNotaAsync(Nota nota);
        Task<bool> ActualizarNotaAsync(int id, decimal nuevaNota);


    }

    public class NotaRepository : INotaRepository
    {
        private readonly ApplicationDbContext _context;

        public NotaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nota>> GetAllNotasAsync()
        {
            return await _context.Notas
                .Include(n => n.Estudiante)
                .Include(n => n.Curso)
                .ToListAsync();
        }

        public async Task<Nota> GetNotaByIdAsync(int id)
        {
            return await _context.Notas
                .Include(n => n.Estudiante)
                .Include(n => n.Curso)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Nota> CrearNotaAsync(Nota nota)
        {
            var query = @"
        INSERT INTO Notas (cedula_es, curso_id, nota, fecha_registro)
        VALUES (@p0, @p1, @p2, @p3);
        SELECT SCOPE_IDENTITY();";

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction(); // ✅ inicia transacción
            using var command = connection.CreateCommand();

            command.Transaction = transaction; // 💡 clave: asocia el comando a la transacción
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@p0", nota.CedulaEstudiante));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@p1", nota.CursoId));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@p2", nota.Valor));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@p3", nota.FechaRegistro));

            try
            {
                var result = await command.ExecuteScalarAsync();
                nota.Id = Convert.ToInt32(result);

                await transaction.CommitAsync(); // ✅ confirma cambios

                return nota;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // ❌ revierte si algo falla
                throw;
            }
        }


        public async Task<bool> ActualizarNotaAsync(int id, decimal nuevaNota)
        {
            var query = @"
        UPDATE Notas
        SET nota = @p0, fecha_registro = @p1
        WHERE id = @p2";

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            using var command = connection.CreateCommand();

            command.Transaction = transaction;
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@p0", nuevaNota));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@p1", DateTime.Now));
            command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@p2", id));

            try
            {
                var rowsAffected = await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return rowsAffected > 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }





    }

}
