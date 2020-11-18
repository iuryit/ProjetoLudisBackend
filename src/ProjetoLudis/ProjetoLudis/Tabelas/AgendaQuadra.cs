using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class AgendaQuadra
    {
        public AgendaQuadra() { }

        public AgendaQuadra(int id,
                            DateTime horaInicio,
                            DateTime horaFim,
                            int esporteId,
                            string descricao,
                            int quadraId,
                            int esportistaId)
        {
            this.Id = id;
            this.HoraInicio = horaInicio;
            this.HoraFim = horaFim;
            this.Descricao = descricao;
            this.EsporteId = esporteId;
            this.QuadraId = quadraId;
            this.EsportistaId = esportistaId;

        }

        public int Id { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim {get; set; }

        public int EsporteId { get; set; }

        public Esporte Esporte { get; set; }

        public string Descricao { get; set; }

        public int QuadraId { get; set; }

        public Quadra Quadra { get; set; }

        public int EsportistaId { get; set; }

        public Esportista Esportista { get; set; }
    }
}
