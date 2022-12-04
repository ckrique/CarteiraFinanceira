using CarteiraFinanceira.Data;
using CarteiraFinanceira.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CarteiraFinanceira.Data.MovimentacaoFinanceiraContext;

namespace CarteiraFinanceira.Repositories
{
    public class MovimentacaoFinanceiraRepository : IMovimentacaoFinanceiraRepository
    {
        private readonly IMovimentacaoFinanceiraContext _context;

        public MovimentacaoFinanceiraRepository(IMovimentacaoFinanceiraContext context)
        {
            _context = context;
        }

        public async Task CreateMovimentacaoFinanceira(MovimentacaoFinanceira movimentacaoFinanceira)
        {
            await _context.MovimentacaoesFinanceiras.InsertOneAsync(movimentacaoFinanceira);
        }

        public async Task<IEnumerable<MovimentacaoFinanceira>> GetMovimentacoesFinanceiras()
        {
            return await _context.MovimentacaoesFinanceiras.Find(p => true).ToListAsync();
        }
    }
}
