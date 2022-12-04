using CarteiraFinanceira.Entities;
using CarteiraFinanceira.Repositories;
using CarteiraFinanceira.Service;
using MongoDB.Driver;

namespace CarteiraFinanceira.Business
{
    public class CarteiraFinanceiraService : ICarteiraFinanceiraService
    {
        //SIGLAS ISO 4217
        public const string TIPO_MOVIMENTACAO_ENTRADA = "ENTRADA";
        public const string TIPO_MOVIMENTACAO_SAIDA = "SAIDA";
                
        private readonly ISaldoRepository _saldoRepository;

        public CarteiraFinanceiraService(ISaldoRepository saldoRepository)
        {            
            _saldoRepository = saldoRepository;

            InicializaBancoDeDados();
        }
        
        public void InicializaSaldoNoBancoDeDados()
        {
            Saldo saldoExistente = _saldoRepository.GetSaldoSync();

            if (saldoExistente is null)
            {
                Saldo saldoNaCarteira = new Saldo();

                saldoNaCarteira.valor = 0;

                _saldoRepository.CreateSaldoSync(saldoNaCarteira);
            }
        }

        public void InicializaBancoDeDados()
        {
            InicializaSaldoNoBancoDeDados();
        }
    }
}