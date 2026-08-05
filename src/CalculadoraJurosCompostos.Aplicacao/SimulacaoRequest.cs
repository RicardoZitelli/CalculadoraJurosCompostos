namespace CalculadoraJurosCompostos.Aplicacao
{
    /// <summary>
    /// Entrada do caso de uso, em valores crus. Cabe a esta camada traduzi-los nos
    /// tipos do domínio, para que quem chama não precise conhecê-los.
    /// </summary>
    /// <param name="ValorInicial">Aplicação inicial.</param>
    /// <param name="AporteMensal">Valor depositado ao fim de cada mês.</param>
    /// <param name="TaxaAnual">Taxa de juros anual, em percentual.</param>
    /// <param name="Periodo">Prazo, na unidade indicada por <paramref name="TipoPeriodo"/>.</param>
    /// <param name="TipoPeriodo">Se o prazo está em anos ou em meses.</param>
    public sealed record SimulacaoRequest(
        decimal ValorInicial,
        decimal AporteMensal,
        decimal TaxaAnual,
        int Periodo,
        TipoPeriodo TipoPeriodo);
}
