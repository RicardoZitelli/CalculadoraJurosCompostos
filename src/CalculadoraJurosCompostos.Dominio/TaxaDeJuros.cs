namespace CalculadoraJurosCompostos.Dominio
{
    /// <summary>
    /// Taxa de juros expressa em percentual, com a conversão entre o período anual e o mensal.
    /// </summary>
    public sealed class TaxaDeJuros
    {
        /// <summary>
        /// Percentual mínimo aceito. Em -100% o capital é integralmente consumido no período,
        /// e abaixo disso a taxa não tem equivalente real.
        /// </summary>
        public const decimal PercentualMinimo = -100m;

        private const int MesesNoAno = 12;

        private TaxaDeJuros(decimal percentualAnual, decimal percentualMensal)
        {
            PercentualAnual = percentualAnual;
            PercentualMensal = percentualMensal;
        }

        public decimal PercentualAnual { get; }

        public decimal PercentualMensal { get; }

        /// <summary>
        /// Cria a taxa a partir do percentual anual, já calculando o mensal equivalente.
        /// </summary>
        public static TaxaDeJuros Anual(decimal percentual)
        {
            if (percentual < PercentualMinimo)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(percentual),
                    percentual,
                    $"A taxa anual não pode ser inferior a {PercentualMinimo}%.");
            }

            return new TaxaDeJuros(percentual, ConverterAnualEmMensal(percentual));
        }

        /// <summary>
        /// Converte o percentual anual no mensal equivalente por capitalização composta:
        /// <c>((1 + i) ^ (1/12)) - 1</c>. Dividir a taxa anual por 12 daria a taxa linear,
        /// que ao ser composta por 12 meses supera a taxa anual contratada.
        /// </summary>
        private static decimal ConverterAnualEmMensal(decimal percentualAnual)
        {
            if (percentualAnual == PercentualMinimo)
                return PercentualMinimo;

            double mensal = Math.Pow(1 + ((double)percentualAnual / 100), 1d / MesesNoAno) - 1;

            return (decimal)(mensal * 100);
        }
    }
}
