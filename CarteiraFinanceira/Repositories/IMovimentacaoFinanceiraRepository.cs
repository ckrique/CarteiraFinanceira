using CarteiraFinanceira.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarteiraFinanceira.Repositories
{
    public interface IMovimentacaoFinanceiraRepository
    {
        Task<IEnumerable<MovimentacaoFinanceira>> GetMovimentacoesFinanceiras();
        Task CreateMovimentacaoFinanceira(MovimentacaoFinanceira movimentacaoFinanceira);
    }
}
