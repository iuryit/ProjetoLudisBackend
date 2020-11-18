using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class QuadraEsportes
    {
        public QuadraEsportes() { }

        public QuadraEsportes(int id,
                              int esporteId,
                              int quadraId
                           )
        {
            this.Id = id;
            this.EsporteId = esporteId;
            this.QuadraId = quadraId;

        }

        public int Id { get; set; }

        public int EsporteId { get; set; }

        public Esporte Esporte { get; set; }

        public int QuadraId { get; set; }

        public Quadra Quadra { get; set; }
    }
}
