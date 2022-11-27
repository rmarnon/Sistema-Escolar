using FluentAssertions;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Validators.Validators;

namespace Sistema.Escolar.Tests.UnitTests
{
    [TestClass]
    public class CursoTest
    {
        private static readonly string Curso = "Engeharia de Software";

        [TestMethod]
        public void Testa_Curso_Ativo_True()
        {
            //Arrange
            var curso = new Curso
            {
                Nome = Curso,
                Situacao = Status.Ativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Curso_Inativo_False()
        {
            //Arrange
            var curso = new Curso
            {
                Nome = Curso,
                Situacao = Status.Inativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Sem_Situação()
        {
            //Assert
            var curso = new Curso
            {
                Nome = Curso
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Nome_Empty()
        {
            //Arrange
            var curso = new Curso
            {
                Nome = string.Empty,
                Situacao = Status.Ativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Nome_Null()
        {
            //Arrange
            var curso = new Curso
            {
                Nome = null,
                Situacao = Status.Ativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Sem_Nome()
        {
            //Arrange
            var curso = new Curso
            {
                Situacao = Status.Ativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Com_Números()
        {
            //Arragne
            var curso = new Curso
            {
                Nome = "0123456789",
                Situacao = Status.Ativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Com_Muitas_Letras()
        {
            //Arrange
            var curso = new Curso
            {
                Nome = "resultNomeDoCursoComMaisDeCinquentaCaracteresEmresult",
                Situacao = Status.Ativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Arrange
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Com_02_Letras()
        {
            //Arrange
            var curso = new Curso
            {
                Nome = "Ze",
                Situacao = Status.Ativo
            };

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Curso_Com_Materia_Ativa()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = "result materia",
                Cadastro = DateTime.Now,
                Descricao = "result descrição",
                Status = Status.Ativo
            };

            var curso = new Curso
            {
                Nome = "result Padawan",
                Situacao = Status.Ativo
            };

            curso.CursoMaterias.Add(new CursoMateria
            {
                Curso = curso,
                Materia = materia
            });

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Curso_Com_Materia_Inativa()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = "result materia",
                Cadastro = DateTime.Now,
                Descricao = "result descrição",
                Status = Status.Inativo
            };

            var curso = new Curso
            {
                Nome = "result Padawan",
                Situacao = Status.Ativo
            };

            curso.CursoMaterias.Add(new CursoMateria
            {
                Curso = curso,
                Materia = materia
            });

            //Act
            var validations = new CursoValidator();
            var result = validations.Validate(curso);

            //Assert
            result.IsValid.Should().BeFalse();
        }
    }
}
