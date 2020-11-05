using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Dtos
{
    public class QuadraRegistrar
    {
        public string? Nome { get; set; }

        public string? Telefone { get; set; }

        public string? Endereco { get; set; }

        public string? CEP { get; set; }

        public string? Cidade { get; set; }

        public string? Bairro { get; set; }

        public string? Complemento { get; set; }

        public string? UF { get; set; }

        public int ComercianteId { get; set; }
    }
}
