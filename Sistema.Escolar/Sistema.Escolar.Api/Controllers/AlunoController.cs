using Microsoft.AspNetCore.Mvc;
using Sistema.Escolar.BusinessRule.Interfaces;
using Sistema.Escolar.Models;

namespace Sistema.Escolar.Api.Controllers
{
    [ApiController]
    [Route("aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly IAluno _aluno;
        public AlunoController(IAluno aluno) => _aluno = aluno;

        [HttpPost]
        [Route("matricula")]
        public async Task<IActionResult> MatriculaAsync(Aluno aluno, string nomeCurso)
            => Ok(await _aluno.MatricularAsync(aluno, nomeCurso));

        [HttpPut]
        [Route("insereNota")]
        public async Task<IActionResult> InsereNotaAsync(string cpf, string nomeMateria, double nota)
            => Ok(await _aluno.InserirNotaAsync(cpf, nomeMateria, nota));

        [HttpPut]
        [Route("alteraNome")]
        public async Task<IActionResult> AlteraNomeAsync(string cpf, string novoNome)
            => Ok(await _aluno.AlterarNomeAsync(cpf, novoNome));

        [HttpPut]
        [Route("alteraSobrenome")]
        public async Task<IActionResult> AlterasobrenomeAsync(string cpf, string novoSobrenome)
            => Ok(await _aluno.AlterarSobrenomeAsync(cpf, novoSobrenome));

        [HttpDelete]
        [Route("exclui")]
        public async Task<IActionResult> ExcluiAsync(string nome)
            => Ok(await _aluno.ExcluirAsync(nome));
    }
}
