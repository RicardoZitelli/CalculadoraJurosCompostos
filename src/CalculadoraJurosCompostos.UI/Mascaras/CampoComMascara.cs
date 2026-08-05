using System;
using System.Windows.Forms;

namespace CalculadoraJurosCompostos.Mascaras
{
    /// <summary>
    /// Liga uma <see cref="MascaraDeValor"/> a um <see cref="TextBox"/>, reaplicando a
    /// máscara a cada digitação e reposicionando o cursor.
    /// </summary>
    public sealed class CampoComMascara
    {
        private readonly TextBox _campo;
        private readonly MascaraDeValor _mascara;

        private bool _aplicando;

        public CampoComMascara(TextBox campo, MascaraDeValor mascara)
        {
            _campo = campo ?? throw new ArgumentNullException(nameof(campo));
            _mascara = mascara ?? throw new ArgumentNullException(nameof(mascara));

            _campo.TextChanged += AoDigitar;
            _campo.Leave += AoSair;
            _campo.Click += AoClicar;
        }

        /// <summary>Valor atualmente exibido no campo.</summary>
        public decimal Valor => MascaraDeValor.Converter(_campo.Text);

        private void AoDigitar(object? sender, EventArgs e)
        {
            // Atribuir Text aqui dispara TextChanged de novo; a guarda corta o reentrante.
            if (_aplicando)
                return;

            _aplicando = true;

            try
            {
                string texto = _mascara.FormatarDigitacao(_campo.Text);

                _campo.Text = texto;
                _campo.SelectionStart = _mascara.PosicaoDoCursor(texto);
                _campo.SelectionLength = 0;
            }
            finally
            {
                _aplicando = false;
            }
        }

        /// <summary>Ao sair, campo vazio assume o valor zero formatado.</summary>
        private void AoSair(object? sender, EventArgs e)
        {
            _campo.Text = _mascara.Formatar(Valor);
        }

        private void AoClicar(object? sender, EventArgs e)
        {
            _campo.SelectAll();
        }
    }
}
