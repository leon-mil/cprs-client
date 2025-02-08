namespace Cprs
{
    partial class frmMarkCaseReview
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCasesCount = new System.Windows.Forms.Label();
            this.tbCaseReview = new System.Windows.Forms.TabControl();
            this.tbProject = new System.Windows.Forms.TabPage();
            this.dgProjMarkCase = new System.Windows.Forms.DataGridView();
            this.tbRespondent = new System.Windows.Forms.TabPage();
            this.dgRespMarkCase = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRESPValueItem = new System.Windows.Forms.ComboBox();
            this.lbldatef = new System.Windows.Forms.Label();
            this.txtRESPValueItem = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.cbIDValueItem = new System.Windows.Forms.ComboBox();
            this.txtIDValueItem = new System.Windows.Forms.TextBox();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnData = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.lblTab = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tbCaseReview.SuspendLayout();
            this.tbProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjMarkCase)).BeginInit();
            this.tbRespondent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRespMarkCase)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCasesCount);
            this.panel1.Controls.Add(this.tbCaseReview);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(35, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1138, 657);
            this.panel1.TabIndex = 9;
            // 
            // lblCasesCount
            // 
            this.lblCasesCount.AutoSize = true;
            this.lblCasesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCasesCount.Location = new System.Drawing.Point(6, 72);
            this.lblCasesCount.Name = "lblCasesCount";
            this.lblCasesCount.Size = new System.Drawing.Size(35, 16);
            this.lblCasesCount.TabIndex = 31;
            this.lblCasesCount.Text = "xxxx";
            // 
            // tbCaseReview
            // 
            this.tbCaseReview.Controls.Add(this.tbProject);
            this.tbCaseReview.Controls.Add(this.tbRespondent);
            this.tbCaseReview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCaseReview.Location = new System.Drawing.Point(2, 91);
            this.tbCaseReview.Name = "tbCaseReview";
            this.tbCaseReview.SelectedIndex = 0;
            this.tbCaseReview.Size = new System.Drawing.Size(1133, 566);
            this.tbCaseReview.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbCaseReview.TabIndex = 19;
            this.tbCaseReview.SelectedIndexChanged += new System.EventHandler(this.tbCaseReview_SelectedIndexChanged);
            this.tbCaseReview.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbCaseReview_Selected);
            // 
            // tbProject
            // 
            this.tbProject.Controls.Add(this.dgProjMarkCase);
            this.tbProject.Location = new System.Drawing.Point(4, 22);
            this.tbProject.Name = "tbProject";
            this.tbProject.Padding = new System.Windows.Forms.Padding(3);
            this.tbProject.Size = new System.Drawing.Size(1125, 540);
            this.tbProject.TabIndex = 2;
            this.tbProject.Text = "PROJECT";
            this.tbProject.UseVisualStyleBackColor = true;
            // 
            // dgProjMarkCase
            // 
            this.dgProjMarkCase.AllowUserToAddRows = false;
            this.dgProjMarkCase.AllowUserToDeleteRows = false;
            this.dgProjMarkCase.AllowUserToResizeColumns = false;
            this.dgProjMarkCase.AllowUserToResizeRows = false;
            this.dgProjMarkCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgProjMarkCase.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgProjMarkCase.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgProjMarkCase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProjMarkCase.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgProjMarkCase.Location = new System.Drawing.Point(3, 3);
            this.dgProjMarkCase.MultiSelect = false;
            this.dgProjMarkCase.Name = "dgProjMarkCase";
            this.dgProjMarkCase.ReadOnly = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgProjMarkCase.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgProjMarkCase.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgProjMarkCase.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgProjMarkCase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProjMarkCase.Size = new System.Drawing.Size(1116, 531);
            this.dgProjMarkCase.TabIndex = 4;
            this.dgProjMarkCase.SelectionChanged += new System.EventHandler(this.dgProjMarkCase_SelectionChanged);
            // 
            // tbRespondent
            // 
            this.tbRespondent.Controls.Add(this.dgRespMarkCase);
            this.tbRespondent.Location = new System.Drawing.Point(4, 22);
            this.tbRespondent.Name = "tbRespondent";
            this.tbRespondent.Padding = new System.Windows.Forms.Padding(3);
            this.tbRespondent.Size = new System.Drawing.Size(1125, 540);
            this.tbRespondent.TabIndex = 1;
            this.tbRespondent.Text = "RESPONDENT";
            this.tbRespondent.UseVisualStyleBackColor = true;
            // 
            // dgRespMarkCase
            // 
            this.dgRespMarkCase.AllowUserToAddRows = false;
            this.dgRespMarkCase.AllowUserToDeleteRows = false;
            this.dgRespMarkCase.AllowUserToResizeColumns = false;
            this.dgRespMarkCase.AllowUserToResizeRows = false;
            this.dgRespMarkCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRespMarkCase.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgRespMarkCase.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgRespMarkCase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRespMarkCase.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgRespMarkCase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRespMarkCase.Location = new System.Drawing.Point(3, 3);
            this.dgRespMarkCase.MultiSelect = false;
            this.dgRespMarkCase.Name = "dgRespMarkCase";
            this.dgRespMarkCase.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRespMarkCase.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgRespMarkCase.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgRespMarkCase.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgRespMarkCase.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgRespMarkCase.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgRespMarkCase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRespMarkCase.Size = new System.Drawing.Size(1116, 530);
            this.dgRespMarkCase.TabIndex = 5;
            this.dgRespMarkCase.SelectionChanged += new System.EventHandler(this.dgRespMarkCase_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRESPValueItem);
            this.groupBox1.Controls.Add(this.lbldatef);
            this.groupBox1.Controls.Add(this.txtRESPValueItem);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.cbIDValueItem);
            this.groupBox1.Controls.Add(this.txtIDValueItem);
            this.groupBox1.Controls.Add(this.cbItem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(247, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 34);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cbRESPValueItem
            // 
            this.cbRESPValueItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRESPValueItem.FormattingEnabled = true;
            this.cbRESPValueItem.Location = new System.Drawing.Point(185, 8);
            this.cbRESPValueItem.Name = "cbRESPValueItem";
            this.cbRESPValueItem.Size = new System.Drawing.Size(97, 21);
            this.cbRESPValueItem.TabIndex = 20;
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
            // txtRESPValueItem
            // 
            this.txtRESPValueItem.Location = new System.Drawing.Point(185, 8);
            this.txtRESPValueItem.Name = "txtRESPValueItem";
            this.txtRESPValueItem.Size = new System.Drawing.Size(97, 20);
            this.txtRESPValueItem.TabIndex = 6;
            this.txtRESPValueItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRESPValueItem_KeyPress);
            // 
            // btnClear
            // 
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
            this.btnSearchItem.Location = new System.Drawing.Point(386, 8);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchItem.TabIndex = 5;
            this.btnSearchItem.Text = "Search";
            this.btnSearchItem.UseVisualStyleBackColor = true;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbIDValueItem
            // 
            this.cbIDValueItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIDValueItem.FormattingEnabled = true;
            this.cbIDValueItem.Location = new System.Drawing.Point(185, 8);
            this.cbIDValueItem.Name = "cbIDValueItem";
            this.cbIDValueItem.Size = new System.Drawing.Size(97, 21);
            this.cbIDValueItem.TabIndex = 4;
            // 
            // txtIDValueItem
            // 
            this.txtIDValueItem.Location = new System.Drawing.Point(185, 8);
            this.txtIDValueItem.Name = "txtIDValueItem";
            this.txtIDValueItem.Size = new System.Drawing.Size(97, 20);
            this.txtIDValueItem.TabIndex = 3;
            this.txtIDValueItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIDValueItem_KeyPress);
            // 
            // cbItem
            // 
            this.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItem.FormattingEnabled = true;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(497, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "MARK CASE REVIEW";
            // 
            // btnData
            // 
            this.btnData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnData.Location = new System.Drawing.Point(542, 814);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(133, 23);
            this.btnData.TabIndex = 26;
            this.btnData.TabStop = false;
            this.btnData.Text = "C-700";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDelete.Location = new System.Drawing.Point(354, 814);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(133, 23);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(740, 814);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 25;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.EnabledChanged += new System.EventHandler(this.btnPrint_EnabledChanged);
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblTab
            // 
            this.lblTab.AutoSize = true;
            this.lblTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab.Location = new System.Drawing.Point(581, 97);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(49, 18);
            this.lblTab.TabIndex = 28;
            this.lblTab.Text = "Table";
            // 
            // frmMarkCaseReview
            // 
            this.AcceptButton = this.btnSearchItem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 861);
            this.Controls.Add(this.lblTab);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "frmMarkCaseReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMarkCaseReview_FormClosing);
            this.Load += new System.EventHandler(this.frmMarkCase_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnData, 0);
            this.Controls.SetChildIndex(this.lblTab, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbCaseReview.ResumeLayout(false);
            this.tbProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProjMarkCase)).EndInit();
            this.tbRespondent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRespMarkCase)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbldatef;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbIDValueItem;
        private System.Windows.Forms.TextBox txtIDValueItem;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tbCaseReview;
        private System.Windows.Forms.TabPage tbProject;
        private System.Windows.Forms.DataGridView dgProjMarkCase;
        private System.Windows.Forms.TabPage tbRespondent;
        private System.Windows.Forms.DataGridView dgRespMarkCase;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.ComboBox cbRESPValueItem;
        private System.Windows.Forms.TextBox txtRESPValueItem;
        private System.Windows.Forms.Label lblCasesCount;
    }
}