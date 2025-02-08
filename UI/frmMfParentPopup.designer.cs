namespace Cprs
{
    partial class frmMfParentPopup
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
            this.gbChooseType = new System.Windows.Forms.GroupBox();
            this.rbtnPreSample = new System.Windows.Forms.RadioButton();
            this.rbtnSample = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblParentType = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.gbChooseType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbChooseType
            // 
            this.gbChooseType.Controls.Add(this.rbtnPreSample);
            this.gbChooseType.Controls.Add(this.rbtnSample);
            this.gbChooseType.Location = new System.Drawing.Point(68, 12);
            this.gbChooseType.Name = "gbChooseType";
            this.gbChooseType.Size = new System.Drawing.Size(492, 37);
            this.gbChooseType.TabIndex = 16;
            this.gbChooseType.TabStop = false;
            this.gbChooseType.Text = "Choose Parent Location";
            // 
            // rbtnPreSample
            // 
            this.rbtnPreSample.AutoSize = true;
            this.rbtnPreSample.Checked = true;
            this.rbtnPreSample.Location = new System.Drawing.Point(175, 14);
            this.rbtnPreSample.Name = "rbtnPreSample";
            this.rbtnPreSample.Size = new System.Drawing.Size(76, 17);
            this.rbtnPreSample.TabIndex = 13;
            this.rbtnPreSample.TabStop = true;
            this.rbtnPreSample.Text = "PreSample";
            this.rbtnPreSample.UseVisualStyleBackColor = true;
            this.rbtnPreSample.Click += new System.EventHandler(this.rbtnPreSample_Click);
            // 
            // rbtnSample
            // 
            this.rbtnSample.AutoSize = true;
            this.rbtnSample.Location = new System.Drawing.Point(306, 14);
            this.rbtnSample.Name = "rbtnSample";
            this.rbtnSample.Size = new System.Drawing.Size(60, 17);
            this.rbtnSample.TabIndex = 14;
            this.rbtnSample.Text = "Sample";
            this.rbtnSample.UseVisualStyleBackColor = true;
            this.rbtnSample.Click += new System.EventHandler(this.rbtnSample_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(211, 139);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(347, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblParentType
            // 
            this.lblParentType.AutoSize = true;
            this.lblParentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParentType.Location = new System.Drawing.Point(183, 83);
            this.lblParentType.Name = "lblParentType";
            this.lblParentType.Size = new System.Drawing.Size(153, 20);
            this.lblParentType.TabIndex = 19;
            this.lblParentType.Text = "Enter Parent\'s ID:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(373, 83);
            this.txtID.MaxLength = 7;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(70, 20);
            this.txtID.TabIndex = 20;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // frmMfParentPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 193);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblParentType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbChooseType);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 223);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 223);
            this.Name = "frmMfParentPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Parent";
            this.Load += new System.EventHandler(this.frmMfParentPopup_Load);
            this.Click += new System.EventHandler(this.btnCancel_Click);
            this.gbChooseType.ResumeLayout(false);
            this.gbChooseType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbChooseType;
        private System.Windows.Forms.RadioButton rbtnPreSample;
        private System.Windows.Forms.RadioButton rbtnSample;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblParentType;
        private System.Windows.Forms.TextBox txtID;
    }
}