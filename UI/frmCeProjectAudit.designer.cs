namespace Cprs
{
    partial class frmCeProjectAudit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbldatef = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.cbValueItem = new System.Windows.Forms.ComboBox();
            this.txtValueItem = new System.Windows.Forms.TextBox();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgItem = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItem)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(247, 19);
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
            this.txtValueItem.MaxLength = 0;
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
            "VARNME",
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
            // dgItem
            // 
            this.dgItem.AllowUserToAddRows = false;
            this.dgItem.AllowUserToDeleteRows = false;
            this.dgItem.AllowUserToResizeColumns = false;
            this.dgItem.AllowUserToResizeRows = false;
            this.dgItem.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgItem.Location = new System.Drawing.Point(25, 85);
            this.dgItem.Name = "dgItem";
            this.dgItem.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItem.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItem.Size = new System.Drawing.Size(1069, 591);
            this.dgItem.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(424, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "IMPROVEMENTS PROJECT AUDIT";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dgItem);
            this.panel1.Location = new System.Drawing.Point(35, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1129, 688);
            this.panel1.TabIndex = 5;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrevious.Location = new System.Drawing.Point(573, 814);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(133, 23);
            this.btnPrevious.TabIndex = 35;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.Text = "PREVIOUS";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // frmCeProjectAudit
            // 
            this.AcceptButton = this.btnSearchItem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1216, 873);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.MinimumSize = new System.Drawing.Size(1224, 900);
            this.Name = "frmCeProjectAudit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCeProjectAudit_FormClosing);
            this.Load += new System.EventHandler(this.frmCeProjectAudit_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItem)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgItem;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbValueItem;
        private System.Windows.Forms.TextBox txtValueItem;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.Label lbldatef;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnPrevious;
    }
}
