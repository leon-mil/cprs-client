namespace Cprs
{
    partial class frmDistribution
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.cbDiv = new System.Windows.Forms.ComboBox();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDate = new System.Windows.Forms.ComboBox();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblt31 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(883, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 54;
            this.label5.Text = "State";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(515, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 53;
            this.label4.Text = "Division";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(219, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 52;
            this.label3.Text = "Region";
            // 
            // cbState
            // 
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(933, 166);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(58, 21);
            this.cbState.TabIndex = 51;
            this.cbState.SelectedIndexChanged += new System.EventHandler(this.cbState_SelectedIndexChanged);
            // 
            // cbDiv
            // 
            this.cbDiv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiv.FormattingEnabled = true;
            this.cbDiv.Items.AddRange(new object[] {
            "0 - US Total",
            "1 - New England",
            "2 - Middle Atlantic",
            "3 - East North Central",
            "4 - West North Central",
            "5 - South Atlantic",
            "6 - East South Central",
            "7 - West South Central",
            "8 - Mountain",
            "9 - Pacific"});
            this.cbDiv.Location = new System.Drawing.Point(585, 165);
            this.cbDiv.Name = "cbDiv";
            this.cbDiv.Size = new System.Drawing.Size(131, 21);
            this.cbDiv.TabIndex = 50;
            this.cbDiv.SelectedIndexChanged += new System.EventHandler(this.cbDiv_SelectedIndexChanged);
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Items.AddRange(new object[] {
            "0 - US Total",
            "1 - Northeast",
            "2 - Midwest",
            "3 - South",
            "4 - West"});
            this.cbRegion.Location = new System.Drawing.Point(283, 165);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(89, 21);
            this.cbRegion.TabIndex = 49;
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.cbRegion_SelectedIndexChanged);
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle2.Location = new System.Drawing.Point(494, 84);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(236, 25);
            this.lblTitle2.TabIndex = 56;
            this.lblTitle2.Text = "Thousands of Dollars";
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle1.Location = new System.Drawing.Point(399, 48);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(458, 25);
            this.lblTitle1.TabIndex = 55;
            this.lblTitle1.Text = "Non Residential YYYYMM VIP Distribution";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(558, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 58;
            this.label1.Text = "Date";
            // 
            // cbDate
            // 
            this.cbDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDate.FormattingEnabled = true;
            this.cbDate.Items.AddRange(new object[] {
            "201705",
            "201704",
            "201703",
            "201702",
            "201701",
            "201612"});
            this.cbDate.Location = new System.Drawing.Point(605, 128);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(81, 21);
            this.cbDate.TabIndex = 57;
            this.cbDate.SelectedIndexChanged += new System.EventHandler(this.cbDate_SelectedIndexChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.Location = new System.Drawing.Point(67, 225);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgData.RowHeadersVisible = false;
            this.dgData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(1082, 529);
            this.dgData.TabIndex = 59;
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrev.Location = new System.Drawing.Point(430, 792);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(120, 23);
            this.btnPrev.TabIndex = 61;
            this.btnPrev.Text = "PREVIOUS";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(684, 792);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 23);
            this.btnPrint.TabIndex = 60;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblt31
            // 
            this.lblt31.BackColor = System.Drawing.SystemColors.Control;
            this.lblt31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblt31.Location = new System.Drawing.Point(343, 211);
            this.lblt31.Name = "lblt31";
            this.lblt31.Size = new System.Drawing.Size(195, 13);
            this.lblt31.TabIndex = 62;
            this.lblt31.Text = "Current Month VIP";
            this.lblt31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(544, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "%Distribution";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(749, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "# of Cases";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(950, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 13);
            this.label7.TabIndex = 65;
            this.label7.Text = "%Distribution";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 850);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblt31);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.lblTitle1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.cbDiv);
            this.Controls.Add(this.cbRegion);
            this.Name = "frmDistribution";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDistribution_FormClosing);
            this.Load += new System.EventHandler(this.frmDistribution_Load);
            this.Controls.SetChildIndex(this.cbRegion, 0);
            this.Controls.SetChildIndex(this.cbDiv, 0);
            this.Controls.SetChildIndex(this.cbState, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lblTitle1, 0);
            this.Controls.SetChildIndex(this.lblTitle2, 0);
            this.Controls.SetChildIndex(this.cbDate, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dgData, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnPrev, 0);
            this.Controls.SetChildIndex(this.lblt31, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.ComboBox cbDiv;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDate;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblt31;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}