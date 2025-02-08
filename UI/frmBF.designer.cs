namespace Cprs
{
    partial class frmBF
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rd1u = new System.Windows.Forms.RadioButton();
            this.rd1f = new System.Windows.Forms.RadioButton();
            this.rd1n = new System.Windows.Forms.RadioButton();
            this.rd1p = new System.Windows.Forms.RadioButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc00 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc04 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc05 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc06 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc07 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc08 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc09 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tc1t = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bFdataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnInsert = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bFdataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rd1u
            // 
            this.rd1u.AutoSize = true;
            this.rd1u.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1u.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1u.Location = new System.Drawing.Point(856, 96);
            this.rd1u.Name = "rd1u";
            this.rd1u.Size = new System.Drawing.Size(78, 20);
            this.rd1u.TabIndex = 74;
            this.rd1u.Text = "Utilities";
            this.rd1u.UseVisualStyleBackColor = true;
            this.rd1u.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rd1f
            // 
            this.rd1f.AutoSize = true;
            this.rd1f.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1f.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1f.Location = new System.Drawing.Point(585, 96);
            this.rd1f.Name = "rd1f";
            this.rd1f.Size = new System.Drawing.Size(80, 20);
            this.rd1f.TabIndex = 71;
            this.rd1f.Text = "Federal";
            this.rd1f.UseVisualStyleBackColor = true;
            this.rd1f.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rd1n
            // 
            this.rd1n.AutoSize = true;
            this.rd1n.Checked = true;
            this.rd1n.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1n.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1n.Location = new System.Drawing.Point(695, 96);
            this.rd1n.Name = "rd1n";
            this.rd1n.Size = new System.Drawing.Size(127, 20);
            this.rd1n.TabIndex = 70;
            this.rd1n.TabStop = true;
            this.rd1n.Text = "Nonresidential";
            this.rd1n.UseVisualStyleBackColor = true;
            this.rd1n.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rd1p
            // 
            this.rd1p.AutoSize = true;
            this.rd1p.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1p.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1p.Location = new System.Drawing.Point(436, 96);
            this.rd1p.Name = "rd1p";
            this.rd1p.Size = new System.Drawing.Size(134, 20);
            this.rd1p.TabIndex = 69;
            this.rd1p.Text = "State and Local";
            this.rd1p.UseVisualStyleBackColor = true;
            this.rd1p.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(52, 48);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1111, 25);
            this.lblTitle.TabIndex = 68;
            this.lblTitle.Text = " BOOST FACTORS";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.AutoGenerateColumns = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.tc00,
            this.tc01,
            this.tc02,
            this.tc03,
            this.tc04,
            this.tc05,
            this.tc06,
            this.tc07,
            this.tc08,
            this.tc09,
            this.tc10,
            this.tc11,
            this.tc12,
            this.tc13,
            this.tc14,
            this.tc15,
            this.tc16,
            this.tc19,
            this.tc1t});
            this.dgData.DataSource = this.bFdataBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Format = "0.000";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgData.Location = new System.Drawing.Point(52, 139);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgData.RowHeadersVisible = false;
            this.dgData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgData.Size = new System.Drawing.Size(1111, 658);
            this.dgData.TabIndex = 67;
            this.dgData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgData_CellBeginEdit);
            this.dgData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellEndEdit);
            this.dgData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgData_DataError);
            this.dgData.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgData_EditingControlShowing);
            // 
            // Date
            // 
            this.Date.DataPropertyName = "SDATE";
            dataGridViewCellStyle2.Format = "N0";
            this.Date.DefaultCellStyle = dataGridViewCellStyle2;
            this.Date.HeaderText = "DATE";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Date.Width = 45;
            // 
            // tc00
            // 
            this.tc00.DataPropertyName = "00";
            this.tc00.HeaderText = "00";
            this.tc00.Name = "tc00";
            this.tc00.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc00.Width = 55;
            // 
            // tc01
            // 
            this.tc01.DataPropertyName = "01";
            this.tc01.HeaderText = "01";
            this.tc01.Name = "tc01";
            this.tc01.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc01.Width = 55;
            // 
            // tc02
            // 
            this.tc02.DataPropertyName = "02";
            this.tc02.HeaderText = "02";
            this.tc02.Name = "tc02";
            this.tc02.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc02.Width = 55;
            // 
            // tc03
            // 
            this.tc03.DataPropertyName = "03";
            this.tc03.HeaderText = "03";
            this.tc03.Name = "tc03";
            this.tc03.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc03.Width = 55;
            // 
            // tc04
            // 
            this.tc04.DataPropertyName = "04";
            this.tc04.HeaderText = "04";
            this.tc04.Name = "tc04";
            this.tc04.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc04.Width = 54;
            // 
            // tc05
            // 
            this.tc05.DataPropertyName = "05";
            this.tc05.HeaderText = "05";
            this.tc05.Name = "tc05";
            this.tc05.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc05.Width = 55;
            // 
            // tc06
            // 
            this.tc06.DataPropertyName = "06";
            this.tc06.HeaderText = "06";
            this.tc06.Name = "tc06";
            this.tc06.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc06.Width = 55;
            // 
            // tc07
            // 
            this.tc07.DataPropertyName = "07";
            this.tc07.HeaderText = "07";
            this.tc07.Name = "tc07";
            this.tc07.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc07.Width = 55;
            // 
            // tc08
            // 
            this.tc08.DataPropertyName = "08";
            this.tc08.HeaderText = "08";
            this.tc08.Name = "tc08";
            this.tc08.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc08.Width = 55;
            // 
            // tc09
            // 
            this.tc09.DataPropertyName = "09";
            this.tc09.HeaderText = "09";
            this.tc09.Name = "tc09";
            this.tc09.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc09.Width = 55;
            // 
            // tc10
            // 
            this.tc10.DataPropertyName = "10";
            this.tc10.HeaderText = "10";
            this.tc10.Name = "tc10";
            this.tc10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc10.Width = 55;
            // 
            // tc11
            // 
            this.tc11.DataPropertyName = "11";
            this.tc11.HeaderText = "11";
            this.tc11.Name = "tc11";
            this.tc11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc11.Width = 55;
            // 
            // tc12
            // 
            this.tc12.DataPropertyName = "12";
            this.tc12.HeaderText = "12";
            this.tc12.Name = "tc12";
            this.tc12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc12.Width = 55;
            // 
            // tc13
            // 
            this.tc13.DataPropertyName = "13";
            this.tc13.HeaderText = "13";
            this.tc13.Name = "tc13";
            this.tc13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc13.Width = 55;
            // 
            // tc14
            // 
            this.tc14.DataPropertyName = "14";
            this.tc14.HeaderText = "14";
            this.tc14.Name = "tc14";
            this.tc14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc14.Width = 54;
            // 
            // tc15
            // 
            this.tc15.DataPropertyName = "15";
            this.tc15.HeaderText = "15";
            this.tc15.Name = "tc15";
            this.tc15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc15.Width = 55;
            // 
            // tc16
            // 
            this.tc16.DataPropertyName = "16";
            this.tc16.HeaderText = "16";
            this.tc16.Name = "tc16";
            this.tc16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc16.Width = 55;
            // 
            // tc19
            // 
            this.tc19.DataPropertyName = "19";
            this.tc19.HeaderText = "19";
            this.tc19.Name = "tc19";
            this.tc19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc19.Width = 55;
            // 
            // tc1t
            // 
            this.tc1t.DataPropertyName = "1T";
            this.tc1t.HeaderText = "1T";
            this.tc1t.Name = "tc1t";
            this.tc1t.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tc1t.Width = 55;
            // 
            // bFdataBindingSource
            // 
            this.bFdataBindingSource.DataSource = typeof(CprsDAL.BFdata);
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnInsert.Location = new System.Drawing.Point(525, 812);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(120, 23);
            this.btnInsert.TabIndex = 75;
            this.btnInsert.Text = "INSERT MONTHS";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblType.Location = new System.Drawing.Point(311, 98);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(108, 16);
            this.lblType.TabIndex = 76;
            this.lblType.Text = "Select Survey:";
            // 
            // frmBF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 861);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.rd1u);
            this.Controls.Add(this.rd1f);
            this.Controls.Add(this.rd1n);
            this.Controls.Add(this.rd1p);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgData);
            this.Name = "frmBF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBF_FormClosing);
            this.Load += new System.EventHandler(this.frmBF_Load);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.rd1p, 0);
            this.Controls.SetChildIndex(this.rd1n, 0);
            this.Controls.SetChildIndex(this.rd1f, 0);
            this.Controls.SetChildIndex(this.rd1u, 0);
            this.Controls.SetChildIndex(this.btnInsert, 0);
            this.Controls.SetChildIndex(this.lblType, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bFdataBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rd1u;
        private System.Windows.Forms.RadioButton rd1f;
        private System.Windows.Forms.RadioButton rd1n;
        private System.Windows.Forms.RadioButton rd1p;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc00;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc01;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc02;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc03;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc04;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc05;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc06;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc07;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc08;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc09;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc10;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc11;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc12;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc13;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc14;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc15;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc16;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc19;
        private System.Windows.Forms.DataGridViewTextBoxColumn tc1t;
        private System.Windows.Forms.BindingSource bFdataBindingSource;
        private System.Windows.Forms.Label lblType;
    }
}