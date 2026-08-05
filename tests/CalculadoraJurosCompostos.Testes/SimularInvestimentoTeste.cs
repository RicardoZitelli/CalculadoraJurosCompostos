using CalculadoraJurosCompostos.Aplicacao;
using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Testes
{
    public class SimularInvestimentoTeste
    {
        private readonly ISimularInvestimento _casoDeUso =
            new SimularInvestimento(new SimuladorDeJurosCompostos());

        private static SimulacaoRequest Request(
            int periodo,
            TipoPeriodo tipoPeriodo,
            decimal valorInicial = 1000m,
            decimal aporteMensal = 100m,
            decimal taxaAnual = 12m) =>
            new SimulacaoRequest(valorInicial, aporteMensal, taxaAnual, periodo, tipoPeriodo);

        [Fact]
        public void Executar_DeveConverterPeriodoEmAnosParaMeses()
        {
            SimulacaoResponse resposta = _casoDeUso.Executar(Request(2, TipoPeriodo.Anos));

            Assert.Equal(24, resposta.Evolucao.Count);
        }

        [Fact]
        public void Executar_DevePreservarPeriodoInformadoEmMeses()
        {
            SimulacaoResponse resposta = _casoDeUso.Executar(Request(24, TipoPeriodo.Meses));

            Assert.Equal(24, resposta.Evolucao.Count);
        }

        /// <summary>
        /// Compara campo a campo em vez de comparar as respostas inteiras: SimulacaoResponse
        /// é record, mas o membro de coleção cai no comparador padrão, que para List é
        /// igualdade por referência. Duas respostas de mesmo conteúdo nunca seriam iguais.
        /// </summary>
        [Fact]
        public void Executar_DeveProduzirOMesmoResultado_ParaAnosOuOEquivalenteEmMeses()
        {
            SimulacaoResponse porAnos = _casoDeUso.Executar(Request(2, TipoPeriodo.Anos));
            SimulacaoResponse porMeses = _casoDeUso.Executar(Request(24, TipoPeriodo.Meses));

            Assert.Equal(porAnos.Evolucao, porMeses.Evolucao);
            Assert.Equal(porAnos.ValorInicial, porMeses.ValorInicial);
            Assert.Equal(porAnos.TotalInvestido, porMeses.TotalInvestido);
            Assert.Equal(porAnos.TotalJuros, porMeses.TotalJuros);
            Assert.Equal(porAnos.TotalAcumulado, porMeses.TotalAcumulado);
        }

        /// <summary>
        /// A resposta tem que carregar os mesmos números que o domínio produziu, sem
        /// perder nada na tradução para DTO.
        /// </summary>
        [Fact]
        public void Executar_DeveMapearAResposta_SemAlterarOsValoresDoDominio()
        {
            var parametros = new ParametrosSimulacao(
                valorInicial: 40000m,
                aporteMensal: 1500m,
                taxa: TaxaDeJuros.Anual(12m),
                periodo: PeriodoDeInvestimento.DeMeses(30));

            ResultadoSimulacao esperado = new SimuladorDeJurosCompostos().Simular(parametros);

            SimulacaoResponse resposta = _casoDeUso.Executar(
                Request(30, TipoPeriodo.Meses, valorInicial: 40000m, aporteMensal: 1500m, taxaAnual: 12m));

            Assert.Equal(esperado.Parametros.ValorInicial, resposta.ValorInicial);
            Assert.Equal(esperado.TotalInvestido, resposta.TotalInvestido);
            Assert.Equal(esperado.TotalJuros, resposta.TotalJuros);
            Assert.Equal(esperado.TotalAcumulado, resposta.TotalAcumulado);
            Assert.Equal(esperado.Evolucao.Count, resposta.Evolucao.Count);

            for (int i = 0; i < esperado.Evolucao.Count; i++)
            {
                EvolucaoMensal doDominio = esperado.Evolucao[i];
                EvolucaoMensalResponse daResposta = resposta.Evolucao[i];

                Assert.Equal(doDominio.Ano, daResposta.Ano);
                Assert.Equal(doDominio.Mes, daResposta.Mes);
                Assert.Equal(doDominio.Juros, daResposta.Juros);
                Assert.Equal(doDominio.TotalInvestido, daResposta.TotalInvestido);
                Assert.Equal(doDominio.TotalJuros, daResposta.TotalJuros);
                Assert.Equal(doDominio.TotalAcumulado, daResposta.TotalAcumulado);
            }
        }

        [Fact]
        public void Executar_DeveRecusarRequestNulo()
        {
            Assert.Throws<ArgumentNullException>(() => _casoDeUso.Executar(null!));
        }

        [Fact]
        public void Executar_DeveRecusarTipoDePeriodoDesconhecido()
        {
            SimulacaoRequest request = Request(12, (TipoPeriodo)99);

            Assert.Throws<ArgumentOutOfRangeException>(() => _casoDeUso.Executar(request));
        }

        /// <summary>
        /// A validação continua sendo do domínio; a camada de aplicação apenas deixa passar.
        /// </summary>
        [Fact]
        public void Executar_DevePropagarAValidacaoDoDominio()
        {
            SimulacaoRequest request = Request(12, TipoPeriodo.Meses, valorInicial: -1m);

            Assert.Throws<ArgumentOutOfRangeException>(() => _casoDeUso.Executar(request));
        }

        [Fact]
        public void Construtor_DeveRecusarSimuladorNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new SimularInvestimento(null!));
        }
    }
}
