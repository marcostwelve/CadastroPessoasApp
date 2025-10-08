namespace Application.Helpers;

public class PessoaModelErrorsMessages
{
    public static string NomeRequired = "O nome é obrigatório.";
    public static string NomeMinLength = "O nome deve ter no mínimo 2 caracteres.";
    public static string NomeMaxLength = "O nome deve ter no máximo 50 caracteres.";

    public static string CpfRequired = "O CPF é obrigatório.";
    public static string CpfLength = "O CPF deve ter exatamente 11 caracteres.";

    public static string DataNascimentoRequired = "A data de nascimento é obrigatória.";
}
