namespace Cprs
{
    partial class frmHistory
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtRespid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbComments = new System.Windows.Forms.TabControl();
            this.tbProject = new System.Windows.Forms.TabPage();
            this.dgProjHistory = new System.Windows.Forms.DataGridView();
            this.tbRespondent = new System.Windows.Forms.TabPage();
            this.dgRespHistory = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.lblTab = new System.Windows.Forms.Label();
            this.lblRespname = new System.Windows.Forms.Label();
            this.lblResporg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddRemark = new System.Windows.Forms.Button();
            this.tbComments.SuspendLayout();
            this.tbProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjHistory)).BeginInit();
            this.tbRespondent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRespHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(547, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "COMMENTS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(40, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "ID";
            // 
            // txtRespid
            // 
            this.txtRespid.ForeColor = System.Drawing.Color.Black;
            this.txtRespid.Location = new System.Drawing.Point(1068, 83);
            this.txtRespid.Name = "txtRespid";
            this.txtRespid.ReadOnly = true;
            this.txtRespid.Size = new System.Drawing.Size(79, 20);
            this.txtRespid.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(1009, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "RESPID";
            // 
            // tbComments
            // 
            this.tbComments.Controls.Add(this.tbProject);
            this.tbComments.Controls.Add(this.tbRespondent);
            this.tbComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbComments.Location = new System.Drawing.Point(43, 155);
            this.tbComments.Name = "tbComments";
            this.tbComments.SelectedIndex = 0;
            this.tbComments.Size = new System.Drawing.Size(1129, 579);
            this.tbComments.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbComments.TabIndex = 18;
            this.tbComments.SelectedIndexChanged += new System.EventHandler(this.tbComments_SelectedIndexChanged);
            this.tbComments.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbComments_Selecting);
            this.tbComments.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbComments_Selected);
            // 
            // tbProject
            // 
            this.tbProject.Controls.Add(this.dgProjHistory);
            this.tbProject.Location = new System.Drawing.Point(4, 22);
            this.tbProject.Name = "tbProject";
            this.tbProject.Padding = new System.Windows.Forms.Padding(3);
            this.tbProject.Size = new System.Drawing.Size(1121, 553);
            this.tbProject.TabIndex = 2;
            this.tbProject.Text = "PROJECT";
            this.tbProject.UseVisualStyleBackColor = true;
            // 
            // dgProjHistory
            // 
            this.dgProjHistory.AllowUserToAddRows = false;
            this.dgProjHistory.AllowUserToDeleteRows = false;
            this.dgProjHistory.AllowUserToResizeColumns = false;
            this.dgProjHistory.AllowUserToResizeRows = false;
            this.dgProjHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgProjHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgProjHistory.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgProjHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProjHistory.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgProjHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgProjHistory.Location = new System.Drawing.Point(3, 3);
            this.dgProjHistory.MultiSelect = false;
            this.dgProjHistory.Name = "dgProjHistory";
            this.dgProjHistory.ReadOnly = true;
            this.dgProjHistory.RowHeadersVisible = false;
            this.dgProjHistory.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgProjHistory.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgProjHistory.Size = new System.Drawing.Size(1112, 547);
            this.dgProjHistory.TabIndex = 4;
            this.dgProjHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProjHistory_CellContentClick);
            this.dgProjHistory.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgProjHistory_DataBindingComplete);
            // 
            // tbRespondent
            // 
            this.tbRespondent.Controls.Add(this.dgRespHistory);
            this.tbRespondent.Location = new System.Drawing.Point(4, 22);
            this.tbRespondent.Name = "tbRespondent";
            this.tbRespondent.Padding = new System.Windows.Forms.Padding(3);
            this.tbRespondent.Size = new System.Drawing.Size(1121, 553);
            this.tbRespondent.TabIndex = 1;
            this.tbRespondent.Text = "RESPONDENT";
            this.tbRespondent.UseVisualStyleBackColor = true;
            // 
            // dgRespHistory
            // 
            this.dgRespHistory.AllowUserToAddRows = false;
            this.dgRespHistory.AllowUserToDeleteRows = false;
            this.dgRespHistory.AllowUserToResizeColumns = false;
            this.dgRespHistory.AllowUserToResizeRows = false;
            this.dgRespHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRespHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgRespHistory.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgRespHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRespHistory.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgRespHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRespHistory.Location = new System.Drawing.Point(3, 3);
            this.dgRespHistory.MultiSelect = false;
            this.dgRespHistory.Name = "dgRespHistory";
            this.dgRespHistory.ReadOnly = true;
            this.dgRespHistory.RowHeadersVisible = false;
            this.dgRespHistory.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgRespHistory.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgRespHistory.Size = new System.Drawing.Size(1116, 547);
            this.dgRespHistory.TabIndex = 5;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(754, 787);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 19;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnReturn.Location = new System.Drawing.Point(542, 787);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(133, 23);
            this.btnReturn.TabIndex = 20;
            this.btnReturn.TabStop = false;
            this.btnReturn.Text = "PREVIOUS";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // txtId
            // 
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(66, 83);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(81, 20);
            this.txtId.TabIndex = 21;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // lblTab
            // 
            this.lblTab.AutoSize = true;
            this.lblTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab.Location = new System.Drawing.Point(585, 46);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(49, 18);
            this.lblTab.TabIndex = 23;
            this.lblTab.Text = "Table";
            // 
            // lblRespname
            // 
            this.lblRespname.AutoSize = true;
            this.lblRespname.ForeColor = System.Drawing.Color.Black;
            this.lblRespname.Location = new System.Drawing.Point(656, 127);
            this.lblRespname.Name = "lblRespname";
            this.lblRespname.Size = new System.Drawing.Size(77, 13);
            this.lblRespname.TabIndex = 24;
            this.lblRespname.Text = "XXXXXXXXXX";
            // 
            // lblResporg
            // 
            this.lblResporg.AutoSize = true;
            this.lblResporg.ForeColor = System.Drawing.Color.Black;
            this.lblResporg.Location = new System.Drawing.Point(154, 127);
            this.lblResporg.Name = "lblResporg";
            this.lblResporg.Size = new System.Drawing.Size(84, 13);
            this.lblResporg.TabIndex = 25;
            this.lblResporg.Text = "XXXXXXXXXXX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(585, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "CONTACT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "ORGANIZATION";
            // 
            // btnAddRemark
            // 
            this.btnAddRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRemark.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAddRemark.Location = new System.Drawing.Point(336, 787);
            this.btnAddRemark.Name = "btnAddRemark";
            this.btnAddRemark.Size = new System.Drawing.Size(133, 23);
            this.btnAddRemark.TabIndex = 21;
            this.btnAddRemark.TabStop = false;
            this.btnAddRemark.Text = "ADD COMMENT";
            this.btnAddRemark.UseVisualStyleBackColor = true;
            this.btnAddRemark.Click += new System.EventHandler(this.btnAddRemark_Click);
            // 
            // frmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 869);
            this.Controls.Add(this.btnAddRemark);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblResporg);
            this.Controls.Add(this.lblRespname);
            this.Controls.Add(this.lblTab);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tbComments);
            this.Controls.Add(this.txtRespid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.DarkBlue;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1224, 900);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1224, 900);
            this.Name = "frmHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Value of Construction Put in Place";
            this.Load += new System.EventHandler(this.frmHistory_Load);
            this.tbComments.ResumeLayout(false);
            this.tbProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProjHistory)).EndInit();
            this.tbRespondent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRespHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRespid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tbComments;
        private System.Windows.Forms.TabPage tbProject;
        private System.Windows.Forms.TabPage tbRespondent;
        private System.Windows.Forms.DataGridView dgProjHistory;
        private System.Windows.Forms.DataGridView dgRespHistory;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.TextBox txtId;
        private System.Drawing.Printing.PrintDocument printDocument1;
        //private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.Label lblRespname;
        private System.Windows.Forms.Label lblResporg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddRemark;
        
    }
}