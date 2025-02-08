namespace Cprs
{
    partial class frmImpReferral
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
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnAddReferral = new System.Windows.Forms.Button();
            this.dgProjReferrals = new System.Windows.Forms.DataGridView();
            this.btnUpdReferral = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjReferrals)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnReturn.Location = new System.Drawing.Point(635, 824);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(140, 23);
            this.btnReturn.TabIndex = 38;
            this.btnReturn.TabStop = false;
            this.btnReturn.Text = "PREVIOUS";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnAddReferral
            // 
            this.btnAddReferral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddReferral.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAddReferral.Location = new System.Drawing.Point(232, 824);
            this.btnAddReferral.Name = "btnAddReferral";
            this.btnAddReferral.Size = new System.Drawing.Size(140, 23);
            this.btnAddReferral.TabIndex = 39;
            this.btnAddReferral.TabStop = false;
            this.btnAddReferral.Text = "ADD REFERRAL";
            this.btnAddReferral.UseVisualStyleBackColor = true;
            this.btnAddReferral.Click += new System.EventHandler(this.btnAddReferral_Click);
            // 
            // dgProjReferrals
            // 
            this.dgProjReferrals.AllowUserToAddRows = false;
            this.dgProjReferrals.AllowUserToDeleteRows = false;
            this.dgProjReferrals.AllowUserToResizeColumns = false;
            this.dgProjReferrals.AllowUserToResizeRows = false;
            this.dgProjReferrals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgProjReferrals.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgProjReferrals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgProjReferrals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProjReferrals.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgProjReferrals.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgProjReferrals.Location = new System.Drawing.Point(44, 118);
            this.dgProjReferrals.MultiSelect = false;
            this.dgProjReferrals.Name = "dgProjReferrals";
            this.dgProjReferrals.ReadOnly = true;
            this.dgProjReferrals.RowHeadersVisible = false;
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgProjReferrals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProjReferrals.Size = new System.Drawing.Size(1129, 579);
            this.dgProjReferrals.TabIndex = 4;
            // 
            // btnUpdReferral
            // 
            this.btnUpdReferral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdReferral.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnUpdReferral.Location = new System.Drawing.Point(434, 824);
            this.btnUpdReferral.Name = "btnUpdReferral";
            this.btnUpdReferral.Size = new System.Drawing.Size(140, 23);
            this.btnUpdReferral.TabIndex = 41;
            this.btnUpdReferral.TabStop = false;
            this.btnUpdReferral.Text = "UPDATE REFERRAL";
            this.btnUpdReferral.UseVisualStyleBackColor = true;
            this.btnUpdReferral.Click += new System.EventHandler(this.btnUpdReferral_Click);
            // 
            
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(845, 824);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 23);
            this.btnPrint.TabIndex = 37;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtId
            // 
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(85, 83);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(81, 20);
            this.txtId.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(47, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "ID";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(440, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 25);
            this.label1.TabIndex = 32;
            this.label1.Text = "IMPROVEMENTS REFERRALS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmImpReferral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.dgProjReferrals);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnAddReferral);
            this.Controls.Add(this.btnUpdReferral);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1224, 900);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1224, 900);
            this.Name = "frmImpReferral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Value of Construction Put in Place";
            this.Load += new System.EventHandler(this.frmReferral_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgProjReferrals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnAddReferral;
        private System.Windows.Forms.DataGridView dgProjReferrals;
        private System.Windows.Forms.Button btnUpdReferral;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
    }
}