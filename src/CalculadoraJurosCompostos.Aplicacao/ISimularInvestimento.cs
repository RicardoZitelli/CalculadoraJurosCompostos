namespace CalculadoraJurosCompostos.Aplicacao
{
    /// <summary>
    /// Caso de uso: simular a evolução de um investimento a partir dos dados informados.
    /// </summary>
    public interface ISimularInvestimento
    {
        SimulacaoResponse Executar(SimulacaoRequest request);
    }
}
