using Sistema.Escolar.Models;
using Sistema.Escolar.Util;

namespace Sistema.Escolar.BusinessRule.Interfaces
{
    public interface IUsuario
    {
        Task<Result<Usuario>> ConectarAsync(string login, string senha);
        Task<Result<Usuario>> CadastrarAsync(Usuario usuario);
        Task<Result<Usuario>> ExcluirAsync(string login, string senha);
        Task<Result<Usuario>> AlterarLoginAsync(string login, string novoLogin, string senha);
        Task<Result<Usuario>> AlteraSenhaAsync(string login, string senha, string novaSenha);
    }
}
