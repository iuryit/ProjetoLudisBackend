using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class Esporte
    {
        public Esporte() { }

        public Esporte(int id,
                       string nome)
        {
            this.Id = id;
            this.Nome = nome;

        }

        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
