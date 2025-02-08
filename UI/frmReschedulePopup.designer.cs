namespace Cprs
{
    partial class frmReschedulePopup
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdRespondent = new System.Windows.Forms.RadioButton();
            this.rdInterviewer = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblRespid = new System.Windows.Forms.Label();
            this.lblOwner = new System.Windows.Forms.Label();
            this.lblAddr = new System.Windows.Forms.Label();
            this.lblTimezone = new System.Windows.Forms.Label();
            this.lblResptime = new System.Windows.Forms.Label();
            this.lbleInttime = new System.Windows.Forms.Label();
            this.txtEtime = new System.Windows.Forms.TextBox();
            this.txtBtime = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(183, 292);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(75, 291);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "RESPID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Owner:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Address:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Timezone:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Current Respondent Time:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(225, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Current Interview Time:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdRespondent);
            this.groupBox1.Controls.Add(this.rdInterviewer);
            this.groupBox1.Location = new System.Drawing.Point(22, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(115, 72);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Time";
            // 
            // rdRespondent
            // 
            this.rdRespondent.AutoSize = true;
            this.rdRespondent.Checked = true;
            this.rdRespondent.Location = new System.Drawing.Point(8, 19);
            this.rdRespondent.Name = "rdRespondent";
            this.rdRespondent.Size = new System.Drawing.Size(83, 17);
            this.rdRespondent.TabIndex = 36;
            this.rdRespondent.TabStop = true;
            this.rdRespondent.Text = "Respondent";
            this.rdRespondent.UseVisualStyleBackColor = true;
            // 
            // rdInterviewer
            // 
            this.rdInterviewer.AutoSize = true;
            this.rdInterviewer.Location = new System.Drawing.Point(8, 42);
            this.rdInterviewer.Name = "rdInterviewer";
            this.rdInterviewer.Size = new System.Drawing.Size(77, 17);
            this.rdInterviewer.TabIndex = 35;
            this.rdInterviewer.Text = "Interviewer";
            this.rdInterviewer.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(197, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Date (MMDD):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 211);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Begin Time (HHMM):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(180, 236);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "End Time(HHMM):";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.Location = new System.Drawing.Point(69, 15);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(35, 13);
            this.lblId.TabIndex = 44;
            this.lblId.Text = "label2";
            // 
            // lblRespid
            // 
            this.lblRespid.AutoSize = true;
            this.lblRespid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRespid.Location = new System.Drawing.Point(225, 15);
            this.lblRespid.Name = "lblRespid";
            this.lblRespid.Size = new System.Drawing.Size(35, 13);
            this.lblRespid.TabIndex = 45;
            this.lblRespid.Text = "label2";
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOwner.Location = new System.Drawing.Point(69, 41);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(35, 13);
            this.lblOwner.TabIndex = 46;
            this.lblOwner.Text = "label2";
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddr.Location = new System.Drawing.Point(69, 71);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(35, 13);
            this.lblAddr.TabIndex = 47;
            this.lblAddr.Text = "label2";
            // 
            // lblTimezone
            // 
            this.lblTimezone.AutoSize = true;
            this.lblTimezone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimezone.Location = new System.Drawing.Point(72, 103);
            this.lblTimezone.Name = "lblTimezone";
            this.lblTimezone.Size = new System.Drawing.Size(35, 13);
            this.lblTimezone.TabIndex = 48;
            this.lblTimezone.Text = "label2";
            // 
            // lblResptime
            // 
            this.lblResptime.AutoSize = true;
            this.lblResptime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResptime.Location = new System.Drawing.Point(19, 148);
            this.lblResptime.Name = "lblResptime";
            this.lblResptime.Size = new System.Drawing.Size(35, 13);
            this.lblResptime.TabIndex = 49;
            this.lblResptime.Text = "label2";
            // 
            // lbleInttime
            // 
            this.lbleInttime.AutoSize = true;
            this.lbleInttime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbleInttime.Location = new System.Drawing.Point(225, 148);
            this.lbleInttime.Name = "lbleInttime";
            this.lbleInttime.Size = new System.Drawing.Size(35, 13);
            this.lbleInttime.TabIndex = 50;
            this.lbleInttime.Text = "label2";
            // 
            // txtEtime
            // 
            this.txtEtime.Location = new System.Drawing.Point(281, 233);
            this.txtEtime.MaxLength = 4;
            this.txtEtime.Name = "txtEtime";
            this.txtEtime.Size = new System.Drawing.Size(52, 20);
            this.txtEtime.TabIndex = 51;
            this.txtEtime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEtime_KeyPress);
            // 
            // txtBtime
            // 
            this.txtBtime.Location = new System.Drawing.Point(281, 207);
            this.txtBtime.MaxLength = 4;
            this.txtBtime.Name = "txtBtime";
            this.txtBtime.Size = new System.Drawing.Size(52, 20);
            this.txtBtime.TabIndex = 52;
            this.txtBtime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBtime_KeyPress);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(279, 181);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(54, 20);
            this.dateTimePicker1.TabIndex = 53;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // frmReschedulePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 327);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtBtime);
            this.Controls.Add(this.txtEtime);
            this.Controls.Add(this.lbleInttime);
            this.Controls.Add(this.lblResptime);
            this.Controls.Add(this.lblTimezone);
            this.Controls.Add(this.lblAddr);
            this.Controls.Add(this.lblOwner);
            this.Controls.Add(this.lblRespid);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReschedulePopup";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Reschedule";
            this.Load += new System.EventHandler(this.frmReschedulePopup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdRespondent;
        private System.Windows.Forms.RadioButton rdInterviewer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblRespid;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.Label lblTimezone;
        private System.Windows.Forms.Label lblResptime;
        private System.Windows.Forms.Label lbleInttime;
        private System.Windows.Forms.TextBox txtEtime;
        private System.Windows.Forms.TextBox txtBtime;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}