namespace Cprs
{
    partial class frmImpUnlockCase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.dgLockedCases = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgLockedCases)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUnlock
            // 
            this.btnUnlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlock.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnUnlock.Location = new System.Drawing.Point(542, 814);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(133, 23);
            this.btnUnlock.TabIndex = 34;
            this.btnUnlock.TabStop = false;
            this.btnUnlock.Text = "UNLOCK";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            this.btnUnlock.Paint += new System.Windows.Forms.PaintEventHandler(this.btnUnlock_Paint);
            // 
            // dgLockedCases
            // 
            this.dgLockedCases.AllowUserToAddRows = false;
            this.dgLockedCases.AllowUserToDeleteRows = false;
            this.dgLockedCases.AllowUserToResizeColumns = false;
            this.dgLockedCases.AllowUserToResizeRows = false;
            this.dgLockedCases.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLockedCases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgLockedCases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLockedCases.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgLockedCases.Location = new System.Drawing.Point(24, 143);
            this.dgLockedCases.MultiSelect = false;
            this.dgLockedCases.Name = "dgLockedCases";
            this.dgLockedCases.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLockedCases.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgLockedCases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLockedCases.Size = new System.Drawing.Size(1167, 639);
            this.dgLockedCases.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(418, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "IMPROVEMENTS LOCKED CASES";
            // 
            // frmImpUnlockCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.dgLockedCases);
            this.Controls.Add(this.label2);
            this.Name = "frmImpUnlockCase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImpUnlockCase_FormClosing);
            this.Load += new System.EventHandler(this.frmImpUnlockCase_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dgLockedCases, 0);
            this.Controls.SetChildIndex(this.btnUnlock, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgLockedCases)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.DataGridView dgLockedCases;
        private System.Windows.Forms.Label label2;
    }
}