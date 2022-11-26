using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Escolar.Models;

namespace Sistema.Escolar.Repositories.Data.Configurations
{
    public class AlunoMateriaConfiguration : IEntityTypeConfiguration<AlunoMateria>
    {
        public void Configure(EntityTypeBuilder<AlunoMateria> builder)
        {
            builder.ToTable(nameof(AlunoMateria));

            builder.HasKey(p => new
            {
                p.AlunoId,
                p.MateriaId
            });
        }
    }
}
