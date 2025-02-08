namespace Cprs
{
    partial class frmChangeRespidPopup
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
            this.gbChooseStatus = new System.Windows.Forms.GroupBox();
            this.rbtnAdd = new System.Windows.Forms.RadioButton();
            this.rbtnDelete = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRespid = new System.Windows.Forms.TextBox();
            this.gbChooseStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbChooseStatus
            // 
            this.gbChooseStatus.Controls.Add(this.rbtnAdd);
            this.gbChooseStatus.Controls.Add(this.rbtnDelete);
            this.gbChooseStatus.Location = new System.Drawing.Point(33, 36);
            this.gbChooseStatus.Name = "gbChooseStatus";
            this.gbChooseStatus.Size = new System.Drawing.Size(360, 37);
            this.gbChooseStatus.TabIndex = 25;
            this.gbChooseStatus.TabStop = false;
            this.gbChooseStatus.Text = "Select Option:";
            // 
            // rbtnAdd
            // 
            this.rbtnAdd.AutoSize = true;
            this.rbtnAdd.Checked = true;
            this.rbtnAdd.Location = new System.Drawing.Point(126, 14);
            this.rbtnAdd.Name = "rbtnAdd";
            this.rbtnAdd.Size = new System.Drawing.Size(61, 17);
            this.rbtnAdd.TabIndex = 13;
            this.rbtnAdd.TabStop = true;
            this.rbtnAdd.Text = "Existing";
            this.rbtnAdd.UseVisualStyleBackColor = true;
            this.rbtnAdd.Click += new System.EventHandler(this.rbtnAdd_Click);
            // 
            // rbtnDelete
            // 
            this.rbtnDelete.AutoSize = true;
            this.rbtnDelete.Location = new System.Drawing.Point(244, 14);
            this.rbtnDelete.Name = "rbtnDelete";
            this.rbtnDelete.Size = new System.Drawing.Size(56, 17);
            this.rbtnDelete.TabIndex = 14;
            this.rbtnDelete.Text = "Delete";
            this.rbtnDelete.UseVisualStyleBackColor = true;
            this.rbtnDelete.Click += new System.EventHandler(this.rbtnDelete_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(97, 151);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 27);
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(237, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 27);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(135, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Enter RESPID:";
            // 
            // txtRespid
            // 
            this.txtRespid.Location = new System.Drawing.Point(219, 100);
            this.txtRespid.MaxLength = 7;
            this.txtRespid.Name = "txtRespid";
            this.txtRespid.Size = new System.Drawing.Size(72, 20);
            this.txtRespid.TabIndex = 26;
            this.txtRespid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtRespid_MouseClick);
            this.txtRespid.TextChanged += new System.EventHandler(this.txtRespid_TextChanged);
            this.txtRespid.Enter += new System.EventHandler(this.txtRespid_Enter);
            this.txtRespid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRespid_KeyPress);
            // 
            // frmChangeRespidPopup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 216);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRespid);
            this.Controls.Add(this.gbChooseStatus);
            this.Name = "frmChangeRespidPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Respid";
            this.Load += new System.EventHandler(this.frmChangeRespidPopup_Load);
            this.gbChooseStatus.ResumeLayout(false);
            this.gbChooseStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbChooseStatus;
        private System.Windows.Forms.RadioButton rbtnAdd;
        private System.Windows.Forms.RadioButton rbtnDelete;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRespid;
    }
}