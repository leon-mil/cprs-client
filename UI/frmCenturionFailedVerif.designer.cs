namespace Cprs
{
    partial class frmCenturionFailedVerif
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
            this.ckMySec = new System.Windows.Forms.CheckBox();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.btnC700 = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnMark = new System.Windows.Forms.Button();
            this.lblCases = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(430, 58);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(349, 25);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "FAILED VERIFICATION REVIEW";
            // 
            // ckMySec
            // 
            this.ckMySec.AutoSize = true;
            this.ckMySec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckMySec.ForeColor = System.Drawing.Color.DarkBlue;
            this.ckMySec.Location = new System.Drawing.Point(558, 97);
            this.ckMySec.Name = "ckMySec";
            this.ckMySec.Size = new System.Drawing.Size(96, 20);
            this.ckMySec.TabIndex = 10;
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
            this.dgData.Location = new System.Drawing.Point(70, 123);
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
            this.dgData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1053, 615);
            this.dgData.TabIndex = 11;
            // 
            // btnC700
            // 
            this.btnC700.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC700.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnC700.Location = new System.Drawing.Point(288, 810);
            this.btnC700.Name = "btnC700";
            this.btnC700.Size = new System.Drawing.Size(137, 23);
            this.btnC700.TabIndex = 14;
            this.btnC700.Text = "C-700";
            this.btnC700.UseVisualStyleBackColor = true;
            this.btnC700.Click += new System.EventHandler(this.btnC700_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(764, 810);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(137, 23);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnMark
            // 
            this.btnMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMark.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnMark.Location = new System.Drawing.Point(517, 810);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(137, 23);
            this.btnMark.TabIndex = 15;
            this.btnMark.Text = "MARK RESOLVED";
            this.btnMark.UseVisualStyleBackColor = true;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // lblCases
            // 
            this.lblCases.AutoSize = true;
            this.lblCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCases.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCases.Location = new System.Drawing.Point(67, 104);
            this.lblCases.Name = "lblCases";
            this.lblCases.Size = new System.Drawing.Size(60, 16);
            this.lblCases.TabIndex = 32;
            this.lblCases.Text = "0 CASE";
            // 
            // frmCenturionFailedVerif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 869);
            this.Controls.Add(this.lblCases);
            this.Controls.Add(this.btnMark);
            this.Controls.Add(this.btnC700);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.ckMySec);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmCenturionFailedVerif";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCenturionFailedVerif_FormClosing);
            this.Load += new System.EventHandler(this.frmCenturionFailedVerif_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.ckMySec, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnC700, 0);
            this.Controls.SetChildIndex(this.btnMark, 0);
            this.Controls.SetChildIndex(this.lblCases, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox ckMySec;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnC700;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Label lblCases;
    }
}