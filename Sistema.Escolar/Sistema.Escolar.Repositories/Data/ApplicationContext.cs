using Microsoft.EntityFrameworkCore;
using Sistema.Escolar.Models;

namespace Sistema.Escolar.Repositories.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<AlunoCurso> AlunosCursos { get; set; }
        public DbSet<CursoMateria> CursosMaterias { get; set; }
        public DbSet<AlunoMateria> AlunosMateria { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=SABRL9XNGD33\\SQLEXPRESS;Initial Catalog=SistemaEscolarDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=False",
                p => p.MigrationsHistoryTable("Sistema_Escolar_History"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
