using CarteiraFinanceira.Data;
using CarteiraFinanceira.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CarteiraFinanceira.Data.SaldoContext;

namespace CarteiraFinanceira.Repositories
{
    public class SaldoRepository : ISaldoRepository
    {
        private readonly ISaldoContext _context;

        public SaldoRepository(ISaldoContext context)
        {
            _context = context;
        }

        public async Task CreateSaldo(Saldo saldo)
        {
            await _context.Saldos.InsertOneAsync(saldo);
        }
        
        public void CreateSaldoSync(Saldo saldo)
        {
            _context.Saldos.InsertOneAsync(saldo);
        }

        public async Task<Saldo> GetSaldo()
        {
            return await _context.Saldos.Find(p => true).FirstOrDefaultAsync();
        }

        public Saldo GetSaldoSync()
        {
            return _context.Saldos.Find(p => true).FirstOrDefault();
        }
        
        public async Task<bool> UpdateSaldo(Saldo saldo)
        {
            var updateResult = await _context.Saldos.ReplaceOneAsync(
               filter: g => g.Id == saldo.Id, replacement: saldo);

            return updateResult.IsAcknowledged
              && updateResult.ModifiedCount > 0;
        }
    }
}
