using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Util;

namespace Sistema.Escolar.BusinessRule.Interfaces
{
    public interface ICurso
    {
        Task<Result<Curso>> CadastraAsync(Curso curso);
        Task<Result<Curso>> AlteraSituacaoAsync(string curso, Status status);
        Task<Result<Curso>> VinculaMateriaAsync(string curso, string materia);
        Task<Result<Curso>> ExcluirAsync(string nome);
    }
}
