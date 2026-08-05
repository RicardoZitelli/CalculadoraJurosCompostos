using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Testes
{
    public class TaxaDeJurosTeste
    {
        [Fact]
        public void Anual_DevePreservarOPercentualInformado()
        {
            TaxaDeJuros taxa = TaxaDeJuros.Anual(12m);

            Assert.Equal(12m, taxa.PercentualAnual);
        }

        /// <summary>
        /// A propriedade que define a conversão: compor a taxa mensal por 12 meses tem
        /// que devolver exatamente a taxa anual contratada.
        /// </summary>
        [Theory]
        [InlineData(0.5)]
        [InlineData(6)]
        [InlineData(10)]
        [InlineData(12)]
        [InlineData(100)]
        public void PercentualMensal_CompostoPorDozeMeses_DeveReproduzirATaxaAnual(double percentualAnual)
        {
            TaxaDeJuros taxa = TaxaDeJuros.Anual((decimal)percentualAnual);

            double anualReconstituida = (Math.Pow(1 + ((double)taxa.PercentualMensal / 100), 12) - 1) * 100;

            Assert.Equal(percentualAnual, anualReconstituida, precision: 10);
        }

        /// <summary>
        /// Protege contra a volta da divisão linear por 12, que era o cálculo anterior.
        /// A taxa equivalente composta é sempre menor que a linear para juros positivos.
        /// </summary>
        [Fact]
        public void PercentualMensal_NaoDeveSerATaxaAnualDivididaPorDoze()
        {
            TaxaDeJuros taxa = TaxaDeJuros.Anual(12m);

            Assert.NotEqual(1m, taxa.PercentualMensal);
            Assert.True(taxa.PercentualMensal < 1m, $"Esperado menor que 1%, obtido {taxa.PercentualMensal}%.");
        }

        [Fact]
        public void PercentualMensal_DeveSerZero_QuandoATaxaAnualForZero()
        {
            TaxaDeJuros taxa = TaxaDeJuros.Anual(decimal.Zero);

            Assert.Equal(decimal.Zero, taxa.PercentualMensal);
        }

        [Fact]
        public void PercentualMensal_DeveSerMenosCem_NoLimiteInferior()
        {
            TaxaDeJuros taxa = TaxaDeJuros.Anual(TaxaDeJuros.PercentualMinimo);

            Assert.Equal(TaxaDeJuros.PercentualMinimo, taxa.PercentualMensal);
        }

        [Theory]
        [InlineData(-100.01)]
        [InlineData(-150)]
        public void Anual_DeveRecusarTaxaAbaixoDoLimiteInferior(double percentualAnual)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TaxaDeJuros.Anual((decimal)percentualAnual));
        }
    }
}
