namespace Cprs
{
    partial class frmTableStandardErrors
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblt32 = new System.Windows.Forms.Label();
            this.lblt31 = new System.Windows.Forms.Label();
            this.rdf = new System.Windows.Forms.RadioButton();
            this.rdv = new System.Windows.Forms.RadioButton();
            this.rds = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rdt = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnAnnual = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(579, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 52;
            this.label2.Text = "(percent)";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(249, 48);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(742, 25);
            this.lblTitle.TabIndex = 51;
            this.lblTitle.Text = "Coefficients of Variation and Standard Errors by Type of Construction";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblt32);
            this.panel1.Controls.Add(this.lblt31);
            this.panel1.Controls.Add(this.rdf);
            this.panel1.Controls.Add(this.rdv);
            this.panel1.Controls.Add(this.rds);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rdt);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Location = new System.Drawing.Point(43, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1129, 635);
            this.panel1.TabIndex = 50;
            // 
            // lblt32
            // 
            this.lblt32.BackColor = System.Drawing.SystemColors.Control;
            this.lblt32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblt32.Location = new System.Drawing.Point(598, 50);
            this.lblt32.Name = "lblt32";
            this.lblt32.Size = new System.Drawing.Size(467, 13);
            this.lblt32.TabIndex = 50;
            this.lblt32.Text = "Standard error";
            this.lblt32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblt31
            // 
            this.lblt31.BackColor = System.Drawing.SystemColors.Control;
            this.lblt31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblt31.Location = new System.Drawing.Point(292, 50);
            this.lblt31.Name = "lblt31";
            this.lblt31.Size = new System.Drawing.Size(300, 13);
            this.lblt31.TabIndex = 49;
            this.lblt31.Text = "Coefficient of Variation";
            this.lblt31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdf
            // 
            this.rdf.AutoSize = true;
            this.rdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdf.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdf.Location = new System.Drawing.Point(716, 14);
            this.rdf.Name = "rdf";
            this.rdf.Size = new System.Drawing.Size(80, 20);
            this.rdf.TabIndex = 37;
            this.rdf.Text = "Federal";
            this.rdf.UseVisualStyleBackColor = true;
            this.rdf.CheckedChanged += new System.EventHandler(this.rdf_CheckedChanged);
            // 
            // rdv
            // 
            this.rdv.AutoSize = true;
            this.rdv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdv.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdv.Location = new System.Drawing.Point(484, 14);
            this.rdv.Name = "rdv";
            this.rdv.Size = new System.Drawing.Size(75, 20);
            this.rdv.TabIndex = 25;
            this.rdv.Text = "Private";
            this.rdv.UseVisualStyleBackColor = true;
            this.rdv.CheckedChanged += new System.EventHandler(this.rdv_CheckedChanged);
            // 
            // rds
            // 
            this.rds.AutoSize = true;
            this.rds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rds.ForeColor = System.Drawing.Color.DarkBlue;
            this.rds.Location = new System.Drawing.Point(576, 14);
            this.rds.Name = "rds";
            this.rds.Size = new System.Drawing.Size(134, 20);
            this.rds.TabIndex = 35;
            this.rds.Text = "State and Local";
            this.rds.UseVisualStyleBackColor = true;
            this.rds.CheckedChanged += new System.EventHandler(this.rds_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(280, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "Select Survey:";
            // 
            // rdt
            // 
            this.rdt.AutoSize = true;
            this.rdt.Checked = true;
            this.rdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdt.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdt.Location = new System.Drawing.Point(412, 14);
            this.rdt.Name = "rdt";
            this.rdt.Size = new System.Drawing.Size(62, 20);
            this.rdt.TabIndex = 23;
            this.rdt.TabStop = true;
            this.rdt.Text = "Total";
            this.rdt.UseVisualStyleBackColor = true;
            this.rdt.CheckedChanged += new System.EventHandler(this.rdt_CheckedChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgData.Location = new System.Drawing.Point(29, 66);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgData.RowHeadersVisible = false;
            this.dgData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1061, 552);
            this.dgData.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(663, 792);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 54;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(424, 792);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 53;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnAnnual
            // 
            this.btnAnnual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnnual.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAnnual.Location = new System.Drawing.Point(545, 754);
            this.btnAnnual.Name = "btnAnnual";
            this.btnAnnual.Size = new System.Drawing.Size(138, 23);
            this.btnAnnual.TabIndex = 55;
            this.btnAnnual.TabStop = false;
            this.btnAnnual.Text = "ANNUAL TABLES";
            this.btnAnnual.UseVisualStyleBackColor = true;
            this.btnAnnual.Click += new System.EventHandler(this.btnAnnual_Click);
            // 
            // frmTableStandardErrors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 853);
            this.Controls.Add(this.btnAnnual);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Name = "frmTableStandardErrors";
            this.Load += new System.EventHandler(this.frmTableStandardErrors_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnAnnual, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdf;
        private System.Windows.Forms.RadioButton rdv;
        private System.Windows.Forms.RadioButton rds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdt;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Label lblt32;
        private System.Windows.Forms.Label lblt31;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnAnnual;
    }
}