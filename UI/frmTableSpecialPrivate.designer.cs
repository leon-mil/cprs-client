namespace Cprs
{
    partial class frmTableSpecialPrivate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdNsa = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rdNnsa = new System.Windows.Forms.RadioButton();
            this.rdRsa = new System.Windows.Forms.RadioButton();
            this.rdRnsa = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.dgPrint = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnTable = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(557, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 20);
            this.label5.TabIndex = 38;
            this.label5.Text = "Millions of Dollars.";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle2.Location = new System.Drawing.Point(530, 75);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(277, 20);
            this.lblTitle2.TabIndex = 37;
            this.lblTitle2.Text = "Seasonally Adjusted Annual Rate";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rdNsa);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rdNnsa);
            this.panel1.Controls.Add(this.rdRsa);
            this.panel1.Controls.Add(this.rdRnsa);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Controls.Add(this.dgPrint);
            this.panel1.Location = new System.Drawing.Point(30, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 639);
            this.panel1.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(940, 641);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "* Red = All Time High\'";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(48, 641);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "* Blue = All Time Low\'";
            // 
            // rdNsa
            // 
            this.rdNsa.AutoSize = true;
            this.rdNsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdNsa.Location = new System.Drawing.Point(591, 10);
            this.rdNsa.Name = "rdNsa";
            this.rdNsa.Size = new System.Drawing.Size(151, 20);
            this.rdNsa.TabIndex = 26;
            this.rdNsa.Text = "Nonresidential SA";
            this.rdNsa.UseVisualStyleBackColor = true;
            this.rdNsa.CheckedChanged += new System.EventHandler(this.rdNsa_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(244, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Select :";
            // 
            // rdNnsa
            // 
            this.rdNnsa.AutoSize = true;
            this.rdNnsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNnsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdNnsa.Location = new System.Drawing.Point(748, 8);
            this.rdNnsa.Name = "rdNnsa";
            this.rdNnsa.Size = new System.Drawing.Size(162, 20);
            this.rdNnsa.TabIndex = 25;
            this.rdNnsa.Text = "Nonresidential NSA";
            this.rdNnsa.UseVisualStyleBackColor = true;
            this.rdNnsa.CheckedChanged += new System.EventHandler(this.rdNnsa_CheckedChanged);
            // 
            // rdRsa
            // 
            this.rdRsa.AutoSize = true;
            this.rdRsa.Checked = true;
            this.rdRsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdRsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdRsa.Location = new System.Drawing.Point(310, 8);
            this.rdRsa.Name = "rdRsa";
            this.rdRsa.Size = new System.Drawing.Size(129, 20);
            this.rdRsa.TabIndex = 24;
            this.rdRsa.TabStop = true;
            this.rdRsa.Text = "Residential SA";
            this.rdRsa.UseVisualStyleBackColor = true;
            this.rdRsa.CheckedChanged += new System.EventHandler(this.rdRsa_CheckedChanged);
            // 
            // rdRnsa
            // 
            this.rdRnsa.AutoSize = true;
            this.rdRnsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdRnsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdRnsa.Location = new System.Drawing.Point(445, 8);
            this.rdRnsa.Name = "rdRnsa";
            this.rdRnsa.Size = new System.Drawing.Size(140, 20);
            this.rdRnsa.TabIndex = 23;
            this.rdRnsa.Text = "Residential NSA";
            this.rdRnsa.UseVisualStyleBackColor = true;
            this.rdRnsa.CheckedChanged += new System.EventHandler(this.rdRnsa_CheckedChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.Location = new System.Drawing.Point(6, 36);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgData.RowHeadersVisible = false;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1130, 602);
            this.dgData.TabIndex = 0;
            // 
            // dgPrint
            // 
            this.dgPrint.AllowUserToAddRows = false;
            this.dgPrint.AllowUserToDeleteRows = false;
            this.dgPrint.AllowUserToResizeColumns = false;
            this.dgPrint.AllowUserToResizeRows = false;
            this.dgPrint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPrint.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPrint.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgPrint.Location = new System.Drawing.Point(202, 119);
            this.dgPrint.Name = "dgPrint";
            this.dgPrint.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgPrint.RowHeadersVisible = false;
            this.dgPrint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPrint.Size = new System.Drawing.Size(850, 408);
            this.dgPrint.TabIndex = 31;
            this.dgPrint.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(361, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(579, 25);
            this.lblTitle.TabIndex = 36;
            this.lblTitle.Text = "Value of Private Residential Construction Put in Place";
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(424, 814);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 40;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(724, 814);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 39;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmTableSpecialPrivate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmTableSpecialPrivate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTableSpecialPrivate_FormClosing);
            this.Load += new System.EventHandler(this.frmTableSpecialPrivate_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblTitle2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdNsa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdNnsa;
        private System.Windows.Forms.RadioButton rdRsa;
        private System.Windows.Forms.RadioButton rdRnsa;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.DataGridView dgPrint;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Button btnPrint;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}