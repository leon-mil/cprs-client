namespace Cprs
{
    partial class frmTabAnnualBea
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
            this.dgData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rd1u = new System.Windows.Forms.RadioButton();
            this.rd1m = new System.Windows.Forms.RadioButton();
            this.rd1f = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rd1n = new System.Windows.Forms.RadioButton();
            this.rd1p = new System.Windows.Forms.RadioButton();
            this.btnRevision = new System.Windows.Forms.Button();
            this.btnWork = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnBoost = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
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
            this.dgData.Location = new System.Drawing.Point(40, 222);
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
            this.dgData.Size = new System.Drawing.Size(1090, 478);
            this.dgData.TabIndex = 75;
            this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(535, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 74;
            this.label1.Text = "Unboosted";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(485, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 72;
            this.label3.Text = "Select Year:";
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(575, 195);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(86, 21);
            this.cbYear.TabIndex = 73;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(501, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "Thousands of Dollars";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(35, 42);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1105, 37);
            this.lblTitle.TabIndex = 70;
            this.lblTitle.Text = "STATE AND LOCAL 2017 VIP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rd1u
            // 
            this.rd1u.AutoSize = true;
            this.rd1u.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1u.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1u.Location = new System.Drawing.Point(848, 158);
            this.rd1u.Name = "rd1u";
            this.rd1u.Size = new System.Drawing.Size(77, 20);
            this.rd1u.TabIndex = 81;
            this.rd1u.Text = "Utilities";
            this.rd1u.UseVisualStyleBackColor = true;
            this.rd1u.CheckedChanged += new System.EventHandler(this.rd1u_CheckedChanged);
            // 
            // rd1m
            // 
            this.rd1m.AutoSize = true;
            this.rd1m.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1m.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1m.Location = new System.Drawing.Point(743, 158);
            this.rd1m.Name = "rd1m";
            this.rd1m.Size = new System.Drawing.Size(98, 20);
            this.rd1m.TabIndex = 80;
            this.rd1m.Text = "Multifamily";
            this.rd1m.UseVisualStyleBackColor = true;
            this.rd1m.CheckedChanged += new System.EventHandler(this.rd1m_CheckedChanged);
            // 
            // rd1f
            // 
            this.rd1f.AutoSize = true;
            this.rd1f.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1f.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1f.Location = new System.Drawing.Point(524, 158);
            this.rd1f.Name = "rd1f";
            this.rd1f.Size = new System.Drawing.Size(79, 20);
            this.rd1f.TabIndex = 78;
            this.rd1f.Text = "Federal";
            this.rd1f.UseVisualStyleBackColor = true;
            this.rd1f.CheckedChanged += new System.EventHandler(this.rd1f_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(270, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 16);
            this.label4.TabIndex = 79;
            this.label4.Text = "Select Survey:";
            // 
            // rd1n
            // 
            this.rd1n.AutoSize = true;
            this.rd1n.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1n.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1n.Location = new System.Drawing.Point(610, 158);
            this.rd1n.Name = "rd1n";
            this.rd1n.Size = new System.Drawing.Size(126, 20);
            this.rd1n.TabIndex = 77;
            this.rd1n.Text = "Nonresidential";
            this.rd1n.UseVisualStyleBackColor = true;
            this.rd1n.CheckedChanged += new System.EventHandler(this.rd1n_CheckedChanged);
            // 
            // rd1p
            // 
            this.rd1p.AutoSize = true;
            this.rd1p.Checked = true;
            this.rd1p.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1p.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1p.Location = new System.Drawing.Point(384, 157);
            this.rd1p.Name = "rd1p";
            this.rd1p.Size = new System.Drawing.Size(133, 20);
            this.rd1p.TabIndex = 76;
            this.rd1p.TabStop = true;
            this.rd1p.Text = "State and Local";
            this.rd1p.UseVisualStyleBackColor = true;
            this.rd1p.CheckedChanged += new System.EventHandler(this.rd1p_CheckedChanged);
            // 
            // btnRevision
            // 
            this.btnRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevision.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRevision.Location = new System.Drawing.Point(511, 794);
            this.btnRevision.Name = "btnRevision";
            this.btnRevision.Size = new System.Drawing.Size(133, 23);
            this.btnRevision.TabIndex = 84;
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
            this.btnWork.Location = new System.Drawing.Point(290, 794);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(133, 23);
            this.btnWork.TabIndex = 83;
            this.btnWork.TabStop = false;
            this.btnWork.Text = "WORKSHEET";
            this.btnWork.UseVisualStyleBackColor = true;
            this.btnWork.EnabledChanged += new System.EventHandler(this.btnWork_EnabledChanged);
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(955, 794);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 82;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnBoost
            // 
            this.btnBoost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoost.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnBoost.Location = new System.Drawing.Point(89, 794);
            this.btnBoost.Name = "btnBoost";
            this.btnBoost.Size = new System.Drawing.Size(133, 23);
            this.btnBoost.TabIndex = 85;
            this.btnBoost.TabStop = false;
            this.btnBoost.Text = "BOOSTED";
            this.btnBoost.UseVisualStyleBackColor = true;
            this.btnBoost.EnabledChanged += new System.EventHandler(this.btnBoost_EnabledChanged);
            this.btnBoost.Click += new System.EventHandler(this.btnBoost_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(740, 794);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 86;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.EnabledChanged += new System.EventHandler(this.btnTable_EnabledChanged);
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnUpdate.Location = new System.Drawing.Point(385, 747);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(133, 23);
            this.btnUpdate.TabIndex = 87;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Text = "NEWTC UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.EnabledChanged += new System.EventHandler(this.btnUpdate_EnabledChanged);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSub
            // 
            this.btnSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSub.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSub.Location = new System.Drawing.Point(610, 747);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(133, 23);
            this.btnSub.TabIndex = 88;
            this.btnSub.TabStop = false;
            this.btnSub.Text = "UTILITES SUBTC";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.EnabledChanged += new System.EventHandler(this.btnSub_EnabledChanged);
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // frmTabAnnualBea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 853);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.btnBoost);
            this.Controls.Add(this.btnRevision);
            this.Controls.Add(this.btnWork);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.rd1u);
            this.Controls.Add(this.rd1m);
            this.Controls.Add(this.rd1f);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rd1n);
            this.Controls.Add(this.rd1p);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmTabAnnualBea";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTabAnnualBea_FormClosing);
            this.Load += new System.EventHandler(this.frmTabAnnualBea_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cbYear, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.rd1p, 0);
            this.Controls.SetChildIndex(this.rd1n, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.rd1f, 0);
            this.Controls.SetChildIndex(this.rd1m, 0);
            this.Controls.SetChildIndex(this.rd1u, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnWork, 0);
            this.Controls.SetChildIndex(this.btnRevision, 0);
            this.Controls.SetChildIndex(this.btnBoost, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.btnUpdate, 0);
            this.Controls.SetChildIndex(this.btnSub, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RadioButton rd1u;
        private System.Windows.Forms.RadioButton rd1m;
        private System.Windows.Forms.RadioButton rd1f;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rd1n;
        private System.Windows.Forms.RadioButton rd1p;
        private System.Windows.Forms.Button btnRevision;
        private System.Windows.Forms.Button btnWork;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnBoost;
        private System.Windows.Forms.Button btnTable;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSub;
    }
}