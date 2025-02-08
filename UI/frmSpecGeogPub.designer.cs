namespace Cprs
{
    partial class frmSpecGeogPub
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdState = new System.Windows.Forms.RadioButton();
            this.rdDivision = new System.Windows.Forms.RadioButton();
            this.rdRegion = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdCV = new System.Windows.Forms.RadioButton();
            this.rdValue = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lbllabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(43, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1105, 37);
            this.lblTitle.TabIndex = 33;
            this.lblTitle.Text = "ANNUAL VALUE OF STATE AND LOCAL CONSTRUCTION";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(516, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 16);
            this.label2.TabIndex = 35;
            this.label2.Text = "Millions of Dollars";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(475, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 39;
            this.label3.Text = "Select Year:";
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Items.AddRange(new object[] {
            "2017",
            "2016"});
            this.cbYear.Location = new System.Drawing.Point(565, 148);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(86, 21);
            this.cbYear.TabIndex = 40;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(686, 809);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 61;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(390, 809);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 60;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdState);
            this.panel1.Controls.Add(this.rdDivision);
            this.panel1.Controls.Add(this.rdRegion);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(605, 182);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 28);
            this.panel1.TabIndex = 66;
            // 
            // rdState
            // 
            this.rdState.AutoSize = true;
            this.rdState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdState.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdState.Location = new System.Drawing.Point(349, 4);
            this.rdState.Name = "rdState";
            this.rdState.Size = new System.Drawing.Size(55, 17);
            this.rdState.TabIndex = 64;
            this.rdState.Text = "State";
            this.rdState.UseVisualStyleBackColor = true;
            this.rdState.CheckedChanged += new System.EventHandler(this.rdState_CheckedChanged);
            // 
            // rdDivision
            // 
            this.rdDivision.AutoSize = true;
            this.rdDivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDivision.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdDivision.Location = new System.Drawing.Point(246, 4);
            this.rdDivision.Name = "rdDivision";
            this.rdDivision.Size = new System.Drawing.Size(70, 17);
            this.rdDivision.TabIndex = 63;
            this.rdDivision.Text = "Division";
            this.rdDivision.UseVisualStyleBackColor = true;
            this.rdDivision.CheckedChanged += new System.EventHandler(this.rdDivision_CheckedChanged);
            // 
            // rdRegion
            // 
            this.rdRegion.AutoSize = true;
            this.rdRegion.Checked = true;
            this.rdRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdRegion.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdRegion.Location = new System.Drawing.Point(154, 4);
            this.rdRegion.Name = "rdRegion";
            this.rdRegion.Size = new System.Drawing.Size(65, 17);
            this.rdRegion.TabIndex = 62;
            this.rdRegion.TabStop = true;
            this.rdRegion.Text = "Region";
            this.rdRegion.UseVisualStyleBackColor = true;
            this.rdRegion.CheckedChanged += new System.EventHandler(this.rdRegion_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 15);
            this.label4.TabIndex = 61;
            this.label4.Text = "Select Geographic:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(535, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 65;
            this.label1.Text = "By Region";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdCV);
            this.panel2.Controls.Add(this.rdValue);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(181, 182);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 28);
            this.panel2.TabIndex = 67;
            // 
            // rdCV
            // 
            this.rdCV.AutoSize = true;
            this.rdCV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdCV.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdCV.Location = new System.Drawing.Point(180, 3);
            this.rdCV.Name = "rdCV";
            this.rdCV.Size = new System.Drawing.Size(47, 17);
            this.rdCV.TabIndex = 67;
            this.rdCV.Text = "CVs";
            this.rdCV.UseVisualStyleBackColor = true;
            this.rdCV.CheckedChanged += new System.EventHandler(this.rdCV_CheckedChanged);
            // 
            // rdValue
            // 
            this.rdValue.AutoSize = true;
            this.rdValue.Checked = true;
            this.rdValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdValue.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdValue.Location = new System.Drawing.Point(111, 3);
            this.rdValue.Name = "rdValue";
            this.rdValue.Size = new System.Drawing.Size(63, 17);
            this.rdValue.TabIndex = 64;
            this.rdValue.TabStop = true;
            this.rdValue.Text = "Values";
            this.rdValue.UseVisualStyleBackColor = true;
            this.rdValue.CheckedChanged += new System.EventHandler(this.rdValue_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 15);
            this.label5.TabIndex = 63;
            this.label5.Text = "Select Display:";
            // 
            // lbllabel
            // 
            this.lbllabel.AutoSize = true;
            this.lbllabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbllabel.Location = new System.Drawing.Point(923, 742);
            this.lbllabel.Name = "lbllabel";
            this.lbllabel.Size = new System.Drawing.Size(215, 16);
            this.lbllabel.TabIndex = 68;
            this.lbllabel.Text = "Double Click a Line to Expand";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgData.Location = new System.Drawing.Point(48, 215);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgData.RowHeadersVisible = false;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1090, 524);
            this.dgData.TabIndex = 69;
            this.dgData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellDoubleClick);
            // 
            // frmSpecGeogPub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 870);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.lbllabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmSpecGeogPub";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSpecGeogPub_FormClosing);
            this.Load += new System.EventHandler(this.frmSpecGeogPub_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cbYear, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.lbllabel, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdState;
        private System.Windows.Forms.RadioButton rdDivision;
        private System.Windows.Forms.RadioButton rdRegion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdCV;
        private System.Windows.Forms.RadioButton rdValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbllabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgData;
    }
}