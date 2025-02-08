namespace Cprs
{
    partial class frmAddPrinterPopup
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
            this.txtPrinterName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rdHQ = new System.Windows.Forms.RadioButton();
            this.rdNPC = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtPrinterName
            // 
            this.txtPrinterName.Location = new System.Drawing.Point(38, 113);
            this.txtPrinterName.MaxLength = 40;
            this.txtPrinterName.Multiline = true;
            this.txtPrinterName.Name = "txtPrinterName";
            this.txtPrinterName.Size = new System.Drawing.Size(542, 29);
            this.txtPrinterName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Enter Printer Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(333, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(196, 167);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Choose Location:";
            // 
            // rdHQ
            // 
            this.rdHQ.AutoSize = true;
            this.rdHQ.Checked = true;
            this.rdHQ.Location = new System.Drawing.Point(170, 39);
            this.rdHQ.Name = "rdHQ";
            this.rdHQ.Size = new System.Drawing.Size(41, 17);
            this.rdHQ.TabIndex = 22;
            this.rdHQ.TabStop = true;
            this.rdHQ.Text = "HQ";
            this.rdHQ.UseVisualStyleBackColor = true;
            // 
            // rdNPC
            // 
            this.rdNPC.AutoSize = true;
            this.rdNPC.Location = new System.Drawing.Point(233, 39);
            this.rdNPC.Name = "rdNPC";
            this.rdNPC.Size = new System.Drawing.Size(47, 17);
            this.rdNPC.TabIndex = 23;
            this.rdNPC.Text = "NPC";
            this.rdNPC.UseVisualStyleBackColor = true;
            // 
            // frmAddPrinterPopup
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 238);
            this.Controls.Add(this.rdNPC);
            this.Controls.Add(this.rdHQ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrinterName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddPrinterPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Printer";
            //this.Load += new System.EventHandler(this.frmAddPrinterPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrinterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdHQ;
        private System.Windows.Forms.RadioButton rdNPC;
    }
}