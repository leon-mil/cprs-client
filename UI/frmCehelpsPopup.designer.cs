namespace Cprs
{
    partial class frmCehelpsPopup
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
            this.lblHelp = new System.Windows.Forms.Label();
            this.dgHelp = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHelp.AutoSize = true;
            this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelp.ForeColor = System.Drawing.Color.Black;
            this.lblHelp.Location = new System.Drawing.Point(433, 22);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(71, 25);
            this.lblHelp.TabIndex = 4;
            this.lblHelp.Text = "HELP";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgHelp
            // 
            this.dgHelp.AllowUserToAddRows = false;
            this.dgHelp.AllowUserToDeleteRows = false;
            this.dgHelp.AllowUserToResizeColumns = false;
            this.dgHelp.AllowUserToResizeRows = false;
            this.dgHelp.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgHelp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgHelp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHelp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgHelp.Location = new System.Drawing.Point(38, 50);
            this.dgHelp.MultiSelect = false;
            this.dgHelp.Name = "dgHelp";
            this.dgHelp.ReadOnly = true;
            this.dgHelp.RowHeadersVisible = false;
            this.dgHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgHelp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHelp.Size = new System.Drawing.Size(942, 460);
            this.dgHelp.TabIndex = 5;
            this.dgHelp.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(418, 526);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(133, 23);
            this.btnBack.TabIndex = 53;
            this.btnBack.Text = "CANCEL";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmCehelpsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 561);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgHelp);
            this.Controls.Add(this.lblHelp);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1010, 591);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1010, 591);
            this.Name = "frmCehelpsPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            this.Load += new System.EventHandler(this.frmCehelps_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.DataGridView dgHelp;
        private System.Windows.Forms.Button btnBack;
    }
}