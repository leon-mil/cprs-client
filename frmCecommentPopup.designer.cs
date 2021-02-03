namespace Cprs
{
    partial class frmCecommentPopup
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.cbApp = new System.Windows.Forms.ComboBox();
            this.cbCost = new System.Windows.Forms.ComboBox();
            this.btnApp = new System.Windows.Forms.Button();
            this.btnCost = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Comment:";
            // 
            // txtRemark
            // 
            this.txtRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemark.Location = new System.Drawing.Point(49, 53);
            this.txtRemark.MaxLength = 240;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(542, 40);
            this.txtRemark.TabIndex = 1;
            // 
            // cbApp
            // 
            this.cbApp.FormattingEnabled = true;
            this.cbApp.Items.AddRange(new object[] {
            "REMOVED APPLIANCE COST FROM SINGLE/MULT MONTHS(R)",
            "ADJUST MONTHLY COSTS TO INCLUDE APPLIANCE COST(R)",
            "OTHER APPLIANCE COST ADJUSTMENT(I)"});
            this.cbApp.Location = new System.Drawing.Point(49, 53);
            this.cbApp.Name = "cbApp";
            this.cbApp.Size = new System.Drawing.Size(542, 21);
            this.cbApp.TabIndex = 2;
            // 
            // cbCost
            // 
            this.cbCost.FormattingEnabled = true;
            this.cbCost.Items.AddRange(new object[] {
            "CURRENT MONTH COST < PREVIOUSLY REPORTED COST, KEEP CURRENT COST(R)",
            "CURRENT MONTH COST = PREVIOUSLY REPORTED COST, KEEP CURRENT COST BASED ON JOB(R)",
            "CURRENT MONTH COST > PREVIOUSLY REPORTED COST SUBTRACTION(I)",
            "CURRENT MONTH COST = PREVIOUSLY REPORTED COST, REMOVE CURRENT BASED ON JOB(I)",
            "OTHER MONTHLY COST ADJUSTMENT(I)"});
            this.cbCost.Location = new System.Drawing.Point(49, 53);
            this.cbCost.Name = "cbCost";
            this.cbCost.Size = new System.Drawing.Size(542, 21);
            this.cbCost.TabIndex = 3;
            // 
            // btnApp
            // 
            this.btnApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApp.Location = new System.Drawing.Point(153, 117);
            this.btnApp.Name = "btnApp";
            this.btnApp.Size = new System.Drawing.Size(151, 23);
            this.btnApp.TabIndex = 4;
            this.btnApp.Text = "Select Appliance Comment";
            this.btnApp.UseVisualStyleBackColor = true;
            this.btnApp.Click += new System.EventHandler(this.btnApp_Click);
            // 
            // btnCost
            // 
            this.btnCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCost.Location = new System.Drawing.Point(335, 117);
            this.btnCost.Name = "btnCost";
            this.btnCost.Size = new System.Drawing.Size(151, 23);
            this.btnCost.TabIndex = 5;
            this.btnCost.Text = "Select Cost Comment\r\n";
            this.btnCost.UseVisualStyleBackColor = true;
            this.btnCost.Click += new System.EventHandler(this.btnCost_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(226, 157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(332, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmCecommentPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 193);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCost);
            this.Controls.Add(this.btnApp);
            this.Controls.Add(this.cbCost);
            this.Controls.Add(this.cbApp);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 223);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 223);
            this.Name = "frmCecommentPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comment";
            this.Load += new System.EventHandler(this.frmCecommentPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.ComboBox cbApp;
        private System.Windows.Forms.ComboBox cbCost;
        private System.Windows.Forms.Button btnApp;
        private System.Windows.Forms.Button btnCost;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}