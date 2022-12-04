namespace CarteiraFinanceira.DTO
{
    public class SaldoDTO
    {
        public decimal valor { get; set; }
        
        public string GetDescricaoDoSaldo()
        {
            return string.Format("O saldo da Carteira é de {0}", valor.ToString("C"));
        }
    }
}
