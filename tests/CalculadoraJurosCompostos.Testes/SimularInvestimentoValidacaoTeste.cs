using CalculadoraJurosCompostos.Aplicacao;
using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Testes
{
    public class SimularInvestimentoValidacaoTeste
    {
        private readonly ISimularInvestimento _casoDeUso =
            new SimularInvestimento(new SimuladorDeJurosCompostos());

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Executar_DeveRecusarPeriodoNaoPositivo(int periodo)
        {
            var request = new SimulacaoRequest(1000m, 100m, 12m, periodo, TipoPeriodo.Meses);

            SimulacaoInvalidaException erro =
                Assert.Throws<SimulacaoInvalidaException>(() => _casoDeUso.Executar(request));

            Assert.Equal("Informe um período maior que zero.", erro.Message);
        }

        [Fact]
        public void Executar_DeveRecusarSimulacaoSemValorInicialESemAporte()
        {
            var request = new SimulacaoRequest(decimal.Zero, decimal.Zero, 12m, 12, TipoPeriodo.Meses);

            SimulacaoInvalidaException erro =
                Assert.Throws<SimulacaoInvalidaException>(() => _casoDeUso.Executar(request));

            Assert.Equal("Informe um valor inicial ou um aporte mensal.", erro.Message);
        }

        [Fact]
        public void Executar_DeveAceitarSimulacaoApenasComAporte()
        {
            var request = new SimulacaoRequest(decimal.Zero, 100m, 12m, 12, TipoPeriodo.Meses);

            SimulacaoResponse resposta = _casoDeUso.Executar(request);

            Assert.Equal(12, resposta.Evolucao.Count);
            Assert.Equal(1200m, resposta.TotalInvestido);
        }

        [Fact]
        public void Executar_DeveAceitarSimulacaoApenasComValorInicial()
        {
            var request = new SimulacaoRequest(1000m, decimal.Zero, 12m, 12, TipoPeriodo.Meses);

            SimulacaoResponse resposta = _casoDeUso.Executar(request);

            Assert.Equal(1120.00m, resposta.TotalAcumulado);
        }

        /// <summary>
        /// Taxa zero é entrada legítima: simula o cenário sem rendimento.
        /// </summary>
        [Fact]
        public void Executar_DeveAceitarTaxaZero()
        {
            var request = new SimulacaoRequest(1000m, 100m, decimal.Zero, 12, TipoPeriodo.Meses);

            SimulacaoResponse resposta = _casoDeUso.Executar(request);

            Assert.Equal(decimal.Zero, resposta.TotalJuros);
        }
    }
}
