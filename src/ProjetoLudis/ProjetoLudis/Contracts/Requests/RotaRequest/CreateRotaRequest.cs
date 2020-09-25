using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Contracts.Requests.Rota
{
    public class CreateRotaRequest
    {
        public string origem { get; set; }
        public string destino { get; set; }
        public string pontos { get; set; }
    }
}
