namespace Cprs
{
    partial class frmRespidEditPopup
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
            this.txtNewRespid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rdbtnExisting = new System.Windows.Forms.RadioButton();
            this.rdbtnNew = new System.Windows.Forms.RadioButton();
            this.rdbtnDelete = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNewRespid
            // 
            this.txtNewRespid.Location = new System.Drawing.Point(208, 102);
            this.txtNewRespid.MaxLength = 7;
            this.txtNewRespid.Name = "txtNewRespid";
            this.txtNewRespid.Size = new System.Drawing.Size(55, 20);
            this.txtNewRespid.TabIndex = 0;
            this.txtNewRespid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewRespid_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Respid";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(251, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(113, 138);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 26);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rdbtnExisting
            // 
            this.rdbtnExisting.AutoSize = true;
            this.rdbtnExisting.Checked = true;
            this.rdbtnExisting.Location = new System.Drawing.Point(75, 17);
            this.rdbtnExisting.Name = "rdbtnExisting";
            this.rdbtnExisting.Size = new System.Drawing.Size(61, 17);
            this.rdbtnExisting.TabIndex = 4;
            this.rdbtnExisting.TabStop = true;
            this.rdbtnExisting.Text = "Existing";
            this.rdbtnExisting.UseVisualStyleBackColor = true;
            this.rdbtnExisting.CheckedChanged += new System.EventHandler(this.rdbtnExisting_CheckedChanged);
            // 
            // rdbtnNew
            // 
            this.rdbtnNew.AutoSize = true;
            this.rdbtnNew.Location = new System.Drawing.Point(198, 17);
            this.rdbtnNew.Name = "rdbtnNew";
            this.rdbtnNew.Size = new System.Drawing.Size(47, 17);
            this.rdbtnNew.TabIndex = 5;
            this.rdbtnNew.Text = "New";
            this.rdbtnNew.UseVisualStyleBackColor = true;
            this.rdbtnNew.CheckedChanged += new System.EventHandler(this.rdbtnNew_CheckedChanged);
            // 
            // rdbtnDelete
            // 
            this.rdbtnDelete.AutoSize = true;
            this.rdbtnDelete.Location = new System.Drawing.Point(303, 17);
            this.rdbtnDelete.Name = "rdbtnDelete";
            this.rdbtnDelete.Size = new System.Drawing.Size(56, 17);
            this.rdbtnDelete.TabIndex = 6;
            this.rdbtnDelete.TabStop = true;
            this.rdbtnDelete.Text = "Delete";
            this.rdbtnDelete.UseVisualStyleBackColor = true;
            this.rdbtnDelete.CheckedChanged += new System.EventHandler(this.rdbtnDelete_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(147, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Change Respid";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbtnDelete);
            this.groupBox1.Controls.Add(this.rdbtnNew);
            this.groupBox1.Controls.Add(this.rdbtnExisting);
            this.groupBox1.Location = new System.Drawing.Point(10, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 50);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Option";
            // 
            // frmRespidEditPopup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(431, 176);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNewRespid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(441, 208);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(441, 208);
            this.Name = "frmRespidEditPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Respid";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRespidEditPopup_FormClosing);
            this.Load += new System.EventHandler(this.frmRespidEditPopup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewRespid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rdbtnExisting;
        private System.Windows.Forms.RadioButton rdbtnNew;
        private System.Windows.Forms.RadioButton rdbtnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}