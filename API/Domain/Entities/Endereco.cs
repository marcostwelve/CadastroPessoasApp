﻿namespace Domain.Entities
{
    public class Endereco
    {
        public int Id {get; set;}
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public int PessoaId { get; set; }
        public Pessoa pessoa { get; set; }
    }
}
