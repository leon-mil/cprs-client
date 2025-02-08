namespace Cprs
{
    partial class frmDisplayNotesPopup
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
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContent.Location = new System.Drawing.Point(12, 21);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(1190, 785);
            this.txtContent.TabIndex = 7;
            this.txtContent.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClose.Location = new System.Drawing.Point(526, 834);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(123, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "PREVIOUS";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmDisplayNotesPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 869);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtContent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDisplayNotesPopup";
            this.Text = "DisplayNotes";
            this.Load += new System.EventHandler(this.frmDisplayNotesPopup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtContent;
        private System.Windows.Forms.Button btnClose;
    }
}