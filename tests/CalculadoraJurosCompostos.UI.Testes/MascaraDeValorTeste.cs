using CalculadoraJurosCompostos.Mascaras;

namespace CalculadoraJurosCompostos.Testes
{
    public class MascaraDeValorTeste
    {
        /// <summary>
        /// Os dígitos entram pela direita: cada tecla desloca o valor uma casa.
        /// </summary>
        [Theory]
        [InlineData("4", "R$ 0,04")]
        [InlineData("40", "R$ 0,40")]
        [InlineData("400", "R$ 4,00")]
        [InlineData("4000", "R$ 40,00")]
        [InlineData("40000", "R$ 400,00")]
        [InlineData("4000000", "R$ 40.000,00")]
        public void FormatarDigitacao_Moeda_DeveDeslocarOsDigitosPelaDireita(string digitado, string esperado)
        {
            Assert.Equal(esperado, MascaraDeValor.Moeda.FormatarDigitacao(digitado));
        }

        [Theory]
        [InlineData("1", "0,01%")]
        [InlineData("12", "0,12%")]
        [InlineData("125", "1,25%")]
        [InlineData("1250", "12,50%")]
        public void FormatarDigitacao_Percentual_DeveDeslocarOsDigitosPelaDireita(string digitado, string esperado)
        {
            Assert.Equal(esperado, MascaraDeValor.Percentual.FormatarDigitacao(digitado));
        }

        /// <summary>
        /// Reaplicar a máscara sobre o texto já formatado tem que ser idempotente, porque
        /// é exatamente o que acontece a cada disparo de TextChanged.
        /// </summary>
        [Theory]
        [InlineData("R$ 40.000,00")]
        [InlineData("R$ 0,04")]
        public void FormatarDigitacao_DeveSerIdempotente(string textoJaFormatado)
        {
            Assert.Equal(textoJaFormatado, MascaraDeValor.Moeda.FormatarDigitacao(textoJaFormatado));
        }

        /// <summary>
        /// O backspace apaga o último caractere, que é sempre um dígito; a máscara então
        /// recompõe o valor com um dígito a menos.
        /// </summary>
        [Fact]
        public void FormatarDigitacao_AposBackspace_DeveRemoverUmaCasa()
        {
            const string antes = "R$ 400,00";
            string aposBackspace = antes.Substring(0, antes.Length - 1);

            Assert.Equal("R$ 40,00", MascaraDeValor.Moeda.FormatarDigitacao(aposBackspace));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("R$ ")]
        [InlineData("abc")]
        public void FormatarDigitacao_SemDigitos_DeveResultarEmTextoVazio(string digitado)
        {
            Assert.Equal(string.Empty, MascaraDeValor.Moeda.FormatarDigitacao(digitado));
        }

        [Fact]
        public void FormatarDigitacao_DeveLimitarAQuantidadeDeDigitos()
        {
            string exagerado = new string('9', 40);

            string formatado = MascaraDeValor.Moeda.FormatarDigitacao(exagerado);

            Assert.False(string.IsNullOrEmpty(formatado));
        }

        [Fact]
        public void PosicaoDoCursor_Moeda_DeveFicarNoFimDoTexto()
        {
            Assert.Equal("R$ 40,00".Length, MascaraDeValor.Moeda.PosicaoDoCursor("R$ 40,00"));
        }

        /// <summary>
        /// No percentual o cursor fica antes do símbolo, para que o backspace apague um
        /// dígito em vez do "%".
        /// </summary>
        [Fact]
        public void PosicaoDoCursor_Percentual_DeveFicarAntesDoSimbolo()
        {
            Assert.Equal("12,50%".Length - 1, MascaraDeValor.Percentual.PosicaoDoCursor("12,50%"));
        }

        [Theory]
        [InlineData("R$ 40.000,00", 40000)]
        [InlineData("R$ 1.234,56", 1234.56)]
        [InlineData("12,50%", 12.50)]
        [InlineData("0,00%", 0)]
        [InlineData("R$ 0,05", 0.05)]
        [InlineData("", 0)]
        [InlineData("texto sem número", 0)]
        public void Converter_DeveLerOTextoExibidoDeVoltaComoNumero(string texto, double esperado)
        {
            Assert.Equal((decimal)esperado, MascaraDeValor.Converter(texto));
        }

        /// <summary>
        /// Fecha o ciclo: o que a máscara escreve, o Converter tem que ler de volta igual.
        /// </summary>
        [Theory]
        [InlineData("4000000", 40000)]
        [InlineData("123456", 1234.56)]
        public void Converter_DeveDesfazerOQueFormatarDigitacaoProduziu(string digitado, double esperado)
        {
            string formatado = MascaraDeValor.Moeda.FormatarDigitacao(digitado);

            Assert.Equal((decimal)esperado, MascaraDeValor.Converter(formatado));
        }

        [Fact]
        public void Formatar_DeveProduzirZeroFormatado_ParaCampoVazio()
        {
            Assert.Equal("R$ 0,00", MascaraDeValor.Moeda.Formatar(decimal.Zero));
            Assert.Equal("0,00%", MascaraDeValor.Percentual.Formatar(decimal.Zero));
        }
    }
}
