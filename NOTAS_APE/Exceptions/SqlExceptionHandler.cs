using Microsoft.Data.SqlClient;
using System.Text;
using System;


namespace NOTAS_APE.Exceptions
{
    public static class SqlExceptionHandler
    {
        public static void HandleSqlException(SqlException ex)
        {
            var errorBuilder = new StringBuilder();
            errorBuilder.AppendLine("Error de SQL Server:");

            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorBuilder.AppendLine($"- Número: {ex.Errors[i].Number}");
                errorBuilder.AppendLine($"- Mensaje: {ex.Errors[i].Message}");
                errorBuilder.AppendLine($"- Procedimiento: {ex.Errors[i].Procedure}");
                errorBuilder.AppendLine($"- Línea: {ex.Errors[i].LineNumber}");
                errorBuilder.AppendLine($"- Nivel de severidad: {ex.Errors[i].Class}");
                errorBuilder.AppendLine($"- Servidor: {ex.Errors[i].Server}");
                errorBuilder.AppendLine();
            }

            // Clasificar el tipo de error
            switch (ex.Number)
            {
                case 2627:  // Violación  clave unica
                case 547:   // restriccion 
                case 2601:  // índice único
                    throw new ApplicationException("Error de integridad de datos: " + ex.Errors[0].Message, ex);

                case 208:   // No exites la tabla
                case 207:  // no existe la columna
                    throw new ApplicationException("Error en el esquema de la base de datos: " + ex.Errors[0].Message, ex);

                case 18456: // Error de inicio de sesión
                case 4060:  // Base de datos no existe
                    throw new ApplicationException("Error de conexión a la base de datos: " + ex.Errors[0].Message, ex);

                default:
                    throw new ApplicationException(errorBuilder.ToString(), ex);
            }
        }
    }
}