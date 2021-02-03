namespace Cprs
{
    partial class frmUnlock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.tbResearch = new System.Windows.Forms.TabControl();
            this.tbUsers = new System.Windows.Forms.TabPage();
            this.dgUsers = new System.Windows.Forms.DataGridView();
            this.tbResp = new System.Windows.Forms.TabPage();
            this.dgResp = new System.Windows.Forms.DataGridView();
            this.tbPreSamp = new System.Windows.Forms.TabPage();
            this.dgSample = new System.Windows.Forms.DataGridView();
            this.tbSpecialcase = new System.Windows.Forms.TabPage();
            this.dgSpecial = new System.Windows.Forms.DataGridView();
            this.tbDup = new System.Windows.Forms.TabPage();
            this.dgDup = new System.Windows.Forms.DataGridView();
            this.tbResearch.SuspendLayout();
            this.tbUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).BeginInit();
            this.tbResp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResp)).BeginInit();
            this.tbPreSamp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSample)).BeginInit();
            this.tbSpecialcase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpecial)).BeginInit();
            this.tbDup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDup)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(556, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "UNLOCK";
            // 
            // btnUnlock
            // 
            this.btnUnlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlock.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnUnlock.Location = new System.Drawing.Point(540, 792);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(137, 23);
            this.btnUnlock.TabIndex = 12;
            this.btnUnlock.Text = "UNLOCK";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // tbResearch
            // 
            this.tbResearch.Controls.Add(this.tbUsers);
            this.tbResearch.Controls.Add(this.tbResp);
            this.tbResearch.Controls.Add(this.tbPreSamp);
            this.tbResearch.Controls.Add(this.tbSpecialcase);
            this.tbResearch.Controls.Add(this.tbDup);
            this.tbResearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbResearch.Location = new System.Drawing.Point(42, 100);
            this.tbResearch.Name = "tbResearch";
            this.tbResearch.SelectedIndex = 0;
            this.tbResearch.Size = new System.Drawing.Size(1133, 636);
            this.tbResearch.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbResearch.TabIndex = 20;
            this.tbResearch.Click += new System.EventHandler(this.tbUnlock_Click);
            // 
            // tbUsers
            // 
            this.tbUsers.Controls.Add(this.dgUsers);
            this.tbUsers.Location = new System.Drawing.Point(4, 22);
            this.tbUsers.Name = "tbUsers";
            this.tbUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tbUsers.Size = new System.Drawing.Size(1125, 610);
            this.tbUsers.TabIndex = 2;
            this.tbUsers.Text = "USERS";
            this.tbUsers.UseVisualStyleBackColor = true;
            // 
            // dgUsers
            // 
            this.dgUsers.AllowUserToAddRows = false;
            this.dgUsers.AllowUserToDeleteRows = false;
            this.dgUsers.AllowUserToResizeColumns = false;
            this.dgUsers.AllowUserToResizeRows = false;
            this.dgUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgUsers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgUsers.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgUsers.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgUsers.Location = new System.Drawing.Point(3, 3);
            this.dgUsers.MultiSelect = false;
            this.dgUsers.Name = "dgUsers";
            this.dgUsers.ReadOnly = true;
            this.dgUsers.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgUsers.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgUsers.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgUsers.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUsers.Size = new System.Drawing.Size(1116, 601);
            this.dgUsers.TabIndex = 4;
            // 
            // tbResp
            // 
            this.tbResp.Controls.Add(this.dgResp);
            this.tbResp.Location = new System.Drawing.Point(4, 22);
            this.tbResp.Name = "tbResp";
            this.tbResp.Padding = new System.Windows.Forms.Padding(3);
            this.tbResp.Size = new System.Drawing.Size(1125, 610);
            this.tbResp.TabIndex = 1;
            this.tbResp.Text = "RESPONDENT";
            this.tbResp.UseVisualStyleBackColor = true;
            // 
            // dgResp
            // 
            this.dgResp.AllowUserToAddRows = false;
            this.dgResp.AllowUserToDeleteRows = false;
            this.dgResp.AllowUserToResizeColumns = false;
            this.dgResp.AllowUserToResizeRows = false;
            this.dgResp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgResp.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgResp.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgResp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgResp.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgResp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgResp.Location = new System.Drawing.Point(3, 3);
            this.dgResp.MultiSelect = false;
            this.dgResp.Name = "dgResp";
            this.dgResp.ReadOnly = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgResp.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgResp.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgResp.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgResp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgResp.Size = new System.Drawing.Size(1121, 601);
            this.dgResp.TabIndex = 5;
            // 
            // tbPreSamp
            // 
            this.tbPreSamp.Controls.Add(this.dgSample);
            this.tbPreSamp.Location = new System.Drawing.Point(4, 22);
            this.tbPreSamp.Name = "tbPreSamp";
            this.tbPreSamp.Size = new System.Drawing.Size(1125, 610);
            this.tbPreSamp.TabIndex = 3;
            this.tbPreSamp.Text = "PRESAMPLE";
            this.tbPreSamp.UseVisualStyleBackColor = true;
            // 
            // dgSample
            // 
            this.dgSample.AllowUserToAddRows = false;
            this.dgSample.AllowUserToDeleteRows = false;
            this.dgSample.AllowUserToResizeColumns = false;
            this.dgSample.AllowUserToResizeRows = false;
            this.dgSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSample.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgSample.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgSample.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSample.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgSample.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgSample.Location = new System.Drawing.Point(2, 5);
            this.dgSample.MultiSelect = false;
            this.dgSample.Name = "dgSample";
            this.dgSample.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgSample.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgSample.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgSample.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgSample.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSample.Size = new System.Drawing.Size(1121, 601);
            this.dgSample.TabIndex = 6;
            // 
            // tbSpecialcase
            // 
            this.tbSpecialcase.Controls.Add(this.dgSpecial);
            this.tbSpecialcase.Location = new System.Drawing.Point(4, 22);
            this.tbSpecialcase.Name = "tbSpecialcase";
            this.tbSpecialcase.Padding = new System.Windows.Forms.Padding(3);
            this.tbSpecialcase.Size = new System.Drawing.Size(1125, 610);
            this.tbSpecialcase.TabIndex = 4;
            this.tbSpecialcase.Text = "SPECIAL CASE";
            this.tbSpecialcase.UseVisualStyleBackColor = true;
            // 
            // dgSpecial
            // 
            this.dgSpecial.AllowUserToAddRows = false;
            this.dgSpecial.AllowUserToDeleteRows = false;
            this.dgSpecial.AllowUserToResizeColumns = false;
            this.dgSpecial.AllowUserToResizeRows = false;
            this.dgSpecial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSpecial.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgSpecial.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgSpecial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSpecial.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgSpecial.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgSpecial.Location = new System.Drawing.Point(2, 5);
            this.dgSpecial.MultiSelect = false;
            this.dgSpecial.Name = "dgSpecial";
            this.dgSpecial.ReadOnly = true;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgSpecial.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgSpecial.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgSpecial.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgSpecial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSpecial.Size = new System.Drawing.Size(1121, 601);
            this.dgSpecial.TabIndex = 7;
            // 
            // tbDup
            // 
            this.tbDup.Controls.Add(this.dgDup);
            this.tbDup.Location = new System.Drawing.Point(4, 22);
            this.tbDup.Name = "tbDup";
            this.tbDup.Padding = new System.Windows.Forms.Padding(3);
            this.tbDup.Size = new System.Drawing.Size(1125, 610);
            this.tbDup.TabIndex = 5;
            this.tbDup.Text = "DUP RESEARCH";
            this.tbDup.UseVisualStyleBackColor = true;
            // 
            // dgDup
            // 
            this.dgDup.AllowUserToAddRows = false;
            this.dgDup.AllowUserToDeleteRows = false;
            this.dgDup.AllowUserToResizeColumns = false;
            this.dgDup.AllowUserToResizeRows = false;
            this.dgDup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDup.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgDup.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgDup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDup.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgDup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgDup.Location = new System.Drawing.Point(2, 5);
            this.dgDup.MultiSelect = false;
            this.dgDup.Name = "dgDup";
            this.dgDup.ReadOnly = true;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgDup.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgDup.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgDup.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgDup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDup.Size = new System.Drawing.Size(1121, 601);
            this.dgDup.TabIndex = 8;
            // 
            // frmUnlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 853);
            this.Controls.Add(this.tbResearch);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.label2);
            this.Name = "frmUnlock";
            this.Load += new System.EventHandler(this.frmUnlock_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnUnlock, 0);
            this.Controls.SetChildIndex(this.tbResearch, 0);
            this.tbResearch.ResumeLayout(false);
            this.tbUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).EndInit();
            this.tbResp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResp)).EndInit();
            this.tbPreSamp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSample)).EndInit();
            this.tbSpecialcase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSpecial)).EndInit();
            this.tbDup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.TabControl tbResearch;
        private System.Windows.Forms.TabPage tbUsers;
        private System.Windows.Forms.DataGridView dgUsers;
        private System.Windows.Forms.TabPage tbResp;
        private System.Windows.Forms.DataGridView dgResp;
        private System.Windows.Forms.TabPage tbPreSamp;
        private System.Windows.Forms.DataGridView dgSample;
        private System.Windows.Forms.TabPage tbSpecialcase;
        private System.Windows.Forms.DataGridView dgSpecial;
        private System.Windows.Forms.TabPage tbDup;
        private System.Windows.Forms.DataGridView dgDup;
    }
}