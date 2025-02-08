namespace Cprs
{
    partial class frmTimeExternal
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlType = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rdnsa = new System.Windows.Forms.RadioButton();
            this.rdsa = new System.Windows.Forms.RadioButton();
            this.pnlSurvey = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.rdf = new System.Windows.Forms.RadioButton();
            this.rdt = new System.Windows.Forms.RadioButton();
            this.rds = new System.Windows.Forms.RadioButton();
            this.rdp = new System.Windows.Forms.RadioButton();
            this.rdv = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.h = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTable = new System.Windows.Forms.Button();
            this.lblSA = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.pnlType.SuspendLayout();
            this.pnlSurvey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(394, 42);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(433, 25);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "Value of Total Construction Put in Place";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlType);
            this.panel1.Controls.Add(this.pnlSurvey);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Location = new System.Drawing.Point(44, 130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1116, 658);
            this.panel1.TabIndex = 13;
            // 
            // pnlType
            // 
            this.pnlType.Controls.Add(this.label4);
            this.pnlType.Controls.Add(this.rdnsa);
            this.pnlType.Controls.Add(this.rdsa);
            this.pnlType.Location = new System.Drawing.Point(359, 25);
            this.pnlType.Name = "pnlType";
            this.pnlType.Size = new System.Drawing.Size(437, 27);
            this.pnlType.TabIndex = 44;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(86, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 42;
            this.label4.Text = "Select Type:";
            // 
            // rdnsa
            // 
            this.rdnsa.AutoSize = true;
            this.rdnsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdnsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdnsa.Location = new System.Drawing.Point(269, 4);
            this.rdnsa.Name = "rdnsa";
            this.rdnsa.Size = new System.Drawing.Size(57, 20);
            this.rdnsa.TabIndex = 38;
            this.rdnsa.Text = "NSA";
            this.rdnsa.UseVisualStyleBackColor = true;
            this.rdnsa.CheckedChanged += new System.EventHandler(this.RadioType_CheckedChanged);
            // 
            // rdsa
            // 
            this.rdsa.AutoSize = true;
            this.rdsa.Checked = true;
            this.rdsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdsa.Location = new System.Drawing.Point(201, 4);
            this.rdsa.Name = "rdsa";
            this.rdsa.Size = new System.Drawing.Size(46, 20);
            this.rdsa.TabIndex = 36;
            this.rdsa.TabStop = true;
            this.rdsa.Text = "SA";
            this.rdsa.UseVisualStyleBackColor = true;
            this.rdsa.CheckedChanged += new System.EventHandler(this.RadioType_CheckedChanged);
            // 
            // pnlSurvey
            // 
            this.pnlSurvey.Controls.Add(this.label3);
            this.pnlSurvey.Controls.Add(this.rdf);
            this.pnlSurvey.Controls.Add(this.rdt);
            this.pnlSurvey.Controls.Add(this.rds);
            this.pnlSurvey.Controls.Add(this.rdp);
            this.pnlSurvey.Controls.Add(this.rdv);
            this.pnlSurvey.Location = new System.Drawing.Point(193, 3);
            this.pnlSurvey.Name = "pnlSurvey";
            this.pnlSurvey.Size = new System.Drawing.Size(737, 22);
            this.pnlSurvey.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(61, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "Select Survey:";
            // 
            // rdf
            // 
            this.rdf.AutoSize = true;
            this.rdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdf.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdf.Location = new System.Drawing.Point(612, -2);
            this.rdf.Name = "rdf";
            this.rdf.Size = new System.Drawing.Size(80, 20);
            this.rdf.TabIndex = 37;
            this.rdf.Text = "Federal";
            this.rdf.UseVisualStyleBackColor = true;
            this.rdf.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rdt
            // 
            this.rdt.AutoSize = true;
            this.rdt.Checked = true;
            this.rdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdt.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdt.Location = new System.Drawing.Point(192, -2);
            this.rdt.Name = "rdt";
            this.rdt.Size = new System.Drawing.Size(62, 20);
            this.rdt.TabIndex = 23;
            this.rdt.TabStop = true;
            this.rdt.Text = "Total";
            this.rdt.UseVisualStyleBackColor = true;
            this.rdt.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rds
            // 
            this.rds.AutoSize = true;
            this.rds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rds.ForeColor = System.Drawing.Color.DarkBlue;
            this.rds.Location = new System.Drawing.Point(456, -2);
            this.rds.Name = "rds";
            this.rds.Size = new System.Drawing.Size(134, 20);
            this.rds.TabIndex = 35;
            this.rds.Text = "State and Local";
            this.rds.UseVisualStyleBackColor = true;
            this.rds.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rdp
            // 
            this.rdp.AutoSize = true;
            this.rdp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdp.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdp.Location = new System.Drawing.Point(367, -2);
            this.rdp.Name = "rdp";
            this.rdp.Size = new System.Drawing.Size(69, 20);
            this.rdp.TabIndex = 33;
            this.rdp.Text = "Public";
            this.rdp.UseVisualStyleBackColor = true;
            this.rdp.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rdv
            // 
            this.rdv.AutoSize = true;
            this.rdv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdv.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdv.Location = new System.Drawing.Point(273, -2);
            this.rdv.Name = "rdv";
            this.rdv.Size = new System.Drawing.Size(75, 20);
            this.rdv.TabIndex = 25;
            this.rdv.Text = "Private";
            this.rdv.UseVisualStyleBackColor = true;
            this.rdv.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeight = 4;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.h});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.Location = new System.Drawing.Point(17, 62);
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
            this.dgData.Size = new System.Drawing.Size(1081, 593);
            this.dgData.TabIndex = 0;
            this.dgData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgData_CellFormatting);
            // 
            // h
            // 
            this.h.DataPropertyName = "date6";
            this.h.HeaderText = "Column1";
            this.h.Name = "h";
            this.h.ReadOnly = true;
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(510, 794);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 34;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // lblSA
            // 
            this.lblSA.AutoSize = true;
            this.lblSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSA.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblSA.Location = new System.Drawing.Point(490, 67);
            this.lblSA.Name = "lblSA";
            this.lblSA.Size = new System.Drawing.Size(227, 25);
            this.lblSA.TabIndex = 35;
            this.lblSA.Text = "Seasonally Adjusted";
            this.lblSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSA.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(342, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(535, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "(Millions of dollars. Details may not add to totals due to rounding.)";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmTimeExternal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 853);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSA);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Name = "frmTimeExternal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTimeExternal_FormClosing);
            this.Load += new System.EventHandler(this.frmTimeExternal_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.lblSA, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.panel1.ResumeLayout(false);
            this.pnlType.ResumeLayout(false);
            this.pnlType.PerformLayout();
            this.pnlSurvey.ResumeLayout(false);
            this.pnlSurvey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.RadioButton rdnsa;
        private System.Windows.Forms.RadioButton rdsa;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Label lblSA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlType;
        private System.Windows.Forms.Panel pnlSurvey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdf;
        private System.Windows.Forms.RadioButton rdt;
        private System.Windows.Forms.RadioButton rds;
        private System.Windows.Forms.RadioButton rdp;
        private System.Windows.Forms.RadioButton rdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn h;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}