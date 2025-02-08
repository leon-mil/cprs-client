namespace Cprs
{
    partial class frmFlagTypeReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgPrint = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flagReportDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblType = new System.Windows.Forms.Label();
            this.rdNpc = new System.Windows.Forms.RadioButton();
            this.rdHq = new System.Windows.Forms.RadioButton();
            this.ckMySec = new System.Windows.Forms.CheckBox();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.flagno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allcases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slcases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nrcases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mfcases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.flagReportDataBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnDescription = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagReportDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagReportDataBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(341, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(534, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "REVIEW SCHEDULER RECORDS BY FLAG TYPE";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgPrint);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.rdNpc);
            this.panel1.Controls.Add(this.rdHq);
            this.panel1.Controls.Add(this.ckMySec);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Location = new System.Drawing.Point(38, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1129, 659);
            this.panel1.TabIndex = 16;
            // 
            // dgPrint
            // 
            this.dgPrint.AllowUserToAddRows = false;
            this.dgPrint.AllowUserToDeleteRows = false;
            this.dgPrint.AllowUserToResizeColumns = false;
            this.dgPrint.AllowUserToResizeRows = false;
            this.dgPrint.AutoGenerateColumns = false;
            this.dgPrint.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPrint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9});
            this.dgPrint.DataSource = this.flagReportDataBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPrint.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPrint.Location = new System.Drawing.Point(48, 66);
            this.dgPrint.Name = "dgPrint";
            this.dgPrint.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgPrint.RowHeadersVisible = false;
            this.dgPrint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgPrint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPrint.ShowCellToolTips = false;
            this.dgPrint.Size = new System.Drawing.Size(1033, 527);
            this.dgPrint.TabIndex = 32;
            this.dgPrint.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "fno";
            this.dataGridViewTextBoxColumn1.HeaderText = "fno";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "sqno";
            this.dataGridViewTextBoxColumn2.HeaderText = "sqno";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn3.HeaderText = "FLAG TYPE";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 420;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "allcase";
            this.dataGridViewTextBoxColumn4.HeaderText = "ALL SURVEYS";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "pubcase";
            this.dataGridViewTextBoxColumn5.HeaderText = "STATE AND LOCAL";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "fedcase";
            this.dataGridViewTextBoxColumn6.HeaderText = "FEDERAL";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "nonrcase";
            this.dataGridViewTextBoxColumn7.HeaderText = "NON RESIDENTIAL";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "multicase";
            this.dataGridViewTextBoxColumn8.HeaderText = "MULTIFAMILY";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "utilcase";
            this.dataGridViewTextBoxColumn9.HeaderText = "UTILITIES";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // flagReportDataBindingSource
            // 
            this.flagReportDataBindingSource.DataSource = typeof(CprsDAL.FlagReportData);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblType.Location = new System.Drawing.Point(402, 35);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(96, 16);
            this.lblType.TabIndex = 30;
            this.lblType.Text = "Select Type:";
            // 
            // rdNpc
            // 
            this.rdNpc.AutoSize = true;
            this.rdNpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNpc.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdNpc.Location = new System.Drawing.Point(626, 35);
            this.rdNpc.Name = "rdNpc";
            this.rdNpc.Size = new System.Drawing.Size(100, 20);
            this.rdNpc.TabIndex = 29;
            this.rdNpc.Text = "NPC Flags";
            this.rdNpc.UseVisualStyleBackColor = true;
            this.rdNpc.Click += new System.EventHandler(this.rdNpc_Click);
            // 
            // rdHq
            // 
            this.rdHq.AutoSize = true;
            this.rdHq.Checked = true;
            this.rdHq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdHq.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdHq.Location = new System.Drawing.Point(516, 35);
            this.rdHq.Name = "rdHq";
            this.rdHq.Size = new System.Drawing.Size(91, 20);
            this.rdHq.TabIndex = 28;
            this.rdHq.TabStop = true;
            this.rdHq.Text = "HQ Flags";
            this.rdHq.UseVisualStyleBackColor = true;
            this.rdHq.Click += new System.EventHandler(this.rdHq_Click);
            // 
            // ckMySec
            // 
            this.ckMySec.AutoSize = true;
            this.ckMySec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckMySec.ForeColor = System.Drawing.Color.DarkBlue;
            this.ckMySec.Location = new System.Drawing.Point(516, 3);
            this.ckMySec.Name = "ckMySec";
            this.ckMySec.Size = new System.Drawing.Size(96, 20);
            this.ckMySec.TabIndex = 2;
            this.ckMySec.Text = "My Sector";
            this.ckMySec.UseVisualStyleBackColor = true;
            this.ckMySec.CheckedChanged += new System.EventHandler(this.ckMySec_CheckedChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.AutoGenerateColumns = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flagno,
            this.sqno,
            this.fname,
            this.allcases,
            this.slcases,
            this.fcases,
            this.nrcases,
            this.mfcases,
            this.ucases});
            this.dgData.DataSource = this.flagReportDataBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgData.Location = new System.Drawing.Point(20, 61);
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
            this.dgData.ShowCellToolTips = false;
            this.dgData.Size = new System.Drawing.Size(1101, 582);
            this.dgData.TabIndex = 0;
            this.dgData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgData_CellFormatting);
            this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
            // 
            // flagno
            // 
            this.flagno.DataPropertyName = "fno";
            this.flagno.HeaderText = "fno";
            this.flagno.Name = "flagno";
            this.flagno.ReadOnly = true;
            this.flagno.Visible = false;
            // 
            // sqno
            // 
            this.sqno.DataPropertyName = "sqno";
            this.sqno.HeaderText = "sqno";
            this.sqno.Name = "sqno";
            this.sqno.ReadOnly = true;
            this.sqno.Visible = false;
            // 
            // fname
            // 
            this.fname.DataPropertyName = "title";
            this.fname.HeaderText = "FLAG NAME";
            this.fname.Name = "fname";
            this.fname.ReadOnly = true;
            this.fname.Width = 348;
            // 
            // allcases
            // 
            this.allcases.DataPropertyName = "allcase";
            this.allcases.HeaderText = "ALL SURVEYS";
            this.allcases.Name = "allcases";
            this.allcases.ReadOnly = true;
            this.allcases.Width = 122;
            // 
            // slcases
            // 
            this.slcases.DataPropertyName = "pubcase";
            this.slcases.HeaderText = "STATE AND LOCAL";
            this.slcases.Name = "slcases";
            this.slcases.ReadOnly = true;
            this.slcases.Width = 122;
            // 
            // fcases
            // 
            this.fcases.DataPropertyName = "fedcase";
            this.fcases.HeaderText = "FEDERAL";
            this.fcases.Name = "fcases";
            this.fcases.ReadOnly = true;
            this.fcases.Width = 122;
            // 
            // nrcases
            // 
            this.nrcases.DataPropertyName = "nonrcase";
            this.nrcases.HeaderText = "NON RESIDENTIAL";
            this.nrcases.Name = "nrcases";
            this.nrcases.ReadOnly = true;
            this.nrcases.Width = 122;
            // 
            // mfcases
            // 
            this.mfcases.DataPropertyName = "multicase";
            this.mfcases.HeaderText = "MULTIFAMILY";
            this.mfcases.Name = "mfcases";
            this.mfcases.ReadOnly = true;
            this.mfcases.Width = 122;
            // 
            // ucases
            // 
            this.ucases.DataPropertyName = "utilcase";
            this.ucases.HeaderText = "UTILITIES";
            this.ucases.Name = "ucases";
            this.ucases.ReadOnly = true;
            this.ucases.Width = 122;
            // 
            // btnReview
            // 
            this.btnReview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReview.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnReview.Location = new System.Drawing.Point(285, 785);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(133, 23);
            this.btnReview.TabIndex = 26;
            this.btnReview.TabStop = false;
            this.btnReview.Text = "REVIEW";
            this.btnReview.UseVisualStyleBackColor = true;
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(754, 785);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 25;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // flagReportDataBindingSource1
            // 
            this.flagReportDataBindingSource1.DataSource = typeof(CprsDAL.FlagReportData);
            // 
            // btnDescription
            // 
            this.btnDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescription.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDescription.Location = new System.Drawing.Point(518, 785);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new System.Drawing.Size(133, 23);
            this.btnDescription.TabIndex = 27;
            this.btnDescription.TabStop = false;
            this.btnDescription.Text = "DESCRIPTIONS";
            this.btnDescription.UseVisualStyleBackColor = true;
            this.btnDescription.Click += new System.EventHandler(this.btnDescription_Click);
            // 
            // frmFlagTypeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.btnDescription);
            this.Controls.Add(this.btnReview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "frmFlagTypeReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFlagTypeReport_FormClosing);
            this.Load += new System.EventHandler(this.frmFlagTypeReport_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnReview, 0);
            this.Controls.SetChildIndex(this.btnDescription, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagReportDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagReportDataBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.CheckBox ckMySec;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.RadioButton rdNpc;
        private System.Windows.Forms.RadioButton rdHq;
        private System.Windows.Forms.BindingSource flagReportDataBindingSource;
        private System.Windows.Forms.BindingSource flagReportDataBindingSource1;
        private System.Windows.Forms.Button btnDescription;
        private System.Windows.Forms.DataGridView dgPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagno;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqno;
        private System.Windows.Forms.DataGridViewTextBoxColumn fname;
        private System.Windows.Forms.DataGridViewTextBoxColumn allcases;
        private System.Windows.Forms.DataGridViewTextBoxColumn slcases;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcases;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrcases;
        private System.Windows.Forms.DataGridViewTextBoxColumn mfcases;
        private System.Windows.Forms.DataGridViewTextBoxColumn ucases;
    }
}