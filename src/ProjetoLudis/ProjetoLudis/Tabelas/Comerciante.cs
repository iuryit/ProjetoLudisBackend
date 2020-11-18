using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class Comerciante
    {
        public Comerciante(){}

        public Comerciante(int id,
                           string razaoSocial,
                           string cpfCnpj,
                           int  regime,
                           string nome,
                           string telefone,
                           string cep,
                           string cidade,
                           string endereco,
                           string numero,
                           string bairro,
                           string uf)                            
        {
            this.Id = id;
            this.RazaoSocial = razaoSocial;
            this.CPFCNPJ = cpfCnpj;
            this.Regime = regime;
            this.Nome = nome;
            this.Telefone = telefone;
            this.CEP = cep;
            this.Cidade = cidade;
            this.Endereco = endereco;
            this.Endereco = endereco;
            this.Numero = numero;
            this.Bairro = bairro;
            this.UF = uf;

        }

        public int Id { get; set; }

        public string? Nome { get; set; }

        public string RazaoSocial { get; set; }

        public string CPFCNPJ { get; set; }

        public string InscricaoEstadual { get; set; } = " ";
                
        public string InscricaoMunicipal { get; set; } = " ";

        public int IndicadorIE { get; set; } = 0;

        public int Regime { get; set; }

        public string? Telefone { get; set; }

        public string? Endereco { get; set; }

        public string? Numero { get; set; }

        public string? CEP { get; set; }

        public string? Cidade { get; set; }

        public string? Bairro { get; set; }

        public string? Complemento { get; set; }

        public string? UF { get; set; }

        public IEnumerable<Quadra> Quadras { get; set; }

        internal bool Any(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
