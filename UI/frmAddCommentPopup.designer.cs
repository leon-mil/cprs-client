namespace Cprs
{
    partial class frmAddCommentPopup
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
            this.rbtnProject = new System.Windows.Forms.RadioButton();
            this.rbtnRespondent = new System.Windows.Forms.RadioButton();
            this.gbChooseType = new System.Windows.Forms.GroupBox();
            this.gbChooseType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(322, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(185, 146);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Location = new System.Drawing.Point(47, 82);
            this.txtRemark.MaxLength = 240;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(542, 40);
            this.txtRemark.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter Comment:";
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
            // 
            // gbChooseType
            // 
            this.gbChooseType.Controls.Add(this.rbtnProject);
            this.gbChooseType.Controls.Add(this.rbtnRespondent);
            this.gbChooseType.Location = new System.Drawing.Point(47, 12);
            this.gbChooseType.Name = "gbChooseType";
            this.gbChooseType.Size = new System.Drawing.Size(542, 37);
            this.gbChooseType.TabIndex = 15;
            this.gbChooseType.TabStop = false;
            this.gbChooseType.Text = "Choose Comment Type";
            // 
            // frmAddCommentPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 193);
            this.Controls.Add(this.gbChooseType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 223);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 223);
            this.Name = "frmAddCommentPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Comment";
            this.Load += new System.EventHandler(this.frmAddCommentPopup_Load);
            this.gbChooseType.ResumeLayout(false);
            this.gbChooseType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtnProject;
        private System.Windows.Forms.RadioButton rbtnRespondent;
        private System.Windows.Forms.GroupBox gbChooseType;
    }
}