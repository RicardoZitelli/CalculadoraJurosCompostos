namespace CalculadoraJurosCompostos.Aplicacao
{
    /// <summary>
    /// Uma linha da tabela de evolução, pronta para exibição.
    /// </summary>
    public sealed record EvolucaoMensalResponse(
        int Ano,
        int Mes,
        decimal Juros,
        decimal TotalInvestido,
        decimal TotalJuros,
        decimal TotalAcumulado);

    /// <summary>
    /// Saída do caso de uso. Existe para que a apresentação dependa desta camada e não
    /// dos tipos do domínio, que assim ficam livres para mudar sem quebrar a tela.
    /// </summary>
    public sealed record SimulacaoResponse(
        decimal ValorInicial,
        IReadOnlyList<EvolucaoMensalResponse> Evolucao,
        decimal TotalInvestido,
        decimal TotalJuros,
        decimal TotalAcumulado);
}
