using FluentAssertions;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Validators.Validators;

namespace Sistema.Escolar.Tests.UnitTests
{
    [TestClass]
    public class AlunoTest
    {
        [TestMethod]
        public void Testa_Aluno_True()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste Nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Nome_Null()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = null,
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Vazio()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = string.Empty,
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Com_Numeros()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "999999999",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Com_02_Letras()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Te",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Com_Muitas_Letras()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "TestNomeComMaisDeVinteLetras",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Sobrenome_Com_Muitas_Letras()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste",
                Sobrenome = "TestSobrenomeComMaisDeVinteLetras",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Não_Informado()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Sobrenomeome_Não_Informado()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nascimento_Não_Informado()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste Nome",
                Sobrenome = "Teste Sobrenome",
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nascimento_Maior_01_01_2002()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2002, 01, 02),
                Cpf = "01234567890"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_CPF_Com_Letras()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "A5477C13187"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_CPF_Com_Muitos_Numeros()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "1234567891011"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_CPF_Com_Menos_Numeros()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "012345"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_CPF_vazio()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = string.Empty
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_CPF_Null()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = null
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_CPF_Não_Informado()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01)
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_CPF_No_Formato_Padrao()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "012.345.678-90"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_CPF_No_Formato_Invalido()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "054*773 136/17"
            };

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Matricula_Curso_Ativo()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var curso = new Curso
            {
                Nome = "Teste matricula",
                Situacao = Status.Ativo
            };

            aluno.AlunoCursos.Add(new AlunoCurso
            {
                Aluno = aluno,
                Curso = curso
            });

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Matricula_Curso_Inativo()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var curso = new Curso
            {
                Nome = "Teste matricula",
                Situacao = Status.Inativo
            };

            aluno.AlunoCursos.Add(new AlunoCurso
            {
                Aluno = aluno,
                Curso = curso
            });

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Maxima()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia,
                Nota = 100.0
            });

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Zero()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia,
                Nota = 0.0
            });

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Negativa()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia,
                Nota = -1.0
            });

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Maior_Que_100()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia,
                Nota = 101.0
            });

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nota_Não_Informada_Padrao_Zero()
        {
            //Arrange
            var validation = new AlunoValidator();
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia
            });

            //Act
            var result = validation.Validate(aluno);

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
