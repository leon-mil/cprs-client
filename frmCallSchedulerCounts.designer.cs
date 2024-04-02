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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
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
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle49.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle49.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle49.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle49.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle49;
            this.dgt1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle50.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle50.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgt1.DefaultCellStyle = dataGridViewCellStyle50;
            this.dgt1.Location = new System.Drawing.Point(15, 0);
            this.dgt1.Name = "dgt1";
            this.dgt1.ReadOnly = true;
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle51.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle51.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle51.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle51.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle51.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt1.RowHeadersDefaultCellStyle = dataGridViewCellStyle51;
            this.dgt1.RowHeadersVisible = false;
            dataGridViewCellStyle52.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgt1.RowsDefaultCellStyle = dataGridViewCellStyle52;
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
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle53.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle53.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle53.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle53.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle53.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle53.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle53;
            this.dgb1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle54.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle54.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle54.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle54.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle54.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle54.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgb1.DefaultCellStyle = dataGridViewCellStyle54;
            this.dgb1.Location = new System.Drawing.Point(15, 440);
            this.dgb1.Name = "dgb1";
            this.dgb1.ReadOnly = true;
            dataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle55.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle55.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle55.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle55.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle55.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle55.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb1.RowHeadersDefaultCellStyle = dataGridViewCellStyle55;
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
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle56.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle56.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle56.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle56.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle56.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle56.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle56;
            this.dgPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle57.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle57.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle57.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle57.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle57.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle57.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPrint.DefaultCellStyle = dataGridViewCellStyle57;
            this.dgPrint.Location = new System.Drawing.Point(71, 222);
            this.dgPrint.MultiSelect = false;
            this.dgPrint.Name = "dgPrint";
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle58.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle58.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle58.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle58.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle58.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPrint.RowHeadersDefaultCellStyle = dataGridViewCellStyle58;
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
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle59.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle59.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle59.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle59.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle59.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle59.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle59;
            this.dgb2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle60.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle60.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle60.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle60.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle60.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle60.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgb2.DefaultCellStyle = dataGridViewCellStyle60;
            this.dgb2.Location = new System.Drawing.Point(15, 449);
            this.dgb2.Name = "dgb2";
            this.dgb2.ReadOnly = true;
            dataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle61.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle61.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle61.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle61.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle61.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle61.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgb2.RowHeadersDefaultCellStyle = dataGridViewCellStyle61;
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
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle62.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle62.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle62.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle62.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle62.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle62.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle62;
            this.dgt2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle63.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle63.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle63.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle63.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle63.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle63.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgt2.DefaultCellStyle = dataGridViewCellStyle63;
            this.dgt2.Location = new System.Drawing.Point(15, 0);
            this.dgt2.Name = "dgt2";
            this.dgt2.ReadOnly = true;
            dataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle64.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle64.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle64.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle64.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle64.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle64.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgt2.RowHeadersDefaultCellStyle = dataGridViewCellStyle64;
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
            this.rd1u.Size = new System.Drawing.Size(77, 20);
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
            this.rd1m.Size = new System.Drawing.Size(98, 20);
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
            this.rd1f.Size = new System.Drawing.Size(79, 20);
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
            this.rd1n.Size = new System.Drawing.Size(126, 20);
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
            this.rd1s.Size = new System.Drawing.Size(133, 20);
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
            this.rd1a.Size = new System.Drawing.Size(103, 20);
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
            this.btnPrint.EnabledChanged += new System.EventHandler(this.btnPrint_EnabledChanged);
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
            this.btnC700.EnabledChanged += new System.EventHandler(this.btnC700_EnabledChanged);
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
            this.btnNA.EnabledChanged += new System.EventHandler(this.btnNA_EnabledChanged);
            this.btnNA.Click += new System.EventHandler(this.btnNA_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblType.Location = new System.Drawing.Point(233, 107);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(107, 16);
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
            this.btnHist.EnabledChanged += new System.EventHandler(this.btnHist_EnabledChanged);
            this.btnHist.Click += new System.EventHandler(this.btnHist_Click);
            // 
            // frmCallSchedulerCounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 861);
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