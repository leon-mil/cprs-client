namespace Cprs
{
    partial class frmVerifyDeletePopup
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
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.lblVerify = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNo
            // 
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Location = new System.Drawing.Point(214, 77);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 5;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(91, 77);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 4;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // lblVerify
            // 
            this.lblVerify.AutoSize = true;
            this.lblVerify.Location = new System.Drawing.Point(68, 31);
            this.lblVerify.Name = "lblVerify";
            this.lblVerify.Size = new System.Drawing.Size(225, 13);
            this.lblVerify.TabIndex = 3;
            this.lblVerify.Text = "This will be permanently deleted. CONTINUE?";
            // 
            // frmVerifyDeletePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 128);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblVerify);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(410, 158);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(410, 158);
            this.Name = "frmVerifyDeletePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verify Deletion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Label lblVerify;
    }
}