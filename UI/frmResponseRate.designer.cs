namespace Cprs
{
    partial class frmResponseRate
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
            this.lbltitle = new System.Windows.Forms.Label();
            this.rdm = new System.Windows.Forms.RadioButton();
            this.rdf = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rdp = new System.Windows.Forms.RadioButton();
            this.rdt = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbRev = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rds = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.rdvip_urr = new System.Windows.Forms.RadioButton();
            this.rdvip_tqrr = new System.Windows.Forms.RadioButton();
            this.rdvip_imp = new System.Windows.Forms.RadioButton();
            this.rdsc_urr = new System.Windows.Forms.RadioButton();
            this.rdsc_tqrr = new System.Windows.Forms.RadioButton();
            this.rdsc_imp = new System.Windows.Forms.RadioButton();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgt0 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgt0)).BeginInit();
            this.SuspendLayout();
            // 
            // lbltitle
            // 
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbltitle.Location = new System.Drawing.Point(77, 50);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(1018, 25);
            this.lbltitle.TabIndex = 16;
            this.lbltitle.Text = "TOTAL RESPONSE RATE REPORTS  for PRELIMINARY";
            this.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdm
            // 
            this.rdm.AutoSize = true;
            this.rdm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdm.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdm.Location = new System.Drawing.Point(586, 3);
            this.rdm.Name = "rdm";
            this.rdm.Size = new System.Drawing.Size(99, 20);
            this.rdm.TabIndex = 88;
            this.rdm.Text = "Multifamily";
            this.rdm.UseVisualStyleBackColor = true;
            this.rdm.CheckedChanged += new System.EventHandler(this.rdm_CheckedChanged);
            // 
            // rdf
            // 
            this.rdf.AutoSize = true;
            this.rdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdf.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdf.Location = new System.Drawing.Point(367, 3);
            this.rdf.Name = "rdf";
            this.rdf.Size = new System.Drawing.Size(80, 20);
            this.rdf.TabIndex = 86;
            this.rdf.Text = "Federal";
            this.rdf.UseVisualStyleBackColor = true;
            this.rdf.CheckedChanged += new System.EventHandler(this.rdf_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 16);
            this.label4.TabIndex = 87;
            this.label4.Text = "Select Survey:";
            // 
            // rdp
            // 
            this.rdp.AutoSize = true;
            this.rdp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdp.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdp.Location = new System.Drawing.Point(453, 3);
            this.rdp.Name = "rdp";
            this.rdp.Size = new System.Drawing.Size(127, 20);
            this.rdp.TabIndex = 85;
            this.rdp.Text = "Nonresidential";
            this.rdp.UseVisualStyleBackColor = true;
            this.rdp.CheckedChanged += new System.EventHandler(this.rdp_CheckedChanged);
            // 
            // rdt
            // 
            this.rdt.AutoSize = true;
            this.rdt.Checked = true;
            this.rdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdt.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdt.Location = new System.Drawing.Point(117, 3);
            this.rdt.Name = "rdt";
            this.rdt.Size = new System.Drawing.Size(104, 20);
            this.rdt.TabIndex = 84;
            this.rdt.TabStop = true;
            this.rdt.Text = "All Surveys";
            this.rdt.UseVisualStyleBackColor = true;
            this.rdt.CheckedChanged += new System.EventHandler(this.rdt_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(452, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 15);
            this.label3.TabIndex = 82;
            this.label3.Text = "Select Revision:";
            // 
            // cbRev
            // 
            this.cbRev.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRev.FormattingEnabled = true;
            this.cbRev.Items.AddRange(new object[] {
            "Preliminary",
            "First Revision",
            "Second Revision"});
            this.cbRev.Location = new System.Drawing.Point(568, 164);
            this.cbRev.Name = "cbRev";
            this.cbRev.Size = new System.Drawing.Size(104, 21);
            this.cbRev.TabIndex = 83;
            this.cbRev.SelectedIndexChanged += new System.EventHandler(this.cbRev_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.rdt);
            this.flowLayoutPanel1.Controls.Add(this.rds);
            this.flowLayoutPanel1.Controls.Add(this.rdf);
            this.flowLayoutPanel1.Controls.Add(this.rdp);
            this.flowLayoutPanel1.Controls.Add(this.rdm);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(234, 88);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(694, 29);
            this.flowLayoutPanel1.TabIndex = 90;
            // 
            // rds
            // 
            this.rds.AutoSize = true;
            this.rds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rds.ForeColor = System.Drawing.Color.DarkBlue;
            this.rds.Location = new System.Drawing.Point(227, 3);
            this.rds.Name = "rds";
            this.rds.Size = new System.Drawing.Size(134, 20);
            this.rds.TabIndex = 89;
            this.rds.Text = "State and Local";
            this.rds.UseVisualStyleBackColor = true;
            this.rds.CheckedChanged += new System.EventHandler(this.rds_CheckedChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.rdvip_urr);
            this.flowLayoutPanel2.Controls.Add(this.rdvip_tqrr);
            this.flowLayoutPanel2.Controls.Add(this.rdvip_imp);
            this.flowLayoutPanel2.Controls.Add(this.rdsc_urr);
            this.flowLayoutPanel2.Controls.Add(this.rdsc_tqrr);
            this.flowLayoutPanel2.Controls.Add(this.rdsc_imp);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(234, 123);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(668, 29);
            this.flowLayoutPanel2.TabIndex = 91;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 87;
            this.label1.Text = "Select Rate:";
            // 
            // rdvip_urr
            // 
            this.rdvip_urr.AutoSize = true;
            this.rdvip_urr.Checked = true;
            this.rdvip_urr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdvip_urr.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdvip_urr.Location = new System.Drawing.Point(102, 3);
            this.rdvip_urr.Name = "rdvip_urr";
            this.rdvip_urr.Size = new System.Drawing.Size(87, 20);
            this.rdvip_urr.TabIndex = 84;
            this.rdvip_urr.TabStop = true;
            this.rdvip_urr.Text = "VIP URR";
            this.rdvip_urr.UseVisualStyleBackColor = true;
            this.rdvip_urr.CheckedChanged += new System.EventHandler(this.rdvip_urr_CheckedChanged);
            // 
            // rdvip_tqrr
            // 
            this.rdvip_tqrr.AutoSize = true;
            this.rdvip_tqrr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdvip_tqrr.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdvip_tqrr.Location = new System.Drawing.Point(195, 3);
            this.rdvip_tqrr.Name = "rdvip_tqrr";
            this.rdvip_tqrr.Size = new System.Drawing.Size(97, 20);
            this.rdvip_tqrr.TabIndex = 85;
            this.rdvip_tqrr.Text = "VIP TQRR";
            this.rdvip_tqrr.UseVisualStyleBackColor = true;
            this.rdvip_tqrr.CheckedChanged += new System.EventHandler(this.rdvip_tqrr_CheckedChanged);
            // 
            // rdvip_imp
            // 
            this.rdvip_imp.AutoSize = true;
            this.rdvip_imp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdvip_imp.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdvip_imp.Location = new System.Drawing.Point(298, 3);
            this.rdvip_imp.Name = "rdvip_imp";
            this.rdvip_imp.Size = new System.Drawing.Size(80, 20);
            this.rdvip_imp.TabIndex = 86;
            this.rdvip_imp.Text = "VIP IMP";
            this.rdvip_imp.UseVisualStyleBackColor = true;
            this.rdvip_imp.CheckedChanged += new System.EventHandler(this.rdvip_imp_CheckedChanged);
            // 
            // rdsc_urr
            // 
            this.rdsc_urr.AutoSize = true;
            this.rdsc_urr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsc_urr.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdsc_urr.Location = new System.Drawing.Point(384, 3);
            this.rdsc_urr.Name = "rdsc_urr";
            this.rdsc_urr.Size = new System.Drawing.Size(81, 20);
            this.rdsc_urr.TabIndex = 88;
            this.rdsc_urr.Text = "5C URR";
            this.rdsc_urr.UseVisualStyleBackColor = true;
            this.rdsc_urr.CheckedChanged += new System.EventHandler(this.rdsc_urr_CheckedChanged);
            // 
            // rdsc_tqrr
            // 
            this.rdsc_tqrr.AutoSize = true;
            this.rdsc_tqrr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsc_tqrr.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdsc_tqrr.Location = new System.Drawing.Point(471, 3);
            this.rdsc_tqrr.Name = "rdsc_tqrr";
            this.rdsc_tqrr.Size = new System.Drawing.Size(91, 20);
            this.rdsc_tqrr.TabIndex = 89;
            this.rdsc_tqrr.Text = "5C TQRR";
            this.rdsc_tqrr.UseVisualStyleBackColor = true;
            this.rdsc_tqrr.CheckedChanged += new System.EventHandler(this.rdsc_tqrr_CheckedChanged);
            // 
            // rdsc_imp
            // 
            this.rdsc_imp.AutoSize = true;
            this.rdsc_imp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsc_imp.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdsc_imp.Location = new System.Drawing.Point(568, 3);
            this.rdsc_imp.Name = "rdsc_imp";
            this.rdsc_imp.Size = new System.Drawing.Size(74, 20);
            this.rdsc_imp.TabIndex = 90;
            this.rdsc_imp.Text = "5C IMP";
            this.rdsc_imp.UseVisualStyleBackColor = true;
            this.rdsc_imp.CheckedChanged += new System.EventHandler(this.rdsc_imp_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(672, 789);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(137, 23);
            this.btnPrint.TabIndex = 93;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnTable
            // 
            this.btnTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTable.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnTable.Location = new System.Drawing.Point(399, 789);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(137, 23);
            this.btnTable.TabIndex = 20;
            this.btnTable.Text = "CREATE TABLES";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // dgt0
            // 
            this.dgt0.AllowUserToAddRows = false;
            this.dgt0.AllowUserToDeleteRows = false;
            this.dgt0.AllowUserToResizeColumns = false;
            this.dgt0.AllowUserToResizeRows = false;
            this.dgt0.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt0.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgt0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgt0.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgt0.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgt0.Location = new System.Drawing.Point(23, 191);
            this.dgt0.Name = "dgt0";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt0.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgt0.RowHeadersVisible = false;
            this.dgt0.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgt0.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgt0.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgt0.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgt0.Size = new System.Drawing.Size(1153, 530);
            this.dgt0.TabIndex = 1;
            // 
            // frmResponseRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 869);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgt0);
            this.Controls.Add(this.cbRev);
            this.Controls.Add(this.lbltitle);
            this.Name = "frmResponseRate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmResponseRate_FormClosing);
            this.Load += new System.EventHandler(this.frmResponseRate_Load);
            this.Controls.SetChildIndex(this.lbltitle, 0);
            this.Controls.SetChildIndex(this.cbRev, 0);
            this.Controls.SetChildIndex(this.dgt0, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnTable, 0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgt0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.RadioButton rdm;
        private System.Windows.Forms.RadioButton rdf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdp;
        private System.Windows.Forms.RadioButton rdt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbRev;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdvip_urr;
        private System.Windows.Forms.RadioButton rdvip_tqrr;
        private System.Windows.Forms.RadioButton rdvip_imp;
        private System.Windows.Forms.RadioButton rdsc_urr;
        private System.Windows.Forms.RadioButton rdsc_tqrr;
        private System.Windows.Forms.RadioButton rdsc_imp;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.RadioButton rds;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgt0;
    }
}