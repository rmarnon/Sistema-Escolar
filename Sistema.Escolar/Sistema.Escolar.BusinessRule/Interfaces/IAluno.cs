using Sistema.Escolar.Models;
using Sistema.Escolar.Util;

namespace Sistema.Escolar.BusinessRule.Interfaces
{
    public interface IAluno
    {
        Task<Result<Aluno>> MatricularAsync(Aluno aluno, string curso);
        Task<Result<Aluno>> InserirNotaAsync(string cpf, string materia, double nota);
        Task<Result<Aluno>> AlterarNomeAsync(string cpf, string novoNome);
        Task<Result<Aluno>> AlterarSobrenomeAsync(string cpf, string novoSobrenome);
        Task<Result<Aluno>> ExcluirAsync(string cpf);
    }
}
