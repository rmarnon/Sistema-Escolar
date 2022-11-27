using Microsoft.AspNetCore.Mvc;
using Sistema.Escolar.BusinessRule.Interfaces;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Api.Controllers
{
    [ApiController]
    [Route("materia")]
    public class MateriaController : ControllerBase
    {
        private readonly IMateria _materia;

        public MateriaController(IMateria materia) => _materia = materia;

        [HttpPost]
        [Route("cadastra")]
        public async Task<IActionResult> CadastraAsync(Materia materia)
            => Ok(await _materia.CadastrarAsync(materia));

        [HttpPut]
        [Route("alteracoes")]
        public async Task<IActionResult> AlteracoesAsync(string nome, string novoNome, string descricao)
            => Ok(await _materia.AlteracoesAsync(nome, novoNome, descricao));

        [HttpPut]
        [Route("alteraStatus")]
        public async Task<IActionResult> AlteraStatusAsync(string nome, Status status)
            => Ok(await _materia.AlteraStatusAsync(nome, status));

        [HttpDelete]
        [Route("deleta")]
        public async Task<IActionResult> ExcluiAsync(string nome)
            => Ok(await _materia.ExcluirAsync(nome));
    }
}
