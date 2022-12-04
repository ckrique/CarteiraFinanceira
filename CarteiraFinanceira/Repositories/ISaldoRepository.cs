using CarteiraFinanceira.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarteiraFinanceira.Repositories
{
    public interface ISaldoRepository
    {
        public Task<Saldo> GetSaldo();
        public Saldo GetSaldoSync();
        public Task CreateSaldo(Saldo saldo);
        public void CreateSaldoSync(Saldo saldo);
        public Task<bool> UpdateSaldo(Saldo saldo);
    }
}
