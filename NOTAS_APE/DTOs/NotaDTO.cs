namespace NOTAS_APE.DTOs
{
    public class NotaDTO
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CursoId { get; set; }
        public string Curso { get; set; }
        public decimal Nota { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
