namespace Cprs
{
    partial class frmTabAnnualTabs
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
            this.dgData = new System.Windows.Forms.DataGridView();
            this.lbllabel = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblFactor = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdnonseasadj = new System.Windows.Forms.RadioButton();
            this.rdseasadj = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rdmulti = new System.Windows.Forms.RadioButton();
            this.rdfed = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rdnonres = new System.Windows.Forms.RadioButton();
            this.rdsl = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.label1 = new System.Windows.Forms.Label();
            this.dgPrint = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
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
            this.dgData.Location = new System.Drawing.Point(12, 204);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgData.RowHeadersVisible = false;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1127, 534);
            this.dgData.TabIndex = 69;
            this.dgData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellDoubleClick);
            this.dgData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgData_CellFormatting);
            // 
            // lbllabel
            // 
            this.lbllabel.AutoSize = true;
            this.lbllabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbllabel.Location = new System.Drawing.Point(915, 752);
            this.lbllabel.Name = "lbllabel";
            this.lbllabel.Size = new System.Drawing.Size(215, 16);
            this.lbllabel.TabIndex = 68;
            this.lbllabel.Text = "Double Click a Line to Expand";
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnApply.Location = new System.Drawing.Point(377, 792);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(150, 23);
            this.btnApply.TabIndex = 67;
            this.btnApply.TabStop = false;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(635, 792);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 23);
            this.btnPrint.TabIndex = 66;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblFactor
            // 
            this.lblFactor.AutoSize = true;
            this.lblFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblFactor.Location = new System.Drawing.Point(527, 115);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(88, 16);
            this.lblFactor.TabIndex = 64;
            this.lblFactor.Text = "Old Factors";
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoney.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblMoney.Location = new System.Drawing.Point(494, 85);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(148, 20);
            this.lblMoney.TabIndex = 63;
            this.lblMoney.Text = "Billions of Dollars";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdnonseasadj);
            this.panel2.Controls.Add(this.rdseasadj);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(455, 139);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 33);
            this.panel2.TabIndex = 70;
            // 
            // rdnonseasadj
            // 
            this.rdnonseasadj.AutoSize = true;
            this.rdnonseasadj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdnonseasadj.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdnonseasadj.Location = new System.Drawing.Point(177, 4);
            this.rdnonseasadj.Name = "rdnonseasadj";
            this.rdnonseasadj.Size = new System.Drawing.Size(52, 19);
            this.rdnonseasadj.TabIndex = 46;
            this.rdnonseasadj.Text = "NSA";
            this.rdnonseasadj.UseVisualStyleBackColor = true;
            this.rdnonseasadj.CheckedChanged += new System.EventHandler(this.rdtype_CheckedChanged);
            // 
            // rdseasadj
            // 
            this.rdseasadj.AutoSize = true;
            this.rdseasadj.CausesValidation = false;
            this.rdseasadj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdseasadj.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdseasadj.Location = new System.Drawing.Point(118, 4);
            this.rdseasadj.Name = "rdseasadj";
            this.rdseasadj.Size = new System.Drawing.Size(42, 19);
            this.rdseasadj.TabIndex = 45;
            this.rdseasadj.Text = "SA";
            this.rdseasadj.UseVisualStyleBackColor = true;
            this.rdseasadj.CheckedChanged += new System.EventHandler(this.rdtype_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(16, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 42;
            this.label4.Text = "Select Type:";
            // 
            // rdmulti
            // 
            this.rdmulti.AutoSize = true;
            this.rdmulti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdmulti.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdmulti.Location = new System.Drawing.Point(801, 178);
            this.rdmulti.Name = "rdmulti";
            this.rdmulti.Size = new System.Drawing.Size(99, 20);
            this.rdmulti.TabIndex = 86;
            this.rdmulti.Text = "Multifamily";
            this.rdmulti.UseVisualStyleBackColor = true;
            this.rdmulti.CheckedChanged += new System.EventHandler(this.rdsurvey_CheckedChanged);
            // 
            // rdfed
            // 
            this.rdfed.AutoSize = true;
            this.rdfed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdfed.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdfed.Location = new System.Drawing.Point(562, 178);
            this.rdfed.Name = "rdfed";
            this.rdfed.Size = new System.Drawing.Size(80, 20);
            this.rdfed.TabIndex = 84;
            this.rdfed.Text = "Federal";
            this.rdfed.UseVisualStyleBackColor = true;
            this.rdfed.CheckedChanged += new System.EventHandler(this.rdsurvey_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(292, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 85;
            this.label2.Text = "Select Survey:";
            // 
            // rdnonres
            // 
            this.rdnonres.AutoSize = true;
            this.rdnonres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdnonres.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdnonres.Location = new System.Drawing.Point(658, 178);
            this.rdnonres.Name = "rdnonres";
            this.rdnonres.Size = new System.Drawing.Size(127, 20);
            this.rdnonres.TabIndex = 83;
            this.rdnonres.Text = "Nonresidential";
            this.rdnonres.UseVisualStyleBackColor = true;
            this.rdnonres.CheckedChanged += new System.EventHandler(this.rdsurvey_CheckedChanged);
            // 
            // rdsl
            // 
            this.rdsl.AutoSize = true;
            this.rdsl.CausesValidation = false;
            this.rdsl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsl.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdsl.Location = new System.Drawing.Point(416, 178);
            this.rdsl.Name = "rdsl";
            this.rdsl.Size = new System.Drawing.Size(134, 20);
            this.rdsl.TabIndex = 82;
            this.rdsl.Text = "State and Local";
            this.rdsl.UseVisualStyleBackColor = true;
            this.rdsl.CheckedChanged += new System.EventHandler(this.rdsurvey_CheckedChanged);
            // 
            // btnView
            // 
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnView.Location = new System.Drawing.Point(126, 792);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(150, 23);
            this.btnView.TabIndex = 87;
            this.btnView.TabStop = false;
            this.btnView.Text = "VIEW ANNUAL";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnExport.Location = new System.Drawing.Point(894, 792);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 23);
            this.btnExport.TabIndex = 88;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "EXPORT TO TSAR";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(121, 47);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(923, 21);
            this.lblTitle.TabIndex = 89;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(252, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 45);
            this.panel1.TabIndex = 90;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(12, 752);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 16);
            this.label1.TabIndex = 91;
            this.label1.Text = "* Indicates Unpublished TC";
            // 
            // dgPrint
            // 
            this.dgPrint.AllowUserToAddRows = false;
            this.dgPrint.AllowUserToDeleteRows = false;
            this.dgPrint.AllowUserToResizeColumns = false;
            this.dgPrint.AllowUserToResizeRows = false;
            this.dgPrint.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
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
            this.dgPrint.Location = new System.Drawing.Point(41, 244);
            this.dgPrint.MultiSelect = false;
            this.dgPrint.Name = "dgPrint";
            this.dgPrint.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgPrint.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgPrint.RowHeadersVisible = false;
            this.dgPrint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPrint.Size = new System.Drawing.Size(1003, 389);
            this.dgPrint.TabIndex = 92;
            this.dgPrint.Visible = false;
            // 
            // frmTabAnnualTabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 846);
            this.Controls.Add(this.dgPrint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.rdmulti);
            this.Controls.Add(this.rdfed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rdnonres);
            this.Controls.Add(this.rdsl);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.lbllabel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblFactor);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.panel1);
            this.Name = "frmTabAnnualTabs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTabAnnualTabs_FormClosing);
            this.Load += new System.EventHandler(this.frmTabAnnualTab_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblMoney, 0);
            this.Controls.SetChildIndex(this.lblFactor, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnApply, 0);
            this.Controls.SetChildIndex(this.lbllabel, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.rdsl, 0);
            this.Controls.SetChildIndex(this.rdnonres, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.rdfed, 0);
            this.Controls.SetChildIndex(this.rdmulti, 0);
            this.Controls.SetChildIndex(this.btnView, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dgPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Label lbllabel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdnonseasadj;
        private System.Windows.Forms.RadioButton rdseasadj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdmulti;
        private System.Windows.Forms.RadioButton rdfed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdnonres;
        private System.Windows.Forms.RadioButton rdsl;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgPrint;
    }
}