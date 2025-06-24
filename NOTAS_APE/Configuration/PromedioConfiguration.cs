using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NOTAS_APE.Models;

namespace NOTAS_APE.Configuration
{
    public class PromedioConfiguration : IEntityTypeConfiguration<Promedio>
    {
        public void Configure(EntityTypeBuilder<Promedio> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CedulaEst)
                .HasColumnName("cedula_est")
                .IsRequired();

            builder.Property(p => p.ValorPromedio)
                .HasColumnName("promedio")
                .IsRequired();

            // Relación FK con Estudiante
            builder.HasOne(p => p.Estudiante)
                .WithMany() // Si Estudiante tiene colección, aquí pones .WithMany(e => e.Promedios)
                .HasForeignKey(p => p.CedulaEst)
                .HasConstraintName("FK_Promedios_Estudiantes")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
