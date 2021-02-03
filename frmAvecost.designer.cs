namespace Cprs
{
    partial class frmAvecost
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgPriorYr = new System.Windows.Forms.DataGridView();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.cbDiv = new System.Windows.Forms.ComboBox();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.lblPriorYr = new System.Windows.Forms.Label();
            this.lblCurrYr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCases = new System.Windows.Forms.Button();
            this.dgCurrentYr = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtPYTtunits = new System.Windows.Forms.TextBox();
            this.txtPYTtvalue = new System.Windows.Forms.TextBox();
            this.txtPYCpu = new System.Windows.Forms.TextBox();
            this.txtCYTtunits = new System.Windows.Forms.TextBox();
            this.txtCYTtvalue = new System.Windows.Forms.TextBox();
            this.txtCYCpu = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dgPriorYr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCurrentYr)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(422, 49);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 25);
            this.lblTitle.TabIndex = 11;
            // 
            // dgPriorYr
            // 
            this.dgPriorYr.AllowUserToAddRows = false;
            this.dgPriorYr.AllowUserToDeleteRows = false;
            this.dgPriorYr.AllowUserToResizeColumns = false;
            this.dgPriorYr.AllowUserToResizeRows = false;
            this.dgPriorYr.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPriorYr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPriorYr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPriorYr.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPriorYr.Location = new System.Drawing.Point(212, 138);
            this.dgPriorYr.MultiSelect = false;
            this.dgPriorYr.Name = "dgPriorYr";
            this.dgPriorYr.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPriorYr.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgPriorYr.RowHeadersVisible = false;
            this.dgPriorYr.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgPriorYr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPriorYr.Size = new System.Drawing.Size(793, 287);
            this.dgPriorYr.TabIndex = 12;
            this.dgPriorYr.SelectionChanged += new System.EventHandler(this.dgPriorYr_SelectionChanged);
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
            this.cbRegion.Location = new System.Drawing.Point(300, 98);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(89, 21);
            this.cbRegion.TabIndex = 40;
            this.cbRegion.SelectedValueChanged += new System.EventHandler(this.cbRegion_SelectedValueChanged);
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
            this.cbDiv.Location = new System.Drawing.Point(586, 98);
            this.cbDiv.Name = "cbDiv";
            this.cbDiv.Size = new System.Drawing.Size(131, 21);
            this.cbDiv.TabIndex = 41;
            this.cbDiv.SelectedValueChanged += new System.EventHandler(this.cbDiv_SelectedValueChanged);
            // 
            // cbState
            // 
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(916, 98);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(58, 21);
            this.cbState.TabIndex = 42;
            this.cbState.SelectedValueChanged += new System.EventHandler(this.cbState_SelectedValueChanged);
            // 
            // lblPriorYr
            // 
            this.lblPriorYr.AutoSize = true;
            this.lblPriorYr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriorYr.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPriorYr.Location = new System.Drawing.Point(79, 255);
            this.lblPriorYr.Name = "lblPriorYr";
            this.lblPriorYr.Size = new System.Drawing.Size(89, 20);
            this.lblPriorYr.TabIndex = 44;
            this.lblPriorYr.Text = "Prior Year";
            // 
            // lblCurrYr
            // 
            this.lblCurrYr.AutoSize = true;
            this.lblCurrYr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrYr.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCurrYr.Location = new System.Drawing.Point(68, 563);
            this.lblCurrYr.Name = "lblCurrYr";
            this.lblCurrYr.Size = new System.Drawing.Size(112, 20);
            this.lblCurrYr.TabIndex = 45;
            this.lblCurrYr.Text = "Current Year";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(236, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "Region";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(516, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 47;
            this.label4.Text = "Division";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(866, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 48;
            this.label5.Text = "State";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(217, 431);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "PY Total";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkBlue;
            this.label10.Location = new System.Drawing.Point(217, 751);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 20);
            this.label10.TabIndex = 53;
            this.label10.Text = "CY Total";
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrev.Location = new System.Drawing.Point(675, 791);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(120, 23);
            this.btnPrev.TabIndex = 58;
            this.btnPrev.Text = "PREVIOUS";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(885, 791);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 23);
            this.btnPrint.TabIndex = 57;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCases
            // 
            this.btnCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCases.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnCases.Location = new System.Drawing.Point(443, 791);
            this.btnCases.Name = "btnCases";
            this.btnCases.Size = new System.Drawing.Size(120, 23);
            this.btnCases.TabIndex = 59;
            this.btnCases.Text = "DISPLAY CASES";
            this.btnCases.UseVisualStyleBackColor = true;
            this.btnCases.Click += new System.EventHandler(this.btnCases_Click);
            // 
            // dgCurrentYr
            // 
            this.dgCurrentYr.AllowUserToAddRows = false;
            this.dgCurrentYr.AllowUserToDeleteRows = false;
            this.dgCurrentYr.AllowUserToResizeColumns = false;
            this.dgCurrentYr.AllowUserToResizeRows = false;
            this.dgCurrentYr.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCurrentYr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgCurrentYr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCurrentYr.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgCurrentYr.Location = new System.Drawing.Point(212, 458);
            this.dgCurrentYr.MultiSelect = false;
            this.dgCurrentYr.Name = "dgCurrentYr";
            this.dgCurrentYr.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCurrentYr.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgCurrentYr.RowHeadersVisible = false;
            this.dgCurrentYr.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgCurrentYr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCurrentYr.Size = new System.Drawing.Size(793, 287);
            this.dgCurrentYr.TabIndex = 60;
            this.dgCurrentYr.SelectionChanged += new System.EventHandler(this.dgCurrentYr_SelectionChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnRefresh.Location = new System.Drawing.Point(221, 791);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 23);
            this.btnRefresh.TabIndex = 61;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtPYTtunits
            // 
            this.txtPYTtunits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPYTtunits.Location = new System.Drawing.Point(370, 431);
            this.txtPYTtunits.Name = "txtPYTtunits";
            this.txtPYTtunits.Size = new System.Drawing.Size(133, 20);
            this.txtPYTtunits.TabIndex = 63;
            this.txtPYTtunits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPYTtvalue
            // 
            this.txtPYTtvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPYTtvalue.Location = new System.Drawing.Point(618, 431);
            this.txtPYTtvalue.Name = "txtPYTtvalue";
            this.txtPYTtvalue.Size = new System.Drawing.Size(133, 20);
            this.txtPYTtvalue.TabIndex = 64;
            this.txtPYTtvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPYCpu
            // 
            this.txtPYCpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPYCpu.Location = new System.Drawing.Point(867, 431);
            this.txtPYCpu.Name = "txtPYCpu";
            this.txtPYCpu.Size = new System.Drawing.Size(133, 20);
            this.txtPYCpu.TabIndex = 65;
            this.txtPYCpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCYTtunits
            // 
            this.txtCYTtunits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCYTtunits.Location = new System.Drawing.Point(370, 753);
            this.txtCYTtunits.Name = "txtCYTtunits";
            this.txtCYTtunits.Size = new System.Drawing.Size(133, 20);
            this.txtCYTtunits.TabIndex = 66;
            this.txtCYTtunits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCYTtvalue
            // 
            this.txtCYTtvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCYTtvalue.Location = new System.Drawing.Point(618, 751);
            this.txtCYTtvalue.Name = "txtCYTtvalue";
            this.txtCYTtvalue.Size = new System.Drawing.Size(133, 20);
            this.txtCYTtvalue.TabIndex = 67;
            this.txtCYTtvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCYCpu
            // 
            this.txtCYCpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCYCpu.Location = new System.Drawing.Point(867, 751);
            this.txtCYCpu.Name = "txtCYCpu";
            this.txtCYCpu.Size = new System.Drawing.Size(133, 20);
            this.txtCYCpu.TabIndex = 68;
            this.txtCYCpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmAvecost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 850);
            this.Controls.Add(this.txtCYCpu);
            this.Controls.Add(this.txtCYTtvalue);
            this.Controls.Add(this.txtCYTtunits);
            this.Controls.Add(this.txtPYCpu);
            this.Controls.Add(this.txtPYTtvalue);
            this.Controls.Add(this.txtPYTtunits);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgCurrentYr);
            this.Controls.Add(this.btnCases);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCurrYr);
            this.Controls.Add(this.lblPriorYr);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.cbDiv);
            this.Controls.Add(this.cbRegion);
            this.Controls.Add(this.dgPriorYr);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmAvecost";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAvecost_FormClosing);
            this.Load += new System.EventHandler(this.frmAvecost_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.dgPriorYr, 0);
            this.Controls.SetChildIndex(this.cbRegion, 0);
            this.Controls.SetChildIndex(this.cbDiv, 0);
            this.Controls.SetChildIndex(this.cbState, 0);
            this.Controls.SetChildIndex(this.lblPriorYr, 0);
            this.Controls.SetChildIndex(this.lblCurrYr, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnPrev, 0);
            this.Controls.SetChildIndex(this.btnCases, 0);
            this.Controls.SetChildIndex(this.dgCurrentYr, 0);
            this.Controls.SetChildIndex(this.btnRefresh, 0);
            this.Controls.SetChildIndex(this.txtPYTtunits, 0);
            this.Controls.SetChildIndex(this.txtPYTtvalue, 0);
            this.Controls.SetChildIndex(this.txtPYCpu, 0);
            this.Controls.SetChildIndex(this.txtCYTtunits, 0);
            this.Controls.SetChildIndex(this.txtCYTtvalue, 0);
            this.Controls.SetChildIndex(this.txtCYCpu, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgPriorYr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCurrentYr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgPriorYr;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.ComboBox cbDiv;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.Label lblPriorYr;
        private System.Windows.Forms.Label lblCurrYr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCases;
        private System.Windows.Forms.DataGridView dgCurrentYr;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtPYTtunits;
        private System.Windows.Forms.TextBox txtPYTtvalue;
        private System.Windows.Forms.TextBox txtPYCpu;
        private System.Windows.Forms.TextBox txtCYTtunits;
        private System.Windows.Forms.TextBox txtCYTtvalue;
        private System.Windows.Forms.TextBox txtCYCpu;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}