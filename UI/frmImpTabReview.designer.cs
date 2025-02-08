namespace Cprs
{
    partial class frmImpTabReview
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
            this.tbImprovements = new System.Windows.Forms.TabControl();
            this.tbWinsorized = new System.Windows.Forms.TabPage();
            this.dgWinsorized = new System.Windows.Forms.DataGridView();
            this.tbUnwinsorized = new System.Windows.Forms.TabPage();
            this.dgUnwinsorized = new System.Windows.Forms.DataGridView();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTab = new System.Windows.Forms.Label();
            this.tbImprovements.SuspendLayout();
            this.tbWinsorized.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWinsorized)).BeginInit();
            this.tbUnwinsorized.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUnwinsorized)).BeginInit();
            this.SuspendLayout();
            // 
            // tbImprovements
            // 
            this.tbImprovements.Controls.Add(this.tbWinsorized);
            this.tbImprovements.Controls.Add(this.tbUnwinsorized);
            this.tbImprovements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbImprovements.ItemSize = new System.Drawing.Size(150, 18);
            this.tbImprovements.Location = new System.Drawing.Point(31, 125);
            this.tbImprovements.Name = "tbImprovements";
            this.tbImprovements.SelectedIndex = 0;
            this.tbImprovements.Size = new System.Drawing.Size(1130, 625);
            this.tbImprovements.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbImprovements.TabIndex = 19;
            this.tbImprovements.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbImprovements_Selected);
            // 
            // tbWinsorized
            // 
            this.tbWinsorized.Controls.Add(this.dgWinsorized);
            this.tbWinsorized.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWinsorized.Location = new System.Drawing.Point(4, 22);
            this.tbWinsorized.Name = "tbWinsorized";
            this.tbWinsorized.Padding = new System.Windows.Forms.Padding(6);
            this.tbWinsorized.Size = new System.Drawing.Size(1122, 599);
            this.tbWinsorized.TabIndex = 2;
            this.tbWinsorized.Text = "WINSORIZED";
            this.tbWinsorized.UseVisualStyleBackColor = true;
            // 
            // dgWinsorized
            // 
            this.dgWinsorized.AllowUserToAddRows = false;
            this.dgWinsorized.AllowUserToDeleteRows = false;
            this.dgWinsorized.AllowUserToResizeColumns = false;
            this.dgWinsorized.AllowUserToResizeRows = false;
            this.dgWinsorized.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgWinsorized.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgWinsorized.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgWinsorized.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgWinsorized.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgWinsorized.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgWinsorized.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgWinsorized.EnableHeadersVisualStyles = false;
            this.dgWinsorized.Location = new System.Drawing.Point(3, 3);
            this.dgWinsorized.MultiSelect = false;
            this.dgWinsorized.Name = "dgWinsorized";
            this.dgWinsorized.ReadOnly = true;
            this.dgWinsorized.RowHeadersVisible = false;
            this.dgWinsorized.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWinsorized.Size = new System.Drawing.Size(1115, 593);
            this.dgWinsorized.TabIndex = 4;
            // 
            // tbUnwinsorized
            // 
            this.tbUnwinsorized.Controls.Add(this.dgUnwinsorized);
            this.tbUnwinsorized.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUnwinsorized.Location = new System.Drawing.Point(4, 22);
            this.tbUnwinsorized.Name = "tbUnwinsorized";
            this.tbUnwinsorized.Padding = new System.Windows.Forms.Padding(3);
            this.tbUnwinsorized.Size = new System.Drawing.Size(1122, 599);
            this.tbUnwinsorized.TabIndex = 1;
            this.tbUnwinsorized.Text = "UNWINSORIZED";
            this.tbUnwinsorized.UseVisualStyleBackColor = true;
            // 
            // dgUnwinsorized
            // 
            this.dgUnwinsorized.AllowUserToAddRows = false;
            this.dgUnwinsorized.AllowUserToDeleteRows = false;
            this.dgUnwinsorized.AllowUserToResizeColumns = false;
            this.dgUnwinsorized.AllowUserToResizeRows = false;
            this.dgUnwinsorized.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgUnwinsorized.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgUnwinsorized.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgUnwinsorized.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgUnwinsorized.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgUnwinsorized.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgUnwinsorized.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgUnwinsorized.Location = new System.Drawing.Point(3, 3);
            this.dgUnwinsorized.MultiSelect = false;
            this.dgUnwinsorized.Name = "dgUnwinsorized";
            this.dgUnwinsorized.ReadOnly = true;
            this.dgUnwinsorized.RowHeadersVisible = false;
            this.dgUnwinsorized.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUnwinsorized.Size = new System.Drawing.Size(1115, 593);
            this.dgUnwinsorized.TabIndex = 5;
            // 
           
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(542, 814);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(133, 23);
            this.btnPrint.TabIndex = 21;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(424, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(340, 25);
            this.label2.TabIndex = 22;
            this.label2.Text = "IMPROVEMENTS TAB REVIEW";
            // 
            // lblTab
            // 
            this.lblTab.AutoSize = true;
            this.lblTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab.Location = new System.Drawing.Point(0, 0);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(0, 18);
            this.lblTab.TabIndex = 23;
            // 
            // frmImpTabReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.lblTab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tbImprovements);
            this.Name = "frmImpTabReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImpTabReview_FormClosing);
            this.Load += new System.EventHandler(this.frmImpTabReview_Load);
            this.Controls.SetChildIndex(this.tbImprovements, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblTab, 0);
            this.tbImprovements.ResumeLayout(false);
            this.tbWinsorized.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWinsorized)).EndInit();
            this.tbUnwinsorized.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUnwinsorized)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbImprovements;
        private System.Windows.Forms.TabPage tbWinsorized;
        private System.Windows.Forms.DataGridView dgWinsorized;
        private System.Windows.Forms.TabPage tbUnwinsorized;
        private System.Windows.Forms.DataGridView dgUnwinsorized;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTab;
    }
}