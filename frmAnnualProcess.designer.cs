namespace Cprs
{
    partial class frmAnnualProcess
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
            this.label2 = new System.Windows.Forms.Label();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.btnP1 = new System.Windows.Forms.Button();
            this.btnP2 = new System.Windows.Forms.Button();
            this.btnP3 = new System.Windows.Forms.Button();
            this.btnP4 = new System.Windows.Forms.Button();
            this.btnP5 = new System.Windows.Forms.Button();
            this.btnP6 = new System.Windows.Forms.Button();
            this.btnP8 = new System.Windows.Forms.Button();
            this.btnP9 = new System.Windows.Forms.Button();
            this.btnP11 = new System.Windows.Forms.Button();
            this.btnP12 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnP7 = new System.Windows.Forms.Button();
            this.btnP10 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(456, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 25);
            this.label2.TabIndex = 25;
            this.label2.Text = "ANNUAL PROCESSING";
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.Location = new System.Drawing.Point(28, 105);
            this.dgData.Name = "dgData";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgData.RowHeadersVisible = false;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1134, 579);
            this.dgData.TabIndex = 26;
            this.dgData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgData_CellFormatting);
            // 
            // btnP1
            // 
            this.btnP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP1.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP1.Location = new System.Drawing.Point(103, 722);
            this.btnP1.Name = "btnP1";
            this.btnP1.Size = new System.Drawing.Size(145, 23);
            this.btnP1.TabIndex = 28;
            this.btnP1.Text = "ANNUAL VARIANCE";
            this.btnP1.UseVisualStyleBackColor = true;
            this.btnP1.EnabledChanged += new System.EventHandler(this.btnP1_EnabledChanged);
            this.btnP1.Click += new System.EventHandler(this.btnP1_Click);
            // 
            // btnP2
            // 
            this.btnP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP2.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP2.Location = new System.Drawing.Point(267, 722);
            this.btnP2.Name = "btnP2";
            this.btnP2.Size = new System.Drawing.Size(137, 23);
            this.btnP2.TabIndex = 29;
            this.btnP2.Text = "ANNUAL LSF";
            this.btnP2.UseVisualStyleBackColor = true;
            this.btnP2.EnabledChanged += new System.EventHandler(this.btnP2_EnabledChanged);
            this.btnP2.Click += new System.EventHandler(this.btnP2_Click);
            // 
            // btnP3
            // 
            this.btnP3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP3.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP3.Location = new System.Drawing.Point(433, 722);
            this.btnP3.Name = "btnP3";
            this.btnP3.Size = new System.Drawing.Size(137, 23);
            this.btnP3.TabIndex = 30;
            this.btnP3.Text = "ANNUAL FACTORS";
            this.btnP3.UseVisualStyleBackColor = true;
            this.btnP3.EnabledChanged += new System.EventHandler(this.btnP3_EnabledChanged);
            this.btnP3.Click += new System.EventHandler(this.btnP3_Click);
            // 
            // btnP4
            // 
            this.btnP4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP4.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP4.Location = new System.Drawing.Point(600, 722);
            this.btnP4.Name = "btnP4";
            this.btnP4.Size = new System.Drawing.Size(137, 23);
            this.btnP4.TabIndex = 31;
            this.btnP4.Text = "VIP RATIOS";
            this.btnP4.UseVisualStyleBackColor = true;
            this.btnP4.EnabledChanged += new System.EventHandler(this.btnP4_EnabledChanged);
            this.btnP4.Click += new System.EventHandler(this.btnP4_Click);
            // 
            // btnP5
            // 
            this.btnP5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP5.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP5.Location = new System.Drawing.Point(767, 722);
            this.btnP5.Name = "btnP5";
            this.btnP5.Size = new System.Drawing.Size(137, 23);
            this.btnP5.TabIndex = 32;
            this.btnP5.Text = "BEA INDEX";
            this.btnP5.UseVisualStyleBackColor = true;
            this.btnP5.EnabledChanged += new System.EventHandler(this.btnP5_EnabledChanged);
            this.btnP5.Click += new System.EventHandler(this.btnP5_Click);
            // 
            // btnP6
            // 
            this.btnP6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP6.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP6.Location = new System.Drawing.Point(929, 722);
            this.btnP6.Name = "btnP6";
            this.btnP6.Size = new System.Drawing.Size(145, 23);
            this.btnP6.TabIndex = 33;
            this.btnP6.Text = "VIPHIST UPDATE";
            this.btnP6.UseVisualStyleBackColor = true;
            this.btnP6.EnabledChanged += new System.EventHandler(this.btnP6_EnabledChanged);
            this.btnP6.Click += new System.EventHandler(this.btnP6_Click);
            // 
            // btnP8
            // 
            this.btnP8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP8.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP8.Location = new System.Drawing.Point(267, 766);
            this.btnP8.Name = "btnP8";
            this.btnP8.Size = new System.Drawing.Size(137, 23);
            this.btnP8.TabIndex = 34;
            this.btnP8.Text = "FEDERAL BOOST";
            this.btnP8.UseVisualStyleBackColor = true;
            this.btnP8.EnabledChanged += new System.EventHandler(this.btnP8_EnabledChanged);
            this.btnP8.Click += new System.EventHandler(this.btnP8_Click);
            // 
            // btnP9
            // 
            this.btnP9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP9.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP9.Location = new System.Drawing.Point(433, 766);
            this.btnP9.Name = "btnP9";
            this.btnP9.Size = new System.Drawing.Size(137, 23);
            this.btnP9.TabIndex = 35;
            this.btnP9.Text = "1 UNIT LOAD";
            this.btnP9.UseVisualStyleBackColor = true;
            this.btnP9.EnabledChanged += new System.EventHandler(this.btnP9_EnabledChanged);
            this.btnP9.Click += new System.EventHandler(this.btnP9_Click);
            // 
            // btnP11
            // 
            this.btnP11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP11.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP11.Location = new System.Drawing.Point(767, 766);
            this.btnP11.Name = "btnP11";
            this.btnP11.Size = new System.Drawing.Size(137, 23);
            this.btnP11.TabIndex = 36;
            this.btnP11.Text = "TSAR PROCESSING";
            this.btnP11.UseVisualStyleBackColor = true;
            this.btnP11.EnabledChanged += new System.EventHandler(this.btnP11_EnabledChanged);
            this.btnP11.Click += new System.EventHandler(this.btnP11_Click);
            // 
            // btnP12
            // 
            this.btnP12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP12.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP12.Location = new System.Drawing.Point(929, 766);
            this.btnP12.Name = "btnP12";
            this.btnP12.Size = new System.Drawing.Size(137, 23);
            this.btnP12.TabIndex = 37;
            this.btnP12.Text = "SEASONAL UPDATE";
            this.btnP12.UseVisualStyleBackColor = true;
            this.btnP12.EnabledChanged += new System.EventHandler(this.btnP12_EnabledChanged);
            this.btnP12.Click += new System.EventHandler(this.btnP12_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRefresh.Location = new System.Drawing.Point(531, 808);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(137, 23);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnP7
            // 
            this.btnP7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP7.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP7.Location = new System.Drawing.Point(103, 766);
            this.btnP7.Name = "btnP7";
            this.btnP7.Size = new System.Drawing.Size(137, 23);
            this.btnP7.TabIndex = 43;
            this.btnP7.Text = "FEDERAL LOAD";
            this.btnP7.UseVisualStyleBackColor = true;
            this.btnP7.EnabledChanged += new System.EventHandler(this.btnP7_EnabledChanged);
            this.btnP7.Click += new System.EventHandler(this.btnP7_Click);
            // 
            // btnP10
            // 
            this.btnP10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP10.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnP10.Location = new System.Drawing.Point(600, 766);
            this.btnP10.Name = "btnP10";
            this.btnP10.Size = new System.Drawing.Size(137, 23);
            this.btnP10.TabIndex = 44;
            this.btnP10.Text = "XSERIES LOAD";
            this.btnP10.UseVisualStyleBackColor = true;
            this.btnP10.EnabledChanged += new System.EventHandler(this.btnP10_EnabledChanged);
            this.btnP10.Click += new System.EventHandler(this.btnP10_Click);
            // 
            // frmAnnualProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 861);
            this.Controls.Add(this.btnP10);
            this.Controls.Add(this.btnP7);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnP12);
            this.Controls.Add(this.btnP11);
            this.Controls.Add(this.btnP9);
            this.Controls.Add(this.btnP8);
            this.Controls.Add(this.btnP6);
            this.Controls.Add(this.btnP5);
            this.Controls.Add(this.btnP4);
            this.Controls.Add(this.btnP3);
            this.Controls.Add(this.btnP2);
            this.Controls.Add(this.btnP1);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.label2);
            this.Name = "frmAnnualProcess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAnnualProcess_FormClosing);
            this.Load += new System.EventHandler(this.frmAnnualProcess_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.btnP1, 0);
            this.Controls.SetChildIndex(this.btnP2, 0);
            this.Controls.SetChildIndex(this.btnP3, 0);
            this.Controls.SetChildIndex(this.btnP4, 0);
            this.Controls.SetChildIndex(this.btnP5, 0);
            this.Controls.SetChildIndex(this.btnP6, 0);
            this.Controls.SetChildIndex(this.btnP8, 0);
            this.Controls.SetChildIndex(this.btnP9, 0);
            this.Controls.SetChildIndex(this.btnP11, 0);
            this.Controls.SetChildIndex(this.btnP12, 0);
            this.Controls.SetChildIndex(this.btnRefresh, 0);
            this.Controls.SetChildIndex(this.btnP7, 0);
            this.Controls.SetChildIndex(this.btnP10, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnP1;
        private System.Windows.Forms.Button btnP2;
        private System.Windows.Forms.Button btnP3;
        private System.Windows.Forms.Button btnP4;
        private System.Windows.Forms.Button btnP5;
        private System.Windows.Forms.Button btnP6;
        private System.Windows.Forms.Button btnP8;
        private System.Windows.Forms.Button btnP9;
        private System.Windows.Forms.Button btnP11;
        private System.Windows.Forms.Button btnP12;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnP7;
        private System.Windows.Forms.Button btnP10;
    }
}