using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Dtos
{
    public class QuadraAgendar
    {

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }

        public int EsporteId { get; set; }

        public string Descricao { get; set; }

        public int QuadraId { get; set; }


    }
}
