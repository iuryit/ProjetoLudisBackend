using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Data;
using ProjetoLudis.Tabelas;

namespace ProjetoLudis.Servicos.RotaPontoServico
{
    public class RotaPontoServico : IRotaPontoServico
    {
        private readonly Context _BancoDeDados;

        public RotaPontoServico(Context bancoDeDados)
        {
            _BancoDeDados = bancoDeDados;
        }

        public async Task<bool> CreateRotaPontoAsync(RotaPonto rotaPonto)
        {
            await _BancoDeDados.RotaPontoDb.AddAsync(rotaPonto);
            var created = await _BancoDeDados.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<RotaPonto>> GetRotaPontoAsync()
        {
            return await _BancoDeDados.RotaPontoDb.ToListAsync();
        }

        public async Task<RotaPonto> GetRotaPontoByIdAsync(int rotaPontoId)
        {
            return await _BancoDeDados.RotaPontoDb.SingleOrDefaultAsync(predicate: x => x.IdRotaPonto == rotaPontoId);
        }

        public async Task<bool> UpdateRotaPontoAsync(RotaPonto rotaPontoToUpdate)
        {
            _BancoDeDados.RotaPontoDb.Update(rotaPontoToUpdate);
            var updated = await _BancoDeDados.SaveChangesAsync();
            return updated > 0;
        }
    }
}
