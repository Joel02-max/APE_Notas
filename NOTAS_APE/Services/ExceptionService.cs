using Microsoft.Data.SqlClient;


namespace NOTAS_APE.Services
{
    public class ExceptionService
    {
        public void HandleSqlException(SqlException ex)
        {
            if (ex.Number == 2627)  // Primary Key violation
            {
                throw new Exception("Registro duplicado.");
            }
            else if (ex.Number == 547)  // Foreign Key violation
            {
                throw new Exception("Error de integridad referencial.");
            }
            else
            {
                throw new Exception($"Codigo Repetido. Código: {ex.Number}, Mensaje: {ex.Message}");
            }
        }
    }
}