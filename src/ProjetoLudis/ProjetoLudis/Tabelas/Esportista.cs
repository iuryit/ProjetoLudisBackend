using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class Esportista
    {
        public Esportista(){}

        public Esportista(int id,
                          string cpf,
                          string nome,
                          string telefone,
                          string cep,
                          string cidade,
                          string endereco,
                          string bairro,
                          string uf)
        {
            this.Id = id;
            this.CPF = cpf;
            this.Nome = nome;
            this.Telefone = telefone;
            this.CEP = cep;
            this.Cidade = cidade;
            this.Endereco = endereco;
            this.Bairro = bairro;
            this.UF = uf;
        }

        public int Id { get; set; }

        public string? Nome { get; set; }

        public string CPF { get; set; }

        public string? Telefone { get; set; }

        public string? Endereco { get; set; }

        public string? CEP { get; set; }

        public string? Cidade { get; set; }

        public string? Bairro { get; set; }

        public string? Complemento { get; set; }

        public string? UF { get; set; }

    }
}
