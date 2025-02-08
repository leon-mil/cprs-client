namespace Cprs
{
    partial class frmTableMonRelease
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
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rdv = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rdt = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdnsa = new System.Windows.Forms.RadioButton();
            this.rdsa = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblt1 = new System.Windows.Forms.Label();
            this.lblt2 = new System.Windows.Forms.Label();
            this.rdf = new System.Windows.Forms.RadioButton();
            this.rds = new System.Windows.Forms.RadioButton();
            this.rdp = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnAnnual = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle3
            // 
            this.lblTitle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle3.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle3.Location = new System.Drawing.Point(78, 93);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(1059, 20);
            this.lblTitle3.TabIndex = 45;
            this.lblTitle3.Text = "Millions of dollars.   Details may not add to totals due to rounding.";
            this.lblTitle3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle2.Location = new System.Drawing.Point(348, 68);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(514, 25);
            this.lblTitle2.TabIndex = 44;
            this.lblTitle2.Text = "Seasonally Adjusted Annual Rate";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(343, 43);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(552, 25);
            this.lblTitle.TabIndex = 43;
            this.lblTitle.Text = "Value of Construction Put in Place";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdv
            // 
            this.rdv.AutoSize = true;
            this.rdv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdv.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdv.Location = new System.Drawing.Point(466, 12);
            this.rdv.Name = "rdv";
            this.rdv.Size = new System.Drawing.Size(74, 20);
            this.rdv.TabIndex = 25;
            this.rdv.Text = "Private";
            this.rdv.UseVisualStyleBackColor = true;
            this.rdv.CheckedChanged += new System.EventHandler(this.rdv_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(262, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "Select Survey:";
            // 
            // rdt
            // 
            this.rdt.AutoSize = true;
            this.rdt.Checked = true;
            this.rdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdt.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdt.Location = new System.Drawing.Point(394, 12);
            this.rdt.Name = "rdt";
            this.rdt.Size = new System.Drawing.Size(61, 20);
            this.rdt.TabIndex = 23;
            this.rdt.TabStop = true;
            this.rdt.Text = "Total";
            this.rdt.UseVisualStyleBackColor = true;
            this.rdt.CheckedChanged += new System.EventHandler(this.rdt_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblt1);
            this.panel1.Controls.Add(this.lblt2);
            this.panel1.Controls.Add(this.rdf);
            this.panel1.Controls.Add(this.rdv);
            this.panel1.Controls.Add(this.rds);
            this.panel1.Controls.Add(this.rdp);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rdt);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Location = new System.Drawing.Point(29, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 605);
            this.panel1.TabIndex = 42;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdnsa);
            this.panel2.Controls.Add(this.rdsa);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(451, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 26);
            this.panel2.TabIndex = 48;
            // 
            // rdnsa
            // 
            this.rdnsa.AutoSize = true;
            this.rdnsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdnsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdnsa.Location = new System.Drawing.Point(177, 4);
            this.rdnsa.Name = "rdnsa";
            this.rdnsa.Size = new System.Drawing.Size(52, 19);
            this.rdnsa.TabIndex = 46;
            this.rdnsa.Text = "NSA";
            this.rdnsa.UseVisualStyleBackColor = true;
            this.rdnsa.CheckedChanged += new System.EventHandler(this.rdnsa_CheckedChanged);
            // 
            // rdsa
            // 
            this.rdsa.AutoSize = true;
            this.rdsa.Checked = true;
            this.rdsa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsa.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdsa.Location = new System.Drawing.Point(129, 4);
            this.rdsa.Name = "rdsa";
            this.rdsa.Size = new System.Drawing.Size(42, 19);
            this.rdsa.TabIndex = 45;
            this.rdsa.TabStop = true;
            this.rdsa.Text = "SA";
            this.rdsa.UseVisualStyleBackColor = true;
            this.rdsa.CheckedChanged += new System.EventHandler(this.rdsa_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(27, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 42;
            this.label4.Text = "Select Type:";
            // 
            // lblt1
            // 
            this.lblt1.BackColor = System.Drawing.SystemColors.Control;
            this.lblt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblt1.Location = new System.Drawing.Point(909, 55);
            this.lblt1.Name = "lblt1";
            this.lblt1.Size = new System.Drawing.Size(199, 19);
            this.lblt1.TabIndex = 44;
            this.lblt1.Text = "Percent Change May 2017 from -";
            this.lblt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblt2
            // 
            this.lblt2.BackColor = System.Drawing.SystemColors.Control;
            this.lblt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblt2.Location = new System.Drawing.Point(841, 55);
            this.lblt2.Name = "lblt2";
            this.lblt2.Size = new System.Drawing.Size(267, 19);
            this.lblt2.TabIndex = 43;
            this.lblt2.Text = "Year - to - Date";
            this.lblt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdf
            // 
            this.rdf.AutoSize = true;
            this.rdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdf.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdf.Location = new System.Drawing.Point(798, 12);
            this.rdf.Name = "rdf";
            this.rdf.Size = new System.Drawing.Size(79, 20);
            this.rdf.TabIndex = 37;
            this.rdf.Text = "Federal";
            this.rdf.UseVisualStyleBackColor = true;
            this.rdf.CheckedChanged += new System.EventHandler(this.rdf_CheckedChanged);
            // 
            // rds
            // 
            this.rds.AutoSize = true;
            this.rds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rds.ForeColor = System.Drawing.Color.DarkBlue;
            this.rds.Location = new System.Drawing.Point(645, 12);
            this.rds.Name = "rds";
            this.rds.Size = new System.Drawing.Size(133, 20);
            this.rds.TabIndex = 35;
            this.rds.Text = "State and Local";
            this.rds.UseVisualStyleBackColor = true;
            this.rds.CheckedChanged += new System.EventHandler(this.rds_CheckedChanged);
            // 
            // rdp
            // 
            this.rdp.AutoSize = true;
            this.rdp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdp.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdp.Location = new System.Drawing.Point(556, 12);
            this.rdp.Name = "rdp";
            this.rdp.Size = new System.Drawing.Size(68, 20);
            this.rdp.TabIndex = 33;
            this.rdp.Text = "Public";
            this.rdp.UseVisualStyleBackColor = true;
            this.rdp.CheckedChanged += new System.EventHandler(this.rdp_CheckedChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
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
            this.dgData.Location = new System.Drawing.Point(16, 77);
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
            this.dgData.Size = new System.Drawing.Size(1105, 506);
            this.dgData.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(753, 792);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 47;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(353, 792);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 46;
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
            this.btnAnnual.Location = new System.Drawing.Point(550, 753);
            this.btnAnnual.Name = "btnAnnual";
            this.btnAnnual.Size = new System.Drawing.Size(138, 23);
            this.btnAnnual.TabIndex = 48;
            this.btnAnnual.TabStop = false;
            this.btnAnnual.Text = "ANNUAL TABLES";
            this.btnAnnual.UseVisualStyleBackColor = true;
            this.btnAnnual.EnabledChanged += new System.EventHandler(this.btnAnnual_EnabledChanged);
            this.btnAnnual.Click += new System.EventHandler(this.btnAnnual_Click);
            // 
            // frmTableMonRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 853);
            this.Controls.Add(this.btnAnnual);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.lblTitle3);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Name = "frmTableMonRelease";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTableMonRelease_FormClosing);
            this.Load += new System.EventHandler(this.frmTableMonRelease_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblTitle2, 0);
            this.Controls.SetChildIndex(this.lblTitle3, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnAnnual, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RadioButton rdv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdf;
        private System.Windows.Forms.RadioButton rds;
        private System.Windows.Forms.RadioButton rdp;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Label lblt2;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Label lblt1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdnsa;
        private System.Windows.Forms.RadioButton rdsa;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnAnnual;
    }
}