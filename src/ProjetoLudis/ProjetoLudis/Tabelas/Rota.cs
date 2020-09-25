using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class Rota
    {
        [Key]
        public int IdRota { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string Pontos { get; set; }
    }
}
