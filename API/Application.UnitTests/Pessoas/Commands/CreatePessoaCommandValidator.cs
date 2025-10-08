using Application.Helpers;
using Domain.Dto;
using FluentValidation.TestHelper;
using System;
using Xunit;

// ATEN��O: Ajuste este namespace para o local onde seu validador est�
// using SeuProjeto.Application.Clientes.Commands; 

namespace Application.UnitTests.Clientes.Commands
{
    public class CreateClienteCommandValidatorTests
    {
        private readonly PessoaModelValidator _validator;

        public CreateClienteCommandValidatorTests()
        {
            _validator = new PessoaModelValidator();
        }

        [Fact]
        public void Should_Have_Error_When_DataNascimento_Is_In_The_Future()
        {
            var command = new PessoaDto
            {
                Nome = "Nome V�lido",
                Cpf = "12345678901",
                DataNascimento = DateTime.Today.AddDays(1)
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.DataNascimento);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new PessoaDto
            {
                Nome = "Nome V�lido",
                Cpf = "21281025020",
                DataNascimento = new DateTime(2000, 1, 1) 
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.DataNascimento);
        }
    }
}