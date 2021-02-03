namespace Cprs
{
    partial class frmImpMonthProc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.dgMonProc = new System.Windows.Forms.DataGridView();
            this.btnRunTabulations = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnRunCELoad = new System.Windows.Forms.Button();
            this.btnRunVIPLoad = new System.Windows.Forms.Button();
            this.btnRunForecasting = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgMonProc)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(369, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(467, 25);
            this.label2.TabIndex = 23;
            this.label2.Text = "IMPROVEMENTS MONTHLY PROCESSING";
            // 
            // dgMonProc
            // 
            this.dgMonProc.AllowUserToAddRows = false;
            this.dgMonProc.AllowUserToDeleteRows = false;
            this.dgMonProc.AllowUserToResizeColumns = false;
            this.dgMonProc.AllowUserToResizeRows = false;
            this.dgMonProc.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMonProc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMonProc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMonProc.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgMonProc.Location = new System.Drawing.Point(24, 131);
            this.dgMonProc.Name = "dgMonProc";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMonProc.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgMonProc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMonProc.Size = new System.Drawing.Size(1169, 559);
            this.dgMonProc.TabIndex = 24;
            this.dgMonProc.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgMonProc_CellFormatting);
            // 
            // btnRunTabulations
            // 
            this.btnRunTabulations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunTabulations.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRunTabulations.Location = new System.Drawing.Point(383, 730);
            this.btnRunTabulations.Name = "btnRunTabulations";
            this.btnRunTabulations.Size = new System.Drawing.Size(133, 23);
            this.btnRunTabulations.TabIndex = 27;
            this.btnRunTabulations.TabStop = false;
            this.btnRunTabulations.Text = "RUN TABULATIONS";
            this.btnRunTabulations.UseVisualStyleBackColor = true;
            this.btnRunTabulations.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnRunTabulations.Click += new System.EventHandler(this.btnRunTabulations_Click);
            this.btnRunTabulations.Paint += new System.Windows.Forms.PaintEventHandler(this.btnRunTabulations_Paint);
            // 
            // btnRunCELoad
            // 
            this.btnRunCELoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunCELoad.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRunCELoad.Location = new System.Drawing.Point(158, 730);
            this.btnRunCELoad.Name = "btnRunCELoad";
            this.btnRunCELoad.Size = new System.Drawing.Size(133, 23);
            this.btnRunCELoad.TabIndex = 26;
            this.btnRunCELoad.TabStop = false;
            this.btnRunCELoad.Text = "RUN CE LOAD";
            this.btnRunCELoad.UseVisualStyleBackColor = true;
            this.btnRunCELoad.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnRunCELoad.Click += new System.EventHandler(this.btnRunCELoad_Click);
            this.btnRunCELoad.Paint += new System.Windows.Forms.PaintEventHandler(this.btnRunCELoad_Paint);
            // 
            // btnRunVIPLoad
            // 
            this.btnRunVIPLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunVIPLoad.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRunVIPLoad.Location = new System.Drawing.Point(884, 730);
            this.btnRunVIPLoad.Name = "btnRunVIPLoad";
            this.btnRunVIPLoad.Size = new System.Drawing.Size(133, 23);
            this.btnRunVIPLoad.TabIndex = 29;
            this.btnRunVIPLoad.TabStop = false;
            this.btnRunVIPLoad.Text = "RUN VIP LOAD";
            this.btnRunVIPLoad.UseVisualStyleBackColor = true;
            this.btnRunVIPLoad.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnRunVIPLoad.Click += new System.EventHandler(this.btnRunVIPLoad_Click);
            this.btnRunVIPLoad.Paint += new System.Windows.Forms.PaintEventHandler(this.btnRunVIPLoad_Paint);
            // 
            // btnRunForecasting
            // 
            this.btnRunForecasting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunForecasting.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRunForecasting.Location = new System.Drawing.Point(640, 730);
            this.btnRunForecasting.Name = "btnRunForecasting";
            this.btnRunForecasting.Size = new System.Drawing.Size(133, 23);
            this.btnRunForecasting.TabIndex = 28;
            this.btnRunForecasting.TabStop = false;
            this.btnRunForecasting.Text = "RUN FORECASTING";
            this.btnRunForecasting.UseVisualStyleBackColor = true;
            this.btnRunForecasting.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnRunForecasting.Click += new System.EventHandler(this.btnRunForecasting_Click);
            this.btnRunForecasting.Paint += new System.Windows.Forms.PaintEventHandler(this.btnRunForecasting_Paint);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRefresh.Location = new System.Drawing.Point(514, 814);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(133, 23);
            this.btnRefresh.TabIndex = 30;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnReset.Location = new System.Drawing.Point(514, 773);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(133, 23);
            this.btnReset.TabIndex = 31;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.Paint += new System.Windows.Forms.PaintEventHandler(this.btnReset_Paint);
            // 
            // frmImpMonthProc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnRunVIPLoad);
            this.Controls.Add(this.btnRunForecasting);
            this.Controls.Add(this.btnRunTabulations);
            this.Controls.Add(this.btnRunCELoad);
            this.Controls.Add(this.dgMonProc);
            this.Controls.Add(this.label2);
            this.Name = "frmImpMonthProc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImpMonthProc_FormClosing);
            this.Load += new System.EventHandler(this.frmImpMonthProc_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dgMonProc, 0);
            this.Controls.SetChildIndex(this.btnRunCELoad, 0);
            this.Controls.SetChildIndex(this.btnRunTabulations, 0);
            this.Controls.SetChildIndex(this.btnRunForecasting, 0);
            this.Controls.SetChildIndex(this.btnRunVIPLoad, 0);
            this.Controls.SetChildIndex(this.btnRefresh, 0);
            this.Controls.SetChildIndex(this.btnReset, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgMonProc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgMonProc;
        private System.Windows.Forms.Button btnRunTabulations;
        private System.Windows.Forms.Button btnRunCELoad;
        private System.Windows.Forms.Button btnRunVIPLoad;
        private System.Windows.Forms.Button btnRunForecasting;
        private System.Windows.Forms.Button btnRefresh;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnReset;
    }
}