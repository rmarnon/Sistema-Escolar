using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Util;

namespace Sistema.Escolar.BusinessRule.Interfaces
{
    public interface IMateria
    {
        Task<Result<Materia>> CadastrarAsync(Materia materia);
        Task<Result<Materia>> AlteracoesAsync(string nome, string novoNome, string descricao);
        Task<Result<Materia>> AlteraStatusAsync(string nome, Status status);
        Task<Result<Materia>> ExcluirAsync(string nome);
    }
}
