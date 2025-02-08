namespace Cprs
{
    partial class frmSpecBEACDMon
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.lblFactor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.lbllabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(511, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 24;
            this.label3.Text = "For :";
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(553, 148);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(86, 21);
            this.cbMonth.TabIndex = 25;
            this.cbMonth.SelectedIndexChanged += new System.EventHandler(this.cbMonth_SelectedIndexChanged);
            // 
            // lblFactor
            // 
            this.lblFactor.AutoSize = true;
            this.lblFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblFactor.Location = new System.Drawing.Point(537, 120);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(88, 16);
            this.lblFactor.TabIndex = 23;
            this.lblFactor.Text = "Old Factors";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(510, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Thousands of Dollars";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(51, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1105, 37);
            this.lblTitle.TabIndex = 21;
            this.lblTitle.Text = "FEDERAL VIP NOT SEASONALLY ADJUSTED";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnApply.Location = new System.Drawing.Point(538, 806);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(151, 23);
            this.btnApply.TabIndex = 54;
            this.btnApply.TabStop = false;
            this.btnApply.Text = "APPLY NEW FACTORS";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(777, 806);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 53;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(297, 806);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 52;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
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
            this.dgData.Location = new System.Drawing.Point(249, 181);
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
            this.dgData.Size = new System.Drawing.Size(711, 524);
            this.dgData.TabIndex = 62;
            this.dgData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellDoubleClick);
            // 
            // lbllabel
            // 
            this.lbllabel.AutoSize = true;
            this.lbllabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbllabel.Location = new System.Drawing.Point(745, 708);
            this.lbllabel.Name = "lbllabel";
            this.lbllabel.Size = new System.Drawing.Size(215, 16);
            this.lbllabel.TabIndex = 63;
            this.lbllabel.Text = "Double Click a Line to Expand";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmSpecBEACDMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 870);
            this.Controls.Add(this.lbllabel);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbMonth);
            this.Controls.Add(this.lblFactor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmSpecBEACDMon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSpecBEACDMon_FormClosing);
            this.Load += new System.EventHandler(this.frmSpecBEACDMon_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblFactor, 0);
            this.Controls.SetChildIndex(this.cbMonth, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnApply, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.lbllabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Label lbllabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}