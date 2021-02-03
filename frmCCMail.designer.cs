namespace Cprs
{
    partial class frmCCMail
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.cbJobFlag = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgCCMail = new System.Windows.Forms.DataGridView();
            this.btnAddCCMail = new System.Windows.Forms.Button();
            this.btnDeleteCCMail = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCCMail)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(509, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "EMAIL LIST REVIEW";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.cbJobFlag);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(355, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 34);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(418, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.Location = new System.Drawing.Point(322, 8);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchItem.TabIndex = 5;
            this.btnSearchItem.Text = "Search";
            this.btnSearchItem.UseVisualStyleBackColor = true;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbJobFlag
            // 
            this.cbJobFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJobFlag.FormattingEnabled = true;
            this.cbJobFlag.Location = new System.Drawing.Point(124, 10);
            this.cbJobFlag.Name = "cbJobFlag";
            this.cbJobFlag.Size = new System.Drawing.Size(174, 21);
            this.cbJobFlag.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search by Jobflag:";
            // 
            // dgCCMail
            // 
            this.dgCCMail.AllowUserToAddRows = false;
            this.dgCCMail.AllowUserToDeleteRows = false;
            this.dgCCMail.AllowUserToResizeColumns = false;
            this.dgCCMail.AllowUserToResizeRows = false;
            this.dgCCMail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCCMail.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgCCMail.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCCMail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCCMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCCMail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgCCMail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgCCMail.Location = new System.Drawing.Point(217, 166);
            this.dgCCMail.MultiSelect = false;
            this.dgCCMail.Name = "dgCCMail";
            this.dgCCMail.ReadOnly = true;
            this.dgCCMail.RowHeadersVisible = false;
            this.dgCCMail.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgCCMail.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgCCMail.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgCCMail.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgCCMail.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgCCMail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCCMail.Size = new System.Drawing.Size(783, 513);
            this.dgCCMail.TabIndex = 11;
            this.dgCCMail.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // btnAddCCMail
            // 
            this.btnAddCCMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCCMail.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAddCCMail.Location = new System.Drawing.Point(439, 798);
            this.btnAddCCMail.Name = "btnAddCCMail";
            this.btnAddCCMail.Size = new System.Drawing.Size(140, 23);
            this.btnAddCCMail.TabIndex = 41;
            this.btnAddCCMail.TabStop = false;
            this.btnAddCCMail.Text = "ADD EMAIL";
            this.btnAddCCMail.UseVisualStyleBackColor = true;
            this.btnAddCCMail.Click += new System.EventHandler(this.btnAddCCMail_Click);
            // 
            // btnDeleteCCMail
            // 
            this.btnDeleteCCMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCCMail.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDeleteCCMail.Location = new System.Drawing.Point(644, 798);
            this.btnDeleteCCMail.Name = "btnDeleteCCMail";
            this.btnDeleteCCMail.Size = new System.Drawing.Size(133, 23);
            this.btnDeleteCCMail.TabIndex = 42;
            this.btnDeleteCCMail.TabStop = false;
            this.btnDeleteCCMail.Text = "DELETE  EMAIL";
            this.btnDeleteCCMail.UseVisualStyleBackColor = true;
            this.btnDeleteCCMail.Click += new System.EventHandler(this.btnDeleteCCMail_Click);
            // 
            // frmCCMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 853);
            this.Controls.Add(this.btnDeleteCCMail);
            this.Controls.Add(this.btnAddCCMail);
            this.Controls.Add(this.dgCCMail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCCMail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCCMail_FormClosing);
            this.Load += new System.EventHandler(this.frmCCMail_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dgCCMail, 0);
            this.Controls.SetChildIndex(this.btnAddCCMail, 0);
            this.Controls.SetChildIndex(this.btnDeleteCCMail, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCCMail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbJobFlag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgCCMail;
        private System.Windows.Forms.Button btnAddCCMail;
        private System.Windows.Forms.Button btnDeleteCCMail;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}