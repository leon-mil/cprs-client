namespace Cprs
{
    partial class frmAddMonthlyAuditPopup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdNormal = new System.Windows.Forms.RadioButton();
            this.rdUnusual = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdNormal);
            this.groupBox1.Controls.Add(this.rdUnusual);
            this.groupBox1.Location = new System.Drawing.Point(193, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 67);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SELECT TYPE:";
            // 
            // rdNormal
            // 
            this.rdNormal.AutoSize = true;
            this.rdNormal.Checked = true;
            this.rdNormal.Location = new System.Drawing.Point(6, 19);
            this.rdNormal.Name = "rdNormal";
            this.rdNormal.Size = new System.Drawing.Size(95, 17);
            this.rdNormal.TabIndex = 12;
            this.rdNormal.TabStop = true;
            this.rdNormal.Text = "Normal Activity";
            this.rdNormal.UseVisualStyleBackColor = true;
            this.rdNormal.CheckedChanged += new System.EventHandler(this.rdNormal_CheckedChanged);
            // 
            // rdUnusual
            // 
            this.rdUnusual.AutoSize = true;
            this.rdUnusual.Location = new System.Drawing.Point(6, 38);
            this.rdUnusual.Name = "rdUnusual";
            this.rdUnusual.Size = new System.Drawing.Size(101, 17);
            this.rdUnusual.TabIndex = 13;
            this.rdUnusual.Text = "Unusual Activity";
            this.rdUnusual.UseVisualStyleBackColor = true;
            this.rdUnusual.CheckedChanged += new System.EventHandler(this.rdUnusual_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(318, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(193, 179);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtComment
            // 
            this.txtComment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComment.Location = new System.Drawing.Point(87, 132);
            this.txtComment.MaxLength = 80;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(478, 20);
            this.txtComment.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Comments:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(149, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(244, 24);
            this.lblTitle.TabIndex = 19;
            this.lblTitle.Text = "2016 CURRENT REVIEW";
            // 
            // frmAddMonthlyAuditPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(597, 226);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddMonthlyAuditPopup";
            this.Text = "Current Review";
            this.Load += new System.EventHandler(this.frmAddMonthlyAuditPopup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdNormal;
        private System.Windows.Forms.RadioButton rdUnusual;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
    }
}