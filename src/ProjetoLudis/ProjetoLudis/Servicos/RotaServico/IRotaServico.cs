using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Tabelas;

namespace ProjetoLudis.Servicos.RotaServico
{
    public interface IRotaServico
    {
        Task<List<Rota>> GetRotaAsync();
        
        Task<bool> CreateRotaAsync(Rota rota);

        Task<Rota> GetRotaByIdAsync(int rotaId);

        Task<bool> UpdateRotaAsync(Rota RotaToUpdate);
    }
}
