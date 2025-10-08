using Domain.Dto;
using FluentValidation;

namespace Application.Helpers;

public class PessoaModelValidator : AbstractValidator<PessoaDto>
{
    public PessoaModelValidator()
    {
        const int idadeMaxima = 120;
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 50).WithMessage("O nome deve ter entre 2 e 50 caracteres.")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome deve conter apenas letras e espaços.");
        
        RuleFor(p => p.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter exatamente 11 caracteres.")
            .Must(ValidarCpf).WithMessage("O CPF informado é inválido.");

        RuleFor(p => p.Sexo)
            .Must(s => s == 'M' || s == 'F')
            .WithMessage("O sexo deve ser 'M' para masculino ou 'F' para feminino.");

        RuleFor(p => p.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .GreaterThanOrEqualTo(DateTime.Today.AddYears(-idadeMaxima))
            .WithMessage($"A idade máxima permitida é de {idadeMaxima} anos.")
            .LessThan(DateTime.Now).WithMessage("A data de nascimento não pode ser no futuro.");
    }

    private bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return false;
        }

        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
        {
            return false;
        }

        if (cpf.All(c => c == cpf[0]))
        {
            return false;
        }

        int soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(cpf[i].ToString()) * (10 - i);
        }

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[9].ToString()) != digito1)
        {
            return false;
        }

        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(cpf[i].ToString()) * (11 - i);
        }

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return int.Parse(cpf[10].ToString()) == digito2;
    }
}
