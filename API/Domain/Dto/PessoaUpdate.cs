using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class PessoaUpdate
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        public char? Sexo { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }
        public string? Naturalidade { get; set; }
        public string? Nacionalidade { get; set; }
    }
}
