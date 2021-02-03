namespace Cprs
{
    partial class frmReferralReview
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
            this.lblTab = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckMySec = new System.Windows.Forms.CheckBox();
            this.lblCasesCount = new System.Windows.Forms.Label();
            this.tbReferrals = new System.Windows.Forms.TabControl();
            this.tbProject = new System.Windows.Forms.TabPage();
            this.dgProjReferrals = new System.Windows.Forms.DataGridView();
            this.tbRespondent = new System.Windows.Forms.TabPage();
            this.dgRespReferrals = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRESPValueItem = new System.Windows.Forms.ComboBox();
            this.lbldatef = new System.Windows.Forms.Label();
            this.txtRESPValueItem = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.cbPROJValueItem = new System.Windows.Forms.ComboBox();
            this.txtPROJValueItem = new System.Windows.Forms.TextBox();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnData = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tbReferrals.SuspendLayout();
            this.tbProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjReferrals)).BeginInit();
            this.tbRespondent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRespReferrals)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTab
            // 
            this.lblTab.AutoSize = true;
            this.lblTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTab.Location = new System.Drawing.Point(585, 89);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(49, 18);
            this.lblTab.TabIndex = 34;
            this.lblTab.Text = "Table";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckMySec);
            this.panel1.Controls.Add(this.lblCasesCount);
            this.panel1.Controls.Add(this.tbReferrals);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(39, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1138, 657);
            this.panel1.TabIndex = 30;
            // 
            // ckMySec
            // 
            this.ckMySec.AutoSize = true;
            this.ckMySec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckMySec.ForeColor = System.Drawing.Color.DarkBlue;
            this.ckMySec.Location = new System.Drawing.Point(528, 3);
            this.ckMySec.Name = "ckMySec";
            this.ckMySec.Size = new System.Drawing.Size(96, 20);
            this.ckMySec.TabIndex = 31;
            this.ckMySec.Text = "My Sector";
            this.ckMySec.UseVisualStyleBackColor = true;
            this.ckMySec.CheckedChanged += new System.EventHandler(this.ckMySec_CheckedChanged);
            // 
            // lblCasesCount
            // 
            this.lblCasesCount.AutoSize = true;
            this.lblCasesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCasesCount.Location = new System.Drawing.Point(25, 72);
            this.lblCasesCount.Name = "lblCasesCount";
            this.lblCasesCount.Size = new System.Drawing.Size(36, 16);
            this.lblCasesCount.TabIndex = 30;
            this.lblCasesCount.Text = "xxxx";
            // 
            // tbReferrals
            // 
            this.tbReferrals.Controls.Add(this.tbProject);
            this.tbReferrals.Controls.Add(this.tbRespondent);
            this.tbReferrals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReferrals.Location = new System.Drawing.Point(2, 91);
            this.tbReferrals.Name = "tbReferrals";
            this.tbReferrals.SelectedIndex = 0;
            this.tbReferrals.Size = new System.Drawing.Size(1133, 566);
            this.tbReferrals.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbReferrals.TabIndex = 19;
            this.tbReferrals.SelectedIndexChanged += new System.EventHandler(this.tbReferralReview_SelectedIndexChanged);
            this.tbReferrals.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbReferralReview_Selected);
            // 
            // tbProject
            // 
            this.tbProject.Controls.Add(this.dgProjReferrals);
            this.tbProject.Location = new System.Drawing.Point(4, 22);
            this.tbProject.Name = "tbProject";
            this.tbProject.Padding = new System.Windows.Forms.Padding(3);
            this.tbProject.Size = new System.Drawing.Size(1125, 540);
            this.tbProject.TabIndex = 2;
            this.tbProject.Text = "PROJECT";
            this.tbProject.UseVisualStyleBackColor = true;
            // 
            // dgProjReferrals
            // 
            this.dgProjReferrals.AllowUserToAddRows = false;
            this.dgProjReferrals.AllowUserToDeleteRows = false;
            this.dgProjReferrals.AllowUserToResizeColumns = false;
            this.dgProjReferrals.AllowUserToResizeRows = false;
            this.dgProjReferrals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgProjReferrals.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgProjReferrals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgProjReferrals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProjReferrals.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgProjReferrals.Location = new System.Drawing.Point(3, 3);
            this.dgProjReferrals.MultiSelect = false;
            this.dgProjReferrals.Name = "dgProjReferrals";
            this.dgProjReferrals.ReadOnly = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgProjReferrals.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgProjReferrals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProjReferrals.Size = new System.Drawing.Size(1116, 531);
            this.dgProjReferrals.TabIndex = 4;
            // 
            // tbRespondent
            // 
            this.tbRespondent.Controls.Add(this.dgRespReferrals);
            this.tbRespondent.Location = new System.Drawing.Point(4, 22);
            this.tbRespondent.Name = "tbRespondent";
            this.tbRespondent.Padding = new System.Windows.Forms.Padding(3);
            this.tbRespondent.Size = new System.Drawing.Size(1125, 540);
            this.tbRespondent.TabIndex = 1;
            this.tbRespondent.Text = "RESPONDENT";
            this.tbRespondent.UseVisualStyleBackColor = true;
            // 
            // dgRespReferrals
            // 
            this.dgRespReferrals.AllowUserToAddRows = false;
            this.dgRespReferrals.AllowUserToDeleteRows = false;
            this.dgRespReferrals.AllowUserToResizeColumns = false;
            this.dgRespReferrals.AllowUserToResizeRows = false;
            this.dgRespReferrals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRespReferrals.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgRespReferrals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgRespReferrals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRespReferrals.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgRespReferrals.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRespReferrals.Location = new System.Drawing.Point(3, 3);
            this.dgRespReferrals.MultiSelect = false;
            this.dgRespReferrals.Name = "dgRespReferrals";
            this.dgRespReferrals.ReadOnly = true;
            this.dgRespReferrals.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgRespReferrals.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgRespReferrals.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgRespReferrals.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgRespReferrals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRespReferrals.Size = new System.Drawing.Size(1116, 530);
            this.dgRespReferrals.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRESPValueItem);
            this.groupBox1.Controls.Add(this.lbldatef);
            this.groupBox1.Controls.Add(this.txtRESPValueItem);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.cbPROJValueItem);
            this.groupBox1.Controls.Add(this.txtPROJValueItem);
            this.groupBox1.Controls.Add(this.cbItem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(254, 29);
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
            // cbPROJValueItem
            // 
            this.cbPROJValueItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPROJValueItem.FormattingEnabled = true;
            this.cbPROJValueItem.Location = new System.Drawing.Point(185, 8);
            this.cbPROJValueItem.Name = "cbPROJValueItem";
            this.cbPROJValueItem.Size = new System.Drawing.Size(97, 21);
            this.cbPROJValueItem.TabIndex = 4;
            // 
            // txtPROJValueItem
            // 
            this.txtPROJValueItem.Location = new System.Drawing.Point(185, 8);
            this.txtPROJValueItem.Name = "txtPROJValueItem";
            this.txtPROJValueItem.Size = new System.Drawing.Size(97, 20);
            this.txtPROJValueItem.TabIndex = 3;
            this.txtPROJValueItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPROJValueItem_KeyPress);
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
            this.label2.Location = new System.Drawing.Point(501, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 25);
            this.label2.TabIndex = 29;
            this.label2.Text = "REFERRAL REVIEW";
            // 
            // btnData
            // 
            this.btnData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnData.Location = new System.Drawing.Point(403, 814);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(178, 23);
            this.btnData.TabIndex = 38;
            this.btnData.TabStop = false;
            this.btnData.Text = "C-700";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(635, 814);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(161, 23);
            this.btnPrint.TabIndex = 35;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmReferralReview
            // 
            this.AcceptButton = this.btnSearchItem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 869);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTab);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "frmReferralReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReferralReview_FormClosing);
            this.Load += new System.EventHandler(this.frmReferralReview_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblTab, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnData, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbReferrals.ResumeLayout(false);
            this.tbProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProjReferrals)).EndInit();
            this.tbRespondent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRespReferrals)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbReferrals;
        private System.Windows.Forms.TabPage tbProject;
        private System.Windows.Forms.DataGridView dgProjReferrals;
        private System.Windows.Forms.TabPage tbRespondent;
        private System.Windows.Forms.DataGridView dgRespReferrals;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbRESPValueItem;
        private System.Windows.Forms.Label lbldatef;
        private System.Windows.Forms.TextBox txtRESPValueItem;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbPROJValueItem;
        private System.Windows.Forms.TextBox txtPROJValueItem;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblCasesCount;
        private System.Windows.Forms.CheckBox ckMySec;
    }
}