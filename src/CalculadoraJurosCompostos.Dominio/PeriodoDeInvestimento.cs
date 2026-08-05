namespace CalculadoraJurosCompostos.Dominio
{
    /// <summary>
    /// Prazo do investimento, sempre normalizado em meses.
    /// </summary>
    public sealed class PeriodoDeInvestimento
    {
        private const int MesesNoAno = 12;

        private PeriodoDeInvestimento(int emMeses)
        {
            EmMeses = emMeses;
        }

        public int EmMeses { get; }

        public static PeriodoDeInvestimento DeMeses(int meses)
        {
            if (meses < 0)
                throw new ArgumentOutOfRangeException(nameof(meses), meses, "O período não pode ser negativo.");

            return new PeriodoDeInvestimento(meses);
        }

        public static PeriodoDeInvestimento DeAnos(int anos)
        {
            if (anos < 0)
                throw new ArgumentOutOfRangeException(nameof(anos), anos, "O período não pode ser negativo.");

            return new PeriodoDeInvestimento(anos * MesesNoAno);
        }
    }
}
