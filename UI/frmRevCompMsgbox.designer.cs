namespace Cprs
{
    partial class frmRevCompMsgbox
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
            this.lblRevComp = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRevComp
            // 
            this.lblRevComp.AutoSize = true;
            this.lblRevComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRevComp.Location = new System.Drawing.Point(97, 43);
            this.lblRevComp.Name = "lblRevComp";
            this.lblRevComp.Size = new System.Drawing.Size(208, 16);
            this.lblRevComp.TabIndex = 0;
            this.lblRevComp.Text = "Is Review Complete for this case?";
            // 
            // btnYes
            // 
            this.btnYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Location = new System.Drawing.Point(52, 97);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(66, 23);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(291, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNo
            // 
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.Location = new System.Drawing.Point(172, 95);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(58, 23);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // frmRevCompMsgbox
            // 
            this.AcceptButton = this.btnYes;
            this.ClientSize = new System.Drawing.Size(423, 175);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblRevComp);
            this.Name = "frmRevCompMsgbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Review Complete?";
            this.Load += new System.EventHandler(this.frmRevCompMsgbox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.Button btnOK;
        //private System.Windows.Forms.Button btnCancel;
        //private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.TextBox txtRespid;
        private System.Windows.Forms.Label lblRevComp;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNo;
    }
}