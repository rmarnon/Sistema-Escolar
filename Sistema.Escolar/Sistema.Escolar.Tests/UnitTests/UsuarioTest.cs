using FluentAssertions;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Validators.Validators;

namespace Sistema.Escolar.Tests.UnitTests
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Testa_Usuario_True()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "Teste user",
                Senha = "12345678910",
                Tipo = TipoUsuario.Administrador
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Login_Empty()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "",
                Senha = "12345678910",
                Tipo = TipoUsuario.Professor

            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Senha_Menos_Que_8_Caracteres()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "Teste < 8 caracteres",
                Senha = "1234567",
                Tipo = TipoUsuario.Administrador
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Login_Null()
        {
            //Arrange
            var user = new Usuario
            {
                Login = null,
                Senha = "1234567890",
                Tipo = TipoUsuario.Aluno
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Senha_Null()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "Teste senha null",
                Senha = null,
                Tipo = TipoUsuario.Professor
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Login_Mais50_Letras()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "TesteLoginDoUsuarioComMaisDeCinquentaCaracteresEmTeste",
                Senha = "12345678900"
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Tipo_Usuario_Igual_Tipo()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "Teste tipo aluno",
                Senha = "12345678900",
                Tipo = TipoUsuario.Aluno
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Testa_Login_Não_Informado()
        {
            //Arrange
            var user = new Usuario
            {
                Senha = "1234567890",
                Tipo = TipoUsuario.Aluno
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Tipo_Não_Informado()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "Teste user",
                Senha = "12345678910"
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Senha_Não_Informada()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "Teste de Login",
                Tipo = TipoUsuario.Administrador
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Testa_Senha_Mais50_Caracteres()
        {
            //Arrange
            var user = new Usuario
            {
                Login = "Teste Login",
                Senha = "TesteSenhaDoUsuarioComMaisDeCinquentaCaracteresEmTeste",
                Tipo = TipoUsuario.Professor
            };

            //Act
            var validation = new UsuarioValidator();
            var result = validation.Validate(user);

            //Assert
            result.IsValid.Should().BeFalse();
        }
    }
}
