using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Repositories.Data.Configurations
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable(nameof(Curso));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.Property(p => p.Situacao)
                .HasConversion<int>()
                .HasDefaultValue(Status.Ativo);
        }
    }
}
