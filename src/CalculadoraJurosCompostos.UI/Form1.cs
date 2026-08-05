using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using CalculadoraJurosCompostos.Dominio;

namespace CalculadoraJurosCompostos
{
    public partial class FrmCalculadoraJurosCompostos : Form
    {
        private static readonly CultureInfo CulturaBrasil = CultureInfo.GetCultureInfo("pt-BR");
        private const int MaximoDeDigitos = 15;

        private readonly SimuladorDeJurosCompostos _simulador = new SimuladorDeJurosCompostos();

        private bool _aplicandoMascara;

        public FrmCalculadoraJurosCompostos()
        {
            InitializeComponent();
        }
     
        private void FrmCalculadoraJurosCompostos_Load(object sender, EventArgs e)
        {          
            AjustarCelulas();
            cbPeriodo.SelectedIndex = 0;
            txtValorInicial.Focus();            
            
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                LimparDataGridView();

                ParametrosSimulacao parametros = LerParametros();
                ResultadoSimulacao resultado = _simulador.Simular(parametros);

                ExibirResultado(resultado);

                AjustarCelulas();
            }
            catch (Exception ex)
            {
                LimparDataGridView();
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimparDataGridView()
        {
            dgvCalculo.Rows.Clear();
        }

        /// <summary>
        /// Lê os campos da tela e monta a entrada da simulação. A escolha do combo é leitura
        /// de tela; a equivalência entre anos e meses é regra e fica no domínio.
        /// </summary>
        private ParametrosSimulacao LerParametros()
        {
            decimal valorInicial = ConverterParaDecimal(txtValorInicial.Text);
            decimal aporteMensal = ConverterParaDecimal(txtValorMensal.Text);
            decimal taxaAnual = ConverterParaDecimal(txtTaxaJuros.Text);
            int.TryParse(txtPeriodo.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int periodo);

            bool periodoEmAnos = cbPeriodo.SelectedIndex == 0;

            return new ParametrosSimulacao(
                valorInicial: valorInicial,
                aporteMensal: aporteMensal,
                taxa: TaxaDeJuros.Anual(taxaAnual),
                periodo: periodoEmAnos
                    ? PeriodoDeInvestimento.DeAnos(periodo)
                    : PeriodoDeInvestimento.DeMeses(periodo));
        }

        private void ExibirResultado(ResultadoSimulacao resultado)
        {
            IncluirValoresIniciaisAoDataGridView(resultado.Parametros.ValorInicial);

            foreach (EvolucaoMensal evolucao in resultado.Evolucao)
                AdicionarValoresAoDataGridView(evolucao);
        }

        private void IncluirValoresIniciaisAoDataGridView(decimal valorInicial)
        {
            dgvCalculo.Rows.Add(0
                ,0
                ,decimal.Zero.ToString("C2", CulturaBrasil)
                ,valorInicial.ToString("C2", CulturaBrasil)
                ,decimal.Zero.ToString("C2", CulturaBrasil)
                ,valorInicial.ToString("C2", CulturaBrasil));
        }

        private void AdicionarValoresAoDataGridView(EvolucaoMensal evolucao)
        {
            dgvCalculo.Rows.Add(evolucao.Ano,
                                           evolucao.Mes,
                                           "+ " + evolucao.Juros.ToString("C2", CulturaBrasil),
                                           evolucao.TotalInvestido.ToString("C2", CulturaBrasil),
                                           evolucao.TotalJuros.ToString("C2", CulturaBrasil),
                                           evolucao.TotalAcumulado.ToString("C2", CulturaBrasil));
        }
                
        private void AjustarCelulas()
        {
            for (int i = 0; i < dgvCalculo.Columns.Count; i++)
                dgvCalculo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void txtValorInicial_KeyUp(object sender, KeyEventArgs e)
        {
            PularParaProximoControle(sender, e);
        }

        private void txtValorInicial_TextChanged(object sender, EventArgs e)
        {
            AplicarMascara((TextBox)sender, "C2", "");
        }

        private void txtValorInicial_Leave(object sender, EventArgs e)
        {
            txtValorInicial.Text = TransformarEmMoeda(sender);
        }

        private void txtValorInicial_Click(object sender, EventArgs e)
        {
            txtValorInicial.SelectAll();
        }

        private void txtValorMensal_Click(object sender, EventArgs e)
        {
            txtValorMensal.SelectAll();
        }

        private void txtValorMensal_KeyUp(object sender, KeyEventArgs e)
        {
            PularParaProximoControle(sender, e);
        }

        private void txtValorMensal_TextChanged(object sender, EventArgs e)
        {
            AplicarMascara((TextBox)sender, "C2", "");
        }

        private void txtValorMensal_Leave(object sender, EventArgs e)
        {
            txtValorMensal.Text = TransformarEmMoeda(sender);
        }

        private void txtTaxaJuros_Click(object sender, EventArgs e)
        {
            txtTaxaJuros.SelectAll();
        }

        private void txtTaxaJuros_KeyUp(object sender, KeyEventArgs e)
        {
            PularParaProximoControle(sender, e);
        }

        private void txtTaxaJuros_TextChanged(object sender, EventArgs e)
        {
            AplicarMascara((TextBox)sender, "0.00", "%");
        }

        private void txtTaxaJuros_Leave(object sender, EventArgs e)
        {
            txtTaxaJuros.Text = TransformarEmPercentual((TextBox)sender);
        }

        private void txtPeriodo_Click(object sender, EventArgs e)
        {
            txtPeriodo.SelectAll();
        }

        private void txtPeriodo_KeyUp(object sender, KeyEventArgs e)
        {
            PularParaProximoControle(sender, e);
        }

        private void cbPeriodo_KeyUp(object sender, KeyEventArgs e)
        {
            PularParaProximoControle(sender, e);
        }

        private string TransformarEmMoeda(object value)
        {
            if (value is TextBox textBox)
                return ConverterParaDecimal(textBox.Text).ToString("C2", CulturaBrasil);

            return "";
        }

        private string TransformarEmPercentual(TextBox textBox)
        {
            return ConverterParaDecimal(textBox.Text).ToString("0.00", CulturaBrasil) + "%";
        }

        /// <summary>
        /// Reaplica a máscara a cada digitação: considera apenas os dígitos informados e os
        /// interpreta como centésimos, de modo que os números entrem pela direita
        /// (1 -> 0,01; 12 -> 0,12; 123 -> 1,23). O cursor é reposicionado antes do sufixo.
        /// </summary>
        private void AplicarMascara(TextBox textBox, string formato, string sufixo)
        {
            if (_aplicandoMascara)
                return;

            _aplicandoMascara = true;

            try
            {
                string digitos = ExtrairDigitos(textBox.Text);

                if (digitos.Length == 0)
                {
                    textBox.Clear();
                    return;
                }

                decimal valor = decimal.Parse(digitos, NumberStyles.None, CultureInfo.InvariantCulture) / 100m;
                string texto = valor.ToString(formato, CulturaBrasil) + sufixo;

                textBox.Text = texto;
                textBox.SelectionStart = texto.Length - sufixo.Length;
                textBox.SelectionLength = 0;
            }
            finally
            {
                _aplicandoMascara = false;
            }
        }

        /// <summary>
        /// Extrai apenas os dígitos do texto, descartando zeros à esquerda e limitando a
        /// quantidade para que o valor sempre caiba em um <see cref="decimal"/>.
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

        /// <summary>
        /// Remove a máscara do texto digitado (R$, %, espaços e separador de milhar), normaliza
        /// o separador decimal e converte com cultura fixa, para que o resultado não dependa
        /// da cultura da máquina. Retorna zero quando o texto não é um número válido.
        /// </summary>
        private static decimal ConverterParaDecimal(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return decimal.Zero;

            string valor = texto.Replace("R$", "")
                                .Replace("%", "")
                                .Replace(" ", "")
                                .Replace(" ", "") //espaço não separável (usado por algumas culturas em "R$ 1.234,56")
                                .Replace(".", "")
                                .Replace(",", ".");

            decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal resultado);

            return resultado;
        }

        private void PularParaProximoControle(object sender, KeyEventArgs @event)
        {
            if (@event.KeyCode == Keys.Enter)
                SelectNextControl((Control)sender, true, true, true, true);

        }
    }
}
