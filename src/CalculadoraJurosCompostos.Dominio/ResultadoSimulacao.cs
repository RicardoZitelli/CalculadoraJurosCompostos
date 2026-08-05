namespace CalculadoraJurosCompostos.Dominio
{
    /// <summary>
    /// Resultado de uma simulação: os parâmetros usados e a evolução mês a mês.
    /// </summary>
    public sealed class ResultadoSimulacao
    {
        public ResultadoSimulacao(ParametrosSimulacao parametros, IReadOnlyList<EvolucaoMensal> evolucao)
        {
            Parametros = parametros ?? throw new ArgumentNullException(nameof(parametros));
            Evolucao = evolucao ?? throw new ArgumentNullException(nameof(evolucao));
        }

        public ParametrosSimulacao Parametros { get; }

        /// <summary>
        /// Um item por mês simulado. Não inclui o estado inicial, anterior ao primeiro mês.
        /// </summary>
        public IReadOnlyList<EvolucaoMensal> Evolucao { get; }

        public decimal TotalInvestido =>
            Evolucao.Count == 0 ? Parametros.ValorInicial : Evolucao[^1].TotalInvestido;

        public decimal TotalJuros =>
            Evolucao.Count == 0 ? decimal.Zero : Evolucao[^1].TotalJuros;

        public decimal TotalAcumulado =>
            Evolucao.Count == 0 ? Parametros.ValorInicial : Evolucao[^1].TotalAcumulado;
    }
}
