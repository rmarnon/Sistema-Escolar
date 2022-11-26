using Sistema_Escolar.Models.Enums;

namespace Sistema_Escolar.Models.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Cadastro { get; set; }
        public Status Status { get; set; }
        public ICollection<CursoMateria> MateriaCursos { get; set; } = new List<CursoMateria>();
        public ICollection<AlunoMateria> MateriaAlunos { get; set; } = new List<AlunoMateria>();
    }
}
