namespace Cprs
{
    partial class frmTotalVip
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbllabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckMySec = new System.Windows.Forms.CheckBox();
            this.rd1u = new System.Windows.Forms.RadioButton();
            this.rd1m = new System.Windows.Forms.RadioButton();
            this.rd1f = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rd1n = new System.Windows.Forms.RadioButton();
            this.rd1p = new System.Windows.Forms.RadioButton();
            this.rd1a = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.dgPrint = new System.Windows.Forms.DataGridView();
            this.btnRevision = new System.Windows.Forms.Button();
            this.btnWork = new System.Windows.Forms.Button();
            this.btnMonth = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(401, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(415, 25);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Total Seasonally Adjusted 201611 VIP";
            this.lblTitle.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbllabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ckMySec);
            this.panel1.Controls.Add(this.rd1u);
            this.panel1.Controls.Add(this.rd1m);
            this.panel1.Controls.Add(this.rd1f);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rd1n);
            this.panel1.Controls.Add(this.rd1p);
            this.panel1.Controls.Add(this.rd1a);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Controls.Add(this.dgPrint);
            this.panel1.Location = new System.Drawing.Point(42, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1129, 650);
            this.panel1.TabIndex = 9;
            // 
            // lbllabel
            // 
            this.lbllabel.AutoSize = true;
            this.lbllabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbllabel.Location = new System.Drawing.Point(885, 626);
            this.lbllabel.Name = "lbllabel";
            this.lbllabel.Size = new System.Drawing.Size(214, 16);
            this.lbllabel.TabIndex = 32;
            this.lbllabel.Text = "Double Click a Line to Expand";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(19, 626);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "* Indicates Unpublished TC";
            // 
            // ckMySec
            // 
            this.ckMySec.AutoSize = true;
            this.ckMySec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckMySec.ForeColor = System.Drawing.Color.DarkBlue;
            this.ckMySec.Location = new System.Drawing.Point(516, 15);
            this.ckMySec.Name = "ckMySec";
            this.ckMySec.Size = new System.Drawing.Size(95, 20);
            this.ckMySec.TabIndex = 1;
            this.ckMySec.Text = "My Sector";
            this.ckMySec.UseVisualStyleBackColor = true;
            this.ckMySec.CheckedChanged += new System.EventHandler(this.ckMySec_CheckedChanged);
            // 
            // rd1u
            // 
            this.rd1u.AutoSize = true;
            this.rd1u.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1u.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1u.Location = new System.Drawing.Point(888, 48);
            this.rd1u.Name = "rd1u";
            this.rd1u.Size = new System.Drawing.Size(77, 20);
            this.rd1u.TabIndex = 29;
            this.rd1u.Text = "Utilities";
            this.rd1u.UseVisualStyleBackColor = true;
            this.rd1u.CheckedChanged += new System.EventHandler(this.rd1u_CheckedChanged);
            // 
            // rd1m
            // 
            this.rd1m.AutoSize = true;
            this.rd1m.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1m.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1m.Location = new System.Drawing.Point(771, 48);
            this.rd1m.Name = "rd1m";
            this.rd1m.Size = new System.Drawing.Size(98, 20);
            this.rd1m.TabIndex = 28;
            this.rd1m.Text = "Multifamily";
            this.rd1m.UseVisualStyleBackColor = true;
            this.rd1m.CheckedChanged += new System.EventHandler(this.rd1m_CheckedChanged);
            // 
            // rd1f
            // 
            this.rd1f.AutoSize = true;
            this.rd1f.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1f.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1f.Location = new System.Drawing.Point(516, 48);
            this.rd1f.Name = "rd1f";
            this.rd1f.Size = new System.Drawing.Size(79, 20);
            this.rd1f.TabIndex = 26;
            this.rd1f.Text = "Federal";
            this.rd1f.UseVisualStyleBackColor = true;
            this.rd1f.CheckedChanged += new System.EventHandler(this.rd1f_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(136, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Select Survey:";
            // 
            // rd1n
            // 
            this.rd1n.AutoSize = true;
            this.rd1n.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1n.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1n.Location = new System.Drawing.Point(618, 48);
            this.rd1n.Name = "rd1n";
            this.rd1n.Size = new System.Drawing.Size(126, 20);
            this.rd1n.TabIndex = 25;
            this.rd1n.Text = "Nonresidential";
            this.rd1n.UseVisualStyleBackColor = true;
            this.rd1n.CheckedChanged += new System.EventHandler(this.rd1n_CheckedChanged);
            // 
            // rd1p
            // 
            this.rd1p.AutoSize = true;
            this.rd1p.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1p.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1p.Location = new System.Drawing.Point(375, 48);
            this.rd1p.Name = "rd1p";
            this.rd1p.Size = new System.Drawing.Size(133, 20);
            this.rd1p.TabIndex = 24;
            this.rd1p.Text = "State and Local";
            this.rd1p.UseVisualStyleBackColor = true;
            this.rd1p.CheckedChanged += new System.EventHandler(this.rd1p_CheckedChanged);
            // 
            // rd1a
            // 
            this.rd1a.AutoSize = true;
            this.rd1a.Checked = true;
            this.rd1a.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1a.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1a.Location = new System.Drawing.Point(250, 48);
            this.rd1a.Name = "rd1a";
            this.rd1a.Size = new System.Drawing.Size(103, 20);
            this.rd1a.TabIndex = 23;
            this.rd1a.TabStop = true;
            this.rd1a.Text = "All Surveys";
            this.rd1a.UseVisualStyleBackColor = true;
            this.rd1a.CheckedChanged += new System.EventHandler(this.rd1a_CheckedChanged);
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
            this.dgData.Location = new System.Drawing.Point(22, 74);
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
            this.dgData.Size = new System.Drawing.Size(1083, 538);
            this.dgData.TabIndex = 0;
            this.dgData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellDoubleClick);
            this.dgData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgData_CellFormatting);
            this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
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
            // btnRevision
            // 
            this.btnRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevision.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRevision.Location = new System.Drawing.Point(669, 802);
            this.btnRevision.Name = "btnRevision";
            this.btnRevision.Size = new System.Drawing.Size(133, 23);
            this.btnRevision.TabIndex = 32;
            this.btnRevision.TabStop = false;
            this.btnRevision.Text = "REVISIONS";
            this.btnRevision.UseVisualStyleBackColor = true;
            this.btnRevision.EnabledChanged += new System.EventHandler(this.btnRevision_EnabledChanged);
            this.btnRevision.Click += new System.EventHandler(this.btnRevision_Click);
            // 
            // btnWork
            // 
            this.btnWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWork.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnWork.Location = new System.Drawing.Point(235, 802);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(133, 23);
            this.btnWork.TabIndex = 31;
            this.btnWork.TabStop = false;
            this.btnWork.Text = "WORKSHEET";
            this.btnWork.UseVisualStyleBackColor = true;
            this.btnWork.EnabledChanged += new System.EventHandler(this.btnWork_EnabledChanged);
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnMonth.Location = new System.Drawing.Point(453, 802);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(133, 23);
            this.btnMonth.TabIndex = 30;
            this.btnMonth.TabStop = false;
            this.btnMonth.Text = "MONTH to MONTH";
            this.btnMonth.UseVisualStyleBackColor = true;
            this.btnMonth.EnabledChanged += new System.EventHandler(this.btnMonth_EnabledChanged);
            this.btnMonth.Click += new System.EventHandler(this.btnMonth_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(875, 802);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 29;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(510, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 25);
            this.label1.TabIndex = 33;
            this.label1.Text = "Billions of Dollars";
            // 
            // btnAdjust
            // 
            this.btnAdjust.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdjust.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAdjust.Location = new System.Drawing.Point(558, 759);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(133, 23);
            this.btnAdjust.TabIndex = 34;
            this.btnAdjust.TabStop = false;
            this.btnAdjust.Text = "RATIO ADJUST";
            this.btnAdjust.UseVisualStyleBackColor = true;
            this.btnAdjust.Click += new System.EventHandler(this.btnAdjust_Click);
            // 
            // frmTotalVip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 850);
            this.Controls.Add(this.btnAdjust);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRevision);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnWork);
            this.Controls.Add(this.btnMonth);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Name = "frmTotalVip";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTotalVip_FormClosing);
            this.Load += new System.EventHandler(this.frmTotalVip_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnMonth, 0);
            this.Controls.SetChildIndex(this.btnWork, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnRevision, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnAdjust, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnRevision;
        private System.Windows.Forms.Button btnWork;
        private System.Windows.Forms.Button btnMonth;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckMySec;
        private System.Windows.Forms.RadioButton rd1u;
        private System.Windows.Forms.RadioButton rd1m;
        private System.Windows.Forms.RadioButton rd1f;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rd1n;
        private System.Windows.Forms.RadioButton rd1p;
        private System.Windows.Forms.RadioButton rd1a;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.DataGridView dgPrint;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lbllabel;
    }
}