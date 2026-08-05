using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Testes
{
    public class ParametrosSimulacaoTeste
    {
        private static TaxaDeJuros TaxaValida => TaxaDeJuros.Anual(10m);

        private static PeriodoDeInvestimento PeriodoValido => PeriodoDeInvestimento.DeMeses(12);

        [Fact]
        public void Construtor_DeveRecusarValorInicialNegativo()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new ParametrosSimulacao(-1m, 100m, TaxaValida, PeriodoValido));
        }

        [Fact]
        public void Construtor_DeveRecusarAporteMensalNegativo()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new ParametrosSimulacao(1000m, -1m, TaxaValida, PeriodoValido));
        }

        [Fact]
        public void Construtor_DeveRecusarTaxaNula()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ParametrosSimulacao(1000m, 100m, null!, PeriodoValido));
        }

        [Fact]
        public void Construtor_DeveRecusarPeriodoNulo()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ParametrosSimulacao(1000m, 100m, TaxaValida, null!));
        }

        [Fact]
        public void Construtor_DeveAceitarValoresZerados()
        {
            var parametros = new ParametrosSimulacao(decimal.Zero, decimal.Zero, TaxaValida, PeriodoValido);

            Assert.Equal(decimal.Zero, parametros.ValorInicial);
            Assert.Equal(decimal.Zero, parametros.AporteMensal);
        }
    }
}
