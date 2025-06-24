using NOTAS_APE.Models;
using Microsoft.EntityFrameworkCore;
using NOTAS_APE.Configuration;

namespace NOTAS_APE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Promedio> Promedios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EstudianteConfiguration());
            // Agrega CursoConfiguration si la tienes
            modelBuilder.ApplyConfiguration(new PromedioConfiguration());
        }

    }
}
