using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Escolar.Models;

namespace Sistema.Escolar.Repositories.Data.Configurations
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable(nameof(Aluno));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Sobrenome)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Nascimento)
                .IsRequired();

            builder.Property(p => p.Cpf)
                .IsRequired();
        }
    }
}
