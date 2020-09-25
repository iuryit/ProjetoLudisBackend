using Microsoft.EntityFrameworkCore;
using ProjetoLudis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Tabelas;

namespace ProjetoLudis.Servicos.RotaServico
{
    public class RotaServico : IRotaServico
    {
        private readonly Context _BancoDeDados;

        public RotaServico(Context bancoDeDados)
        {
            _BancoDeDados = bancoDeDados;
        }

        public async Task<bool> CreateRotaAsync(Rota rota)
        {
            await _BancoDeDados.RotaDb.AddAsync(rota);
            var created = await _BancoDeDados.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<Rota>> GetRotaAsync()
        {
            return await _BancoDeDados.RotaDb.ToListAsync();
        }

        public async Task<Rota> GetRotaByIdAsync(int rotaId)
        {
            return await _BancoDeDados.RotaDb.SingleOrDefaultAsync(predicate: x => x.IdRota == rotaId);
        }

        public async Task<bool> UpdateRotaAsync(Rota RotaToUpdate)
        {
            _BancoDeDados.RotaDb.Update(RotaToUpdate);
            var updated = await _BancoDeDados.SaveChangesAsync();
            return updated > 0;
        }
    }
}
