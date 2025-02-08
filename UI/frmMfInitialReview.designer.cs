namespace Cprs
{
    partial class frmMfInitialReview
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
            this.label2 = new System.Windows.Forms.Label();
            this.dgMFInitRev = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtpending = new System.Windows.Forms.TextBox();
            this.txtFinished = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtReview = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtNotStart = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbValueItem2 = new System.Windows.Forms.ComboBox();
            this.txtValueItem2 = new System.Windows.Forms.TextBox();
            this.cbItem2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.cbValueItem = new System.Windows.Forms.ComboBox();
            this.txtValueItem = new System.Windows.Forms.TextBox();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dgMFInitRev)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(400, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(409, 25);
            this.label2.TabIndex = 14;
            this.label2.Text = "MULTIFAMILY INITIAL CASE REVIEW";
            // 
            // dgMFInitRev
            // 
            this.dgMFInitRev.AllowUserToAddRows = false;
            this.dgMFInitRev.AllowUserToDeleteRows = false;
            this.dgMFInitRev.AllowUserToResizeColumns = false;
            this.dgMFInitRev.AllowUserToResizeRows = false;
            this.dgMFInitRev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgMFInitRev.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgMFInitRev.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMFInitRev.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMFInitRev.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMFInitRev.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgMFInitRev.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgMFInitRev.Location = new System.Drawing.Point(12, 182);
            this.dgMFInitRev.MultiSelect = false;
            this.dgMFInitRev.Name = "dgMFInitRev";
            this.dgMFInitRev.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMFInitRev.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgMFInitRev.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMFInitRev.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgMFInitRev.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgMFInitRev.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgMFInitRev.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgMFInitRev.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgMFInitRev.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgMFInitRev.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMFInitRev.Size = new System.Drawing.Size(1184, 411);
            this.dgMFInitRev.TabIndex = 15;
            this.dgMFInitRev.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgMFInitRev_RowPostPaint);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(666, 798);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 23);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnData
            // 
            this.btnData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnData.Location = new System.Drawing.Point(455, 798);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(120, 23);
            this.btnData.TabIndex = 6;
            this.btnData.Text = "DATA";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtpending);
            this.panel2.Controls.Add(this.txtFinished);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.txtReview);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Controls.Add(this.txtNotStart);
            this.panel2.ForeColor = System.Drawing.Color.DarkBlue;
            this.panel2.Location = new System.Drawing.Point(512, 613);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(229, 164);
            this.panel2.TabIndex = 280;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 23);
            this.label5.TabIndex = 283;
            this.label5.Text = "PENDING";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpending
            // 
            this.txtpending.BackColor = System.Drawing.Color.LightGray;
            this.txtpending.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtpending.Location = new System.Drawing.Point(111, 138);
            this.txtpending.Name = "txtpending";
            this.txtpending.Size = new System.Drawing.Size(100, 20);
            this.txtpending.TabIndex = 29;
            this.txtpending.TabStop = false;
            this.txtpending.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFinished
            // 
            this.txtFinished.BackColor = System.Drawing.Color.LightGray;
            this.txtFinished.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtFinished.Location = new System.Drawing.Point(111, 113);
            this.txtFinished.Name = "txtFinished";
            this.txtFinished.Size = new System.Drawing.Size(100, 20);
            this.txtFinished.TabIndex = 28;
            this.txtFinished.TabStop = false;
            this.txtFinished.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 23);
            this.label3.TabIndex = 27;
            this.label3.Text = "FINISHED";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 87);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(108, 23);
            this.label21.TabIndex = 26;
            this.label21.Text = "REVIEWED";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(108, 23);
            this.label20.TabIndex = 25;
            this.label20.Text = "NOT STARTED";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 33);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 23);
            this.label19.TabIndex = 24;
            this.label19.Text = "TOTAL";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkBlue;
            this.label18.Location = new System.Drawing.Point(69, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 23);
            this.label18.TabIndex = 23;
            this.label18.Text = "CASES";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReview
            // 
            this.txtReview.BackColor = System.Drawing.Color.LightGray;
            this.txtReview.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtReview.Location = new System.Drawing.Point(111, 87);
            this.txtReview.Name = "txtReview";
            this.txtReview.Size = new System.Drawing.Size(100, 20);
            this.txtReview.TabIndex = 0;
            this.txtReview.TabStop = false;
            this.txtReview.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.LightGray;
            this.txtTotal.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtTotal.Location = new System.Drawing.Point(111, 32);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 0;
            this.txtTotal.TabStop = false;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNotStart
            // 
            this.txtNotStart.BackColor = System.Drawing.Color.LightGray;
            this.txtNotStart.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtNotStart.Location = new System.Drawing.Point(111, 59);
            this.txtNotStart.Name = "txtNotStart";
            this.txtNotStart.Size = new System.Drawing.Size(100, 20);
            this.txtNotStart.TabIndex = 0;
            this.txtNotStart.TabStop = false;
            this.txtNotStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.cbValueItem2);
            this.groupBox1.Controls.Add(this.txtValueItem2);
            this.groupBox1.Controls.Add(this.cbItem2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.cbValueItem);
            this.groupBox1.Controls.Add(this.txtValueItem);
            this.groupBox1.Controls.Add(this.cbItem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(162, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(845, 85);
            this.groupBox1.TabIndex = 281;
            this.groupBox1.TabStop = false;
            // 
            // cbValueItem2
            // 
            this.cbValueItem2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValueItem2.Enabled = false;
            this.cbValueItem2.FormattingEnabled = true;
            this.cbValueItem2.Location = new System.Drawing.Point(616, 10);
            this.cbValueItem2.Name = "cbValueItem2";
            this.cbValueItem2.Size = new System.Drawing.Size(97, 21);
            this.cbValueItem2.TabIndex = 282;
            // 
            // txtValueItem2
            // 
            this.txtValueItem2.Location = new System.Drawing.Point(616, 11);
            this.txtValueItem2.MaxLength = 7;
            this.txtValueItem2.Name = "txtValueItem2";
            this.txtValueItem2.Size = new System.Drawing.Size(97, 20);
            this.txtValueItem2.TabIndex = 8;
            this.txtValueItem2.TextChanged += new System.EventHandler(this.txtValueItem2_TextChanged);
            this.txtValueItem2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValueItem_KeyPress);
            // 
            // cbItem2
            // 
            this.cbItem2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItem2.Enabled = false;
            this.cbItem2.FormattingEnabled = true;
            this.cbItem2.Items.AddRange(new object[] {
            "ID",
            "WORK STATUS",
            "DUPLICATE",
            "1st REVIEW",
            "2nd REVIEW"});
            this.cbItem2.Location = new System.Drawing.Point(480, 11);
            this.cbItem2.Name = "cbItem2";
            this.cbItem2.Size = new System.Drawing.Size(121, 21);
            this.cbItem2.TabIndex = 7;
            this.cbItem2.SelectedIndexChanged += new System.EventHandler(this.cbItem2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(446, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "and";
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClear.Location = new System.Drawing.Point(480, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchItem.Location = new System.Drawing.Point(359, 53);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchItem.TabIndex = 3;
            this.btnSearchItem.Text = "Search";
            this.btnSearchItem.UseVisualStyleBackColor = true;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbValueItem
            // 
            this.cbValueItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValueItem.FormattingEnabled = true;
            this.cbValueItem.Location = new System.Drawing.Point(341, 10);
            this.cbValueItem.Name = "cbValueItem";
            this.cbValueItem.Size = new System.Drawing.Size(97, 21);
            this.cbValueItem.TabIndex = 2;
            this.cbValueItem.SelectedIndexChanged += new System.EventHandler(this.cbValueItem_SelectedIndexChanged);
            // 
            // txtValueItem
            // 
            this.txtValueItem.Location = new System.Drawing.Point(342, 11);
            this.txtValueItem.MaxLength = 7;
            this.txtValueItem.Name = "txtValueItem";
            this.txtValueItem.Size = new System.Drawing.Size(97, 20);
            this.txtValueItem.TabIndex = 3;
            this.txtValueItem.TextChanged += new System.EventHandler(this.txtValueItem_TextChanged);
            this.txtValueItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValueItem_KeyPress);
            // 
            // cbItem
            // 
            this.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItem.FormattingEnabled = true;
            this.cbItem.Items.AddRange(new object[] {
            "ID",
            "WORK STATUS",
            "DUPLICATE",
            "1st REVIEW",
            "2nd REVIEW"});
            this.cbItem.Location = new System.Drawing.Point(209, 11);
            this.cbItem.Name = "cbItem";
            this.cbItem.Size = new System.Drawing.Size(121, 21);
            this.cbItem.TabIndex = 1;
            this.cbItem.SelectedIndexChanged += new System.EventHandler(this.cbItem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(144, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search By:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(24, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1180, 132);
            this.panel1.TabIndex = 282;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // frmMfInitialReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1208, 857);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgMFInitRev);
            this.Name = "frmMfInitialReview";
            this.Controls.SetChildIndex(this.dgMFInitRev, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnData, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgMFInitRev)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgMFInitRev;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFinished;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtReview;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtNotStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbValueItem;
        private System.Windows.Forms.TextBox txtValueItem;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ComboBox cbValueItem2;
        private System.Windows.Forms.TextBox txtValueItem2;
        private System.Windows.Forms.ComboBox cbItem2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtpending;
    }
}
