using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Tabelas
{
    public class Comerciante
    {
        public Comerciante(){}

        public Comerciante(int id,
                           string razaoSocial,
                           string cpfCnpj,
                           int  regime)                            
        {
            this.Id = id;
            this.RazaoSocial = razaoSocial;
            this.CPFCNPJ = cpfCnpj;
            this.Regime = regime;

        }

        public int Id { get; set; }

        public string RazaoSocial { get; set; }

        public string CPFCNPJ { get; set; }

        public string InscricaoEstadual { get; set; } = " ";
                
        public string InscricaoMunicipal { get; set; } = " ";

        public int IndicadorIE { get; set; } = 0;

        public int Regime { get; set; }

        internal bool Any(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
