namespace Cprs
{
    partial class frmCeAddReferralPopup
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
            this.gbChooseGrp = new System.Windows.Forms.GroupBox();
            this.rbtnHqSuper = new System.Windows.Forms.RadioButton();
            this.rbtnHqAnalyst = new System.Windows.Forms.RadioButton();
            this.gbChooseRef = new System.Windows.Forms.GroupBox();
            this.rbtnFree = new System.Windows.Forms.RadioButton();
            this.rbtnData = new System.Windows.Forms.RadioButton();
            this.rbtnCorrect = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbChooseGrp.SuspendLayout();
            this.gbChooseRef.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbChooseGrp
            // 
            this.gbChooseGrp.Controls.Add(this.rbtnHqSuper);
            this.gbChooseGrp.Controls.Add(this.rbtnHqAnalyst);
            this.gbChooseGrp.Location = new System.Drawing.Point(231, 144);
            this.gbChooseGrp.Name = "gbChooseGrp";
            this.gbChooseGrp.Size = new System.Drawing.Size(371, 52);
            this.gbChooseGrp.TabIndex = 27;
            this.gbChooseGrp.TabStop = false;
            this.gbChooseGrp.Text = "Choose Referral Group";
            // 
            // rbtnHqSuper
            // 
            this.rbtnHqSuper.AutoSize = true;
            this.rbtnHqSuper.Checked = true;
            this.rbtnHqSuper.Location = new System.Drawing.Point(84, 19);
            this.rbtnHqSuper.Name = "rbtnHqSuper";
            this.rbtnHqSuper.Size = new System.Drawing.Size(94, 17);
            this.rbtnHqSuper.TabIndex = 13;
            this.rbtnHqSuper.TabStop = true;
            this.rbtnHqSuper.Text = "HQ Supervisor";
            this.rbtnHqSuper.UseVisualStyleBackColor = true;
            // 
            // rbtnHqAnalyst
            // 
            this.rbtnHqAnalyst.AutoSize = true;
            this.rbtnHqAnalyst.Location = new System.Drawing.Point(233, 19);
            this.rbtnHqAnalyst.Name = "rbtnHqAnalyst";
            this.rbtnHqAnalyst.Size = new System.Drawing.Size(78, 17);
            this.rbtnHqAnalyst.TabIndex = 14;
            this.rbtnHqAnalyst.Text = "HQ Analyst";
            this.rbtnHqAnalyst.UseVisualStyleBackColor = true;
            // 
            // gbChooseRef
            // 
            this.gbChooseRef.Controls.Add(this.rbtnFree);
            this.gbChooseRef.Controls.Add(this.rbtnData);
            this.gbChooseRef.Controls.Add(this.rbtnCorrect);
            this.gbChooseRef.Location = new System.Drawing.Point(128, 64);
            this.gbChooseRef.Name = "gbChooseRef";
            this.gbChooseRef.Size = new System.Drawing.Size(564, 52);
            this.gbChooseRef.TabIndex = 26;
            this.gbChooseRef.TabStop = false;
            this.gbChooseRef.Text = "Choose Referral Type";
            // 
            // rbtnFree
            // 
            this.rbtnFree.AutoSize = true;
            this.rbtnFree.Location = new System.Drawing.Point(417, 19);
            this.rbtnFree.Name = "rbtnFree";
            this.rbtnFree.Size = new System.Drawing.Size(72, 17);
            this.rbtnFree.TabIndex = 21;
            this.rbtnFree.Text = "Free Form";
            this.rbtnFree.UseVisualStyleBackColor = true;
            // 
            // rbtnData
            // 
            this.rbtnData.AutoSize = true;
            this.rbtnData.Location = new System.Drawing.Point(295, 19);
            this.rbtnData.Name = "rbtnData";
            this.rbtnData.Size = new System.Drawing.Size(76, 17);
            this.rbtnData.TabIndex = 15;
            this.rbtnData.Text = "Data Issue";
            this.rbtnData.UseVisualStyleBackColor = true;
            // 
            // rbtnCorrect
            // 
            this.rbtnCorrect.AutoSize = true;
            this.rbtnCorrect.Checked = true;
            this.rbtnCorrect.Location = new System.Drawing.Point(152, 19);
            this.rbtnCorrect.Name = "rbtnCorrect";
            this.rbtnCorrect.Size = new System.Drawing.Size(87, 17);
            this.rbtnCorrect.TabIndex = 14;
            this.rbtnCorrect.TabStop = true;
            this.rbtnCorrect.Text = "Correct Flags";
            this.rbtnCorrect.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(496, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(280, 354);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Location = new System.Drawing.Point(47, 251);
            this.txtRemark.MaxLength = 240;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(715, 57);
            this.txtRemark.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Enter Note:";
            // 
            // frmCeAddReferralPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 389);
            this.Controls.Add(this.gbChooseGrp);
            this.Controls.Add(this.gbChooseRef);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 419);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 419);
            this.Name = "frmCeAddReferralPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CE Add Referral";
            this.Load += new System.EventHandler(this.frmAddReferralPopup_Load);
            this.gbChooseGrp.ResumeLayout(false);
            this.gbChooseGrp.PerformLayout();
            this.gbChooseRef.ResumeLayout(false);
            this.gbChooseRef.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbChooseGrp;
        private System.Windows.Forms.RadioButton rbtnHqSuper;
        private System.Windows.Forms.RadioButton rbtnHqAnalyst;
        private System.Windows.Forms.GroupBox gbChooseRef;
        private System.Windows.Forms.RadioButton rbtnFree;
        private System.Windows.Forms.RadioButton rbtnData;
        private System.Windows.Forms.RadioButton rbtnCorrect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label1;
    }
}