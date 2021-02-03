namespace Cprs
{
    partial class frmCalendarPopup
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
            this.Calendar1 = new System.Windows.Forms.MonthCalendar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Calendar1
            // 
            this.Calendar1.Location = new System.Drawing.Point(88, 23);
            this.Calendar1.Name = "Calendar1";
            this.Calendar1.ShowToday = false;
            this.Calendar1.ShowTodayCircle = false;
            this.Calendar1.TabIndex = 0;
            this.Calendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar1_DateSelected);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(153, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmCalendarPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 245);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.Calendar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalendarPopup";
            this.Text = "Select Date";
            this.Load += new System.EventHandler(this.frmCalendarPopup_Load);
            this.Shown += new System.EventHandler(this.frmCalendarPopup_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar Calendar1;
        private System.Windows.Forms.Button btnCancel;
    }
}