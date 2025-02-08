namespace Cprs
{
    partial class frmReferral
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRespid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddReferral = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tbReferrals = new System.Windows.Forms.TabControl();
            this.tbProject = new System.Windows.Forms.TabPage();
            this.dgProjReferrals = new System.Windows.Forms.DataGridView();
            this.tbRespondent = new System.Windows.Forms.TabPage();
            this.dgRespReferrals = new System.Windows.Forms.DataGridView();
            this.btnUpdReferral = new System.Windows.Forms.Button();
            this.lblTab = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tbReferrals.SuspendLayout();
            this.tbProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjReferrals)).BeginInit();
            this.tbRespondent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRespReferrals)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(524, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "REFERRALS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtId
            // 
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(76, 75);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(81, 20);
            this.txtId.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(50, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "ID";
            // 
            // txtRespid
            // 
            this.txtRespid.ForeColor = System.Drawing.Color.Black;
            this.txtRespid.Location = new System.Drawing.Point(1093, 75);
            this.txtRespid.Name = "txtRespid";
            this.txtRespid.ReadOnly = true;
            this.txtRespid.Size = new System.Drawing.Size(79, 20);
            this.txtRespid.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(1034, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "RESPID";
            // 
            // btnAddReferral
            // 
            this.btnAddReferral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddReferral.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAddReferral.Location = new System.Drawing.Point(232, 824);
            this.btnAddReferral.Name = "btnAddReferral";
            this.btnAddReferral.Size = new System.Drawing.Size(140, 23);
            this.btnAddReferral.TabIndex = 28;
            this.btnAddReferral.TabStop = false;
            this.btnAddReferral.Text = "ADD REFERRAL";
            this.btnAddReferral.UseVisualStyleBackColor = true;
            this.btnAddReferral.Click += new System.EventHandler(this.btnAddReferral_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnReturn.Location = new System.Drawing.Point(635, 824);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(140, 23);
            this.btnReturn.TabIndex = 27;
            this.btnReturn.TabStop = false;
            this.btnReturn.Text = "PREVIOUS";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(845, 824);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 23);
            this.btnPrint.TabIndex = 26;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tbReferrals
            // 
            this.tbReferrals.Controls.Add(this.tbProject);
            this.tbReferrals.Controls.Add(this.tbRespondent);
            this.tbReferrals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReferrals.Location = new System.Drawing.Point(49, 115);
            this.tbReferrals.Name = "tbReferrals";
            this.tbReferrals.SelectedIndex = 0;
            this.tbReferrals.Size = new System.Drawing.Size(1129, 529);
            this.tbReferrals.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbReferrals.TabIndex = 29;
            this.tbReferrals.SelectedIndexChanged += new System.EventHandler(this.tbReferrals_SelectedIndexChanged);
            this.tbReferrals.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbReferrals_Selecting);
            this.tbReferrals.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbReferrals_Selected);
            // 
            // tbProject
            // 
            this.tbProject.Controls.Add(this.dgProjReferrals);
            this.tbProject.Location = new System.Drawing.Point(4, 22);
            this.tbProject.Name = "tbProject";
            this.tbProject.Padding = new System.Windows.Forms.Padding(3);
            this.tbProject.Size = new System.Drawing.Size(1121, 503);
            this.tbProject.TabIndex = 2;
            this.tbProject.Text = "PROJECT";
            this.tbProject.UseVisualStyleBackColor = true;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProjReferrals.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgProjReferrals.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgProjReferrals.Location = new System.Drawing.Point(3, 3);
            this.dgProjReferrals.MultiSelect = false;
            this.dgProjReferrals.Name = "dgProjReferrals";
            this.dgProjReferrals.ReadOnly = true;
            this.dgProjReferrals.RowHeadersVisible = false;
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            this.dgProjReferrals.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgProjReferrals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProjReferrals.Size = new System.Drawing.Size(1112, 497);
            this.dgProjReferrals.TabIndex = 4;
            this.dgProjReferrals.SelectionChanged += new System.EventHandler(this.dgProjReferrals_SelectionChanged);
            // 
            // tbRespondent
            // 
            this.tbRespondent.Controls.Add(this.dgRespReferrals);
            this.tbRespondent.Location = new System.Drawing.Point(4, 22);
            this.tbRespondent.Name = "tbRespondent";
            this.tbRespondent.Padding = new System.Windows.Forms.Padding(3);
            this.tbRespondent.Size = new System.Drawing.Size(1121, 503);
            this.tbRespondent.TabIndex = 1;
            this.tbRespondent.Text = "RESPONDENT";
            this.tbRespondent.UseVisualStyleBackColor = true;
            // 
            // dgRespReferrals
            // 
            this.dgRespReferrals.AllowUserToAddRows = false;
            this.dgRespReferrals.AllowUserToDeleteRows = false;
            this.dgRespReferrals.AllowUserToResizeColumns = false;
            this.dgRespReferrals.AllowUserToResizeRows = false;
            this.dgRespReferrals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRespReferrals.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgRespReferrals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgRespReferrals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRespReferrals.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgRespReferrals.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRespReferrals.Location = new System.Drawing.Point(3, 3);
            this.dgRespReferrals.MultiSelect = false;
            this.dgRespReferrals.Name = "dgRespReferrals";
            this.dgRespReferrals.ReadOnly = true;
            this.dgRespReferrals.RowHeadersVisible = false;
            this.dgRespReferrals.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgRespReferrals.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgRespReferrals.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            this.dgRespReferrals.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgRespReferrals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRespReferrals.Size = new System.Drawing.Size(1112, 497);
            this.dgRespReferrals.TabIndex = 5;
            this.dgRespReferrals.SelectionChanged += new System.EventHandler(this.dgRespReferrals_SelectionChanged);
            // 
            // btnUpdReferral
            // 
            this.btnUpdReferral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdReferral.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnUpdReferral.Location = new System.Drawing.Point(434, 824);
            this.btnUpdReferral.Name = "btnUpdReferral";
            this.btnUpdReferral.Size = new System.Drawing.Size(140, 23);
            this.btnUpdReferral.TabIndex = 30;
            this.btnUpdReferral.TabStop = false;
            this.btnUpdReferral.Text = "UPDATE REFERRAL";
            this.btnUpdReferral.UseVisualStyleBackColor = true;
            this.btnUpdReferral.Click += new System.EventHandler(this.btnUpdReferral_Click);
            // 
            // lblTab
            // 
            this.lblTab.AutoSize = true;
            this.lblTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab.Location = new System.Drawing.Point(562, 54);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(49, 18);
            this.lblTab.TabIndex = 31;
            this.lblTab.Text = "Table";
            // 
          
            // 
            // frmReferral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.lblTab);
            this.Controls.Add(this.btnUpdReferral);
            this.Controls.Add(this.tbReferrals);
            this.Controls.Add(this.btnAddReferral);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtRespid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1224, 900);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1224, 880);
            this.Name = "frmReferral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Value of Construction Put in Place";
            this.Load += new System.EventHandler(this.frmReferral_Load);
            this.tbReferrals.ResumeLayout(false);
            this.tbProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProjReferrals)).EndInit();
            this.tbRespondent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRespReferrals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRespid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddReferral;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TabControl tbReferrals;
        private System.Windows.Forms.TabPage tbProject;
        private System.Windows.Forms.DataGridView dgProjReferrals;
        private System.Windows.Forms.TabPage tbRespondent;
        private System.Windows.Forms.DataGridView dgRespReferrals;
        private System.Windows.Forms.Button btnUpdReferral;
        private System.Windows.Forms.Label lblTab;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}