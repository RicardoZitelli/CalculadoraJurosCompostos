namespace CalculadoraJurosCompostos
{
    partial class FrmCalculadoraJurosCompostos
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValorInicial = new System.Windows.Forms.TextBox();
            this.txtValorMensal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaxaJuros = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPeriodo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPeriodo = new System.Windows.Forms.ComboBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.dgvCalculo = new System.Windows.Forms.DataGridView();
            this.Ano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Juros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalInvestido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalJuros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAcumulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbInput.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculo)).BeginInit();
            this.SuspendLayout();
            // 
            // gbInput
            // 
            this.gbInput.Controls.Add(this.btnCalcular);
            this.gbInput.Controls.Add(this.cbPeriodo);
            this.gbInput.Controls.Add(this.txtPeriodo);
            this.gbInput.Controls.Add(this.label4);
            this.gbInput.Controls.Add(this.txtTaxaJuros);
            this.gbInput.Controls.Add(this.label3);
            this.gbInput.Controls.Add(this.txtValorMensal);
            this.gbInput.Controls.Add(this.label2);
            this.gbInput.Controls.Add(this.txtValorInicial);
            this.gbInput.Controls.Add(this.label1);
            this.gbInput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInput.Location = new System.Drawing.Point(12, 12);
            this.gbInput.Name = "gbInput";
            this.gbInput.Size = new System.Drawing.Size(370, 426);
            this.gbInput.TabIndex = 0;
            this.gbInput.TabStop = false;
            this.gbInput.Text = "Entradas";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvCalculo);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(388, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(682, 426);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cálculo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Valor Inicial";
            // 
            // txtValorInicial
            // 
            this.txtValorInicial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorInicial.Location = new System.Drawing.Point(9, 38);
            this.txtValorInicial.Name = "txtValorInicial";
            this.txtValorInicial.Size = new System.Drawing.Size(105, 23);
            this.txtValorInicial.TabIndex = 1;
            this.txtValorInicial.Click += new System.EventHandler(this.txtValorInicial_Click);
            this.txtValorInicial.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtValorInicial_KeyUp);
            this.txtValorInicial.Leave += new System.EventHandler(this.txtValorInicial_Leave);
            // 
            // txtValorMensal
            // 
            this.txtValorMensal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorMensal.Location = new System.Drawing.Point(125, 37);
            this.txtValorMensal.Name = "txtValorMensal";
            this.txtValorMensal.Size = new System.Drawing.Size(105, 23);
            this.txtValorMensal.TabIndex = 3;
            this.txtValorMensal.Click += new System.EventHandler(this.txtValorMensal_Click);
            this.txtValorMensal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtValorMensal_KeyUp);
            this.txtValorMensal.Leave += new System.EventHandler(this.txtValorMensal_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Valor Mensal";
            // 
            // txtTaxaJuros
            // 
            this.txtTaxaJuros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxaJuros.Location = new System.Drawing.Point(9, 91);
            this.txtTaxaJuros.Name = "txtTaxaJuros";
            this.txtTaxaJuros.Size = new System.Drawing.Size(105, 23);
            this.txtTaxaJuros.TabIndex = 5;
            this.txtTaxaJuros.Click += new System.EventHandler(this.txtTaxaJuros_Click);
            this.txtTaxaJuros.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTaxaJuros_KeyUp);
            this.txtTaxaJuros.Leave += new System.EventHandler(this.txtTaxaJuros_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Taxa de Juros Anual";
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPeriodo.Location = new System.Drawing.Point(125, 91);
            this.txtPeriodo.Name = "txtPeriodo";
            this.txtPeriodo.Size = new System.Drawing.Size(105, 23);
            this.txtPeriodo.TabIndex = 7;
            this.txtPeriodo.Click += new System.EventHandler(this.txtPeriodo_Click);
            this.txtPeriodo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPeriodo_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(122, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Perído em";
            // 
            // cbPeriodo
            // 
            this.cbPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriodo.FormattingEnabled = true;
            this.cbPeriodo.Items.AddRange(new object[] {
            "Anos",
            "Meses"});
            this.cbPeriodo.Location = new System.Drawing.Point(236, 91);
            this.cbPeriodo.Name = "cbPeriodo";
            this.cbPeriodo.Size = new System.Drawing.Size(121, 23);
            this.cbPeriodo.TabIndex = 8;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(9, 132);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(348, 23);
            this.btnCalcular.TabIndex = 9;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // dgvCalculo
            // 
            this.dgvCalculo.AllowUserToAddRows = false;
            this.dgvCalculo.AllowUserToDeleteRows = false;
            this.dgvCalculo.AllowUserToResizeColumns = false;
            this.dgvCalculo.AllowUserToResizeRows = false;
            this.dgvCalculo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCalculo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalculo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ano,
            this.Mes,
            this.Juros,
            this.TotalInvestido,
            this.TotalJuros,
            this.TotalAcumulado});
            this.dgvCalculo.Location = new System.Drawing.Point(6, 20);
            this.dgvCalculo.Name = "dgvCalculo";
            this.dgvCalculo.ReadOnly = true;
            this.dgvCalculo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvCalculo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCalculo.Size = new System.Drawing.Size(670, 400);
            this.dgvCalculo.TabIndex = 0;
            // 
            // Ano
            // 
            this.Ano.HeaderText = "Ano";
            this.Ano.Name = "Ano";
            this.Ano.ReadOnly = true;
            // 
            // Mes
            // 
            this.Mes.HeaderText = "Mês";
            this.Mes.Name = "Mes";
            this.Mes.ReadOnly = true;
            // 
            // Juros
            // 
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Juros.DefaultCellStyle = dataGridViewCellStyle37;
            this.Juros.HeaderText = "Juros Mensais ($)";
            this.Juros.Name = "Juros";
            this.Juros.ReadOnly = true;
            // 
            // TotalInvestido
            // 
            this.TotalInvestido.HeaderText = "Total Investido";
            this.TotalInvestido.Name = "TotalInvestido";
            this.TotalInvestido.ReadOnly = true;
            // 
            // TotalJuros
            // 
            this.TotalJuros.HeaderText = "Total de Juros";
            this.TotalJuros.Name = "TotalJuros";
            this.TotalJuros.ReadOnly = true;
            // 
            // TotalAcumulado
            // 
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TotalAcumulado.DefaultCellStyle = dataGridViewCellStyle38;
            this.TotalAcumulado.HeaderText = "Total Acumulado";
            this.TotalAcumulado.Name = "TotalAcumulado";
            this.TotalAcumulado.ReadOnly = true;
            // 
            // FrmCalculadoraJurosCompostos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbInput);
            this.Name = "FrmCalculadoraJurosCompostos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cacluladora de Juros Compostos";
            this.Load += new System.EventHandler(this.FrmCalculadoraJurosCompostos_Load);
            this.gbInput.ResumeLayout(false);
            this.gbInput.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInput;
        private System.Windows.Forms.TextBox txtValorInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbPeriodo;
        private System.Windows.Forms.TextBox txtPeriodo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTaxaJuros;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValorMensal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.DataGridView dgvCalculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ano;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Juros;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalInvestido;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalJuros;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAcumulado;
    }
}

