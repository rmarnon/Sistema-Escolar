using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Status Situacao { get; set; }
        public ICollection<AlunoCurso> CursoAlunos { get; set; } = new List<AlunoCurso>();
        public ICollection<CursoMateria> CursoMaterias { get; set; } = new List<CursoMateria>();
    }
}
