namespace Cprs
{
    partial class frmSpecStrt
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnProject = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdV4 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rdV3 = new System.Windows.Forms.RadioButton();
            this.rdV1 = new System.Windows.Forms.RadioButton();
            this.rdV2 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdFederal = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rdState = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdPrivate = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(24, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1105, 25);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "COMPARISON OF DODGE\'S START DATE vs CENSUS START DATE IN YYYY";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(158, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(828, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "ALL SURVEYS - ALL VALUES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(878, 792);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 48;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnProject
            // 
            this.btnProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProject.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnProject.Location = new System.Drawing.Point(629, 792);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(160, 23);
            this.btnProject.TabIndex = 46;
            this.btnProject.TabStop = false;
            this.btnProject.Text = "PROJECT OVER $100 M";
            this.btnProject.UseVisualStyleBackColor = true;
            this.btnProject.Click += new System.EventHandler(this.btnProject_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(176, 792);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 45;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummary.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSummary.Location = new System.Drawing.Point(399, 792);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(133, 23);
            this.btnSummary.TabIndex = 44;
            this.btnSummary.TabStop = false;
            this.btnSummary.Text = "SUMMARY";
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Location = new System.Drawing.Point(12, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1157, 622);
            this.panel1.TabIndex = 43;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdV4);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.rdV3);
            this.panel3.Controls.Add(this.rdV1);
            this.panel3.Controls.Add(this.rdV2);
            this.panel3.Location = new System.Drawing.Point(220, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(708, 25);
            this.panel3.TabIndex = 12;
            // 
            // rdV4
            // 
            this.rdV4.AutoSize = true;
            this.rdV4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdV4.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdV4.Location = new System.Drawing.Point(546, 4);
            this.rdV4.Name = "rdV4";
            this.rdV4.Size = new System.Drawing.Size(151, 19);
            this.rdV4.TabIndex = 9;
            this.rdV4.Text = "Less than $750,000";
            this.rdV4.UseVisualStyleBackColor = true;
            this.rdV4.CheckedChanged += new System.EventHandler(this.rdV4_CheckedChanged);
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
            // rdV3
            // 
            this.rdV3.AutoSize = true;
            this.rdV3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdV3.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdV3.Location = new System.Drawing.Point(370, 3);
            this.rdV3.Name = "rdV3";
            this.rdV3.Size = new System.Drawing.Size(170, 19);
            this.rdV3.TabIndex = 8;
            this.rdV3.Text = "$750,000 - $4,999,000";
            this.rdV3.UseVisualStyleBackColor = true;
            this.rdV3.CheckedChanged += new System.EventHandler(this.rdV3_CheckedChanged);
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
            this.panel2.Location = new System.Drawing.Point(254, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(637, 28);
            this.panel2.TabIndex = 11;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(458, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Within Dodge\'s Range 60%";
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
            this.dgData.Location = new System.Drawing.Point(39, 93);
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
            this.dgData.Size = new System.Drawing.Size(1090, 514);
            this.dgData.TabIndex = 0;
            this.dgData.SelectionChanged += new System.EventHandler(this.dgData_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(458, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Select Year:";
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(548, 113);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(86, 21);
            this.cbYear.TabIndex = 2;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmSpecStrt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 853);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.btnProject);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.btnSummary);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmSpecStrt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSpecStrt_FormClosing);
            this.Load += new System.EventHandler(this.frmTabSpecStrtdate_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnSummary, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.btnProject, 0);
            this.Controls.SetChildIndex(this.cbYear, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnProject;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.RadioButton rdV4;
        private System.Windows.Forms.RadioButton rdV3;
        private System.Windows.Forms.RadioButton rdV2;
        private System.Windows.Forms.RadioButton rdV1;
        private System.Windows.Forms.RadioButton rdFederal;
        private System.Windows.Forms.RadioButton rdState;
        private System.Windows.Forms.RadioButton rdPrivate;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}