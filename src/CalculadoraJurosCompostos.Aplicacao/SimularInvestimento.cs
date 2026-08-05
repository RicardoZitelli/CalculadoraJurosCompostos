using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Aplicacao
{
    /// <inheritdoc cref="ISimularInvestimento"/>
    public sealed class SimularInvestimento : ISimularInvestimento
    {
        private readonly SimuladorDeJurosCompostos _simulador;

        public SimularInvestimento(SimuladorDeJurosCompostos simulador)
        {
            _simulador = simulador ?? throw new ArgumentNullException(nameof(simulador));
        }

        public SimulacaoResponse Executar(SimulacaoRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            ParametrosSimulacao parametros = MontarParametros(request);
            ResultadoSimulacao resultado = _simulador.Simular(parametros);

            return MontarResposta(resultado);
        }

        private static ParametrosSimulacao MontarParametros(SimulacaoRequest request) =>
            new ParametrosSimulacao(
                valorInicial: request.ValorInicial,
                aporteMensal: request.AporteMensal,
                taxa: TaxaDeJuros.Anual(request.TaxaAnual),
                periodo: MontarPeriodo(request));

        private static PeriodoDeInvestimento MontarPeriodo(SimulacaoRequest request) =>
            request.TipoPeriodo switch
            {
                TipoPeriodo.Anos => PeriodoDeInvestimento.DeAnos(request.Periodo),
                TipoPeriodo.Meses => PeriodoDeInvestimento.DeMeses(request.Periodo),
                _ => throw new ArgumentOutOfRangeException(
                    nameof(request),
                    request.TipoPeriodo,
                    "Tipo de período não reconhecido."),
            };

        private static SimulacaoResponse MontarResposta(ResultadoSimulacao resultado)
        {
            var evolucao = new List<EvolucaoMensalResponse>(resultado.Evolucao.Count);

            foreach (EvolucaoMensal mes in resultado.Evolucao)
            {
                evolucao.Add(new EvolucaoMensalResponse(
                    Ano: mes.Ano,
                    Mes: mes.Mes,
                    Juros: mes.Juros,
                    TotalInvestido: mes.TotalInvestido,
                    TotalJuros: mes.TotalJuros,
                    TotalAcumulado: mes.TotalAcumulado));
            }

            return new SimulacaoResponse(
                ValorInicial: resultado.Parametros.ValorInicial,
                Evolucao: evolucao,
                TotalInvestido: resultado.TotalInvestido,
                TotalJuros: resultado.TotalJuros,
                TotalAcumulado: resultado.TotalAcumulado);
        }
    }
}
