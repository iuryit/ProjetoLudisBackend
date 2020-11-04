using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Dtos
{
    public class UsuarioComerciante
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string RazaoSocial { get; set; }

        public string CPFCNPJ { get; set; }

        public string InscricaoEstadual { get; set; } = " ";
                
        public string InscricaoMunicipal { get; set; } = " ";

        public int IndicadorIE { get; set; } = 0;

        public int Regime { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string CEP { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }

        public string UF { get; set; }

        public string Token { get; set; }

    }
}
