namespace CalculadoraJurosCompostos.Dominio
{
    /// <summary>
    /// Entrada de uma simulação: quanto se aplica, a que taxa e por quanto tempo.
    /// </summary>
    public sealed class ParametrosSimulacao
    {
        public ParametrosSimulacao(
            decimal valorInicial,
            decimal aporteMensal,
            TaxaDeJuros taxa,
            PeriodoDeInvestimento periodo)
        {
            if (valorInicial < 0)
                throw new ArgumentOutOfRangeException(nameof(valorInicial), valorInicial, "O valor inicial não pode ser negativo.");

            if (aporteMensal < 0)
                throw new ArgumentOutOfRangeException(nameof(aporteMensal), aporteMensal, "O aporte mensal não pode ser negativo.");

            ValorInicial = valorInicial;
            AporteMensal = aporteMensal;
            Taxa = taxa ?? throw new ArgumentNullException(nameof(taxa));
            Periodo = periodo ?? throw new ArgumentNullException(nameof(periodo));
        }

        public decimal ValorInicial { get; }

        public decimal AporteMensal { get; }

        public TaxaDeJuros Taxa { get; }

        public PeriodoDeInvestimento Periodo { get; }
    }
}
