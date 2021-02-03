namespace Cprs
{
    partial class frmImpFlgPrintSel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbFlgCnt = new System.Windows.Forms.RadioButton();
            this.rdbProjList = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbFlgCnt);
            this.groupBox1.Controls.Add(this.rdbProjList);
            this.groupBox1.Location = new System.Drawing.Point(23, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 89);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please Select Print Data:";
            // 
            // rdbFlgCnt
            // 
            this.rdbFlgCnt.AutoSize = true;
            this.rdbFlgCnt.Checked = true;
            this.rdbFlgCnt.Location = new System.Drawing.Point(6, 30);
            this.rdbFlgCnt.Name = "rdbFlgCnt";
            this.rdbFlgCnt.Size = new System.Drawing.Size(81, 17);
            this.rdbFlgCnt.TabIndex = 1;
            this.rdbFlgCnt.TabStop = true;
            this.rdbFlgCnt.Text = "Flag Counts";
            this.rdbFlgCnt.UseVisualStyleBackColor = true;
            this.rdbFlgCnt.CheckedChanged += new System.EventHandler(this.rdbFlgCnt_CheckedChanged);
            // 
            // rdbProjList
            // 
            this.rdbProjList.AutoSize = true;
            this.rdbProjList.Location = new System.Drawing.Point(6, 53);
            this.rdbProjList.Name = "rdbProjList";
            this.rdbProjList.Size = new System.Drawing.Size(86, 17);
            this.rdbProjList.TabIndex = 2;
            this.rdbProjList.Text = "Flag Projects";
            this.rdbProjList.UseVisualStyleBackColor = true;
            this.rdbProjList.CheckedChanged += new System.EventHandler(this.rdbProjList_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(23, 147);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(108, 147);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmImpFlgPrintSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 212);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(226, 242);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(226, 242);
            this.Name = "frmImpFlgPrintSel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Print Data";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbFlgCnt;
        private System.Windows.Forms.RadioButton rdbProjList;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}