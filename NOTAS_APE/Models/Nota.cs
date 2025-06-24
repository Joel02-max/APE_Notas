using System.ComponentModel.DataAnnotations.Schema;

namespace NOTAS_APE.Models
{
    public class Nota
    {
        public int Id { get; set; }

        [Column("cedula_es")]
        public string CedulaEstudiante { get; set; }

        [Column("curso_id")]
        public int CursoId { get; set; }

        [Column("nota")]
        public decimal Valor { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        public Estudiante Estudiante { get; set; }
        public Curso Curso { get; set; }
    }
}
