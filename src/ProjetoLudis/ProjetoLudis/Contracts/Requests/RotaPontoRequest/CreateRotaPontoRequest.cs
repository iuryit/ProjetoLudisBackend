using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Contracts.Requests.RotaPontoRequest
{
    public class CreateRotaPontoRequest
    {
        public string descricao { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string categoriaPonto { get; set; }
    }
}
