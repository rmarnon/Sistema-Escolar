namespace Sistema.Escolar.Models
{
    public class AlunoCurso
    {
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }

        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
