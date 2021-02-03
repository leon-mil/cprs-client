namespace Cprs
{
    partial class frmMarkCasesPopup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMarks = new System.Windows.Forms.TabControl();
            this.tbProject = new System.Windows.Forms.TabPage();
            this.dgProjMark = new System.Windows.Forms.DataGridView();
            this.tbRespondent = new System.Windows.Forms.TabPage();
            this.dgRespMark = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.txtRespid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMarks.SuspendLayout();
            this.tbProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjMark)).BeginInit();
            this.tbRespondent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRespMark)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(513, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "MARK NOTES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtId
            // 
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(77, 56);
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
            this.label6.Location = new System.Drawing.Point(51, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "ID";
            // 
            // tbMarks
            // 
            this.tbMarks.Controls.Add(this.tbProject);
            this.tbMarks.Controls.Add(this.tbRespondent);
            this.tbMarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMarks.Location = new System.Drawing.Point(44, 121);
            this.tbMarks.Name = "tbMarks";
            this.tbMarks.SelectedIndex = 0;
            this.tbMarks.Size = new System.Drawing.Size(1129, 579);
            this.tbMarks.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbMarks.TabIndex = 24;
            this.tbMarks.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbMarks_Selecting);
            // 
            // tbProject
            // 
            this.tbProject.Controls.Add(this.dgProjMark);
            this.tbProject.Location = new System.Drawing.Point(4, 22);
            this.tbProject.Name = "tbProject";
            this.tbProject.Padding = new System.Windows.Forms.Padding(3);
            this.tbProject.Size = new System.Drawing.Size(1121, 553);
            this.tbProject.TabIndex = 2;
            this.tbProject.Text = "PROJECT";
            this.tbProject.UseVisualStyleBackColor = true;
            // 
            // dgProjMark
            // 
            this.dgProjMark.AllowUserToAddRows = false;
            this.dgProjMark.AllowUserToDeleteRows = false;
            this.dgProjMark.AllowUserToResizeColumns = false;
            this.dgProjMark.AllowUserToResizeRows = false;
            this.dgProjMark.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgProjMark.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProjMark.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgProjMark.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProjMark.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgProjMark.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgProjMark.Location = new System.Drawing.Point(3, 3);
            this.dgProjMark.MultiSelect = false;
            this.dgProjMark.Name = "dgProjMark";
            this.dgProjMark.RowHeadersVisible = false;
            this.dgProjMark.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgProjMark.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgProjMark.Size = new System.Drawing.Size(1114, 538);
            this.dgProjMark.TabIndex = 16;
            this.dgProjMark.TabStop = false;
            // 
            // tbRespondent
            // 
            this.tbRespondent.Controls.Add(this.dgRespMark);
            this.tbRespondent.Location = new System.Drawing.Point(4, 22);
            this.tbRespondent.Name = "tbRespondent";
            this.tbRespondent.Padding = new System.Windows.Forms.Padding(3);
            this.tbRespondent.Size = new System.Drawing.Size(1121, 553);
            this.tbRespondent.TabIndex = 1;
            this.tbRespondent.Text = "RESPONDENT";
            this.tbRespondent.UseVisualStyleBackColor = true;
            // 
            // dgRespMark
            // 
            this.dgRespMark.AllowUserToAddRows = false;
            this.dgRespMark.AllowUserToDeleteRows = false;
            this.dgRespMark.AllowUserToResizeColumns = false;
            this.dgRespMark.AllowUserToResizeRows = false;
            this.dgRespMark.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgRespMark.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRespMark.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgRespMark.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRespMark.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgRespMark.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRespMark.Location = new System.Drawing.Point(3, 6);
            this.dgRespMark.MultiSelect = false;
            this.dgRespMark.Name = "dgRespMark";
            this.dgRespMark.RowHeadersVisible = false;
            this.dgRespMark.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgRespMark.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgRespMark.Size = new System.Drawing.Size(1114, 538);
            this.dgRespMark.TabIndex = 17;
            this.dgRespMark.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAdd.Location = new System.Drawing.Point(336, 824);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(133, 23);
            this.btnAdd.TabIndex = 27;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "MARK";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnReturn.Location = new System.Drawing.Point(542, 824);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(133, 23);
            this.btnReturn.TabIndex = 26;
            this.btnReturn.TabStop = false;
            this.btnReturn.Text = "PREVIOUS";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(754, 824);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 25;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // txtRespid
            // 
            this.txtRespid.ForeColor = System.Drawing.Color.Black;
            this.txtRespid.Location = new System.Drawing.Point(1090, 63);
            this.txtRespid.Name = "txtRespid";
            this.txtRespid.ReadOnly = true;
            this.txtRespid.Size = new System.Drawing.Size(79, 20);
            this.txtRespid.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(1031, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "RESPID";
            // 
            // frmMarkCasesPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 869);
            this.Controls.Add(this.txtRespid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tbMarks);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1224, 900);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1224, 900);
            this.Name = "frmMarkCasesPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Value of Construction Put in Place";
            this.Load += new System.EventHandler(this.frmMarkCasesPopup_Load);
            this.tbMarks.ResumeLayout(false);
            this.tbProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProjMark)).EndInit();
            this.tbRespondent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRespMark)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tbMarks;
        private System.Windows.Forms.TabPage tbProject;
        private System.Windows.Forms.TabPage tbRespondent;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TextBox txtRespid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgProjMark;
        private System.Windows.Forms.DataGridView dgRespMark;
    }
}