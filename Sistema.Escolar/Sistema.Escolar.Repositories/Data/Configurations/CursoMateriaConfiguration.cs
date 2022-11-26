using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Escolar.Models;

namespace Sistema.Escolar.Repositories.Data.Configurations
{
    public class CursoMateriaConfiguration : IEntityTypeConfiguration<CursoMateria>
    {
        public void Configure(EntityTypeBuilder<CursoMateria> builder)
        {
            builder.ToTable(nameof(CursoMateria));

            builder.HasKey(p => new
            {
                p.CursoId,
                p.MateriaId
            });
        }
    }
}
