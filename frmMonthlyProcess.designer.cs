namespace Cprs
{
    partial class frmMonthlyProcess
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.dgMonProc = new System.Windows.Forms.DataGridView();
            this.btnMonthly = new System.Windows.Forms.Button();
            this.btnDaily = new System.Windows.Forms.Button();
            this.btnCenturion = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgMonProc)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(487, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "MONTHLY PROCESSING";
            // 
            // dgMonProc
            // 
            this.dgMonProc.AllowUserToAddRows = false;
            this.dgMonProc.AllowUserToDeleteRows = false;
            this.dgMonProc.AllowUserToResizeRows = false;
            this.dgMonProc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgMonProc.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMonProc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgMonProc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMonProc.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgMonProc.Location = new System.Drawing.Point(25, 98);
            this.dgMonProc.Name = "dgMonProc";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMonProc.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgMonProc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMonProc.Size = new System.Drawing.Size(1169, 667);
            this.dgMonProc.TabIndex = 25;
            this.dgMonProc.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgMonProc_CellFormatting);
            // 
            // btnMonthly
            // 
            this.btnMonthly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonthly.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnMonthly.Location = new System.Drawing.Point(812, 793);
            this.btnMonthly.Name = "btnMonthly";
            this.btnMonthly.Size = new System.Drawing.Size(209, 23);
            this.btnMonthly.TabIndex = 26;
            this.btnMonthly.Text = "CURRENT MONTH PROCESSING";
            this.btnMonthly.UseVisualStyleBackColor = true;
            this.btnMonthly.EnabledChanged += new System.EventHandler(this.btnMonthly_EnabledChanged);
            this.btnMonthly.Click += new System.EventHandler(this.btnMonthly_Click);
            // 
            // btnDaily
            // 
            this.btnDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDaily.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnDaily.Location = new System.Drawing.Point(249, 793);
            this.btnDaily.Name = "btnDaily";
            this.btnDaily.Size = new System.Drawing.Size(209, 23);
            this.btnDaily.TabIndex = 27;
            this.btnDaily.Text = "DAILY PROCESSING";
            this.btnDaily.UseVisualStyleBackColor = true;
            this.btnDaily.EnabledChanged += new System.EventHandler(this.btnDaily_EnabledChanged);
            this.btnDaily.Click += new System.EventHandler(this.btnDaily_Click);
            // 
            // btnCenturion
            // 
            this.btnCenturion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCenturion.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnCenturion.Location = new System.Drawing.Point(526, 793);
            this.btnCenturion.Name = "btnCenturion";
            this.btnCenturion.Size = new System.Drawing.Size(209, 23);
            this.btnCenturion.TabIndex = 28;
            this.btnCenturion.Text = "CENTURION LOAD";
            this.btnCenturion.UseVisualStyleBackColor = true;
            this.btnCenturion.EnabledChanged += new System.EventHandler(this.btnCenturion_EnabledChanged);
            this.btnCenturion.Click += new System.EventHandler(this.btnCenturion_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmMonthlyProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 853);
            this.Controls.Add(this.btnCenturion);
            this.Controls.Add(this.btnDaily);
            this.Controls.Add(this.btnMonthly);
            this.Controls.Add(this.dgMonProc);
            this.Controls.Add(this.label2);
            this.Name = "frmMonthlyProcess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMonthlyProcess_FormClosing);
            this.Load += new System.EventHandler(this.frmMonthlyProcess_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dgMonProc, 0);
            this.Controls.SetChildIndex(this.btnMonthly, 0);
            this.Controls.SetChildIndex(this.btnDaily, 0);
            this.Controls.SetChildIndex(this.btnCenturion, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgMonProc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgMonProc;
        private System.Windows.Forms.Button btnMonthly;
        private System.Windows.Forms.Button btnDaily;
        private System.Windows.Forms.Button btnCenturion;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}