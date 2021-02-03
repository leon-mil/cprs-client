namespace Cprs
{
    partial class frmImpFlagsReview
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnData = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgFlagCnt = new System.Windows.Forms.DataGridView();
            this.dgCasesFound = new System.Windows.Forms.DataGridView();
            this.lblCasesCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgFlagCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCasesFound)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(423, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(370, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "IMPROVEMENTS FLAGS REVIEW";
            // 
            // btnData
            // 
            this.btnData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnData.Location = new System.Drawing.Point(419, 814);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(133, 23);
            this.btnData.TabIndex = 25;
            this.btnData.TabStop = false;
            this.btnData.Text = "DATA";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(660, 814);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 24;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgFlagCnt
            // 
            this.dgFlagCnt.AllowUserToAddRows = false;
            this.dgFlagCnt.AllowUserToDeleteRows = false;
            this.dgFlagCnt.AllowUserToResizeColumns = false;
            this.dgFlagCnt.AllowUserToResizeRows = false;
            this.dgFlagCnt.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFlagCnt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgFlagCnt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFlagCnt.Location = new System.Drawing.Point(26, 78);
            this.dgFlagCnt.MultiSelect = false;
            this.dgFlagCnt.Name = "dgFlagCnt";
            this.dgFlagCnt.ReadOnly = true;
            this.dgFlagCnt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgFlagCnt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFlagCnt.Size = new System.Drawing.Size(1157, 374);
            this.dgFlagCnt.TabIndex = 26;
            this.dgFlagCnt.SelectionChanged += new System.EventHandler(this.dgFlagCnt_SelectionChanged);
            // 
            // dgCasesFound
            // 
            this.dgCasesFound.AllowUserToAddRows = false;
            this.dgCasesFound.AllowUserToDeleteRows = false;
            this.dgCasesFound.AllowUserToResizeColumns = false;
            this.dgCasesFound.AllowUserToResizeRows = false;
            this.dgCasesFound.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCasesFound.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgCasesFound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCasesFound.Location = new System.Drawing.Point(26, 496);
            this.dgCasesFound.MultiSelect = false;
            this.dgCasesFound.Name = "dgCasesFound";
            this.dgCasesFound.ReadOnly = true;
            this.dgCasesFound.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgCasesFound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCasesFound.Size = new System.Drawing.Size(1157, 271);
            this.dgCasesFound.TabIndex = 27;
            // 
            // lblCasesCount
            // 
            this.lblCasesCount.AutoSize = true;
            this.lblCasesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCasesCount.Location = new System.Drawing.Point(23, 477);
            this.lblCasesCount.Name = "lblCasesCount";
            this.lblCasesCount.Size = new System.Drawing.Size(36, 16);
            this.lblCasesCount.TabIndex = 29;
            this.lblCasesCount.Text = "xxxx";
            // 
            // frmImpFlagsReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.lblCasesCount);
            this.Controls.Add(this.dgCasesFound);
            this.Controls.Add(this.dgFlagCnt);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label2);
            this.Name = "frmImpFlagsReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImpFlagsReview_FormClosing);
            this.Load += new System.EventHandler(this.frmImpFlagsReview_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnData, 0);
            this.Controls.SetChildIndex(this.dgFlagCnt, 0);
            this.Controls.SetChildIndex(this.dgCasesFound, 0);
            this.Controls.SetChildIndex(this.lblCasesCount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgFlagCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCasesFound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgFlagCnt;
        private System.Windows.Forms.DataGridView dgCasesFound;
        private System.Windows.Forms.Label lblCasesCount;
    }
}