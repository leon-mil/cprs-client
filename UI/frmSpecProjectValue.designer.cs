namespace Cprs
{
    partial class frmSpecProjectValue
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
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgPrint = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.ComparisonPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rdV3 = new System.Windows.Forms.RadioButton();
            this.rdV1 = new System.Windows.Forms.RadioButton();
            this.rdV2 = new System.Windows.Forms.RadioButton();
            this.SurveyPanel = new System.Windows.Forms.Panel();
            this.rdFederal = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rdState = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdPrivate = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).BeginInit();
            this.ComparisonPanel.SuspendLayout();
            this.SurveyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(486, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 44;
            this.label3.Text = "Select Year:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgPrint);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.ComparisonPanel);
            this.panel1.Controls.Add(this.SurveyPanel);
            this.panel1.Controls.Add(this.dgData);
            this.panel1.Location = new System.Drawing.Point(19, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1157, 645);
            this.panel1.TabIndex = 47;
            // 
            // dgPrint
            // 
            this.dgPrint.AllowUserToAddRows = false;
            this.dgPrint.AllowUserToDeleteRows = false;
            this.dgPrint.AllowUserToResizeColumns = false;
            this.dgPrint.AllowUserToResizeRows = false;
            this.dgPrint.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPrint.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPrint.Location = new System.Drawing.Point(76, 142);
            this.dgPrint.MultiSelect = false;
            this.dgPrint.Name = "dgPrint";
            this.dgPrint.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgPrint.RowHeadersVisible = false;
            this.dgPrint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPrint.Size = new System.Drawing.Size(793, 296);
            this.dgPrint.TabIndex = 14;
            this.dgPrint.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(508, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Percent Difference";
            // 
            // ComparisonPanel
            // 
            this.ComparisonPanel.Controls.Add(this.label4);
            this.ComparisonPanel.Controls.Add(this.rdV3);
            this.ComparisonPanel.Controls.Add(this.rdV1);
            this.ComparisonPanel.Controls.Add(this.rdV2);
            this.ComparisonPanel.Location = new System.Drawing.Point(51, 37);
            this.ComparisonPanel.Name = "ComparisonPanel";
            this.ComparisonPanel.Size = new System.Drawing.Size(1103, 35);
            this.ComparisonPanel.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(12, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select Comparison Type:";
            // 
            // rdV3
            // 
            this.rdV3.AutoSize = true;
            this.rdV3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdV3.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdV3.Location = new System.Drawing.Point(745, 11);
            this.rdV3.Name = "rdV3";
            this.rdV3.Size = new System.Drawing.Size(311, 19);
            this.rdV3.TabIndex = 8;
            this.rdV3.Text = "Census Final Value vs Census Original Value";
            this.rdV3.UseVisualStyleBackColor = true;
            this.rdV3.CheckedChanged += new System.EventHandler(this.radioButtonsComparison_CheckedChanged);
            // 
            // rdV1
            // 
            this.rdV1.AutoSize = true;
            this.rdV1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdV1.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdV1.Location = new System.Drawing.Point(188, 11);
            this.rdV1.Name = "rdV1";
            this.rdV1.Size = new System.Drawing.Size(273, 19);
            this.rdV1.TabIndex = 6;
            this.rdV1.Text = "Census Original Value vs Project Value";
            this.rdV1.UseVisualStyleBackColor = true;
            this.rdV1.CheckedChanged += new System.EventHandler(this.radioButtonsComparison_CheckedChanged);
            // 
            // rdV2
            // 
            this.rdV2.AutoSize = true;
            this.rdV2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdV2.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdV2.Location = new System.Drawing.Point(485, 11);
            this.rdV2.Name = "rdV2";
            this.rdV2.Size = new System.Drawing.Size(254, 19);
            this.rdV2.TabIndex = 7;
            this.rdV2.Text = "Census Final Value vs Project Value";
            this.rdV2.UseVisualStyleBackColor = true;
            this.rdV2.CheckedChanged += new System.EventHandler(this.radioButtonsComparison_CheckedChanged);
            // 
            // SurveyPanel
            // 
            this.SurveyPanel.Controls.Add(this.rdFederal);
            this.SurveyPanel.Controls.Add(this.label2);
            this.SurveyPanel.Controls.Add(this.rdState);
            this.SurveyPanel.Controls.Add(this.rdAll);
            this.SurveyPanel.Controls.Add(this.rdPrivate);
            this.SurveyPanel.Location = new System.Drawing.Point(253, 3);
            this.SurveyPanel.Name = "SurveyPanel";
            this.SurveyPanel.Size = new System.Drawing.Size(637, 28);
            this.SurveyPanel.TabIndex = 11;
            // 
            // rdFederal
            // 
            this.rdFederal.AutoSize = true;
            this.rdFederal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdFederal.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdFederal.Location = new System.Drawing.Point(500, 6);
            this.rdFederal.Name = "rdFederal";
            this.rdFederal.Size = new System.Drawing.Size(74, 19);
            this.rdFederal.TabIndex = 9;
            this.rdFederal.Text = "Federal";
            this.rdFederal.UseVisualStyleBackColor = true;
            this.rdFederal.CheckedChanged += new System.EventHandler(this.radioButtonsSurvey_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(42, 8);
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
            this.rdState.Location = new System.Drawing.Point(355, 6);
            this.rdState.Name = "rdState";
            this.rdState.Size = new System.Drawing.Size(125, 19);
            this.rdState.TabIndex = 8;
            this.rdState.Text = "State and Local";
            this.rdState.UseVisualStyleBackColor = true;
            this.rdState.CheckedChanged += new System.EventHandler(this.radioButtonsSurvey_CheckedChanged);
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAll.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdAll.Location = new System.Drawing.Point(162, 6);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(94, 19);
            this.rdAll.TabIndex = 6;
            this.rdAll.Text = "All Surveys";
            this.rdAll.UseVisualStyleBackColor = true;
            this.rdAll.CheckedChanged += new System.EventHandler(this.radioButtonsSurvey_CheckedChanged);
            // 
            // rdPrivate
            // 
            this.rdPrivate.AutoSize = true;
            this.rdPrivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPrivate.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdPrivate.Location = new System.Drawing.Point(271, 6);
            this.rdPrivate.Name = "rdPrivate";
            this.rdPrivate.Size = new System.Drawing.Size(69, 19);
            this.rdPrivate.TabIndex = 7;
            this.rdPrivate.Text = "Private";
            this.rdPrivate.UseVisualStyleBackColor = true;
            this.rdPrivate.CheckedChanged += new System.EventHandler(this.radioButtonsSurvey_CheckedChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgData.Location = new System.Drawing.Point(17, 112);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgData.RowHeadersVisible = false;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1120, 520);
            this.dgData.TabIndex = 0;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle2.Location = new System.Drawing.Point(171, 80);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(828, 20);
            this.lblTitle2.TabIndex = 46;
            this.lblTitle2.Text = "Project Completed From 2016-2017 - All Surveys";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(31, 49);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1128, 25);
            this.lblTitle.TabIndex = 45;
            this.lblTitle.Text = "COMPARISON OF CENSUS ORIGINAL VALUE vs DODGE\'S PROJECT VALUE";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(576, 113);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(86, 21);
            this.cbYear.TabIndex = 48;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(690, 813);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 50;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(361, 813);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(133, 23);
            this.btnTable.TabIndex = 49;
            this.btnTable.TabStop = false;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmSpecProjectValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 870);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmSpecProjectValue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSpecProjectValue_FormClosing);
            this.Load += new System.EventHandler(this.frmSpecProjectValue_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblTitle2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cbYear, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).EndInit();
            this.ComparisonPanel.ResumeLayout(false);
            this.ComparisonPanel.PerformLayout();
            this.SurveyPanel.ResumeLayout(false);
            this.SurveyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel ComparisonPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdV3;
        private System.Windows.Forms.RadioButton rdV1;
        private System.Windows.Forms.RadioButton rdV2;
        private System.Windows.Forms.Panel SurveyPanel;
        private System.Windows.Forms.RadioButton rdFederal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdState;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.RadioButton rdPrivate;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Label label5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgPrint;
    }
}