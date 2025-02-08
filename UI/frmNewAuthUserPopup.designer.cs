namespace Cprs
{
    partial class frmNewAuthUserPopup
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPrintq = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbGrade = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cklRole = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtRole = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UserName:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(105, 27);
            this.txtUser.MaxLength = 8;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(121, 20);
            this.txtUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Role:";
            // 
            // cbRole
            // 
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Items.AddRange(new object[] {
            "0 - Programmer",
            "1 - HQManager",
            "2 - HQAnalyst",
            "3 - NPCManager",
            "4 - NPCLead",
            "5 - NPCInterviewer",
            "6 - HQSupport",
            "7 - HQMathStat",
            "8 - HQTester"});
            this.cbRole.Location = new System.Drawing.Point(326, 30);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(121, 21);
            this.cbRole.TabIndex = 2;
            this.cbRole.SelectedIndexChanged += new System.EventHandler(this.cbRole_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Print Queue:";
            // 
            // cbPrintq
            // 
            this.cbPrintq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrintq.FormattingEnabled = true;
            this.cbPrintq.Location = new System.Drawing.Point(105, 58);
            this.cbPrintq.Name = "cbPrintq";
            this.cbPrintq.Size = new System.Drawing.Size(342, 21);
            this.cbPrintq.TabIndex = 3;
            this.cbPrintq.SelectedIndexChanged += new System.EventHandler(this.cbPrintq_SelectIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Grade:";
            // 
            // cbGrade
            // 
            this.cbGrade.BackColor = System.Drawing.Color.White;
            this.cbGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrade.FormattingEnabled = true;
            this.cbGrade.Items.AddRange(new object[] {
            "",
            "4",
            "5"});
            this.cbGrade.Location = new System.Drawing.Point(63, 19);
            this.cbGrade.Name = "cbGrade";
            this.cbGrade.Size = new System.Drawing.Size(121, 21);
            this.cbGrade.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cklRole);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbGrade);
            this.groupBox1.Location = new System.Drawing.Point(27, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NPC Interviewer Only";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Survey Role:";
            // 
            // cklRole
            // 
            this.cklRole.CheckOnClick = true;
            this.cklRole.FormattingEnabled = true;
            this.cklRole.Items.AddRange(new object[] {
            "INITSL",
            "INITFD",
            "INITNR",
            "INITMF",
            "CONTSL",
            "CONTFD",
            "CONTNR",
            "CONTMF"});
            this.cklRole.Location = new System.Drawing.Point(279, 19);
            this.cklRole.Name = "cklRole";
            this.cklRole.Size = new System.Drawing.Size(120, 124);
            this.cklRole.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(124, 287);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(255, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtRole
            // 
            this.txtRole.BackColor = System.Drawing.Color.White;
            this.txtRole.Location = new System.Drawing.Point(326, 30);
            this.txtRole.Name = "txtRole";
            this.txtRole.ReadOnly = true;
            this.txtRole.Size = new System.Drawing.Size(100, 20);
            this.txtRole.TabIndex = 8;
            // 
            // frmNewAuthUserPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(485, 341);
            this.Controls.Add(this.txtRole);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbPrintq);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Name = "frmNewAuthUserPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorized User";
            this.Load += new System.EventHandler(this.frmNewAuthUserPopup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox cklRole;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.ComboBox cbPrintq;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.TextBox txtRole;
        private System.Windows.Forms.ComboBox cbGrade;
    }
}