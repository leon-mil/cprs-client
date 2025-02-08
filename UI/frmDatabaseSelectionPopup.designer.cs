namespace Cprs
{
    partial class frmDatabaseSelectionPopup
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
            this.cbDB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbDB
            // 
            this.cbDB.FormattingEnabled = true;
            this.cbDB.Items.AddRange(new object[] {
            "CPRSDEV",
            "CPRSTEST",
            "CPRSPROD"});
            this.cbDB.Location = new System.Drawing.Point(141, 39);
            this.cbDB.Name = "cbDB";
            this.cbDB.Size = new System.Drawing.Size(121, 21);
            this.cbDB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Database:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(141, 102);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmDatabaseSelectionPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 180);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDB);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(346, 210);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(346, 210);
            this.Name = "frmDatabaseSelectionPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Selection";
            this.Load += new System.EventHandler(this.frmDatabaseSelectionPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
    }
}