using CarteiraFinanceira.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CarteiraFinanceira.Data
{

    public class MovimentacaoFinanceiraContext : IMovimentacaoFinanceiraContext
    {
        public IMongoCollection<MovimentacaoFinanceira> MovimentacaoesFinanceiras { get; }

        public MovimentacaoFinanceiraContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>
               ("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>
               ("DatabaseSettings:DatabaseName"));

            MovimentacaoesFinanceiras = database.GetCollection<MovimentacaoFinanceira>(configuration.GetValue<string>
              ("DatabaseSettings:MovimentacaoFinanceiraCollectionName"));
        }        
    }
}
