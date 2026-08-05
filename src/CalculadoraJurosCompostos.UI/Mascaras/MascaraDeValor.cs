using System;
using System.Globalization;
using System.Text;

namespace CalculadoraJurosCompostos.Mascaras
{
    /// <summary>
    /// Formatação e leitura dos campos numéricos da tela. Não depende de WinForms:
    /// tudo aqui é transformação de texto, o que a torna verificável isoladamente.
    /// </summary>
    public sealed class MascaraDeValor
    {
        private static readonly CultureInfo CulturaBrasil = CultureInfo.GetCultureInfo("pt-BR");

        private const int MaximoDeDigitos = 15;
        private const int CentesimosPorUnidade = 100;

        private readonly string _formato;
        private readonly string _sufixo;

        private MascaraDeValor(string formato, string sufixo)
        {
            _formato = formato;
            _sufixo = sufixo;
        }

        /// <summary>Moeda em real, como "R$ 1.234,56".</summary>
        public static MascaraDeValor Moeda { get; } = new MascaraDeValor("C2", "");

        /// <summary>Percentual com duas casas, como "12,50%".</summary>
        public static MascaraDeValor Percentual { get; } = new MascaraDeValor("0.00", "%");

        /// <summary>
        /// Reaplica a máscara ao que foi digitado: considera apenas os dígitos e os
        /// interpreta como centésimos, fazendo os números entrarem pela direita
        /// (1 vira 0,01; 12 vira 0,12; 123 vira 1,23). Texto sem dígito nenhum
        /// resulta em texto vazio, para não atrapalhar quem está preenchendo.
        /// </summary>
        public string FormatarDigitacao(string texto)
        {
            string digitos = ExtrairDigitos(texto);

            if (digitos.Length == 0)
                return string.Empty;

            decimal valor = decimal.Parse(digitos, NumberStyles.None, CultureInfo.InvariantCulture)
                / CentesimosPorUnidade;

            return Formatar(valor);
        }

        /// <summary>Formata um valor já conhecido, usado ao sair do campo.</summary>
        public string Formatar(decimal valor) => valor.ToString(_formato, CulturaBrasil) + _sufixo;

        /// <summary>
        /// Onde o cursor deve ficar depois de reaplicada a máscara: no fim do texto, mas
        /// antes do sufixo, para que o backspace apague um dígito e não o símbolo.
        /// </summary>
        public int PosicaoDoCursor(string textoFormatado) => textoFormatado.Length - _sufixo.Length;

        /// <summary>
        /// Lê o texto exibido de volta como número. Remove a máscara, normaliza o separador
        /// decimal e converte com cultura fixa, para não depender da cultura da máquina.
        /// Texto inválido resulta em zero.
        /// </summary>
        public static decimal Converter(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return decimal.Zero;

            string valor = texto.Replace("R$", "")
                                .Replace("%", "")
                                .Replace(" ", "")
                                .Replace(" ", "") //espaço não separável (usado por algumas culturas em "R$ 1.234,56")
                                .Replace(".", "")
                                .Replace(",", ".");

            decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal resultado);

            return resultado;
        }

        /// <summary>
        /// Extrai apenas os dígitos, descartando zeros à esquerda e limitando a quantidade
        /// para que o valor sempre caiba em um <see cref="decimal"/>.
        /// </summary>
        private static string ExtrairDigitos(string texto)
        {
            var digitos = new StringBuilder();

            foreach (char caractere in texto)
            {
                if (char.IsDigit(caractere))
                    digitos.Append(caractere);
            }

            string resultado = digitos.ToString().TrimStart('0');

            return resultado.Length > MaximoDeDigitos ? resultado.Substring(0, MaximoDeDigitos) : resultado;
        }
    }
}
