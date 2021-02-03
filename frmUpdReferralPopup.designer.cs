namespace Cprs
{
    partial class frmUpdReferralPopup
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbChooseStatus = new System.Windows.Forms.GroupBox();
            this.rbtnPend = new System.Windows.Forms.RadioButton();
            this.rbtnActive = new System.Windows.Forms.RadioButton();
            this.rbtnComplete = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.lblCase = new System.Windows.Forms.Label();
            this.gbChooseStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(491, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(264, 354);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Location = new System.Drawing.Point(41, 271);
            this.txtRemark.MaxLength = 240;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(715, 57);
            this.txtRemark.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Enter Note:";
            // 
            // gbChooseStatus
            // 
            this.gbChooseStatus.Controls.Add(this.rbtnPend);
            this.gbChooseStatus.Controls.Add(this.rbtnActive);
            this.gbChooseStatus.Controls.Add(this.rbtnComplete);
            this.gbChooseStatus.Location = new System.Drawing.Point(150, 183);
            this.gbChooseStatus.Name = "gbChooseStatus";
            this.gbChooseStatus.Size = new System.Drawing.Size(501, 37);
            this.gbChooseStatus.TabIndex = 24;
            this.gbChooseStatus.TabStop = false;
            this.gbChooseStatus.Text = "Choose Status";
            // 
            // rbtnPend
            // 
            this.rbtnPend.AutoSize = true;
            this.rbtnPend.Location = new System.Drawing.Point(268, 14);
            this.rbtnPend.Name = "rbtnPend";
            this.rbtnPend.Size = new System.Drawing.Size(64, 17);
            this.rbtnPend.TabIndex = 15;
            this.rbtnPend.Text = "Pending";
            this.rbtnPend.UseVisualStyleBackColor = true;
            // 
            // rbtnActive
            // 
            this.rbtnActive.AutoSize = true;
            this.rbtnActive.Checked = true;
            this.rbtnActive.Location = new System.Drawing.Point(181, 14);
            this.rbtnActive.Name = "rbtnActive";
            this.rbtnActive.Size = new System.Drawing.Size(55, 17);
            this.rbtnActive.TabIndex = 13;
            this.rbtnActive.TabStop = true;
            this.rbtnActive.Text = "Active";
            this.rbtnActive.UseVisualStyleBackColor = true;
            // 
            // rbtnComplete
            // 
            this.rbtnComplete.AutoSize = true;
            this.rbtnComplete.Location = new System.Drawing.Point(372, 14);
            this.rbtnComplete.Name = "rbtnComplete";
            this.rbtnComplete.Size = new System.Drawing.Size(69, 17);
            this.rbtnComplete.TabIndex = 14;
            this.rbtnComplete.Text = "Complete";
            this.rbtnComplete.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(304, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "GROUP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(316, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "TYPE";
            // 
            // txtType
            // 
            this.txtType.ForeColor = System.Drawing.Color.Black;
            this.txtType.Location = new System.Drawing.Point(390, 87);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(97, 20);
            this.txtType.TabIndex = 27;
            // 
            // txtGroup
            // 
            this.txtGroup.ForeColor = System.Drawing.Color.Black;
            this.txtGroup.Location = new System.Drawing.Point(390, 134);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.ReadOnly = true;
            this.txtGroup.Size = new System.Drawing.Size(97, 20);
            this.txtGroup.TabIndex = 28;
            // 
            // lblCase
            // 
            this.lblCase.AutoSize = true;
            this.lblCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCase.Location = new System.Drawing.Point(377, 27);
            this.lblCase.Name = "lblCase";
            this.lblCase.Size = new System.Drawing.Size(0, 15);
            this.lblCase.TabIndex = 30;
            // 
            // frmUpdReferralPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 392);
            this.Controls.Add(this.lblCase);
            this.Controls.Add(this.txtGroup);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbChooseStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(808, 419);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(808, 419);
            this.Name = "frmUpdReferralPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Referral";
            this.Load += new System.EventHandler(this.frmUpdReferral_Load);
            this.gbChooseStatus.ResumeLayout(false);
            this.gbChooseStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbChooseStatus;
        private System.Windows.Forms.RadioButton rbtnActive;
        private System.Windows.Forms.RadioButton rbtnComplete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Label lblCase;
        private System.Windows.Forms.RadioButton rbtnPend;
    }
}