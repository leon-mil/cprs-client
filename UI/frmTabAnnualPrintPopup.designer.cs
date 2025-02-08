namespace Cprs
{
    partial class frmTabAnnualPrintPopup
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
            this.grpYears = new System.Windows.Forms.GroupBox();
            this.rdAllYrs = new System.Windows.Forms.RadioButton();
            this.rdYear5 = new System.Windows.Forms.RadioButton();
            this.rdYear4 = new System.Windows.Forms.RadioButton();
            this.rdYear3 = new System.Windows.Forms.RadioButton();
            this.rdYear2 = new System.Windows.Forms.RadioButton();
            this.rdYear1 = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpYears.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpYears
            // 
            this.grpYears.Controls.Add(this.rdAllYrs);
            this.grpYears.Controls.Add(this.rdYear5);
            this.grpYears.Controls.Add(this.rdYear4);
            this.grpYears.Controls.Add(this.rdYear3);
            this.grpYears.Controls.Add(this.rdYear2);
            this.grpYears.Controls.Add(this.rdYear1);
            this.grpYears.Location = new System.Drawing.Point(55, 30);
            this.grpYears.Name = "grpYears";
            this.grpYears.Size = new System.Drawing.Size(179, 162);
            this.grpYears.TabIndex = 1;
            this.grpYears.TabStop = false;
            this.grpYears.Text = "Select Year to Print";
            // 
            // rdAllYrs
            // 
            this.rdAllYrs.AutoSize = true;
            this.rdAllYrs.Location = new System.Drawing.Point(6, 134);
            this.rdAllYrs.Name = "rdAllYrs";
            this.rdAllYrs.Size = new System.Drawing.Size(75, 17);
            this.rdAllYrs.TabIndex = 5;
            this.rdAllYrs.Text = "Five Years";
            this.rdAllYrs.UseVisualStyleBackColor = true;
            this.rdAllYrs.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // rdYear5
            // 
            this.rdYear5.AutoSize = true;
            this.rdYear5.Location = new System.Drawing.Point(6, 111);
            this.rdYear5.Name = "rdYear5";
            this.rdYear5.Size = new System.Drawing.Size(14, 13);
            this.rdYear5.TabIndex = 4;
            this.rdYear5.UseVisualStyleBackColor = true;
            this.rdYear5.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // rdYear4
            // 
            this.rdYear4.AutoSize = true;
            this.rdYear4.Location = new System.Drawing.Point(6, 88);
            this.rdYear4.Name = "rdYear4";
            this.rdYear4.Size = new System.Drawing.Size(14, 13);
            this.rdYear4.TabIndex = 3;
            this.rdYear4.UseVisualStyleBackColor = true;
            this.rdYear4.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // rdYear3
            // 
            this.rdYear3.AutoSize = true;
            this.rdYear3.Location = new System.Drawing.Point(6, 65);
            this.rdYear3.Name = "rdYear3";
            this.rdYear3.Size = new System.Drawing.Size(14, 13);
            this.rdYear3.TabIndex = 2;
            this.rdYear3.UseVisualStyleBackColor = true;
            this.rdYear3.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // rdYear2
            // 
            this.rdYear2.AutoSize = true;
            this.rdYear2.Location = new System.Drawing.Point(6, 42);
            this.rdYear2.Name = "rdYear2";
            this.rdYear2.Size = new System.Drawing.Size(14, 13);
            this.rdYear2.TabIndex = 1;
            this.rdYear2.UseVisualStyleBackColor = true;
            this.rdYear2.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // rdYear1
            // 
            this.rdYear1.AutoSize = true;
            this.rdYear1.Location = new System.Drawing.Point(6, 19);
            this.rdYear1.Name = "rdYear1";
            this.rdYear1.Size = new System.Drawing.Size(14, 13);
            this.rdYear1.TabIndex = 0;
            this.rdYear1.UseVisualStyleBackColor = true;
            this.rdYear1.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(55, 213);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(159, 213);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmTabAnnualPrintPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpYears);
            this.Name = "frmTabAnnualPrintPopup";
            this.Load += new System.EventHandler(this.frmTabAnnualPrintPopup_Load);
            this.grpYears.ResumeLayout(false);
            this.grpYears.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpYears;
        private System.Windows.Forms.RadioButton rdYear5;
        private System.Windows.Forms.RadioButton rdYear4;
        private System.Windows.Forms.RadioButton rdYear3;
        private System.Windows.Forms.RadioButton rdYear2;
        private System.Windows.Forms.RadioButton rdYear1;
        private System.Windows.Forms.RadioButton rdAllYrs;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}