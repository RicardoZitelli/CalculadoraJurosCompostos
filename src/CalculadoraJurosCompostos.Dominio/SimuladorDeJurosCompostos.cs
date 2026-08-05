namespace CalculadoraJurosCompostos.Dominio
{
    /// <summary>
    /// Projeta a evolução de um investimento com aportes mensais sob juros compostos.
    /// </summary>
    public sealed class SimuladorDeJurosCompostos
    {
        private const int MesesNoAno = 12;

        /// <summary>
        /// Simula mês a mês. Os juros de cada mês incidem sobre o saldo anterior ao aporte
        /// daquele mês, ou seja, o aporte é postecipado: só rende a partir do mês seguinte.
        /// </summary>
        public ResultadoSimulacao Simular(ParametrosSimulacao parametros)
        {
            ArgumentNullException.ThrowIfNull(parametros);

            int totalDeMeses = parametros.Periodo.EmMeses;
            var evolucao = new List<EvolucaoMensal>(totalDeMeses);

            decimal totalInvestido = parametros.ValorInicial;
            decimal totalAcumulado = parametros.ValorInicial;
            decimal totalJuros = decimal.Zero;

            for (int mes = 1; mes <= totalDeMeses; mes++)
            {
                decimal juros = Moeda.Arredondar(
                    totalAcumulado * parametros.Taxa.PercentualMensal / 100);

                totalJuros += juros;
                totalInvestido += parametros.AporteMensal;
                totalAcumulado = totalInvestido + totalJuros;

                evolucao.Add(new EvolucaoMensal(
                    Ano: AnoDo(mes),
                    Mes: MesDentroDoAno(mes),
                    Juros: juros,
                    TotalInvestido: totalInvestido,
                    TotalJuros: totalJuros,
                    TotalAcumulado: totalAcumulado));
            }

            return new ResultadoSimulacao(parametros, evolucao);
        }

        /// <summary>Ano da simulação, começando em 1: os meses 1 a 12 são o ano 1.</summary>
        private static int AnoDo(int mes) => ((mes - 1) / MesesNoAno) + 1;

        /// <summary>Mês dentro do ano, de 1 a 12: o mês 13 da simulação é o mês 1 do ano 2.</summary>
        private static int MesDentroDoAno(int mes) => ((mes - 1) % MesesNoAno) + 1;
    }
}
