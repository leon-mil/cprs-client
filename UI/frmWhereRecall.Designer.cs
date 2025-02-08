namespace Cprs
{
    partial class frmWhereRecall
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WHERE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnReturn.Location = new System.Drawing.Point(542, 785);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(133, 23);
            this.btnReturn.TabIndex = 21;
            this.btnReturn.TabStop = false;
            this.btnReturn.Text = "PREVIOUS";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
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
            this.label1.Size = new System.Drawing.Size(190, 25);
            this.label1.TabIndex = 23;
            this.label1.Text = "WHERE RECALL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.WHERE});
            this.dgData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgData.EnableHeadersVisualStyles = false;
            this.dgData.Location = new System.Drawing.Point(113, 141);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowHeadersVisible = false;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1013, 325);
            this.dgData.TabIndex = 24;
            // 
            // Row
            // 
            this.Row.FillWeight = 10F;
            this.Row.HeaderText = "ROW";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.Width = 50;
            // 
            // WHERE
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WHERE.DefaultCellStyle = dataGridViewCellStyle1;
            this.WHERE.HeaderText = "WHERE";
            this.WHERE.Name = "WHERE";
            this.WHERE.ReadOnly = true;
            this.WHERE.Width = 1000;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(506, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "STORED WHERE CLAUSES\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDelete.Location = new System.Drawing.Point(445, 499);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(133, 23);
            this.btnDelete.TabIndex = 28;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSelect.Location = new System.Drawing.Point(638, 499);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(133, 23);
            this.btnSelect.TabIndex = 29;
            this.btnSelect.TabStop = false;
            this.btnSelect.Text = "SELECT";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmWhereRecall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReturn);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1224, 900);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1224, 900);
            this.Name = "frmWhereRecall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Where Recall";
            this.Load += new System.EventHandler(this.frmWhereRecall_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn WHERE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelect;
    }
}