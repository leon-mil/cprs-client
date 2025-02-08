namespace Cprs
{
    partial class frmMySectorsPopup
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
            this.label5 = new System.Windows.Forms.Label();
            this.ckSects = new System.Windows.Forms.CheckedListBox();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "UserName:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Sectors:";
            // 
            // ckSects
            // 
            this.ckSects.CheckOnClick = true;
            this.ckSects.FormattingEnabled = true;
            this.ckSects.Items.AddRange(new object[] {
            "SECT00",
            "SECT01",
            "SECT02",
            "SECT03",
            "SECT04",
            "SECT05",
            "SECT06",
            "SECT07",
            "SECT08",
            "SECT09",
            "SECT10",
            "SECT11",
            "SECT12",
            "SECT13",
            "SECT14",
            "SECT15",
            "SECT16",
            "SECT19",
            "SECT1T"});
            this.ckSects.Location = new System.Drawing.Point(161, 41);
            this.ckSects.Name = "ckSects";
            this.ckSects.Size = new System.Drawing.Size(178, 289);
            this.ckSects.TabIndex = 7;
            // 
            // cbUser
            // 
            this.cbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Items.AddRange(new object[] {
            "0 - Programmer",
            "1 - HQManager",
            "2 - HQAnalyst",
            "3 - NPCManager",
            "4 - NPCLead",
            "5 - NPCInterviewer",
            "6 - HQSupport",
            "7 - HQMathStat",
            "8 - HQTester"});
            this.cbUser.Location = new System.Drawing.Point(27, 41);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(105, 21);
            this.cbUser.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(183, 348);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(57, 348);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(27, 41);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(105, 20);
            this.txtUser.TabIndex = 11;
            // 
            // frmMySectorsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 417);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbUser);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckSects);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMySectorsPopup";
            this.Text = "HQ Sectors";
            this.Load += new System.EventHandler(this.frmMySectorsPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox ckSects;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtUser;
    }
}