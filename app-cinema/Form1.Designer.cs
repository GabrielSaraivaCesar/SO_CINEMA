
namespace app_cinema
{
    partial class Form1
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
            this.salaFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label_arquivo_sala = new System.Windows.Forms.Label();
            this.button_config_sala = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_arquivo_compra_ingressos = new System.Windows.Forms.Label();
            this.button_compras = new System.Windows.Forms.Button();
            this.btn_simular = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.compras_log = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label_info_sessoes = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_info_dimensoes = new System.Windows.Forms.Label();
            this.comprasFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.logs_textbox = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_runtime = new System.Windows.Forms.Label();
            this.btn_extrair = new System.Windows.Forms.Button();
            this.saveLogsDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // salaFileDialog
            // 
            this.salaFileDialog.FileName = "salaFileDialog";
            // 
            // label_arquivo_sala
            // 
            this.label_arquivo_sala.AutoSize = true;
            this.label_arquivo_sala.Location = new System.Drawing.Point(152, 63);
            this.label_arquivo_sala.Name = "label_arquivo_sala";
            this.label_arquivo_sala.Size = new System.Drawing.Size(145, 13);
            this.label_arquivo_sala.TabIndex = 2;
            this.label_arquivo_sala.Text = "Nenhum arquivo selecionado";
            // 
            // button_config_sala
            // 
            this.button_config_sala.Location = new System.Drawing.Point(12, 58);
            this.button_config_sala.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.button_config_sala.Name = "button_config_sala";
            this.button_config_sala.Size = new System.Drawing.Size(134, 23);
            this.button_config_sala.TabIndex = 1;
            this.button_config_sala.Text = "Escolher arquivo (.txt)";
            this.button_config_sala.UseVisualStyleBackColor = true;
            this.button_config_sala.Click += new System.EventHandler(this.button_config_sala_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configurações da sala";
            // 
            // label_arquivo_compra_ingressos
            // 
            this.label_arquivo_compra_ingressos.AutoSize = true;
            this.label_arquivo_compra_ingressos.Location = new System.Drawing.Point(156, 145);
            this.label_arquivo_compra_ingressos.Name = "label_arquivo_compra_ingressos";
            this.label_arquivo_compra_ingressos.Size = new System.Drawing.Size(145, 13);
            this.label_arquivo_compra_ingressos.TabIndex = 5;
            this.label_arquivo_compra_ingressos.Text = "Nenhum arquivo selecionado";
            // 
            // button_compras
            // 
            this.button_compras.Location = new System.Drawing.Point(16, 140);
            this.button_compras.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.button_compras.Name = "button_compras";
            this.button_compras.Size = new System.Drawing.Size(134, 23);
            this.button_compras.TabIndex = 4;
            this.button_compras.Text = "Escolher arquivo (.txt)";
            this.button_compras.UseVisualStyleBackColor = true;
            this.button_compras.Click += new System.EventHandler(this.button_compras_Click);
            // 
            // btn_simular
            // 
            this.btn_simular.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btn_simular.Location = new System.Drawing.Point(16, 206);
            this.btn_simular.Name = "btn_simular";
            this.btn_simular.Size = new System.Drawing.Size(311, 48);
            this.btn_simular.TabIndex = 6;
            this.btn_simular.Text = "Simular";
            this.btn_simular.UseVisualStyleBackColor = true;
            this.btn_simular.Click += new System.EventHandler(this.btn_simular_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Dimensões:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 501);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_label
            // 
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(0, 17);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Requisições de compras";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.compras_log);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label_info_sessoes);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label_info_dimensoes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(333, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 228);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacoes da sala";
            // 
            // compras_log
            // 
            this.compras_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compras_log.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.compras_log.Location = new System.Drawing.Point(9, 125);
            this.compras_log.Name = "compras_log";
            this.compras_log.ReadOnly = true;
            this.compras_log.Size = new System.Drawing.Size(540, 93);
            this.compras_log.TabIndex = 11;
            this.compras_log.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 107);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Requisições de Compras:";
            // 
            // label_info_sessoes
            // 
            this.label_info_sessoes.AutoSize = true;
            this.label_info_sessoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label_info_sessoes.Location = new System.Drawing.Point(6, 82);
            this.label_info_sessoes.Name = "label_info_sessoes";
            this.label_info_sessoes.Size = new System.Drawing.Size(61, 15);
            this.label_info_sessoes.TabIndex = 10;
            this.label_info_sessoes.Text = "Indefinido";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 67);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Sessões:";
            // 
            // label_info_dimensoes
            // 
            this.label_info_dimensoes.AutoSize = true;
            this.label_info_dimensoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label_info_dimensoes.Location = new System.Drawing.Point(6, 42);
            this.label_info_dimensoes.Name = "label_info_dimensoes";
            this.label_info_dimensoes.Size = new System.Drawing.Size(61, 15);
            this.label_info_dimensoes.TabIndex = 8;
            this.label_info_dimensoes.Text = "Indefinido";
            // 
            // comprasFileDialog
            // 
            this.comprasFileDialog.FileName = "comprasFileDialog";
            // 
            // logs_textbox
            // 
            this.logs_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logs_textbox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.logs_textbox.Location = new System.Drawing.Point(16, 285);
            this.logs_textbox.Name = "logs_textbox";
            this.logs_textbox.ReadOnly = true;
            this.logs_textbox.Size = new System.Drawing.Size(866, 189);
            this.logs_textbox.TabIndex = 12;
            this.logs_textbox.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 267);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Logs:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 477);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Tempo de execução:";
            // 
            // label_runtime
            // 
            this.label_runtime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_runtime.AutoSize = true;
            this.label_runtime.Location = new System.Drawing.Point(145, 477);
            this.label_runtime.Name = "label_runtime";
            this.label_runtime.Size = new System.Drawing.Size(26, 13);
            this.label_runtime.TabIndex = 15;
            this.label_runtime.Text = "0ms";
            // 
            // btn_extrair
            // 
            this.btn_extrair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_extrair.Location = new System.Drawing.Point(786, 287);
            this.btn_extrair.Name = "btn_extrair";
            this.btn_extrair.Size = new System.Drawing.Size(94, 29);
            this.btn_extrair.TabIndex = 16;
            this.btn_extrair.Text = "Extair Logs";
            this.btn_extrair.UseVisualStyleBackColor = true;
            this.btn_extrair.Click += new System.EventHandler(this.btn_extrair_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 523);
            this.Controls.Add(this.btn_extrair);
            this.Controls.Add(this.label_runtime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.logs_textbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_simular);
            this.Controls.Add(this.label_arquivo_compra_ingressos);
            this.Controls.Add(this.button_compras);
            this.Controls.Add(this.button_config_sala);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_arquivo_sala);
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog salaFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_config_sala;
        private System.Windows.Forms.Label label_arquivo_sala;
        private System.Windows.Forms.Label label_arquivo_compra_ingressos;
        private System.Windows.Forms.Button button_compras;
        private System.Windows.Forms.Button btn_simular;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox compras_log;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_info_sessoes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_info_dimensoes;
        private System.Windows.Forms.OpenFileDialog comprasFileDialog;
        private System.Windows.Forms.RichTextBox logs_textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_runtime;
        private System.Windows.Forms.Button btn_extrair;
        private System.Windows.Forms.SaveFileDialog saveLogsDialog;
    }
}

