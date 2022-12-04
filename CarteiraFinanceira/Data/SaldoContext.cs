using CarteiraFinanceira.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CarteiraFinanceira.Data
{

    public class SaldoContext : ISaldoContext
    {
        public IMongoCollection<Saldo> Saldos { get; }


        public SaldoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>
               ("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>
               ("DatabaseSettings:DatabaseName"));

            Saldos = database.GetCollection<Saldo>(configuration.GetValue<string>
              ("DatabaseSettings:SaldosCollectionName"));
        }
        
    }

}
