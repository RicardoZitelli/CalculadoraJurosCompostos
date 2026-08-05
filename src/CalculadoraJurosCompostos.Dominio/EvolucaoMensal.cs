namespace CalculadoraJurosCompostos.Dominio
{
    /// <summary>
    /// Retrato do investimento ao fim de um mês da simulação.
    /// </summary>
    /// <param name="Ano">Ano da simulação, começando em 1.</param>
    /// <param name="Mes">Mês dentro do ano, de 1 a 12.</param>
    /// <param name="Juros">Juros creditados neste mês.</param>
    /// <param name="TotalInvestido">Valor inicial somado a todos os aportes até aqui.</param>
    /// <param name="TotalJuros">Juros acumulados desde o início.</param>
    /// <param name="TotalAcumulado">Total investido somado ao total de juros.</param>
    public sealed record EvolucaoMensal(
        int Ano,
        int Mes,
        decimal Juros,
        decimal TotalInvestido,
        decimal TotalJuros,
        decimal TotalAcumulado);
}
