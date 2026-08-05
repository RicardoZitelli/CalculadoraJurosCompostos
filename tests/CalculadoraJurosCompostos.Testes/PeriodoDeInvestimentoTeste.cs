using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Testes
{
    public class PeriodoDeInvestimentoTeste
    {
        [Theory]
        [InlineData(1, 12)]
        [InlineData(2, 24)]
        [InlineData(30, 360)]
        public void DeAnos_DeveConverterEmMeses(int anos, int mesesEsperados)
        {
            Assert.Equal(mesesEsperados, PeriodoDeInvestimento.DeAnos(anos).EmMeses);
        }

        [Fact]
        public void DeMeses_DevePreservarAQuantidadeInformada()
        {
            Assert.Equal(18, PeriodoDeInvestimento.DeMeses(18).EmMeses);
        }

        /// <summary>
        /// Período zero é aceito e resulta em simulação sem meses, preservando o
        /// comportamento de quando o campo da tela fica vazio.
        /// </summary>
        [Fact]
        public void DeMeses_DeveAceitarZero()
        {
            Assert.Equal(0, PeriodoDeInvestimento.DeMeses(0).EmMeses);
        }

        [Fact]
        public void DeMeses_DeveRecusarValorNegativo()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PeriodoDeInvestimento.DeMeses(-1));
        }

        [Fact]
        public void DeAnos_DeveRecusarValorNegativo()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PeriodoDeInvestimento.DeAnos(-1));
        }
    }
}
