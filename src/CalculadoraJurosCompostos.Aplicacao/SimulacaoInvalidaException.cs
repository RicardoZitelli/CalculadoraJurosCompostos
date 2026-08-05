namespace CalculadoraJurosCompostos.Aplicacao
{
    /// <summary>
    /// Entrada recusada por não atender às regras do caso de uso. A mensagem é escrita
    /// para ser exibida ao usuário como está.
    /// </summary>
    public sealed class SimulacaoInvalidaException : Exception
    {
        public SimulacaoInvalidaException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
