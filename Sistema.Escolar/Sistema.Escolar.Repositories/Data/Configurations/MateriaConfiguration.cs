using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Repositories.Data.Configurations
{
    public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable(nameof(Materia));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.Property(p => p.Cadastro)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Descricao)
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasConversion<int>()
                .HasDefaultValue(Status.Ativo);
        }
    }
}
