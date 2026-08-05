namespace CalculadoraJurosCompostos.Dominio
{
    /// <summary>
    /// Convenções de valor monetário adotadas nos cálculos.
    /// </summary>
    public static class Moeda
    {
        /// <summary>Centavos: toda quantia é expressa com duas casas.</summary>
        public const int CasasDecimais = 2;

        /// <summary>
        /// Arredonda para centavos pela convenção comercial, em que o meio vai para cima
        /// em valor absoluto: 0,125 vira 0,13. O padrão do .NET é o arredondamento
        /// bancário, que leva o meio para o dígito par e devolveria 0,12.
        /// </summary>
        public static decimal Arredondar(decimal valor) =>
            Math.Round(valor, CasasDecimais, MidpointRounding.AwayFromZero);
    }
}
