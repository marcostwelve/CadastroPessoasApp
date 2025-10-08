using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class PessoaDtoV2 : PessoaDto
    {
        public EnderecoDto Endereco { get; set; }
    }
}
