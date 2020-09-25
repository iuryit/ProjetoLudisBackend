using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Tabelas;

namespace ProjetoLudis.Servicos.RotaPontoServico
{
    public interface IRotaPontoServico
    {
        Task<List<RotaPonto>> GetRotaPontoAsync();

        Task<bool> CreateRotaPontoAsync(RotaPonto rotaPonto);

        Task<RotaPonto> GetRotaPontoByIdAsync(int rotaPontoId);

        Task<bool> UpdateRotaPontoAsync(RotaPonto rotaPontoToUpdate);
    }
}
