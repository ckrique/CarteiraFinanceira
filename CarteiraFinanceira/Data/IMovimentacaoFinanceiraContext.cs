using CarteiraFinanceira.Entities;
using MongoDB.Driver;

namespace CarteiraFinanceira.Data
{
    public interface IMovimentacaoFinanceiraContext
    {
        IMongoCollection<MovimentacaoFinanceira> MovimentacaoesFinanceiras { get; }
    }
}