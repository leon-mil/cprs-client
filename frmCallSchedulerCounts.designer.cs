namespace Cprs
{
    partial class frmCallSchedulerCounts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblcount1 = new System.Windows.Forms.Label();
            this.dgt1 = new System.Windows.Forms.DataGridView();
            this.dgb1 = new System.Windows.Forms.DataGridView();
            this.dgPrint = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblcount2 = new System.Windows.Forms.Label();
            this.dgb2 = new System.Windows.Forms.DataGridView();
            this.dgt2 = new System.Windows.Forms.DataGridView();
            this.rd1u = new System.Windows.Forms.RadioButton();
            this.rd1m = new System.Windows.Forms.RadioButton();
            this.rd1f = new System.Windows.Forms.RadioButton();
            this.rd1n = new System.Windows.Forms.RadioButton();
            this.rd1s = new System.Windows.Forms.RadioButton();
            this.rd1a = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnC700 = new System.Windows.Forms.Button();
            this.btnNA = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.btnHist = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgt2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(12, 138);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1172, 654);
            this.tabs.TabIndex = 21;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1164, 628);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CONTACT STATUS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblcount1);
            this.panel1.Controls.Add(this.dgt1);
            this.panel1.Controls.Add(this.dgb1);
            this.panel1.Controls.Add(this.dgPrint);
            this.panel1.Location = new System.Drawing.Point(-4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1168, 619);
            this.panel1.TabIndex = 24;
            // 
            // lblcount1
            // 
            this.lblcount1.AutoSize = true;
            this.lblcount1.Location = new System.Drawing.Point(12, 423);
            this.lblcount1.Name = "lblcount1";
            this.lblcount1.Size = new System.Drawing.Size(51, 13);
            this.lblcount1.TabIndex = 41;
            this.lblcount1.Text = "0 CASES";
            // 
            // dgt1
            // 
            this.dgt1.AllowUserToAddRows = false;
            this.dgt1.AllowUserToDeleteRows = false;
            this.dgt1.AllowUserToResizeColumns = false;
            this.dgt1.AllowUserToResizeRows = false;
            this.dgt1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgt1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgt1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgt1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgt1.Location = new System.Drawing.Point(15, 0);
            this.dgt1.Name = "dgt1";
            this.dgt1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgt1.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgt1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgt1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgt1.Size = new System.Drawing.Size(1147, 420);
            this.dgt1.TabIndex = 33;
            this.dgt1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgt1_CellClick);
            this.dgt1.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dgt1_CellStateChanged);
            // 
            // dgb1
            // 
            this.dgb1.AllowUserToAddRows = false;
            this.dgb1.AllowUserToDeleteRows = false;
            this.dgb1.AllowUserToResizeColumns = false;
            this.dgb1.AllowUserToResizeRows = false;
            this.dgb1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgb1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgb1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgb1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgb1.Location = new System.Drawing.Point(15, 440);
            this.dgb1.Name = "dgb1";
            this.dgb1.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgb1.RowHeadersVisible = false;
            this.dgb1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgb1.Size = new System.Drawing.Size(1147, 179);
            this.dgb1.TabIndex = 28;
            // 
            // dgPrint
            // 
            this.dgPrint.AllowUserToAddRows = false;
            this.dgPrint.AllowUserToDeleteRows = false;
            this.dgPrint.AllowUserToResizeColumns = false;
            this.dgPrint.AllowUserToResizeRows = false;
            this.dgPrint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPrint.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPrint.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgPrint.Location = new System.Drawing.Point(71, 222);
            this.dgPrint.MultiSelect = false;
            this.dgPrint.Name = "dgPrint";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgPrint.RowHeadersVisible = false;
            this.dgPrint.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgPrint.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgPrint.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgPrint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPrint.Size = new System.Drawing.Size(982, 50);
            this.dgPrint.TabIndex = 42;
            this.dgPrint.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblcount2);
            this.tabPage2.Controls.Add(this.dgb2);
            this.tabPage2.Controls.Add(this.dgt2);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1164, 628);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VIP STATUS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblcount2
            // 
            this.lblcount2.AutoSize = true;
            this.lblcount2.Location = new System.Drawing.Point(12, 433);
            this.lblcount2.Name = "lblcount2";
            this.lblcount2.Size = new System.Drawing.Size(51, 13);
            this.lblcount2.TabIndex = 42;
            this.lblcount2.Text = "0 CASES";
            // 
            // dgb2
            // 
            this.dgb2.AllowUserToAddRows = false;
            this.dgb2.AllowUserToDeleteRows = false;
            this.dgb2.AllowUserToResizeColumns = false;
            this.dgb2.AllowUserToResizeRows = false;
            this.dgb2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgb2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgb2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgb2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgb2.Location = new System.Drawing.Point(15, 449);
            this.dgb2.Name = "dgb2";
            this.dgb2.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb2.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgb2.RowHeadersVisible = false;
            this.dgb2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgb2.Size = new System.Drawing.Size(1147, 173);
            this.dgb2.TabIndex = 34;
            // 
            // dgt2
            // 
            this.dgt2.AllowUserToAddRows = false;
            this.dgt2.AllowUserToDeleteRows = false;
            this.dgt2.AllowUserToResizeColumns = false;
            this.dgt2.AllowUserToResizeRows = false;
            this.dgt2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgt2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgt2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgt2.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgt2.Location = new System.Drawing.Point(15, 0);
            this.dgt2.Name = "dgt2";
            this.dgt2.ReadOnly = true;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt2.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgt2.RowHeadersVisible = false;
            this.dgt2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgt2.Size = new System.Drawing.Size(1147, 430);
            this.dgt2.TabIndex = 33;
            this.dgt2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgt2_CellClick);
            this.dgt2.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dgt2_CellStateChanged);
            // 
            // rd1u
            // 
            this.rd1u.AutoSize = true;
            this.rd1u.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1u.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1u.Location = new System.Drawing.Point(921, 107);
            this.rd1u.Name = "rd1u";
            this.rd1u.Size = new System.Drawing.Size(78, 20);
            this.rd1u.TabIndex = 40;
            this.rd1u.Text = "Utilities";
            this.rd1u.UseVisualStyleBackColor = true;
            this.rd1u.CheckedChanged += new System.EventHandler(this.rd1u_CheckedChanged);
            // 
            // rd1m
            // 
            this.rd1m.AutoSize = true;
            this.rd1m.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1m.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1m.Location = new System.Drawing.Point(816, 107);
            this.rd1m.Name = "rd1m";
            this.rd1m.Size = new System.Drawing.Size(99, 20);
            this.rd1m.TabIndex = 39;
            this.rd1m.Text = "Multifamily";
            this.rd1m.UseVisualStyleBackColor = true;
            this.rd1m.CheckedChanged += new System.EventHandler(this.rd1m_CheckedChanged);
            // 
            // rd1f
            // 
            this.rd1f.AutoSize = true;
            this.rd1f.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1f.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1f.Location = new System.Drawing.Point(597, 107);
            this.rd1f.Name = "rd1f";
            this.rd1f.Size = new System.Drawing.Size(80, 20);
            this.rd1f.TabIndex = 37;
            this.rd1f.Text = "Federal";
            this.rd1f.UseVisualStyleBackColor = true;
            this.rd1f.CheckedChanged += new System.EventHandler(this.rd1f_CheckedChanged);
            // 
            // rd1n
            // 
            this.rd1n.AutoSize = true;
            this.rd1n.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1n.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1n.Location = new System.Drawing.Point(683, 107);
            this.rd1n.Name = "rd1n";
            this.rd1n.Size = new System.Drawing.Size(127, 20);
            this.rd1n.TabIndex = 36;
            this.rd1n.Text = "Nonresidential";
            this.rd1n.UseVisualStyleBackColor = true;
            this.rd1n.CheckedChanged += new System.EventHandler(this.rd1n_CheckedChanged);
            // 
            // rd1s
            // 
            this.rd1s.AutoSize = true;
            this.rd1s.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1s.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1s.Location = new System.Drawing.Point(457, 107);
            this.rd1s.Name = "rd1s";
            this.rd1s.Size = new System.Drawing.Size(134, 20);
            this.rd1s.TabIndex = 35;
            this.rd1s.Text = "State and Local";
            this.rd1s.UseVisualStyleBackColor = true;
            this.rd1s.CheckedChanged += new System.EventHandler(this.rd1s_CheckedChanged);
            // 
            // rd1a
            // 
            this.rd1a.AutoSize = true;
            this.rd1a.Checked = true;
            this.rd1a.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd1a.ForeColor = System.Drawing.Color.DarkBlue;
            this.rd1a.Location = new System.Drawing.Point(347, 107);
            this.rd1a.Name = "rd1a";
            this.rd1a.Size = new System.Drawing.Size(104, 20);
            this.rd1a.TabIndex = 34;
            this.rd1a.TabStop = true;
            this.rd1a.Text = "All Surveys";
            this.rd1a.UseVisualStyleBackColor = true;
            this.rd1a.CheckedChanged += new System.EventHandler(this.rd1a_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(396, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(415, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "CALL SCHEDULER COUNTS REPORT";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnPrint.Location = new System.Drawing.Point(792, 812);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(137, 23);
            this.btnPrint.TabIndex = 41;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnC700
            // 
            this.btnC700.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC700.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnC700.Location = new System.Drawing.Point(454, 812);
            this.btnC700.Name = "btnC700";
            this.btnC700.Size = new System.Drawing.Size(137, 23);
            this.btnC700.TabIndex = 35;
            this.btnC700.Text = "C700";
            this.btnC700.UseVisualStyleBackColor = true;
            this.btnC700.Click += new System.EventHandler(this.btnC700_Click);
            // 
            // btnNA
            // 
            this.btnNA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNA.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnNA.Location = new System.Drawing.Point(273, 812);
            this.btnNA.Name = "btnNA";
            this.btnNA.Size = new System.Drawing.Size(137, 23);
            this.btnNA.TabIndex = 35;
            this.btnNA.Text = "NAME ADDRESS";
            this.btnNA.UseVisualStyleBackColor = true;
            this.btnNA.Click += new System.EventHandler(this.btnNA_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblType.Location = new System.Drawing.Point(233, 107);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(108, 16);
            this.lblType.TabIndex = 42;
            this.lblType.Text = "Select Survey:";
            // 
            // btnHist
            // 
            this.btnHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHist.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnHist.Location = new System.Drawing.Point(624, 812);
            this.btnHist.Name = "btnHist";
            this.btnHist.Size = new System.Drawing.Size(137, 23);
            this.btnHist.TabIndex = 43;
            this.btnHist.Text = "HISTORY";
            this.btnHist.UseVisualStyleBackColor = true;
            this.btnHist.Click += new System.EventHandler(this.btnHist_Click);
            // 
            // frmCallSchedulerCounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 870);
            this.Controls.Add(this.btnHist);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.btnNA);
            this.Controls.Add(this.btnC700);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.rd1u);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rd1m);
            this.Controls.Add(this.rd1f);
            this.Controls.Add(this.rd1n);
            this.Controls.Add(this.rd1a);
            this.Controls.Add(this.rd1s);
            this.Name = "frmCallSchedulerCounts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCallSchedulerCounts_FormClosing);
            this.Load += new System.EventHandler(this.frmCallSchedulerCounts_Load);
            this.Controls.SetChildIndex(this.rd1s, 0);
            this.Controls.SetChildIndex(this.rd1a, 0);
            this.Controls.SetChildIndex(this.rd1n, 0);
            this.Controls.SetChildIndex(this.rd1f, 0);
            this.Controls.SetChildIndex(this.rd1m, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.rd1u, 0);
            this.Controls.SetChildIndex(this.tabs, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnC700, 0);
            this.Controls.SetChildIndex(this.btnNA, 0);
            this.Controls.SetChildIndex(this.lblType, 0);
            this.Controls.SetChildIndex(this.btnHist, 0);
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPrint)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgt2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgt1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblcount1;
        private System.Windows.Forms.RadioButton rd1u;
        private System.Windows.Forms.RadioButton rd1m;
        private System.Windows.Forms.RadioButton rd1f;
        private System.Windows.Forms.RadioButton rd1n;
        private System.Windows.Forms.RadioButton rd1s;
        private System.Windows.Forms.RadioButton rd1a;
        private System.Windows.Forms.DataGridView dgb1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnC700;
        private System.Windows.Forms.Button btnNA;
        private System.Windows.Forms.DataGridView dgPrint;
        private System.Windows.Forms.Label lblcount2;
        private System.Windows.Forms.DataGridView dgb2;
        private System.Windows.Forms.DataGridView dgt2;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnHist;
    }
}