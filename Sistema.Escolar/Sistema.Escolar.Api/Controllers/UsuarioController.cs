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
        public async Task<IActionResult> ConectarAsync(string login, string senha)
            => Ok(await _usuario.ConectarAsync(login, senha));

        [HttpPost]
        [Route("cadastra")]
        public async Task<IActionResult> CadastrarAsync(Usuario user)
            => Ok(await _usuario.CadastrarAsync(user));

        [HttpDelete]
        [Route("deleta")]
        public async Task<IActionResult> DeletarAsync(string login, string senha)
            => Ok(await _usuario.ExcluirAsync(login, senha));

        [HttpPut]
        [Route("alteraSenha")]
        public async Task<IActionResult> AlteraSenhaAsync(string login, string senha, string novaSenha)
            => Ok(await _usuario.AlteraSenhaAsync(login, senha, novaSenha));

        [HttpPut]
        [Route("alteraLogin")]
        public async Task<IActionResult> AlteraLoginAsync(string login, string novoLogin, string senha)
            => Ok(await _usuario.AlterarLoginAsync(login, novoLogin, senha));
    }
}
