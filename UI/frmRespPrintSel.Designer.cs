namespace Cprs
{
    partial class frmRespPrintSel
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
            this.rdbResp = new System.Windows.Forms.RadioButton();
            this.rdbProject = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbResp
            // 
            this.rdbResp.AutoSize = true;
            this.rdbResp.Checked = true;
            this.rdbResp.Location = new System.Drawing.Point(6, 30);
            this.rdbResp.Name = "rdbResp";
            this.rdbResp.Size = new System.Drawing.Size(86, 17);
            this.rdbResp.TabIndex = 1;
            this.rdbResp.TabStop = true;
            this.rdbResp.Text = "Respondent ";
            this.rdbResp.UseVisualStyleBackColor = true;
            this.rdbResp.CheckedChanged += new System.EventHandler(this.rdbResp_CheckedChanged);
            // 
            // rdbProject
            // 
            this.rdbProject.AutoSize = true;
            this.rdbProject.Location = new System.Drawing.Point(6, 53);
            this.rdbProject.Name = "rdbProject";
            this.rdbProject.Size = new System.Drawing.Size(63, 17);
            this.rdbProject.TabIndex = 2;
            this.rdbProject.Text = "Projects";
            this.rdbProject.UseVisualStyleBackColor = true;
            this.rdbProject.CheckedChanged += new System.EventHandler(this.rdbProject_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbResp);
            this.groupBox1.Controls.Add(this.rdbProject);
            this.groupBox1.Location = new System.Drawing.Point(23, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 89);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please Select Export Data:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(68, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmRespPrintSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 211);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(226, 242);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(226, 242);
            this.Name = "frmRespPrintSel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Export Data";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbResp;
        private System.Windows.Forms.RadioButton rdbProject;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;

    }
}