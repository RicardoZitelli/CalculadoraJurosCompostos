using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraJurosCompostos
{
    public partial class FrmCalculadoraJurosCompostos : Form
    {
        public FrmCalculadoraJurosCompostos()
        {
            InitializeComponent();
        }

        private void FrmCalculadoraJurosCompostos_Load(object sender, EventArgs e)
        {
            cbPeriodo.SelectedIndex = 0;
            txtValorInicial.Focus();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCalculo.Rows.Clear();

                double.TryParse(txtValorInicial.Text.Replace("R$", "").Replace(" ", "").Replace(".", ""), out double valorInicial);
                double.TryParse(txtValorMensal.Text.Replace("R$", "").Replace(" ", "").Replace(".", ""), out double valorMensal);
                int.TryParse(txtPeriodo.Text, out int periodo);
                double.TryParse(txtTaxaJuros.Text.Replace("%", "").Replace(" ", ""), out double taxaDeJuros);

                if (cbPeriodo.SelectedIndex == 0) //Tipo "anos" selecionado
                    periodo *= 12;

                var taxaDeJurosMensal = Math.Round(taxaDeJuros / 12, 2);
                var totalInvestido = valorInicial;
                var totalJuros = 0.00;
                var totalAcumulado = valorInicial;
                var mes = 0;

                dgvCalculo.Rows.Add(0,
                                    0,
                                    0.ToString("C2"),
                                    totalInvestido.ToString("C2"),
                                    0.ToString("C2"),
                                    totalAcumulado.ToString("C2"));

                for (int i = 1; i <= periodo; i++)
                {
                    mes++;

                    if (mes > 12)
                        mes = 1;

                    var juros = Math.Round(totalAcumulado * taxaDeJurosMensal / 100, 2);

                    totalInvestido += valorMensal;
                    totalJuros += juros;
                    totalAcumulado = totalInvestido + totalJuros;

                    var anoAtual = i < 12 ? 1 : Math.Ceiling((i / 12d));

                    dgvCalculo.Rows.Add(anoAtual,
                                        mes,
                                        "+ " + juros.ToString("C2"),
                                        totalInvestido.ToString("C2"),
                                        totalJuros.ToString("C2"),
                                        totalAcumulado.ToString("C2"));
                }

                //Ajustar tamanho das células automáticamente
                for (int i = 0; i < dgvCalculo.Columns.Count; i++)
                    dgvCalculo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
              
        private void txtValorInicial_KeyUp(object sender, KeyEventArgs e)
        {
            PularParaProximoControle(sender, e);
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

        private string TransformarEmMoeda(object value)
        {
            if (value is TextBox textBox)
            {
                double.TryParse(textBox.Text.Replace("R$", "").Replace(" ", "").Replace(".", ""), out double moeda);
                return moeda.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            }

            return "";
        }

        private string TransformarEmPercentual(TextBox textBox)
        {
            textBox.Text = textBox.Text.Replace(",", ".");

            if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
            {
                return valor.ToString("0.00", CultureInfo.GetCultureInfo("pt-BR")) + " %";
            }

            return "0,00 %";
        }

        private void PularParaProximoControle(object sender, KeyEventArgs @event)
        {
            if (@event.KeyCode == Keys.Enter)
                SelectNextControl((Control)sender, true, true, true, true);

        }
    }
}
