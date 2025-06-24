using System.ComponentModel.DataAnnotations.Schema;

namespace NOTAS_APE.Models
{
    public class Promedio
    {
        public int Id { get; set; }

        [Column("cedula_est")]
        public string CedulaEst { get; set; }

        [Column("promedio")]
        public decimal ValorPromedio { get; set; }

        public Estudiante Estudiante { get; set; }
    }
}
