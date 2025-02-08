namespace Cprs
{
    partial class frmMarkPopup
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtMark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdbR = new System.Windows.Forms.RadioButton();
            this.rdbP = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(339, 172);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(240, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(133, 172);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtMark
            // 
            this.txtMark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMark.Location = new System.Drawing.Point(44, 92);
            this.txtMark.MaxLength = 240;
            this.txtMark.Multiline = true;
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(503, 56);
            this.txtMark.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter Mark Note:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rdbR);
            this.groupBox3.Controls.Add(this.rdbP);
            this.groupBox3.Location = new System.Drawing.Point(303, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 35);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Select Type:";
            // 
            // rdbR
            // 
            this.rdbR.AutoSize = true;
            this.rdbR.Location = new System.Drawing.Point(143, 13);
            this.rdbR.Name = "rdbR";
            this.rdbR.Size = new System.Drawing.Size(83, 17);
            this.rdbR.TabIndex = 1;
            this.rdbR.Text = "Respondent";
            this.rdbR.UseVisualStyleBackColor = true;
            this.rdbR.CheckedChanged += new System.EventHandler(this.rdbR_CheckedChanged);
            // 
            // rdbP
            // 
            this.rdbP.AutoSize = true;
            this.rdbP.Checked = true;
            this.rdbP.Location = new System.Drawing.Point(79, 11);
            this.rdbP.Name = "rdbP";
            this.rdbP.Size = new System.Drawing.Size(58, 17);
            this.rdbP.TabIndex = 0;
            this.rdbP.TabStop = true;
            this.rdbP.Text = "Project";
            this.rdbP.UseVisualStyleBackColor = true;
            this.rdbP.CheckedChanged += new System.EventHandler(this.rdbP_CheckedChanged);
            // 
            // frmMarkPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 270);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMark);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(596, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(596, 300);
            this.Name = "frmMarkPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Mark";
            this.Load += new System.EventHandler(this.frmMarkPopup_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtMark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdbR;
        private System.Windows.Forms.RadioButton rdbP;
    }
}