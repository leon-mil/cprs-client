namespace Cprs
{
    partial class frmDCPInitPopup
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnOKDCP = new System.Windows.Forms.Button();
            this.btnCancelDCP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(158, 23);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(96, 27);
            this.btnSelect.TabIndex = 13;
            this.btnSelect.Text = "&SELECT";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnOKDCP
            // 
            this.btnOKDCP.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKDCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOKDCP.Location = new System.Drawing.Point(79, 122);
            this.btnOKDCP.Name = "btnOKDCP";
            this.btnOKDCP.Size = new System.Drawing.Size(96, 27);
            this.btnOKDCP.TabIndex = 14;
            this.btnOKDCP.Text = "&OK";
            this.btnOKDCP.UseVisualStyleBackColor = true;
            this.btnOKDCP.Click += new System.EventHandler(this.btnOKDCP_Click);
            // 
            // btnCancelDCP
            // 
            this.btnCancelDCP.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancelDCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelDCP.Location = new System.Drawing.Point(226, 122);
            this.btnCancelDCP.Name = "btnCancelDCP";
            this.btnCancelDCP.Size = new System.Drawing.Size(96, 27);
            this.btnCancelDCP.TabIndex = 15;
            this.btnCancelDCP.Text = "&Cancel";
            this.btnCancelDCP.UseVisualStyleBackColor = true;
            this.btnCancelDCP.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(141, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Enter ID:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(195, 74);
            this.txtID.MaxLength = 7;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(59, 20);
            this.txtID.TabIndex = 18;
            this.txtID.Click += new System.EventHandler(this.txtID_Click);
            this.txtID.Enter += new System.EventHandler(this.txtID_Enter);
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyDown);
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // frmDCPInitPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 178);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelDCP);
            this.Controls.Add(this.btnOKDCP);
            this.Controls.Add(this.btnSelect);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(428, 205);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(428, 205);
            this.Name = "frmDCPInitPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter ID";
            this.Load += new System.EventHandler(this.frmDCPInitPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnOKDCP;
        private System.Windows.Forms.Button btnCancelDCP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtID;
    }
}