namespace Cprs
{
    partial class frmRespCentpwdPopup
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.lblCentpwd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(111, 86);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 27);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPasswd
            // 
            this.txtPasswd.Location = new System.Drawing.Point(107, 45);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.Size = new System.Drawing.Size(100, 20);
            this.txtPasswd.TabIndex = 8;
            // 
            // lblCentpwd
            // 
            this.lblCentpwd.AutoSize = true;
            this.lblCentpwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCentpwd.Location = new System.Drawing.Point(92, 26);
            this.lblCentpwd.Name = "lblCentpwd";
            this.lblCentpwd.Size = new System.Drawing.Size(127, 16);
            this.lblCentpwd.TabIndex = 9;
            this.lblCentpwd.Text = "Centurion Password";
            // 
            // frmRespCentpwdPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 145);
            this.Controls.Add(this.lblCentpwd);
            this.Controls.Add(this.txtPasswd);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(315, 177);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(315, 177);
            this.Name = "frmRespCentpwdPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmRespCentpwdPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtPasswd;
        private System.Windows.Forms.Label lblCentpwd;
    }
}