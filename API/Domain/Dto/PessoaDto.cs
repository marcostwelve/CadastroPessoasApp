using System.ComponentModel.DataAnnotations;

namespace Domain.Dto;

public class PessoaDto
{
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Cpf { get; set; }

    public char? Sexo { get; set; }

    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string? Email { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }
    public string? Naturalidade { get; set; }
    public string? Nacionalidade { get; set; }
}
