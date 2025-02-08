namespace Cprs
{
    partial class frmSpecStrtSum
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rdV1 = new System.Windows.Forms.RadioButton();
            this.rdV2 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdFederal = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rdState = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdPrivate = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(127, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(893, 20);
            this.label1.TabIndex = 50;
            this.label1.Text = "ALL SURVEYS - ALL VALUES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(25, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1105, 25);
            this.lblTitle.TabIndex = 49;
            this.lblTitle.Text = "SUMMARY FOR COMPARISON OF DODGE\'S START DATE vs CENSUS START DATE ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(643, 792);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 53;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrevious.Location = new System.Drawing.Point(403, 792);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(133, 23);
            this.btnPrevious.TabIndex = 52;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.Text = "PREVIOUS";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Location = new System.Drawing.Point(13, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1157, 601);
            this.panel1.TabIndex = 51;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.rdV1);
            this.panel3.Controls.Add(this.rdV2);
            this.panel3.Location = new System.Drawing.Point(374, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(376, 25);
            this.panel3.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(31, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select Value:";
            // 
            // rdV1
            // 
            this.rdV1.AutoSize = true;
            this.rdV1.Checked = true;
            this.rdV1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdV1.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdV1.Location = new System.Drawing.Point(120, 5);
            this.rdV1.Name = "rdV1";
            this.rdV1.Size = new System.Drawing.Size(88, 19);
            this.rdV1.TabIndex = 6;
            this.rdV1.TabStop = true;
            this.rdV1.Text = "All Values";
            this.rdV1.UseVisualStyleBackColor = true;
            this.rdV1.CheckedChanged += new System.EventHandler(this.rdV1_CheckedChanged);
            // 
            // rdV2
            // 
            this.rdV2.AutoSize = true;
            this.rdV2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdV2.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdV2.Location = new System.Drawing.Point(214, 3);
            this.rdV2.Name = "rdV2";
            this.rdV2.Size = new System.Drawing.Size(143, 19);
            this.rdV2.TabIndex = 7;
            this.rdV2.Text = "$5 Million or More";
            this.rdV2.UseVisualStyleBackColor = true;
            this.rdV2.CheckedChanged += new System.EventHandler(this.rdV2_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdFederal);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.rdState);
            this.panel2.Controls.Add(this.rdAll);
            this.panel2.Controls.Add(this.rdPrivate);
            this.panel2.Location = new System.Drawing.Point(274, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 28);
            this.panel2.TabIndex = 13;
            // 
            // rdFederal
            // 
            this.rdFederal.AutoSize = true;
            this.rdFederal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdFederal.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdFederal.Location = new System.Drawing.Point(494, 8);
            this.rdFederal.Name = "rdFederal";
            this.rdFederal.Size = new System.Drawing.Size(74, 19);
            this.rdFederal.TabIndex = 9;
            this.rdFederal.Text = "Federal";
            this.rdFederal.UseVisualStyleBackColor = true;
            this.rdFederal.CheckedChanged += new System.EventHandler(this.rdFederal_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(60, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Survey:";
            // 
            // rdState
            // 
            this.rdState.AutoSize = true;
            this.rdState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdState.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdState.Location = new System.Drawing.Point(351, 8);
            this.rdState.Name = "rdState";
            this.rdState.Size = new System.Drawing.Size(125, 19);
            this.rdState.TabIndex = 8;
            this.rdState.Text = "State and Local";
            this.rdState.UseVisualStyleBackColor = true;
            this.rdState.CheckedChanged += new System.EventHandler(this.rdState_CheckedChanged);
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Checked = true;
            this.rdAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAll.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdAll.Location = new System.Drawing.Point(163, 8);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(94, 19);
            this.rdAll.TabIndex = 6;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "All Surveys";
            this.rdAll.UseVisualStyleBackColor = true;
            this.rdAll.CheckedChanged += new System.EventHandler(this.rdAll_CheckedChanged);
            // 
            // rdPrivate
            // 
            this.rdPrivate.AutoSize = true;
            this.rdPrivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPrivate.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdPrivate.Location = new System.Drawing.Point(273, 8);
            this.rdPrivate.Name = "rdPrivate";
            this.rdPrivate.Size = new System.Drawing.Size(69, 19);
            this.rdPrivate.TabIndex = 7;
            this.rdPrivate.Text = "Private";
            this.rdPrivate.UseVisualStyleBackColor = true;
            this.rdPrivate.CheckedChanged += new System.EventHandler(this.rdPrivate_CheckedChanged);
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
            this.dgData.Location = new System.Drawing.Point(39, 80);
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
            this.dgData.Size = new System.Drawing.Size(1090, 518);
            this.dgData.TabIndex = 0;
            this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
            // 
            // frmSpecStrtSum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 853);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.panel1);
            this.Name = "frmSpecStrtSum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSpecStrtSum_FormClosing);
            this.Load += new System.EventHandler(this.frmSpecStrtSum_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdV1;
        private System.Windows.Forms.RadioButton rdV2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdFederal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdState;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.RadioButton rdPrivate;
    }
}