namespace Cprs
{
    partial class frmAddReferralPopup
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
            this.gbChooseRef = new System.Windows.Forms.GroupBox();
            this.rbtnFree = new System.Windows.Forms.RadioButton();
            this.rbtnPnr = new System.Windows.Forms.RadioButton();
            this.rbtnData = new System.Windows.Forms.RadioButton();
            this.rbtnLate = new System.Windows.Forms.RadioButton();
            this.rbtnCorrect = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbChooseGrp = new System.Windows.Forms.GroupBox();
            this.rbtnNpcSupervisor = new System.Windows.Forms.RadioButton();
            this.rbtnHqSuper = new System.Windows.Forms.RadioButton();
            this.rbtnHqAnalyst = new System.Windows.Forms.RadioButton();
            this.gbChooseType = new System.Windows.Forms.GroupBox();
            this.rbtnProject = new System.Windows.Forms.RadioButton();
            this.rbtnRespondent = new System.Windows.Forms.RadioButton();
            this.gbChooseRef.SuspendLayout();
            this.gbChooseGrp.SuspendLayout();
            this.gbChooseType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbChooseRef
            // 
            this.gbChooseRef.Controls.Add(this.rbtnFree);
            this.gbChooseRef.Controls.Add(this.rbtnPnr);
            this.gbChooseRef.Controls.Add(this.rbtnData);
            this.gbChooseRef.Controls.Add(this.rbtnLate);
            this.gbChooseRef.Controls.Add(this.rbtnCorrect);
            this.gbChooseRef.Location = new System.Drawing.Point(47, 85);
            this.gbChooseRef.Name = "gbChooseRef";
            this.gbChooseRef.Size = new System.Drawing.Size(715, 52);
            this.gbChooseRef.TabIndex = 20;
            this.gbChooseRef.TabStop = false;
            this.gbChooseRef.Text = "Choose Referral Type";
            // 
            // rbtnFree
            // 
            this.rbtnFree.AutoSize = true;
            this.rbtnFree.Location = new System.Drawing.Point(598, 19);
            this.rbtnFree.Name = "rbtnFree";
            this.rbtnFree.Size = new System.Drawing.Size(51, 17);
            this.rbtnFree.TabIndex = 21;
            this.rbtnFree.Text = "Other";
            this.rbtnFree.UseVisualStyleBackColor = true;
            // 
            // rbtnPnr
            // 
            this.rbtnPnr.AutoSize = true;
            this.rbtnPnr.Location = new System.Drawing.Point(464, 19);
            this.rbtnPnr.Name = "rbtnPnr";
            this.rbtnPnr.Size = new System.Drawing.Size(67, 17);
            this.rbtnPnr.TabIndex = 21;
            this.rbtnPnr.Text = "MF Initial";
            this.rbtnPnr.UseVisualStyleBackColor = true;
            // 
            // rbtnData
            // 
            this.rbtnData.AutoSize = true;
            this.rbtnData.Checked = true;
            this.rbtnData.Location = new System.Drawing.Point(55, 19);
            this.rbtnData.Name = "rbtnData";
            this.rbtnData.Size = new System.Drawing.Size(76, 17);
            this.rbtnData.TabIndex = 15;
            this.rbtnData.TabStop = true;
            this.rbtnData.Text = "Data Issue";
            this.rbtnData.UseVisualStyleBackColor = true;
            // 
            // rbtnLate
            // 
            this.rbtnLate.AutoSize = true;
            this.rbtnLate.Location = new System.Drawing.Point(327, 19);
            this.rbtnLate.Name = "rbtnLate";
            this.rbtnLate.Size = new System.Drawing.Size(84, 17);
            this.rbtnLate.TabIndex = 13;
            this.rbtnLate.Text = "Dodge Initial";
            this.rbtnLate.UseVisualStyleBackColor = true;
            // 
            // rbtnCorrect
            // 
            this.rbtnCorrect.AutoSize = true;
            this.rbtnCorrect.Location = new System.Drawing.Point(183, 19);
            this.rbtnCorrect.Name = "rbtnCorrect";
            this.rbtnCorrect.Size = new System.Drawing.Size(87, 17);
            this.rbtnCorrect.TabIndex = 14;
            this.rbtnCorrect.Text = "Correct Flags";
            this.rbtnCorrect.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(498, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(280, 354);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Location = new System.Drawing.Point(47, 260);
            this.txtRemark.MaxLength = 240;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(715, 57);
            this.txtRemark.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Enter Note:";
            // 
            // gbChooseGrp
            // 
            this.gbChooseGrp.Controls.Add(this.rbtnNpcSupervisor);
            this.gbChooseGrp.Controls.Add(this.rbtnHqSuper);
            this.gbChooseGrp.Controls.Add(this.rbtnHqAnalyst);
            this.gbChooseGrp.Location = new System.Drawing.Point(47, 164);
            this.gbChooseGrp.Name = "gbChooseGrp";
            this.gbChooseGrp.Size = new System.Drawing.Size(715, 52);
            this.gbChooseGrp.TabIndex = 21;
            this.gbChooseGrp.TabStop = false;
            this.gbChooseGrp.Text = "Choose Referral Group";
            // 
            // rbtnNpcSupervisor
            // 
            this.rbtnNpcSupervisor.AutoSize = true;
            this.rbtnNpcSupervisor.Location = new System.Drawing.Point(382, 19);
            this.rbtnNpcSupervisor.Name = "rbtnNpcSupervisor";
            this.rbtnNpcSupervisor.Size = new System.Drawing.Size(98, 17);
            this.rbtnNpcSupervisor.TabIndex = 15;
            this.rbtnNpcSupervisor.Text = "NPC Sup/Lead";
            this.rbtnNpcSupervisor.UseVisualStyleBackColor = true;
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
            // gbChooseType
            // 
            this.gbChooseType.Controls.Add(this.rbtnProject);
            this.gbChooseType.Controls.Add(this.rbtnRespondent);
            this.gbChooseType.Location = new System.Drawing.Point(158, 22);
            this.gbChooseType.Name = "gbChooseType";
            this.gbChooseType.Size = new System.Drawing.Size(501, 37);
            this.gbChooseType.TabIndex = 22;
            this.gbChooseType.TabStop = false;
            this.gbChooseType.Text = "Choose Type";
            // 
            // rbtnProject
            // 
            this.rbtnProject.AutoSize = true;
            this.rbtnProject.Checked = true;
            this.rbtnProject.Location = new System.Drawing.Point(181, 14);
            this.rbtnProject.Name = "rbtnProject";
            this.rbtnProject.Size = new System.Drawing.Size(58, 17);
            this.rbtnProject.TabIndex = 13;
            this.rbtnProject.TabStop = true;
            this.rbtnProject.Text = "Project";
            this.rbtnProject.UseVisualStyleBackColor = true;
            this.rbtnProject.Click += new System.EventHandler(this.rbtnProject_Click);
            // 
            // rbtnRespondent
            // 
            this.rbtnRespondent.AutoSize = true;
            this.rbtnRespondent.Location = new System.Drawing.Point(332, 14);
            this.rbtnRespondent.Name = "rbtnRespondent";
            this.rbtnRespondent.Size = new System.Drawing.Size(83, 17);
            this.rbtnRespondent.TabIndex = 14;
            this.rbtnRespondent.Text = "Respondent";
            this.rbtnRespondent.UseVisualStyleBackColor = true;
            this.rbtnRespondent.Click += new System.EventHandler(this.rbtnRespondent_Click);
            // 
            // frmAddReferralPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 388);
            this.Controls.Add(this.gbChooseType);
            this.Controls.Add(this.gbChooseGrp);
            this.Controls.Add(this.gbChooseRef);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(808, 419);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(808, 419);
            this.Name = "frmAddReferralPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Referral";
            this.Load += new System.EventHandler(this.frmAddReferralPopup_Load);
            this.gbChooseRef.ResumeLayout(false);
            this.gbChooseRef.PerformLayout();
            this.gbChooseGrp.ResumeLayout(false);
            this.gbChooseGrp.PerformLayout();
            this.gbChooseType.ResumeLayout(false);
            this.gbChooseType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbChooseRef;
        private System.Windows.Forms.RadioButton rbtnFree;
        private System.Windows.Forms.RadioButton rbtnPnr;
        private System.Windows.Forms.RadioButton rbtnData;
        private System.Windows.Forms.RadioButton rbtnLate;
        private System.Windows.Forms.RadioButton rbtnCorrect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbChooseGrp;
        private System.Windows.Forms.RadioButton rbtnNpcSupervisor;
        private System.Windows.Forms.RadioButton rbtnHqSuper;
        private System.Windows.Forms.RadioButton rbtnHqAnalyst;
        private System.Windows.Forms.GroupBox gbChooseType;
        private System.Windows.Forms.RadioButton rbtnProject;
        private System.Windows.Forms.RadioButton rbtnRespondent;
    }
}