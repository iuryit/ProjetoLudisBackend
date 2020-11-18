using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Dtos
{
    public class UsuarioEsportista
    {       

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string CEP { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }

        public string UF { get; set; }

        public string Token { get; set; }
    }
}
