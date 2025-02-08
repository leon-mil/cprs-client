namespace Cprs
{
    partial class frmNPCAttemptCases
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnC700 = new System.Windows.Forms.Button();
            this.dgCases = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInterviewer = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgCases)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(543, 788);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(137, 23);
            this.btnPrint.TabIndex = 24;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnC700
            // 
            this.btnC700.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC700.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnC700.Location = new System.Drawing.Point(346, 788);
            this.btnC700.Name = "btnC700";
            this.btnC700.Size = new System.Drawing.Size(137, 23);
            this.btnC700.TabIndex = 25;
            this.btnC700.UseVisualStyleBackColor = true;
            this.btnC700.Click += new System.EventHandler(this.btnC700_Click);
            // 
            // dgCases
            // 
            this.dgCases.AllowUserToAddRows = false;
            this.dgCases.AllowUserToDeleteRows = false;
            this.dgCases.AllowUserToResizeColumns = false;
            this.dgCases.AllowUserToResizeRows = false;
            this.dgCases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgCases.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgCases.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCases.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgCases.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgCases.Location = new System.Drawing.Point(217, 175);
            this.dgCases.MultiSelect = false;
            this.dgCases.Name = "dgCases";
            this.dgCases.ReadOnly = true;
            this.dgCases.RowHeadersVisible = false;
            this.dgCases.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgCases.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgCases.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgCases.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgCases.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgCases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCases.Size = new System.Drawing.Size(775, 520);
            this.dgCases.TabIndex = 26;
            this.dgCases.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_CellFormatting);
            this.dgCases.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dg_CellParsing);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "accesday";
            this.Column3.HeaderText = "DATE";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "accestms";
            this.Column4.HeaderText = "TIME START";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "accestme";
            this.Column5.HeaderText = "TIME END";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(486, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 25);
            this.label2.TabIndex = 27;
            this.label2.Text = "NPC ATTEMPTS CASES\r\n";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblRecordCount.Location = new System.Drawing.Point(214, 105);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(95, 21);
            this.lblRecordCount.TabIndex = 28;
            this.lblRecordCount.Text = "Interviewer: ";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(562, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 21);
            this.label1.TabIndex = 29;
            this.label1.Text = "Date: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrevious.Location = new System.Drawing.Point(733, 788);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(137, 23);
            this.btnPrevious.TabIndex = 30;
            this.btnPrevious.Text = "PREVIOUS";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(852, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 21);
            this.label3.TabIndex = 31;
            this.label3.Text = "Type:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtInterviewer
            // 
            this.txtInterviewer.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtInterviewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInterviewer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInterviewer.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtInterviewer.Location = new System.Drawing.Point(315, 111);
            this.txtInterviewer.Name = "txtInterviewer";
            this.txtInterviewer.Size = new System.Drawing.Size(81, 15);
            this.txtInterviewer.TabIndex = 32;
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtType.Location = new System.Drawing.Point(912, 111);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(88, 15);
            this.txtType.TabIndex = 33;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtDate.Location = new System.Drawing.Point(619, 111);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(61, 15);
            this.txtDate.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(214, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.TabIndex = 35;
            this.label4.Text = "TOTAL CASES:  ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtTotal.Location = new System.Drawing.Point(335, 143);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(57, 15);
            this.txtTotal.TabIndex = 36;
            // 
            // frmNPCAttemptCases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 853);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtInterviewer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgCases);
            this.Controls.Add(this.btnC700);
            this.Controls.Add(this.btnPrint);
            this.Name = "frmNPCAttemptCases";
            this.Load += new System.EventHandler(this.frmNPCAttemptCases_Load);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnC700, 0);
            this.Controls.SetChildIndex(this.dgCases, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblRecordCount, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtInterviewer, 0);
            this.Controls.SetChildIndex(this.txtType, 0);
            this.Controls.SetChildIndex(this.txtDate, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgCases)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnC700;
        private System.Windows.Forms.DataGridView dgCases;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInterviewer;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}