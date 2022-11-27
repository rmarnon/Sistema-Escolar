using Microsoft.AspNetCore.Mvc;
using Sistema.Escolar.BusinessRule.Interfaces;
using Sistema.Escolar.Models;

namespace Sistema.Escolar.Api.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUser _usuario;

        public UsuarioController(IUser user) => _usuario = user;

        [HttpPost]
        [Route("conecta")]
        public ActionResult Conectar(string login, string senha)
            => Ok(_usuario.ConectarAsync(login, senha));

        [HttpPost]
        [Route("cadastra")]
        public ActionResult Cadastrar(Usuario user)
            => Ok(_usuario.CadastrarAsync(user));

        [HttpDelete]
        [Route("deleta")]
        public ActionResult Deletar(string login, string senha)
            => Ok(_usuario.ExcluirAsync(login, senha));

        [HttpPut]
        [Route("alteraSenha")]
        public ActionResult AlteraSenha(string login, string senha, string novaSenha)
            => Ok(_usuario.AlteraSenhaAsync(login, senha, novaSenha));

        [HttpPut]
        [Route("alteraLogin")]
        public ActionResult AlteraLogin(string login, string novoLogin, string senha)
            => Ok(_usuario.AlterarLoginAsync(login, novoLogin, senha));
    }
}
