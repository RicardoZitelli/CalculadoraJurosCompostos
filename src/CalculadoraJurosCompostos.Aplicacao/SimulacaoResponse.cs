using System.Text;

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
        decimal TotalAcumulado)
    {
        /// <summary>
        /// A igualdade gerada por um record compara cada membro com o comparador padrão,
        /// que para uma coleção é igualdade por referência: duas respostas de mesmo
        /// conteúdo nunca seriam iguais. Como record promete igualdade por valor, a
        /// comparação da evolução é feita elemento a elemento.
        /// </summary>
        public bool Equals(SimulacaoResponse? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ValorInicial == other.ValorInicial
                && TotalInvestido == other.TotalInvestido
                && TotalJuros == other.TotalJuros
                && TotalAcumulado == other.TotalAcumulado
                && Evolucao.SequenceEqual(other.Evolucao);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(ValorInicial);
            hash.Add(TotalInvestido);
            hash.Add(TotalJuros);
            hash.Add(TotalAcumulado);

            foreach (EvolucaoMensalResponse mes in Evolucao)
                hash.Add(mes);

            return hash.ToHashCode();
        }

        /// <summary>
        /// O ToString gerado imprimiria o nome do tipo da coleção, que não diz nada. Mostrar
        /// a quantidade de meses torna legível a mensagem de falha de um teste.
        /// </summary>
        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"ValorInicial = {ValorInicial}")
                   .Append($", Meses = {Evolucao.Count}")
                   .Append($", TotalInvestido = {TotalInvestido}")
                   .Append($", TotalJuros = {TotalJuros}")
                   .Append($", TotalAcumulado = {TotalAcumulado}");

            return true;
        }
    }
}
