using CarteiraFinanceira.DTO;
using CarteiraFinanceira.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarteiraFinanceira.Repositories;
using CarteiraFinanceira.Business;
using CarteiraFinanceira.Service;

namespace CarteiraFinanceira.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarteiraFinanceiraController : Controller
    {
        private readonly IMovimentacaoFinanceiraRepository _movimentacaoFinanceiraRepository;
        private readonly ISaldoRepository _saldoRepository;
        private readonly ICarteiraFinanceiraService _carteiraFinanceiraService;

        public CarteiraFinanceiraController(IMovimentacaoFinanceiraRepository MovimentacaoFinanceiraRepository, 
                                        ISaldoRepository saldoRepository,
                                        ICarteiraFinanceiraService carteiraFinanceiraService) 
        {
            _movimentacaoFinanceiraRepository = MovimentacaoFinanceiraRepository;
            _saldoRepository = saldoRepository;
            _carteiraFinanceiraService = carteiraFinanceiraService;            
        }


        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ReceberCompraDeMoeda")]        
        public ActionResult ColocarDinheiro([FromBody] MovimentacaoFinanceiraDTO movimentacaoFinanceiraDTO)
        {
            if (movimentacaoFinanceiraDTO.valor <= 0)
                return BadRequest("Operação Incorreta, verifique os valor enviado");
                        
            MovimentacaoFinanceira movimentacaoFinanceira = new MovimentacaoFinanceira();
                        
            movimentacaoFinanceira.valor = movimentacaoFinanceiraDTO.valor;
            movimentacaoFinanceira.descricaoFinalidade = movimentacaoFinanceiraDTO.descricaoDeFinalidade;
            movimentacaoFinanceira.tipo = CarteiraFinanceiraService.TIPO_MOVIMENTACAO_ENTRADA;
            movimentacaoFinanceira.dataCriacao = DateTime.Now;

            _movimentacaoFinanceiraRepository.CreateMovimentacaoFinanceira(movimentacaoFinanceira);

            Saldo saldo = _saldoRepository.GetSaldoSync();

            _movimentacaoFinanceiraRepository.CreateMovimentacaoFinanceira(movimentacaoFinanceira);

            saldo.valor = saldo.valor + movimentacaoFinanceiraDTO.valor;

            _saldoRepository.UpdateSaldo(saldo);

            return Ok();
        }

        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("RealizarVendaDeMoeda")]
        public ActionResult<MovimentacaoFinanceira> RetirarDinheiro([FromBody] MovimentacaoFinanceiraDTO movimentacaoFinanceiraDTO)
        {
            if (movimentacaoFinanceiraDTO.valor <= 0)
                return BadRequest("Operação Incorreta, verifique o valor enviado");
            
            Saldo saldo = _saldoRepository.GetSaldoSync();

            if (saldo.valor < movimentacaoFinanceiraDTO.valor)
                return BadRequest("Não há saldo suficiente na carteira para realizar esta retirada.");

            MovimentacaoFinanceira movimentacaoFinanceira = new MovimentacaoFinanceira();

            movimentacaoFinanceira.valor = movimentacaoFinanceiraDTO.valor;
            movimentacaoFinanceira.descricaoFinalidade = movimentacaoFinanceiraDTO.descricaoDeFinalidade;
            movimentacaoFinanceira.tipo = CarteiraFinanceiraService.TIPO_MOVIMENTACAO_ENTRADA;

            movimentacaoFinanceira.dataCriacao = DateTime.Now;

            _movimentacaoFinanceiraRepository.CreateMovimentacaoFinanceira(movimentacaoFinanceira);            

            saldo.valor = saldo.valor - movimentacaoFinanceiraDTO.valor;

            _saldoRepository.UpdateSaldo(saldo);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Route("ListarSaldo")]        
        public async Task<ActionResult<IEnumerable<SaldoDTO>>> ListarSaldo()
        {
            List<SaldoDTO> listaDeSaldoDTO = new List<SaldoDTO>();

            Saldo saldo = _saldoRepository.GetSaldoSync();

            SaldoDTO saldoDTO = new SaldoDTO();
            saldoDTO.valor = saldo.valor;                        

            return Ok(saldoDTO.GetDescricaoDoSaldo());
        }

    }
}
