namespace Cprs
{
    partial class frmBenExport
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbEnd = new System.Windows.Forms.ComboBox();
            this.cbStart = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSeries = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cbEnd, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbStart, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(70, 57);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(248, 158);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // cbEnd
            // 
            this.cbEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnd.FormattingEnabled = true;
            this.cbEnd.Items.AddRange(new object[] {
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018"});
            this.cbEnd.Location = new System.Drawing.Point(127, 81);
            this.cbEnd.Name = "cbEnd";
            this.cbEnd.Size = new System.Drawing.Size(118, 21);
            this.cbEnd.TabIndex = 63;
            this.cbEnd.SelectedIndexChanged += new System.EventHandler(this.cbEnd_SelectedIndexChanged);
            // 
            // cbStart
            // 
            this.cbStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStart.FormattingEnabled = true;
            this.cbStart.Location = new System.Drawing.Point(127, 42);
            this.cbStart.Name = "cbStart";
            this.cbStart.Size = new System.Drawing.Size(118, 21);
            this.cbStart.TabIndex = 62;
            this.cbStart.SelectedIndexChanged += new System.EventHandler(this.cbStart_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "Start Year:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 61;
            this.label4.Text = "End Year:";
            // 
            // cbSeries
            // 
            this.cbSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSeries.FormattingEnabled = true;
            this.cbSeries.Location = new System.Drawing.Point(567, 120);
            this.cbSeries.Name = "cbSeries";
            this.cbSeries.Size = new System.Drawing.Size(118, 21);
            this.cbSeries.TabIndex = 59;
            this.cbSeries.TabStop = false;
            this.cbSeries.SelectedValueChanged += new System.EventHandler(this.cbSeries_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(486, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 58;
            this.label1.Text = "Series:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(433, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 25);
            this.label2.TabIndex = 56;
            this.label2.Text = "EXPORT BENCHMARK DATA";
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnExport.Location = new System.Drawing.Point(519, 786);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 23);
            this.btnExport.TabIndex = 59;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(377, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(443, 25);
            this.label5.TabIndex = 60;
            this.label5.Text = "Select Year Range for Benchmark Series";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(400, 354);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 267);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Year Range";
            // 
            // frmBenExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 850);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSeries);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "frmBenExport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBenExport_FormClosing);
            this.Load += new System.EventHandler(this.frmBenExport_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbSeries, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEnd;
        private System.Windows.Forms.ComboBox cbStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSeries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}