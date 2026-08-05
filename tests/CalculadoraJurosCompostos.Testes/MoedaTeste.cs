using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos.Testes
{
    public class MoedaTeste
    {
        /// <summary>
        /// Casos de meio exato em que as duas convenções divergem: o dígito anterior é par,
        /// então o arredondamento bancário manteria o par e a comercial sobe.
        /// </summary>
        [Theory]
        [InlineData(0.125, 0.13)]
        [InlineData(1.005, 1.01)]
        [InlineData(2.345, 2.35)]
        [InlineData(9.485, 9.49)]
        public void Arredondar_NoMeioExato_DeveSubir(double valor, double esperado)
        {
            Assert.Equal((decimal)esperado, Moeda.Arredondar((decimal)valor));
        }

        /// <summary>
        /// Contraprova: com estes mesmos valores o arredondamento bancário desceria,
        /// o que confirma que os casos acima realmente discriminam as convenções.
        /// </summary>
        [Theory]
        [InlineData(0.125, 0.12)]
        [InlineData(1.005, 1.00)]
        [InlineData(2.345, 2.34)]
        [InlineData(9.485, 9.48)]
        public void Arredondar_DeveDivergirDoArredondamentoBancario(double valor, double bancario)
        {
            decimal comercial = Moeda.Arredondar((decimal)valor);

            Assert.Equal((decimal)bancario, Math.Round((decimal)valor, 2, MidpointRounding.ToEven));
            Assert.NotEqual((decimal)bancario, comercial);
        }

        [Theory]
        [InlineData(9.4849, 9.48)]
        [InlineData(9.4851, 9.49)]
        [InlineData(0, 0)]
        [InlineData(1234.56, 1234.56)]
        public void Arredondar_ForaDoMeio_DeveSeguirOVizinhoMaisProximo(double valor, double esperado)
        {
            Assert.Equal((decimal)esperado, Moeda.Arredondar((decimal)valor));
        }

        [Fact]
        public void Arredondar_DeveProduzirNoMaximoDuasCasas()
        {
            decimal arredondado = Moeda.Arredondar(1.23456789m);

            Assert.Equal(1.23m, arredondado);
            Assert.Equal(Moeda.CasasDecimais, decimal.GetBits(arredondado)[3] >> 16 & 0xFF);
        }
    }
}
