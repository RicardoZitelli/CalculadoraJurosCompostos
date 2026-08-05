using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Testes
{
    public class SimuladorDeJurosCompostosTeste
    {
        private readonly SimuladorDeJurosCompostos _simulador = new SimuladorDeJurosCompostos();

        private static ParametrosSimulacao Parametros(
            decimal valorInicial,
            decimal aporteMensal,
            decimal taxaAnual,
            PeriodoDeInvestimento periodo) =>
            new ParametrosSimulacao(valorInicial, aporteMensal, TaxaDeJuros.Anual(taxaAnual), periodo);

        /// <summary>
        /// O teste que dá sentido à conversão composta da taxa: sem aportes, 12 meses a
        /// 12% ao ano têm que render exatamente os 12% contratados. Com a divisão linear
        /// anterior (12/12 = 1% ao mês) o resultado era R$ 1.126,83.
        /// </summary>
        [Fact]
        public void Simular_SemAportes_DeveRenderExatamenteATaxaAnualEmDozeMeses()
        {
            ResultadoSimulacao resultado = _simulador.Simular(
                Parametros(1000m, decimal.Zero, 12m, PeriodoDeInvestimento.DeMeses(12)));

            Assert.Equal(1120.00m, resultado.TotalAcumulado);
            Assert.Equal(1000m, resultado.TotalInvestido);
            Assert.Equal(120.00m, resultado.TotalJuros);
        }

        /// <summary>
        /// O aporte é postecipado: entra no fim do mês e só rende a partir do mês seguinte.
        /// Se os juros incidissem depois do aporte, o primeiro mês renderia 10,44.
        /// </summary>
        [Fact]
        public void Simular_DeveCobrarJurosSobreOSaldoAnteriorAoAporteDoMes()
        {
            ResultadoSimulacao resultado = _simulador.Simular(
                Parametros(1000m, 100m, 12m, PeriodoDeInvestimento.DeMeses(1)));

            EvolucaoMensal primeiroMes = Assert.Single(resultado.Evolucao);

            Assert.Equal(9.49m, primeiroMes.Juros);
            Assert.Equal(1100m, primeiroMes.TotalInvestido);
            Assert.Equal(1109.49m, primeiroMes.TotalAcumulado);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(11, 1, 11)]
        [InlineData(12, 1, 12)]
        [InlineData(13, 2, 1)]
        [InlineData(24, 2, 12)]
        [InlineData(25, 3, 1)]
        [InlineData(30, 3, 6)]
        public void Simular_DeveMapearOIndiceDoMesEmAnoEMes(int mes, int anoEsperado, int mesEsperado)
        {
            ResultadoSimulacao resultado = _simulador.Simular(
                Parametros(1000m, 100m, 10m, PeriodoDeInvestimento.DeMeses(mes)));

            EvolucaoMensal ultimo = resultado.Evolucao[^1];

            Assert.Equal(anoEsperado, ultimo.Ano);
            Assert.Equal(mesEsperado, ultimo.Mes);
        }

        [Fact]
        public void Simular_DeveProduzirOMesmoResultado_ParaPeriodoEmAnosOuNoEquivalenteEmMeses()
        {
            ResultadoSimulacao porAnos = _simulador.Simular(
                Parametros(5000m, 250m, 8m, PeriodoDeInvestimento.DeAnos(2)));

            ResultadoSimulacao porMeses = _simulador.Simular(
                Parametros(5000m, 250m, 8m, PeriodoDeInvestimento.DeMeses(24)));

            Assert.Equal(porAnos.Evolucao, porMeses.Evolucao);
        }

        [Fact]
        public void Simular_ComTaxaZero_DeveAcumularApenasOsAportes()
        {
            ResultadoSimulacao resultado = _simulador.Simular(
                Parametros(1000m, 100m, decimal.Zero, PeriodoDeInvestimento.DeMeses(12)));

            Assert.Equal(decimal.Zero, resultado.TotalJuros);
            Assert.Equal(2200m, resultado.TotalInvestido);
            Assert.Equal(2200m, resultado.TotalAcumulado);
        }

        /// <summary>
        /// Período zero devolve simulação sem meses, sem estourar. É o que acontece quando
        /// o campo de período fica vazio na tela.
        /// </summary>
        [Fact]
        public void Simular_ComPeriodoZero_DeveDevolverEvolucaoVaziaPreservandoOValorInicial()
        {
            ResultadoSimulacao resultado = _simulador.Simular(
                Parametros(1000m, 100m, 12m, PeriodoDeInvestimento.DeMeses(0)));

            Assert.Empty(resultado.Evolucao);
            Assert.Equal(1000m, resultado.TotalInvestido);
            Assert.Equal(1000m, resultado.TotalAcumulado);
            Assert.Equal(decimal.Zero, resultado.TotalJuros);
        }

        [Fact]
        public void Simular_DeveGerarUmaLinhaPorMes()
        {
            ResultadoSimulacao resultado = _simulador.Simular(
                Parametros(1000m, 100m, 10m, PeriodoDeInvestimento.DeAnos(3)));

            Assert.Equal(36, resultado.Evolucao.Count);
        }

        [Fact]
        public void Simular_DeveRecusarParametrosNulos()
        {
            Assert.Throws<ArgumentNullException>(() => _simulador.Simular(null!));
        }

        /// <summary>
        /// Caso de regressão conferido linha a linha contra a implementação anterior à
        /// extração do domínio: as 30 linhas bateram integralmente.
        /// </summary>
        [Fact]
        public void Simular_DeveReproduzirOResultadoConferidoContraAImplementacaoAnterior()
        {
            ResultadoSimulacao resultado = _simulador.Simular(
                Parametros(40000m, 1500m, 12m, PeriodoDeInvestimento.DeMeses(30)));

            EvolucaoMensal primeiro = resultado.Evolucao[0];

            Assert.Equal(1, primeiro.Ano);
            Assert.Equal(1, primeiro.Mes);
            Assert.Equal(379.55m, primeiro.Juros);
            Assert.Equal(41500m, primeiro.TotalInvestido);
            Assert.Equal(379.55m, primeiro.TotalJuros);
            Assert.Equal(41879.55m, primeiro.TotalAcumulado);

            EvolucaoMensal ultimo = resultado.Evolucao[^1];

            Assert.Equal(3, ultimo.Ano);
            Assert.Equal(6, ultimo.Mes);
            Assert.Equal(971.71m, ultimo.Juros);
            Assert.Equal(85000m, ultimo.TotalInvestido);
            Assert.Equal(19878.00m, ultimo.TotalJuros);
            Assert.Equal(104878.00m, ultimo.TotalAcumulado);
        }
    }
}
