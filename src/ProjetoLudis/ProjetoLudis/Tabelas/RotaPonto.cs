using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class RotaPonto
    {
        [Key]
        public int IdRotaPonto { get; set; }
        public string Descricao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CategoriaPonto { get; set; }
    }
}
