namespace Cprs
{
    partial class frmTCDescriptionReview
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgNewTCDesc = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.cbNewtc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateDescription = new System.Windows.Forms.Button();
            this.btnAddDescription = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgNewTCDesc)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgNewTCDesc
            // 
            this.dgNewTCDesc.AllowUserToAddRows = false;
            this.dgNewTCDesc.AllowUserToDeleteRows = false;
            this.dgNewTCDesc.AllowUserToResizeColumns = false;
            this.dgNewTCDesc.AllowUserToResizeRows = false;
            this.dgNewTCDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgNewTCDesc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgNewTCDesc.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgNewTCDesc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgNewTCDesc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgNewTCDesc.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgNewTCDesc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgNewTCDesc.Location = new System.Drawing.Point(217, 181);
            this.dgNewTCDesc.MultiSelect = false;
            this.dgNewTCDesc.Name = "dgNewTCDesc";
            this.dgNewTCDesc.ReadOnly = true;
            this.dgNewTCDesc.RowHeadersVisible = false;
            this.dgNewTCDesc.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgNewTCDesc.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgNewTCDesc.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgNewTCDesc.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgNewTCDesc.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgNewTCDesc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNewTCDesc.Size = new System.Drawing.Size(775, 521);
            this.dgNewTCDesc.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(509, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "NEWTC DESCRIPTION";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.cbNewtc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(355, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 34);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(392, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.Location = new System.Drawing.Point(287, 8);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchItem.TabIndex = 5;
            this.btnSearchItem.Text = "Search";
            this.btnSearchItem.UseVisualStyleBackColor = true;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbNewtc
            // 
            this.cbNewtc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewtc.FormattingEnabled = true;
            this.cbNewtc.Location = new System.Drawing.Point(124, 10);
            this.cbNewtc.Name = "cbNewtc";
            this.cbNewtc.Size = new System.Drawing.Size(108, 21);
            this.cbNewtc.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search by Newtc:";
            // 
            // btnUpdateDescription
            // 
            this.btnUpdateDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDescription.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnUpdateDescription.Location = new System.Drawing.Point(529, 814);
            this.btnUpdateDescription.Name = "btnUpdateDescription";
            this.btnUpdateDescription.Size = new System.Drawing.Size(156, 23);
            this.btnUpdateDescription.TabIndex = 44;
            this.btnUpdateDescription.TabStop = false;
            this.btnUpdateDescription.Text = "UPDATE DESCRIPTION";
            this.btnUpdateDescription.UseVisualStyleBackColor = true;
            this.btnUpdateDescription.EnabledChanged += new System.EventHandler(this.btnUpdateDescription_EnabledChanged);
            this.btnUpdateDescription.Click += new System.EventHandler(this.btnUpdateDescription_Click);
            // 
            // btnAddDescription
            // 
            this.btnAddDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDescription.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAddDescription.Location = new System.Drawing.Point(309, 814);
            this.btnAddDescription.Name = "btnAddDescription";
            this.btnAddDescription.Size = new System.Drawing.Size(156, 23);
            this.btnAddDescription.TabIndex = 43;
            this.btnAddDescription.TabStop = false;
            this.btnAddDescription.Text = "ADD DESCRIPTION";
            this.btnAddDescription.UseVisualStyleBackColor = true;
            this.btnAddDescription.EnabledChanged += new System.EventHandler(this.btnAddDescription_EnabledChanged);
            this.btnAddDescription.Click += new System.EventHandler(this.btnAddDescription_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDelete.Location = new System.Drawing.Point(747, 814);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(156, 23);
            this.btnDelete.TabIndex = 45;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "DELETE DESCRIPTION";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.EnabledChanged += new System.EventHandler(this.btnDelete_EnabledChanged);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmTCDescriptionReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 861);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdateDescription);
            this.Controls.Add(this.btnAddDescription);
            this.Controls.Add(this.dgNewTCDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTCDescriptionReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTCDescriptionReview_FormClosing);
            this.Load += new System.EventHandler(this.frmTCReview_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dgNewTCDesc, 0);
            this.Controls.SetChildIndex(this.btnAddDescription, 0);
            this.Controls.SetChildIndex(this.btnUpdateDescription, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgNewTCDesc)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgNewTCDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbNewtc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateDescription;
        private System.Windows.Forms.Button btnAddDescription;
        private System.Windows.Forms.Button btnDelete;
    }
}