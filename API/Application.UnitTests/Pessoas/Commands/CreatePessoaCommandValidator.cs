using Application.Helpers;
using Domain.Dto;
using FluentValidation.TestHelper;
using System;
using Xunit;

// ATENÇÃO: Ajuste este namespace para o local onde seu validador está
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
                Nome = "Nome Válido",
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
                Nome = "Nome Válido",
                Cpf = "21281025020",
                DataNascimento = new DateTime(2000, 1, 1) 
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.DataNascimento);
        }
    }
}