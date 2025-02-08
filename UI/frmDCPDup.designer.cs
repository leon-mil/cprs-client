namespace Cprs
{
    partial class frmDCPDup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnValid = new System.Windows.Forms.Button();
            this.btnUnmark = new System.Windows.Forms.Button();
            this.dgMaster = new System.Windows.Forms.DataGridView();
            this.btnDup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLockedBy = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.dgPrint = new System.Windows.Forms.DataGridView();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(433, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(347, 25);
            this.lblTitle.TabIndex = 62;
            this.lblTitle.Text = "DCP DUPLICATION RESEARCH";
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeight = 45;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.Location = new System.Drawing.Point(29, 125);
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
            this.dgData.Size = new System.Drawing.Size(1140, 506);
            this.dgData.TabIndex = 61;
            this.dgData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgData_DataBindingComplete);
            this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(782, 779);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 23);
            this.btnPrint.TabIndex = 60;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnValid
            // 
            this.btnValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValid.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnValid.Location = new System.Drawing.Point(414, 779);
            this.btnValid.Name = "btnValid";
            this.btnValid.Size = new System.Drawing.Size(140, 23);
            this.btnValid.TabIndex = 59;
            this.btnValid.TabStop = false;
            this.btnValid.Text = "MARK VALID";
            this.btnValid.UseVisualStyleBackColor = true;
            this.btnValid.EnabledChanged += new System.EventHandler(this.btnValid_EnabledChanged);
            this.btnValid.Click += new System.EventHandler(this.btnValid_Click);
            // 
            // btnUnmark
            // 
            this.btnUnmark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnmark.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnUnmark.Location = new System.Drawing.Point(598, 779);
            this.btnUnmark.Name = "btnUnmark";
            this.btnUnmark.Size = new System.Drawing.Size(140, 23);
            this.btnUnmark.TabIndex = 63;
            this.btnUnmark.TabStop = false;
            this.btnUnmark.Text = "UNMARK";
            this.btnUnmark.UseVisualStyleBackColor = true;
            this.btnUnmark.EnabledChanged += new System.EventHandler(this.btnUnmark_EnabledChanged);
            this.btnUnmark.Click += new System.EventHandler(this.btnUnmark_Click);
            // 
            // dgMaster
            // 
            this.dgMaster.AllowUserToAddRows = false;
            this.dgMaster.AllowUserToDeleteRows = false;
            this.dgMaster.AllowUserToResizeColumns = false;
            this.dgMaster.AllowUserToResizeRows = false;
            this.dgMaster.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMaster.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgMaster.Location = new System.Drawing.Point(29, 674);
            this.dgMaster.MultiSelect = false;
            this.dgMaster.Name = "dgMaster";
            this.dgMaster.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMaster.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgMaster.RowHeadersVisible = false;
            this.dgMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMaster.Size = new System.Drawing.Size(962, 70);
            this.dgMaster.TabIndex = 64;
            // 
            // btnDup
            // 
            this.btnDup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDup.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDup.Location = new System.Drawing.Point(222, 779);
            this.btnDup.Name = "btnDup";
            this.btnDup.Size = new System.Drawing.Size(140, 23);
            this.btnDup.TabIndex = 65;
            this.btnDup.TabStop = false;
            this.btnDup.Text = "MARK DUPLICATE";
            this.btnDup.UseVisualStyleBackColor = true;
            this.btnDup.EnabledChanged += new System.EventHandler(this.btnDup_EnabledChanged);
            this.btnDup.Click += new System.EventHandler(this.btnDup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(25, 651);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 20);
            this.label1.TabIndex = 66;
            this.label1.Text = "MASTER RECORD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(25, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 20);
            this.label2.TabIndex = 67;
            this.label2.Text = "POTENTIAL DUPLICATES";
            // 
            // lblLockedBy
            // 
            this.lblLockedBy.AutoSize = true;
            this.lblLockedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLockedBy.ForeColor = System.Drawing.Color.Yellow;
            this.lblLockedBy.Location = new System.Drawing.Point(141, 45);
            this.lblLockedBy.MaximumSize = new System.Drawing.Size(66, 18);
            this.lblLockedBy.MinimumSize = new System.Drawing.Size(66, 18);
            this.lblLockedBy.Name = "lblLockedBy";
            this.lblLockedBy.Size = new System.Drawing.Size(66, 18);
            this.lblLockedBy.TabIndex = 122;
            this.lblLockedBy.Text = "LOCKED";
            // 
            // dgPrint
            // 
            this.dgPrint.AllowUserToAddRows = false;
            this.dgPrint.AllowUserToDeleteRows = false;
            this.dgPrint.AllowUserToOrderColumns = true;
            this.dgPrint.AllowUserToResizeColumns = false;
            this.dgPrint.AllowUserToResizeRows = false;
            this.dgPrint.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgPrint.ColumnHeadersHeight = 45;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPrint.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgPrint.Location = new System.Drawing.Point(222, 134);
            this.dgPrint.MultiSelect = false;
            this.dgPrint.Name = "dgPrint";
            this.dgPrint.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgPrint.RowHeadersVisible = false;
            this.dgPrint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPrint.Size = new System.Drawing.Size(788, 413);
            this.dgPrint.TabIndex = 123;
            this.dgPrint.Visible = false;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.LightGray;
            this.txtTotal.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtTotal.Location = new System.Drawing.Point(131, 102);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(65, 20);
            this.txtTotal.TabIndex = 124;
            this.txtTotal.TabStop = false;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTotal.Location = new System.Drawing.Point(26, 100);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(99, 23);
            this.lblTotal.TabIndex = 125;
            this.lblTotal.Text = "TOTAL CASES :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDCPDup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 853);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.dgPrint);
            this.Controls.Add(this.lblLockedBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDup);
            this.Controls.Add(this.dgMaster);
            this.Controls.Add(this.btnUnmark);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnValid);
            this.Name = "frmDCPDup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDCPDup_FormClosing);
            this.Load += new System.EventHandler(this.frmDCPDUP_Load);
            this.Controls.SetChildIndex(this.btnValid, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnUnmark, 0);
            this.Controls.SetChildIndex(this.dgMaster, 0);
            this.Controls.SetChildIndex(this.btnDup, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblLockedBy, 0);
            this.Controls.SetChildIndex(this.dgPrint, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            this.Controls.SetChildIndex(this.lblTotal, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnValid;
        private System.Windows.Forms.Button btnUnmark;
        private System.Windows.Forms.DataGridView dgMaster;
        private System.Windows.Forms.Button btnDup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLockedBy;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.DataGridView dgPrint;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
    }
}