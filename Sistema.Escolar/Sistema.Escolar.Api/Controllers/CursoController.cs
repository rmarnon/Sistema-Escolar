using Microsoft.AspNetCore.Mvc;
using Sistema.Escolar.BusinessRule.Interfaces;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Api.Controllers
{
    [ApiController]
    [Route("curso")]
    public class CursoController : ControllerBase
    {
        private readonly ICurso _curso;
        public CursoController(ICurso curso) => _curso = curso;

        [HttpPost]
        [Route("cadastra")]
        public async Task<IActionResult> CadastraAsync(Curso curso)
            => Ok(await _curso.CadastraAsync(curso));

        [HttpPut]
        [Route("alteraSituacao")]
        public async Task<IActionResult> AlteraSituacaoAsync(string nome, Status situacao)
            => Ok(await _curso.AlteraSituacaoAsync(nome, situacao));

        [HttpPut]
        [Route("vinculaMateria")]
        public async Task<IActionResult> VinculaMateria(string nomeCurso, string nomeMateria)
            => Ok(await _curso.VinculaMateriaAsync(nomeCurso, nomeMateria));

        [HttpDelete]
        [Route("deleta")]
        public async Task<IActionResult> ExcluirAsync(string nome)
            => Ok(await _curso.ExcluirAsync(nome));
    }
}
