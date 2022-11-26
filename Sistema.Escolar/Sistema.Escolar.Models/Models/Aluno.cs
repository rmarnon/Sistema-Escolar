namespace Sistema.Escolar.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<AlunoCurso> AlunoCursos { get; set; } = new List<AlunoCurso>();
        public ICollection<AlunoMateria> AlunoMaterias { get; set; } = new List<AlunoMateria>();
    }
}
