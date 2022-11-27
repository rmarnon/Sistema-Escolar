using FluentAssertions;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Validators.Validators;

namespace Sistema.Escolar.Tests.UnitTests
{
    [TestClass]
    public class MateriaTest
    {
        private static readonly string Materia = "TCC";
        private static readonly string Descricao = "TCC2 EAD 2022";

        [TestMethod]
        public void Testa_Materia_True()
        {
            //Arragne
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Status_Não_Ativo_Inativo()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Concluido
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Status_em_String()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Enum.Parse<Status>("Inativo")
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Status_Não_Informado()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01)
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Vazio()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = string.Empty,
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Inativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Null()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = null,
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Com_Mais_De_50_Caracteres()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = "resultNomeDaMateriaComMaisDeCinquentaCaracteresEmresult",
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Com_Espaços()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = "         ",
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Nome_Não_Informado()
        {
            //Arrange
            var materia = new Materia
            {
                Descricao = Descricao,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Inativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Descrição_Com_Numeros()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = "Turma EAD Unicesumar 2022",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Descrição_Com_Null()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = null,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Descrição_Empty()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = string.Empty,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Descrição_Não_Informada()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Data_Futura()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = Descricao,
                Cadastro = new DateTime(2050, 01, 01),
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Sem_Data()
        {
            //Arrange
            var materia = new Materia
            {
                Nome = Materia,
                Descricao = Descricao,
                Status = Status.Ativo
            };

            //Act
            var validation = new MateriaValidator();
            var result = validation.Validate(materia);

            //Arrange
            result.IsValid.Should().BeFalse();
        }
    }
}
