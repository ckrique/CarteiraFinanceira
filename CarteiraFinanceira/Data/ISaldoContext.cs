using CarteiraFinanceira.Entities;
using MongoDB.Driver;

namespace CarteiraFinanceira.Data
{
    public interface ISaldoContext
    {
        IMongoCollection<Saldo> Saldos { get; }
    }
}