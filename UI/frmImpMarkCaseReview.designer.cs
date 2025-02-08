namespace Cprs
{
    partial class frmImpMarkCaseReview
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
            this.label2 = new System.Windows.Forms.Label();
            this.dgReview = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbldatef = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.cbValueItem = new System.Windows.Forms.ComboBox();
            this.txtValueItem = new System.Windows.Forms.TextBox();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCasesCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgReview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(394, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(429, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "IMPROVEMENTS MARK CASE REVIEW";
            // 
            // dgReview
            // 
            this.dgReview.AllowUserToAddRows = false;
            this.dgReview.AllowUserToDeleteRows = false;
            this.dgReview.AllowUserToResizeColumns = false;
            this.dgReview.AllowUserToResizeRows = false;
            this.dgReview.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgReview.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgReview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgReview.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgReview.Location = new System.Drawing.Point(25, 85);
            this.dgReview.Name = "dgReview";
            this.dgReview.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgReview.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgReview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgReview.Size = new System.Drawing.Size(1069, 584);
            this.dgReview.TabIndex = 0;
            this.dgReview.SelectionChanged += new System.EventHandler(this.dgReview_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbldatef);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.cbValueItem);
            this.groupBox1.Controls.Add(this.txtValueItem);
            this.groupBox1.Controls.Add(this.cbItem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(246, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 34);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lbldatef
            // 
            this.lbldatef.AutoSize = true;
            this.lbldatef.Location = new System.Drawing.Point(288, 11);
            this.lbldatef.Name = "lbldatef";
            this.lbldatef.Size = new System.Drawing.Size(79, 13);
            this.lbldatef.TabIndex = 7;
            this.lbldatef.Text = "MM/DD/YYYY";
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClear.Location = new System.Drawing.Point(467, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchItem.Location = new System.Drawing.Point(386, 8);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchItem.TabIndex = 5;
            this.btnSearchItem.Text = "Search";
            this.btnSearchItem.UseVisualStyleBackColor = true;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbValueItem
            // 
            this.cbValueItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValueItem.FormattingEnabled = true;
            this.cbValueItem.Items.AddRange(new object[] {
            "STRTDATE",
            "COMPDATE",
            "NEWTC",
            "STATUS",
            "OWNER",
            "RVITM5C",
            "RESPID",
            "FUTCOMPD",
            "ITEM6",
            "CAPEXP",
            "RBLDGS",
            "RUNITS"});
            this.cbValueItem.Location = new System.Drawing.Point(185, 8);
            this.cbValueItem.Name = "cbValueItem";
            this.cbValueItem.Size = new System.Drawing.Size(97, 21);
            this.cbValueItem.TabIndex = 4;
            // 
            // txtValueItem
            // 
            this.txtValueItem.Location = new System.Drawing.Point(185, 8);
            this.txtValueItem.Name = "txtValueItem";
            this.txtValueItem.Size = new System.Drawing.Size(97, 20);
            this.txtValueItem.TabIndex = 3;
            this.txtValueItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValueItem_KeyPress);
            // 
            // cbItem
            // 
            this.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItem.FormattingEnabled = true;
            this.cbItem.Items.AddRange(new object[] {
            "ID",
            "USRNME",
            "PRGDTM"});
            this.cbItem.Location = new System.Drawing.Point(84, 8);
            this.cbItem.Name = "cbItem";
            this.cbItem.Size = new System.Drawing.Size(95, 21);
            this.cbItem.TabIndex = 2;
            this.cbItem.SelectedIndexChanged += new System.EventHandler(this.cbItem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search By:";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(757, 814);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 22;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnData
            // 
            this.btnData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnData.Location = new System.Drawing.Point(542, 814);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(133, 23);
            this.btnData.TabIndex = 23;
            this.btnData.TabStop = false;
            this.btnData.Text = "DATA";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDelete.Location = new System.Drawing.Point(325, 814);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(133, 23);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCasesCount);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dgReview);
            this.panel1.Location = new System.Drawing.Point(44, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1129, 687);
            this.panel1.TabIndex = 7;
            // 
            // lblCasesCount
            // 
            this.lblCasesCount.AutoSize = true;
            this.lblCasesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCasesCount.Location = new System.Drawing.Point(22, 66);
            this.lblCasesCount.Name = "lblCasesCount";
            this.lblCasesCount.Size = new System.Drawing.Size(36, 16);
            this.lblCasesCount.TabIndex = 31;
            this.lblCasesCount.Text = "xxxx";
            // 
            // frmImpMarkCaseReview
            // 
            this.AcceptButton = this.btnSearchItem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "frmImpMarkCaseReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImpMarkCaseReview_FormClosing);
            this.Load += new System.EventHandler(this.frmImpMarkCase_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValueItem_KeyPress);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgReview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgReview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbldatef;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbValueItem;
        private System.Windows.Forms.TextBox txtValueItem;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCasesCount;
    }
}