namespace NOTAS_APE.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Nota> Notas { get; set; }
    }
}
