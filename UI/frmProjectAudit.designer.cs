namespace Cprs
{
    partial class frmProjectAudit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbldatef = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbValueItem = new System.Windows.Forms.ComboBox();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.txtValueItem = new System.Windows.Forms.TextBox();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgItem = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgVip = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbValueVip = new System.Windows.Forms.ComboBox();
            this.lbldatef1 = new System.Windows.Forms.Label();
            this.btnClear1 = new System.Windows.Forms.Button();
            this.btnSearchVip = new System.Windows.Forms.Button();
            this.txtValueVip = new System.Windows.Forms.TextBox();
            this.cbVip = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdb11 = new System.Windows.Forms.RadioButton();
            this.rdb10 = new System.Windows.Forms.RadioButton();
            this.rdb9 = new System.Windows.Forms.RadioButton();
            this.rdb8 = new System.Windows.Forms.RadioButton();
            this.rdb7 = new System.Windows.Forms.RadioButton();
            this.rdb6 = new System.Windows.Forms.RadioButton();
            this.rdb5 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rdb4 = new System.Windows.Forms.RadioButton();
            this.rdb3 = new System.Windows.Forms.RadioButton();
            this.rdb2 = new System.Windows.Forms.RadioButton();
            this.rdb1 = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItem)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVip)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(28, 129);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1128, 694);
            this.tabs.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dgItem);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1120, 668);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ITEM AUDIT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbldatef);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.cbValueItem);
            this.groupBox1.Controls.Add(this.btnSearchItem);
            this.groupBox1.Controls.Add(this.txtValueItem);
            this.groupBox1.Controls.Add(this.cbItem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(276, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 34);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lbldatef
            // 
            this.lbldatef.AutoSize = true;
            this.lbldatef.Location = new System.Drawing.Point(288, 11);
            this.lbldatef.Name = "lbldatef";
            this.lbldatef.Size = new System.Drawing.Size(79, 13);
            this.lbldatef.TabIndex = 7;
            this.lbldatef.Text = "MM/DD/YYYY";
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClear.Location = new System.Drawing.Point(467, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbValueItem
            // 
            this.cbValueItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValueItem.FormattingEnabled = true;
            this.cbValueItem.Items.AddRange(new object[] {
            "STRTDATE",
            "COMPDATE",
            "NEWTC",
            "STATUS",
            "OWNER",
            "RVITM5C",
            "RESPID",
            "FUTCOMPD",
            "ITEM6",
            "CAPEXP",
            "RBLDGS",
            "RUNITS"});
            this.cbValueItem.Location = new System.Drawing.Point(185, 7);
            this.cbValueItem.Name = "cbValueItem";
            this.cbValueItem.Size = new System.Drawing.Size(97, 21);
            this.cbValueItem.TabIndex = 4;
            this.cbValueItem.SelectedIndexChanged += new System.EventHandler(this.cbValueItem_SelectedIndexChanged);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchItem.Location = new System.Drawing.Point(386, 8);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchItem.TabIndex = 5;
            this.btnSearchItem.Text = "Search";
            this.btnSearchItem.UseVisualStyleBackColor = true;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtValueItem
            // 
            this.txtValueItem.Location = new System.Drawing.Point(185, 9);
            this.txtValueItem.MaxLength = 10;
            this.txtValueItem.Name = "txtValueItem";
            this.txtValueItem.Size = new System.Drawing.Size(97, 20);
            this.txtValueItem.TabIndex = 3;
            this.txtValueItem.TextChanged += new System.EventHandler(this.txtValueItem_TextChanged);
            this.txtValueItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValueItem_KeyDown);
            this.txtValueItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValueItem_KeyPress);
            // 
            // cbItem
            // 
            this.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItem.FormattingEnabled = true;
            this.cbItem.Items.AddRange(new object[] {
            "ID",
            "NEWTC",
            "VARNME",
            "USER",
            "DATE"});
            this.cbItem.Location = new System.Drawing.Point(84, 8);
            this.cbItem.Name = "cbItem";
            this.cbItem.Size = new System.Drawing.Size(95, 21);
            this.cbItem.TabIndex = 2;
            this.cbItem.SelectedIndexChanged += new System.EventHandler(this.cbItem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search By:";
            // 
            // dgItem
            // 
            this.dgItem.AllowUserToAddRows = false;
            this.dgItem.AllowUserToDeleteRows = false;
            this.dgItem.AllowUserToResizeColumns = false;
            this.dgItem.AllowUserToResizeRows = false;
            this.dgItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgItem.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItem.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgItem.Location = new System.Drawing.Point(27, 64);
            this.dgItem.Name = "dgItem";
            this.dgItem.ReadOnly = true;
            this.dgItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItem.Size = new System.Drawing.Size(1066, 580);
            this.dgItem.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgVip);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1120, 668);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VIP AUDIT";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgVip
            // 
            this.dgVip.AllowUserToAddRows = false;
            this.dgVip.AllowUserToDeleteRows = false;
            this.dgVip.AllowUserToResizeColumns = false;
            this.dgVip.AllowUserToResizeRows = false;
            this.dgVip.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgVip.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgVip.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgVip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgVip.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgVip.Location = new System.Drawing.Point(27, 64);
            this.dgVip.Name = "dgVip";
            this.dgVip.ReadOnly = true;
            this.dgVip.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVip.Size = new System.Drawing.Size(1066, 573);
            this.dgVip.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbValueVip);
            this.groupBox2.Controls.Add(this.lbldatef1);
            this.groupBox2.Controls.Add(this.btnClear1);
            this.groupBox2.Controls.Add(this.btnSearchVip);
            this.groupBox2.Controls.Add(this.txtValueVip);
            this.groupBox2.Controls.Add(this.cbVip);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(279, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(562, 34);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // cbValueVip
            // 
            this.cbValueVip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValueVip.FormattingEnabled = true;
            this.cbValueVip.Items.AddRange(new object[] {
            "STRTDATE",
            "COMPDATE",
            "NEWTC",
            "STATUS",
            "OWNER",
            "RVITM5C",
            "RESPID",
            "FUTCOMPD",
            "ITEM6",
            "CAPEXP",
            "RBLDGS",
            "RUNITS"});
            this.cbValueVip.Location = new System.Drawing.Point(185, 7);
            this.cbValueVip.Name = "cbValueVip";
            this.cbValueVip.Size = new System.Drawing.Size(97, 21);
            this.cbValueVip.TabIndex = 8;
            this.cbValueVip.SelectedIndexChanged += new System.EventHandler(this.cbValueVip_SelectedIndexChanged);
            // 
            // lbldatef1
            // 
            this.lbldatef1.AutoSize = true;
            this.lbldatef1.Location = new System.Drawing.Point(288, 13);
            this.lbldatef1.Name = "lbldatef1";
            this.lbldatef1.Size = new System.Drawing.Size(79, 13);
            this.lbldatef1.TabIndex = 7;
            this.lbldatef1.Text = "MM/DD/YYYY";
            // 
            // btnClear1
            // 
            this.btnClear1.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClear1.Location = new System.Drawing.Point(467, 7);
            this.btnClear1.Name = "btnClear1";
            this.btnClear1.Size = new System.Drawing.Size(75, 23);
            this.btnClear1.TabIndex = 6;
            this.btnClear1.Text = "Clear";
            this.btnClear1.UseVisualStyleBackColor = true;
            this.btnClear1.Click += new System.EventHandler(this.btnClear1_Click);
            // 
            // btnSearchVip
            // 
            this.btnSearchVip.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchVip.Location = new System.Drawing.Point(386, 8);
            this.btnSearchVip.Name = "btnSearchVip";
            this.btnSearchVip.Size = new System.Drawing.Size(75, 23);
            this.btnSearchVip.TabIndex = 5;
            this.btnSearchVip.Text = "Search";
            this.btnSearchVip.UseVisualStyleBackColor = true;
            this.btnSearchVip.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // txtValueVip
            // 
            this.txtValueVip.Location = new System.Drawing.Point(185, 9);
            this.txtValueVip.MaxLength = 10;
            this.txtValueVip.Name = "txtValueVip";
            this.txtValueVip.Size = new System.Drawing.Size(97, 20);
            this.txtValueVip.TabIndex = 3;
            this.txtValueVip.TextChanged += new System.EventHandler(this.txtValueVip_TextChanged);
            this.txtValueVip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValueVip_KeyDown);
            this.txtValueVip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValueVip_KeyPress);
            // 
            // cbVip
            // 
            this.cbVip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVip.FormattingEnabled = true;
            this.cbVip.Items.AddRange(new object[] {
            "ID",
            "NEWTC",
            "DATE6",
            "USER",
            "DATE"});
            this.cbVip.Location = new System.Drawing.Point(84, 8);
            this.cbVip.Name = "cbVip";
            this.cbVip.Size = new System.Drawing.Size(95, 21);
            this.cbVip.TabIndex = 2;
            this.cbVip.SelectedIndexChanged += new System.EventHandler(this.cbVip_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Search By:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(513, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "PROJECT AUDIT";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdb11);
            this.groupBox3.Controls.Add(this.rdb10);
            this.groupBox3.Controls.Add(this.rdb9);
            this.groupBox3.Controls.Add(this.rdb8);
            this.groupBox3.Controls.Add(this.rdb7);
            this.groupBox3.Controls.Add(this.rdb6);
            this.groupBox3.Controls.Add(this.rdb5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rdb4);
            this.groupBox3.Controls.Add(this.rdb3);
            this.groupBox3.Controls.Add(this.rdb2);
            this.groupBox3.Controls.Add(this.rdb1);
            this.groupBox3.Location = new System.Drawing.Point(0, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1216, 35);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // rdb11
            // 
            this.rdb11.AutoSize = true;
            this.rdb11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb11.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb11.Location = new System.Drawing.Point(1149, 13);
            this.rdb11.Name = "rdb11";
            this.rdb11.Size = new System.Drawing.Size(61, 20);
            this.rdb11.TabIndex = 6;
            this.rdb11.Text = "Wind";
            this.rdb11.UseVisualStyleBackColor = true;
            this.rdb11.CheckedChanged += new System.EventHandler(this.rdb11_CheckedChanged);
            // 
            // rdb10
            // 
            this.rdb10.AutoSize = true;
            this.rdb10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb10.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb10.Location = new System.Drawing.Point(1098, 13);
            this.rdb10.Name = "rdb10";
            this.rdb10.Size = new System.Drawing.Size(45, 20);
            this.rdb10.TabIndex = 10;
            this.rdb10.Text = "Oil";
            this.rdb10.UseVisualStyleBackColor = true;
            this.rdb10.CheckedChanged += new System.EventHandler(this.rdb10_CheckedChanged);
            // 
            // rdb9
            // 
            this.rdb9.AutoSize = true;
            this.rdb9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb9.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb9.Location = new System.Drawing.Point(1006, 13);
            this.rdb9.Name = "rdb9";
            this.rdb9.Size = new System.Drawing.Size(86, 20);
            this.rdb9.TabIndex = 9;
            this.rdb9.Text = "Railroad";
            this.rdb9.UseVisualStyleBackColor = true;
            this.rdb9.CheckedChanged += new System.EventHandler(this.rdb9_CheckedChanged);
            // 
            // rdb8
            // 
            this.rdb8.AutoSize = true;
            this.rdb8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb8.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb8.Location = new System.Drawing.Point(946, 13);
            this.rdb8.Name = "rdb8";
            this.rdb8.Size = new System.Drawing.Size(54, 20);
            this.rdb8.TabIndex = 8;
            this.rdb8.Text = "Gas";
            this.rdb8.UseVisualStyleBackColor = true;
            this.rdb8.CheckedChanged += new System.EventHandler(this.rdb8_CheckedChanged);
            // 
            // rdb7
            // 
            this.rdb7.AutoSize = true;
            this.rdb7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb7.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb7.Location = new System.Drawing.Point(857, 13);
            this.rdb7.Name = "rdb7";
            this.rdb7.Size = new System.Drawing.Size(91, 20);
            this.rdb7.TabIndex = 7;
            this.rdb7.Text = "Electrical";
            this.rdb7.UseVisualStyleBackColor = true;
            this.rdb7.CheckedChanged += new System.EventHandler(this.rdb7_CheckedChanged);
            // 
            // rdb6
            // 
            this.rdb6.AutoSize = true;
            this.rdb6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb6.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb6.Location = new System.Drawing.Point(682, 13);
            this.rdb6.Name = "rdb6";
            this.rdb6.Size = new System.Drawing.Size(169, 20);
            this.rdb6.TabIndex = 6;
            this.rdb6.Text = "Telecommunications";
            this.rdb6.UseVisualStyleBackColor = true;
            this.rdb6.CheckedChanged += new System.EventHandler(this.rdb6_CheckedChanged);
            // 
            // rdb5
            // 
            this.rdb5.AutoSize = true;
            this.rdb5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb5.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb5.Location = new System.Drawing.Point(577, 13);
            this.rdb5.Name = "rdb5";
            this.rdb5.Size = new System.Drawing.Size(99, 20);
            this.rdb5.TabIndex = 5;
            this.rdb5.Text = "Multifamily";
            this.rdb5.UseVisualStyleBackColor = true;
            this.rdb5.CheckedChanged += new System.EventHandler(this.rdb5_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Select Survey:";
            // 
            // rdb4
            // 
            this.rdb4.AutoSize = true;
            this.rdb4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb4.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb4.Location = new System.Drawing.Point(358, 13);
            this.rdb4.Name = "rdb4";
            this.rdb4.Size = new System.Drawing.Size(80, 20);
            this.rdb4.TabIndex = 3;
            this.rdb4.Text = "Federal";
            this.rdb4.UseVisualStyleBackColor = true;
            this.rdb4.CheckedChanged += new System.EventHandler(this.rdb4_CheckedChanged);
            // 
            // rdb3
            // 
            this.rdb3.AutoSize = true;
            this.rdb3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb3.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb3.Location = new System.Drawing.Point(444, 13);
            this.rdb3.Name = "rdb3";
            this.rdb3.Size = new System.Drawing.Size(127, 20);
            this.rdb3.TabIndex = 2;
            this.rdb3.Text = "Nonresidential";
            this.rdb3.UseVisualStyleBackColor = true;
            this.rdb3.CheckedChanged += new System.EventHandler(this.rdb3_CheckedChanged);
            // 
            // rdb2
            // 
            this.rdb2.AutoSize = true;
            this.rdb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb2.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb2.Location = new System.Drawing.Point(218, 13);
            this.rdb2.Name = "rdb2";
            this.rdb2.Size = new System.Drawing.Size(134, 20);
            this.rdb2.TabIndex = 1;
            this.rdb2.Text = "State and Local";
            this.rdb2.UseVisualStyleBackColor = true;
            this.rdb2.CheckedChanged += new System.EventHandler(this.rdb2_CheckedChanged);
            // 
            // rdb1
            // 
            this.rdb1.AutoSize = true;
            this.rdb1.Checked = true;
            this.rdb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb1.ForeColor = System.Drawing.Color.DarkBlue;
            this.rdb1.Location = new System.Drawing.Point(120, 13);
            this.rdb1.Name = "rdb1";
            this.rdb1.Size = new System.Drawing.Size(92, 20);
            this.rdb1.TabIndex = 0;
            this.rdb1.TabStop = true;
            this.rdb1.Text = "All Cases";
            this.rdb1.UseVisualStyleBackColor = true;
            this.rdb1.CheckedChanged += new System.EventHandler(this.rdb1_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmProjectAudit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1216, 870);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabs);
            this.Name = "frmProjectAudit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProjectAudit_FormClosing);
            this.Load += new System.EventHandler(this.frmProjectAudit_Load);
            this.Controls.SetChildIndex(this.tabs, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItem)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgVip)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgItem;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearchItem;
        private System.Windows.Forms.ComboBox cbValueItem;
        private System.Windows.Forms.TextBox txtValueItem;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.Label lbldatef;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbldatef1;
        private System.Windows.Forms.Button btnClear1;
        private System.Windows.Forms.Button btnSearchVip;
        private System.Windows.Forms.TextBox txtValueVip;
        private System.Windows.Forms.ComboBox cbVip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgVip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbValueVip;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdb4;
        private System.Windows.Forms.RadioButton rdb3;
        private System.Windows.Forms.RadioButton rdb2;
        private System.Windows.Forms.RadioButton rdb1;
        private System.Windows.Forms.RadioButton rdb5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton rdb11;
        private System.Windows.Forms.RadioButton rdb10;
        private System.Windows.Forms.RadioButton rdb9;
        private System.Windows.Forms.RadioButton rdb8;
        private System.Windows.Forms.RadioButton rdb7;
        private System.Windows.Forms.RadioButton rdb6;
    }
}
