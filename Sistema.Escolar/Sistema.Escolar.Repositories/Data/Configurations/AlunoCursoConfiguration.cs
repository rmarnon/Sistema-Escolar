using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Escolar.Models;

namespace Sistema.Escolar.Repositories.Data.Configurations
{
    public class AlunoCursoConfiguration : IEntityTypeConfiguration<AlunoCurso>
    {
        public void Configure(EntityTypeBuilder<AlunoCurso> builder)
        {
            builder.ToTable(nameof(AlunoCurso));

            builder.HasKey(p => new
            {
                p.AlunoId,
                p.CursoId
            });
        }
    }
}
