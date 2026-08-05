using System;
using System.Globalization;
using System.Windows.Forms;
using CalculadoraJurosCompostos.Aplicacao;
using CalculadoraJurosCompostos.Mascaras;

namespace CalculadoraJurosCompostos
{
    public partial class FrmCalculadoraJurosCompostos : Form
    {
        private readonly ISimularInvestimento _simularInvestimento;

        private CampoComMascara _valorInicial = null!;
        private CampoComMascara _valorMensal = null!;
        private CampoComMascara _taxaDeJuros = null!;

        public FrmCalculadoraJurosCompostos(ISimularInvestimento simularInvestimento)
        {
            _simularInvestimento = simularInvestimento ?? throw new ArgumentNullException(nameof(simularInvestimento));

            InitializeComponent();
            AplicarMascaras();
        }

        private void AplicarMascaras()
        {
            _valorInicial = new CampoComMascara(txtValorInicial, MascaraDeValor.Moeda);
            _valorMensal = new CampoComMascara(txtValorMensal, MascaraDeValor.Moeda);
            _taxaDeJuros = new CampoComMascara(txtTaxaJuros, MascaraDeValor.Percentual);
        }

        private void FrmCalculadoraJurosCompostos_Load(object? sender, EventArgs e)
        {
            AjustarCelulas();
            cbPeriodo.SelectedIndex = 0;
            txtValorInicial.Focus();
        }

        private void btnCalcular_Click(object? sender, EventArgs e)
        {
            try
            {
                LimparDataGridView();

                SimulacaoResponse resposta = _simularInvestimento.Executar(LerRequest());

                ExibirResultado(resposta);
                AjustarCelulas();
            }
            catch (SimulacaoInvalidaException ex)
            {
                LimparDataGridView();
                MessageBox.Show(ex.Message, "Dados incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LimparDataGridView();
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Lê os campos da tela e monta a entrada do caso de uso. A tela só informa em que
        /// unidade o prazo foi digitado; converter isso em meses é regra e fica no domínio.
        /// </summary>
        private SimulacaoRequest LerRequest()
        {
            int.TryParse(txtPeriodo.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int periodo);

            return new SimulacaoRequest(
                ValorInicial: _valorInicial.Valor,
                AporteMensal: _valorMensal.Valor,
                TaxaAnual: _taxaDeJuros.Valor,
                Periodo: periodo,
                TipoPeriodo: cbPeriodo.SelectedIndex == 0 ? TipoPeriodo.Anos : TipoPeriodo.Meses);
        }

        private void ExibirResultado(SimulacaoResponse resposta)
        {
            IncluirValoresIniciaisAoDataGridView(resposta.ValorInicial);

            foreach (EvolucaoMensalResponse evolucao in resposta.Evolucao)
                AdicionarValoresAoDataGridView(evolucao);
        }

        private void LimparDataGridView()
        {
            dgvCalculo.Rows.Clear();
        }

        private void IncluirValoresIniciaisAoDataGridView(decimal valorInicial)
        {
            dgvCalculo.Rows.Add(0
                ,0
                ,EmMoeda(decimal.Zero)
                ,EmMoeda(valorInicial)
                ,EmMoeda(decimal.Zero)
                ,EmMoeda(valorInicial));
        }

        private void AdicionarValoresAoDataGridView(EvolucaoMensalResponse evolucao)
        {
            dgvCalculo.Rows.Add(evolucao.Ano,
                                           evolucao.Mes,
                                           "+ " + EmMoeda(evolucao.Juros),
                                           EmMoeda(evolucao.TotalInvestido),
                                           EmMoeda(evolucao.TotalJuros),
                                           EmMoeda(evolucao.TotalAcumulado));
        }

        /// <summary>
        /// Reaproveita a formatação da máscara de moeda: a decisão de como exibir um valor
        /// em real fica num lugar só, valendo tanto para os campos quanto para o grid.
        /// </summary>
        private static string EmMoeda(decimal valor) => MascaraDeValor.Moeda.Formatar(valor);

        private void AjustarCelulas()
        {
            for (int i = 0; i < dgvCalculo.Columns.Count; i++)
                dgvCalculo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void txtValorInicial_KeyUp(object? sender, KeyEventArgs e) => PularParaProximoControle(sender, e);

        private void txtValorMensal_KeyUp(object? sender, KeyEventArgs e) => PularParaProximoControle(sender, e);

        private void txtTaxaJuros_KeyUp(object? sender, KeyEventArgs e) => PularParaProximoControle(sender, e);

        private void txtPeriodo_KeyUp(object? sender, KeyEventArgs e) => PularParaProximoControle(sender, e);

        private void cbPeriodo_KeyUp(object? sender, KeyEventArgs e) => PularParaProximoControle(sender, e);

        private void txtPeriodo_Click(object? sender, EventArgs e) => txtPeriodo.SelectAll();

        private void PularParaProximoControle(object? sender, KeyEventArgs @event)
        {
            if (@event.KeyCode == Keys.Enter && sender is Control controle)
                SelectNextControl(controle, true, true, true, true);
        }
    }
}
