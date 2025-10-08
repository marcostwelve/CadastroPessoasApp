using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public char? Sexo { get; set; }
    public string? Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? Naturalidade { get; set; }
    public string? Nacionalidade { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    public DateTime? DataAtualizacao { get; set; }
    public Endereco Endereco { get; set; }
}
