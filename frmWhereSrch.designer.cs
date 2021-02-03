namespace Cprs
{
    partial class frmWhereSrch
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRecall = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnVariables = new System.Windows.Forms.Button();
            this.chkSample = new System.Windows.Forms.CheckBox();
            this.btnStore = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtWhere = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Respid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Owners = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Newtc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seldate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Strtdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rvitm5c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnC700 = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.btnName = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(478, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "INTERACTIVE SEARCH";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRecall);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnVariables);
            this.groupBox1.Controls.Add(this.chkSample);
            this.groupBox1.Controls.Add(this.btnStore);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtWhere);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(25, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1163, 154);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter Search Criteria";
            // 
            // btnRecall
            // 
            this.btnRecall.Location = new System.Drawing.Point(627, 125);
            this.btnRecall.Name = "btnRecall";
            this.btnRecall.Size = new System.Drawing.Size(82, 23);
            this.btnRecall.TabIndex = 5;
            this.btnRecall.Text = "RECALL";
            this.btnRecall.UseVisualStyleBackColor = true;
            this.btnRecall.Click += new System.EventHandler(this.btnRecall_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1082, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 117;
            this.label3.Text = "Variables";
            // 
            // btnVariables
            // 
            this.btnVariables.Image = global::Cprs.Properties.Resources.Magnify;
            this.btnVariables.Location = new System.Drawing.Point(1100, 52);
            this.btnVariables.Name = "btnVariables";
            this.btnVariables.Size = new System.Drawing.Size(29, 23);
            this.btnVariables.TabIndex = 2;
            this.btnVariables.UseVisualStyleBackColor = true;
            this.btnVariables.Click += new System.EventHandler(this.btnVariables_Click);
            // 
            // chkSample
            // 
            this.chkSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.chkSample.Location = new System.Drawing.Point(453, 81);
            this.chkSample.Name = "chkSample";
            this.chkSample.Size = new System.Drawing.Size(256, 35);
            this.chkSample.TabIndex = 10;
            this.chkSample.TabStop = false;
            this.chkSample.Text = "INCLUDE UNSAMPLED CASES";
            this.chkSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSample.UseVisualStyleBackColor = true;
            // 
            // btnStore
            // 
            this.btnStore.Location = new System.Drawing.Point(470, 125);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(82, 23);
            this.btnStore.TabIndex = 4;
            this.btnStore.Text = "STORE";
            this.btnStore.UseVisualStyleBackColor = true;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(768, 125);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(344, 125);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtWhere
            // 
            this.txtWhere.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWhere.Location = new System.Drawing.Point(138, 29);
            this.txtWhere.MaxLength = 240;
            this.txtWhere.Multiline = true;
            this.txtWhere.Name = "txtWhere";
            this.txtWhere.Size = new System.Drawing.Size(927, 46);
            this.txtWhere.TabIndex = 1;
            this.txtWhere.Enter += new System.EventHandler(this.txtWhere_Enter);
            this.txtWhere.Leave += new System.EventHandler(this.txtWhere_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "WHERE :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.FIN,
            this.Id,
            this.Respid,
            this.Owners,
            this.Newtc,
            this.Seldate,
            this.Strtdate,
            this.Rvitm5c});
            this.dgData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgData.EnableHeadersVisualStyles = false;
            this.dgData.Location = new System.Drawing.Point(25, 303);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1158, 455);
            this.dgData.TabIndex = 7;
            this.dgData.TabStop = false;
            this.dgData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgData_RowPostPaint);
            // 
            // Row
            // 
            this.Row.FillWeight = 10F;
            this.Row.HeaderText = "ROW";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.Width = 13;
            // 
            // FIN
            // 
            this.FIN.HeaderText = "FIN";
            this.FIN.Name = "FIN";
            this.FIN.ReadOnly = true;
            this.FIN.Width = 127;
            // 
            // Id
            // 
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 126;
            // 
            // Respid
            // 
            this.Respid.HeaderText = "RESPID";
            this.Respid.Name = "Respid";
            this.Respid.ReadOnly = true;
            this.Respid.Width = 127;
            // 
            // Owners
            // 
            this.Owners.HeaderText = "OWNER";
            this.Owners.Name = "Owners";
            this.Owners.ReadOnly = true;
            this.Owners.Width = 127;
            // 
            // Newtc
            // 
            this.Newtc.HeaderText = "NEWTC";
            this.Newtc.Name = "Newtc";
            this.Newtc.ReadOnly = true;
            this.Newtc.Width = 127;
            // 
            // Seldate
            // 
            this.Seldate.HeaderText = "SELDATE";
            this.Seldate.Name = "Seldate";
            this.Seldate.ReadOnly = true;
            this.Seldate.Width = 127;
            // 
            // Strtdate
            // 
            this.Strtdate.HeaderText = "STRTDATE";
            this.Strtdate.Name = "Strtdate";
            this.Strtdate.ReadOnly = true;
            this.Strtdate.Width = 127;
            // 
            // Rvitm5c
            // 
            this.Rvitm5c.HeaderText = "RVITM5C";
            this.Rvitm5c.Name = "Rvitm5c";
            this.Rvitm5c.ReadOnly = true;
            this.Rvitm5c.Width = 127;
            // 
            // btnC700
            // 
            this.btnC700.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC700.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnC700.Location = new System.Drawing.Point(748, 814);
            this.btnC700.Name = "btnC700";
            this.btnC700.Size = new System.Drawing.Size(137, 23);
            this.btnC700.TabIndex = 11;
            this.btnC700.Text = "C-700";
            this.btnC700.UseVisualStyleBackColor = true;
            this.btnC700.Click += new System.EventHandler(this.btnC700_Click);
            // 
            // btnSource
            // 
            this.btnSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSource.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSource.Location = new System.Drawing.Point(535, 814);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(137, 23);
            this.btnSource.TabIndex = 10;
            this.btnSource.Text = "SOURCE ADDRESS";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // btnName
            // 
            this.btnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnName.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnName.Location = new System.Drawing.Point(330, 814);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(137, 23);
            this.btnName.TabIndex = 9;
            this.btnName.Text = "NAME ADDRESS";
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(535, 775);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(137, 23);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCount.Location = new System.Drawing.Point(22, 274);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(120, 26);
            this.lblCount.TabIndex = 10;
            this.lblCount.Text = "0 Case";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // frmWhereSrch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1216, 869);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnC700);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.btnName);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Name = "frmWhereSrch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWhereSrch_FormClosing);
            this.Load += new System.EventHandler(this.frmWhereSrch_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnName, 0);
            this.Controls.SetChildIndex(this.btnSource, 0);
            this.Controls.SetChildIndex(this.btnC700, 0);
            this.Controls.SetChildIndex(this.lblCount, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWhere;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkSample;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnC700;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Button btnName;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnVariables;
        private System.Windows.Forms.Button btnRecall;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Respid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Owners;
        private System.Windows.Forms.DataGridViewTextBoxColumn Newtc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seldate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Strtdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rvitm5c;
    }
}
