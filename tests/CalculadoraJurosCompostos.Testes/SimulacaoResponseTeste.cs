using CalculadoraJurosCompostos.Aplicacao;

namespace CalculadoraJurosCompostos.Testes
{
    public class SimulacaoResponseTeste
    {
        private static EvolucaoMensalResponse Mes(int mes) =>
            new EvolucaoMensalResponse(1, mes, 10m, 1000m, 10m * mes, 1000m + (10m * mes));

        private static SimulacaoResponse Resposta(params int[] meses) =>
            new SimulacaoResponse(
                ValorInicial: 1000m,
                Evolucao: meses.Select(Mes).ToList(),
                TotalInvestido: 1000m,
                TotalJuros: 10m * meses.Length,
                TotalAcumulado: 1000m + (10m * meses.Length));

        /// <summary>
        /// O caso que a igualdade gerada errava: mesmo conteúdo, listas distintas.
        /// </summary>
        [Fact]
        public void Equals_DeveSerVerdadeiro_ParaConteudoIgualEmListasDiferentes()
        {
            SimulacaoResponse primeira = Resposta(1, 2, 3);
            SimulacaoResponse segunda = Resposta(1, 2, 3);

            Assert.NotSame(primeira.Evolucao, segunda.Evolucao);
            Assert.Equal(primeira, segunda);
            Assert.True(primeira == segunda);
        }

        [Fact]
        public void GetHashCode_DeveCoincidir_ParaConteudoIgual()
        {
            Assert.Equal(Resposta(1, 2, 3).GetHashCode(), Resposta(1, 2, 3).GetHashCode());
        }

        /// <summary>
        /// Consequência prática de GetHashCode e Equals andarem juntos.
        /// </summary>
        [Fact]
        public void Respostas_DeConteudoIgual_DevemColapsarEmUmHashSet()
        {
            var conjunto = new HashSet<SimulacaoResponse> { Resposta(1, 2, 3), Resposta(1, 2, 3) };

            Assert.Single(conjunto);
        }

        [Fact]
        public void Equals_DeveSerFalso_QuandoAEvolucaoDifere()
        {
            Assert.NotEqual(Resposta(1, 2, 3), Resposta(1, 2));
        }

        [Fact]
        public void Equals_DeveSerFalso_QuandoAOrdemDaEvolucaoDifere()
        {
            Assert.NotEqual(Resposta(1, 2, 3), Resposta(3, 2, 1));
        }

        [Fact]
        public void Equals_DeveSerFalso_QuandoUmTotalDifere()
        {
            SimulacaoResponse original = Resposta(1, 2, 3);
            SimulacaoResponse alterada = original with { TotalAcumulado = original.TotalAcumulado + 1m };

            Assert.NotEqual(original, alterada);
        }

        [Fact]
        public void Equals_DeveSerFalso_ParaNulo()
        {
            Assert.False(Resposta(1).Equals(null));
        }

        /// <summary>
        /// O ToString gerado imprimia o nome do tipo da coleção, o que tornava ilegível a
        /// mensagem de falha de um teste. Deve mostrar a quantidade de meses.
        /// </summary>
        [Fact]
        public void ToString_DeveMostrarAQuantidadeDeMeses_EmVezDoTipoDaColecao()
        {
            string texto = Resposta(1, 2, 3).ToString();

            Assert.Contains("Meses = 3", texto);
            Assert.DoesNotContain("System.Collections", texto);
        }
    }
}
