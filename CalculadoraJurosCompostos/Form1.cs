using System;
using System.Globalization;
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
            AjustarCelulas();
            cbPeriodo.SelectedIndex = 0;
            txtValorInicial.Focus();            
            
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                LimparDataGridView();

                IniciarVariaveis(out double valorMensal,
                    out int periodo,
                    out double taxaDeJurosMensal,
                    out double totalInvestido,                   
                    out double totalAcumulado);

                IncluirValoresIniciaisAoDataGridView(totalInvestido, totalAcumulado);

                Processar(valorMensal
                    , periodo
                    , taxaDeJurosMensal
                    , totalInvestido
                    , default
                    , totalAcumulado                    
                    , default);

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

        private void IniciarVariaveis(out double valorMensal
            ,out int periodo
            ,out double taxaDeJurosMensal
            ,out double totalInvestido
            ,out double totalAcumulado)
        {
            double.TryParse(txtValorInicial.Text.Replace("R$", "").Replace(" ", "").Replace(".", ""), out double valorInicial);
            double.TryParse(txtValorMensal.Text.Replace("R$", "").Replace(" ", "").Replace(".", ""), out valorMensal);
            int.TryParse(txtPeriodo.Text, out periodo);
            double.TryParse(txtTaxaJuros.Text.Replace("%", "").Replace(" ", ""), out double taxaDeJuros);

            taxaDeJurosMensal = Math.Round(taxaDeJuros / 12, 2);
            totalInvestido = valorInicial;          
            totalAcumulado = valorInicial;
           
            if (cbPeriodo.SelectedIndex == 0) //Tipo "anos" selecionado
                periodo *= 12;
        }

        private void IncluirValoresIniciaisAoDataGridView(double totalInvestido, double totalAcumulado)
        {
            dgvCalculo.Rows.Add(0
                ,0
                ,0.ToString("C2")
                ,totalInvestido.ToString("C2")
                ,0.ToString("C2")
                ,totalAcumulado.ToString("C2"));
        }
              
        private void Processar(double valorMensal, int periodo, double taxaDeJurosMensal, double totalInvestido, double juros, double totalAcumulado, int mes)
        {
            var investimento = new Investimento(valorMensal:valorMensal
                ,taxaDeJurosMensal: taxaDeJurosMensal
                ,periodo: periodo                
                ,totalInvestido: totalInvestido
                ,totalJuros: default
                ,totalAcumulado: totalAcumulado
                ,juros: juros
                ,ano: default
                ,mes: default);

            for (int i = 1; i <= investimento.Periodo; i++)
            {                
                investimento.IdentificarAno(i);
                investimento.IdentificarMes();   
                
                investimento = investimento.Processar();

                AdicionarValoresAoDataGridView(investimento);                
            }
        }

        private void AdicionarValoresAoDataGridView(Investimento investimento)
        {
            dgvCalculo.Rows.Add(investimento.Ano,
                                           investimento.Mes,
                                           "+ " + investimento.Juros.ToString("C2"),
                                           investimento.TotalInvestido.ToString("C2"),
                                           investimento.TotalJuros.ToString("C2"),
                                           investimento.TotalAcumulado.ToString("C2"));
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

        private void cbPeriodo_KeyUp(object sender, KeyEventArgs e)
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
