namespace Cprs
{
    partial class frmTsarSeriesSelectionPopup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rb44 = new System.Windows.Forms.RadioButton();
            this.rb43 = new System.Windows.Forms.RadioButton();
            this.rb42 = new System.Windows.Forms.RadioButton();
            this.rb3Seasonal = new System.Windows.Forms.RadioButton();
            this.rb3Adjusted = new System.Windows.Forms.RadioButton();
            this.rb3Unadjusted = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rb220 = new System.Windows.Forms.RadioButton();
            this.rb219 = new System.Windows.Forms.RadioButton();
            this.rb216 = new System.Windows.Forms.RadioButton();
            this.rb215 = new System.Windows.Forms.RadioButton();
            this.rb214 = new System.Windows.Forms.RadioButton();
            this.rb213 = new System.Windows.Forms.RadioButton();
            this.rb212 = new System.Windows.Forms.RadioButton();
            this.rb211 = new System.Windows.Forms.RadioButton();
            this.rb210 = new System.Windows.Forms.RadioButton();
            this.rb209 = new System.Windows.Forms.RadioButton();
            this.rb208 = new System.Windows.Forms.RadioButton();
            this.rb207 = new System.Windows.Forms.RadioButton();
            this.rb206 = new System.Windows.Forms.RadioButton();
            this.rb205 = new System.Windows.Forms.RadioButton();
            this.rb204 = new System.Windows.Forms.RadioButton();
            this.rb203 = new System.Windows.Forms.RadioButton();
            this.rb202 = new System.Windows.Forms.RadioButton();
            this.rb201 = new System.Windows.Forms.RadioButton();
            this.rb2NR = new System.Windows.Forms.RadioButton();
            this.rb200 = new System.Windows.Forms.RadioButton();
            this.rb2XX = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbUtilities = new System.Windows.Forms.RadioButton();
            this.rb1Federal = new System.Windows.Forms.RadioButton();
            this.rb1State = new System.Windows.Forms.RadioButton();
            this.rb1Public = new System.Windows.Forms.RadioButton();
            this.rb1Private = new System.Windows.Forms.RadioButton();
            this.rb1Total = new System.Windows.Forms.RadioButton();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSeries = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 523);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.rb3Seasonal);
            this.groupBox4.Controls.Add(this.rb3Adjusted);
            this.groupBox4.Controls.Add(this.rb3Unadjusted);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(347, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(137, 202);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Type";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rb44);
            this.groupBox5.Controls.Add(this.rb43);
            this.groupBox5.Controls.Add(this.rb42);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(17, 88);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(107, 100);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Select Level";
            // 
            // rb44
            // 
            this.rb44.AutoSize = true;
            this.rb44.Location = new System.Drawing.Point(6, 69);
            this.rb44.Name = "rb44";
            this.rb44.Size = new System.Drawing.Size(60, 17);
            this.rb44.TabIndex = 3;
            this.rb44.TabStop = true;
            this.rb44.Text = "4 Digits";
            this.rb44.UseVisualStyleBackColor = true;
            this.rb44.CheckedChanged += new System.EventHandler(this.rb44_CheckedChanged);
            // 
            // rb43
            // 
            this.rb43.AutoSize = true;
            this.rb43.Location = new System.Drawing.Point(6, 46);
            this.rb43.Name = "rb43";
            this.rb43.Size = new System.Drawing.Size(60, 17);
            this.rb43.TabIndex = 2;
            this.rb43.TabStop = true;
            this.rb43.Text = "3 Digits";
            this.rb43.UseVisualStyleBackColor = true;
            this.rb43.CheckedChanged += new System.EventHandler(this.rb43_CheckedChanged);
            // 
            // rb42
            // 
            this.rb42.AutoSize = true;
            this.rb42.Location = new System.Drawing.Point(6, 23);
            this.rb42.Name = "rb42";
            this.rb42.Size = new System.Drawing.Size(60, 17);
            this.rb42.TabIndex = 1;
            this.rb42.TabStop = true;
            this.rb42.Text = "2 Digits";
            this.rb42.UseVisualStyleBackColor = true;
            this.rb42.CheckedChanged += new System.EventHandler(this.rb42_CheckedChanged);
            // 
            // rb3Seasonal
            // 
            this.rb3Seasonal.AutoSize = true;
            this.rb3Seasonal.Location = new System.Drawing.Point(17, 65);
            this.rb3Seasonal.Name = "rb3Seasonal";
            this.rb3Seasonal.Size = new System.Drawing.Size(107, 17);
            this.rb3Seasonal.TabIndex = 3;
            this.rb3Seasonal.TabStop = true;
            this.rb3Seasonal.Text = "Seasonal Factors";
            this.rb3Seasonal.UseVisualStyleBackColor = true;
            this.rb3Seasonal.CheckedChanged += new System.EventHandler(this.rb3Seasonal_CheckedChanged);
            // 
            // rb3Adjusted
            // 
            this.rb3Adjusted.AutoSize = true;
            this.rb3Adjusted.Location = new System.Drawing.Point(17, 42);
            this.rb3Adjusted.Name = "rb3Adjusted";
            this.rb3Adjusted.Size = new System.Drawing.Size(66, 17);
            this.rb3Adjusted.TabIndex = 2;
            this.rb3Adjusted.TabStop = true;
            this.rb3Adjusted.Text = "Adjusted";
            this.rb3Adjusted.UseVisualStyleBackColor = true;
            this.rb3Adjusted.CheckedChanged += new System.EventHandler(this.rb3Adjusted_CheckedChanged);
            // 
            // rb3Unadjusted
            // 
            this.rb3Unadjusted.AutoSize = true;
            this.rb3Unadjusted.Location = new System.Drawing.Point(17, 19);
            this.rb3Unadjusted.Name = "rb3Unadjusted";
            this.rb3Unadjusted.Size = new System.Drawing.Size(79, 17);
            this.rb3Unadjusted.TabIndex = 1;
            this.rb3Unadjusted.TabStop = true;
            this.rb3Unadjusted.Text = "Unadjusted";
            this.rb3Unadjusted.UseVisualStyleBackColor = true;
            this.rb3Unadjusted.CheckedChanged += new System.EventHandler(this.rb3Unadjusted_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rb220);
            this.groupBox3.Controls.Add(this.rb219);
            this.groupBox3.Controls.Add(this.rb216);
            this.groupBox3.Controls.Add(this.rb215);
            this.groupBox3.Controls.Add(this.rb214);
            this.groupBox3.Controls.Add(this.rb213);
            this.groupBox3.Controls.Add(this.rb212);
            this.groupBox3.Controls.Add(this.rb211);
            this.groupBox3.Controls.Add(this.rb210);
            this.groupBox3.Controls.Add(this.rb209);
            this.groupBox3.Controls.Add(this.rb208);
            this.groupBox3.Controls.Add(this.rb207);
            this.groupBox3.Controls.Add(this.rb206);
            this.groupBox3.Controls.Add(this.rb205);
            this.groupBox3.Controls.Add(this.rb204);
            this.groupBox3.Controls.Add(this.rb203);
            this.groupBox3.Controls.Add(this.rb202);
            this.groupBox3.Controls.Add(this.rb201);
            this.groupBox3.Controls.Add(this.rb2NR);
            this.groupBox3.Controls.Add(this.rb200);
            this.groupBox3.Controls.Add(this.rb2XX);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(142, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 501);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Type of Construction";
            // 
            // rb220
            // 
            this.rb220.AutoSize = true;
            this.rb220.Location = new System.Drawing.Point(7, 478);
            this.rb220.Name = "rb220";
            this.rb220.Size = new System.Drawing.Size(108, 17);
            this.rb220.TabIndex = 21;
            this.rb220.TabStop = true;
            this.rb220.Text = "20 Manufacturing";
            this.rb220.UseVisualStyleBackColor = true;
            this.rb220.CheckedChanged += new System.EventHandler(this.rb220_CheckedChanged);
            // 
            // rb219
            // 
            this.rb219.AutoSize = true;
            this.rb219.Location = new System.Drawing.Point(6, 456);
            this.rb219.Name = "rb219";
            this.rb219.Size = new System.Drawing.Size(160, 17);
            this.rb219.TabIndex = 20;
            this.rb219.TabStop = true;
            this.rb219.Text = "19 Regulated Transportation";
            this.rb219.UseVisualStyleBackColor = true;
            this.rb219.CheckedChanged += new System.EventHandler(this.rb219_CheckedChanged);
            // 
            // rb216
            // 
            this.rb216.AutoSize = true;
            this.rb216.Location = new System.Drawing.Point(6, 433);
            this.rb216.Name = "rb216";
            this.rb216.Size = new System.Drawing.Size(122, 17);
            this.rb216.TabIndex = 19;
            this.rb216.TabStop = true;
            this.rb216.Text = "16 Regulated Power";
            this.rb216.UseVisualStyleBackColor = true;
            this.rb216.CheckedChanged += new System.EventHandler(this.rb216_CheckedChanged);
            // 
            // rb215
            // 
            this.rb215.AutoSize = true;
            this.rb215.Location = new System.Drawing.Point(6, 410);
            this.rb215.Name = "rb215";
            this.rb215.Size = new System.Drawing.Size(189, 17);
            this.rb215.TabIndex = 18;
            this.rb215.TabStop = true;
            this.rb215.Text = "15 Conservation and Development";
            this.rb215.UseVisualStyleBackColor = true;
            this.rb215.CheckedChanged += new System.EventHandler(this.rb215_CheckedChanged);
            // 
            // rb214
            // 
            this.rb214.AutoSize = true;
            this.rb214.Location = new System.Drawing.Point(6, 387);
            this.rb214.Name = "rb214";
            this.rb214.Size = new System.Drawing.Size(104, 17);
            this.rb214.TabIndex = 17;
            this.rb214.TabStop = true;
            this.rb214.Text = "14 Water Supply";
            this.rb214.UseVisualStyleBackColor = true;
            this.rb214.CheckedChanged += new System.EventHandler(this.rb214_CheckedChanged);
            // 
            // rb213
            // 
            this.rb213.AutoSize = true;
            this.rb213.Location = new System.Drawing.Point(6, 364);
            this.rb213.Name = "rb213";
            this.rb213.Size = new System.Drawing.Size(175, 17);
            this.rb213.TabIndex = 16;
            this.rb213.TabStop = true;
            this.rb213.Text = "13 Sewage and Waste disposal";
            this.rb213.UseVisualStyleBackColor = true;
            this.rb213.CheckedChanged += new System.EventHandler(this.rb213_CheckedChanged);
            // 
            // rb212
            // 
            this.rb212.AutoSize = true;
            this.rb212.Location = new System.Drawing.Point(6, 341);
            this.rb212.Name = "rb212";
            this.rb212.Size = new System.Drawing.Size(133, 17);
            this.rb212.TabIndex = 15;
            this.rb212.TabStop = true;
            this.rb212.Text = "12 Highway and Street";
            this.rb212.UseVisualStyleBackColor = true;
            this.rb212.CheckedChanged += new System.EventHandler(this.rb212_CheckedChanged);
            // 
            // rb211
            // 
            this.rb211.AutoSize = true;
            this.rb211.Location = new System.Drawing.Point(6, 318);
            this.rb211.Name = "rb211";
            this.rb211.Size = new System.Drawing.Size(70, 17);
            this.rb211.TabIndex = 14;
            this.rb211.TabStop = true;
            this.rb211.Text = "11 Power";
            this.rb211.UseVisualStyleBackColor = true;
            this.rb211.CheckedChanged += new System.EventHandler(this.rb211_CheckedChanged);
            // 
            // rb210
            // 
            this.rb210.AutoSize = true;
            this.rb210.Location = new System.Drawing.Point(6, 295);
            this.rb210.Name = "rb210";
            this.rb210.Size = new System.Drawing.Size(112, 17);
            this.rb210.TabIndex = 13;
            this.rb210.TabStop = true;
            this.rb210.Text = "10 Communication";
            this.rb210.UseVisualStyleBackColor = true;
            this.rb210.CheckedChanged += new System.EventHandler(this.rb210_CheckedChanged);
            // 
            // rb209
            // 
            this.rb209.AutoSize = true;
            this.rb209.Location = new System.Drawing.Point(6, 272);
            this.rb209.Name = "rb209";
            this.rb209.Size = new System.Drawing.Size(108, 17);
            this.rb209.TabIndex = 12;
            this.rb209.TabStop = true;
            this.rb209.Text = "09 Transportation";
            this.rb209.UseVisualStyleBackColor = true;
            this.rb209.CheckedChanged += new System.EventHandler(this.rb209_CheckedChanged);
            // 
            // rb208
            // 
            this.rb208.AutoSize = true;
            this.rb208.Location = new System.Drawing.Point(6, 249);
            this.rb208.Name = "rb208";
            this.rb208.Size = new System.Drawing.Size(171, 17);
            this.rb208.TabIndex = 11;
            this.rb208.TabStop = true;
            this.rb208.Text = "08 Amusement and Recreation";
            this.rb208.UseVisualStyleBackColor = true;
            this.rb208.CheckedChanged += new System.EventHandler(this.rb208_CheckedChanged);
            // 
            // rb207
            // 
            this.rb207.AutoSize = true;
            this.rb207.Location = new System.Drawing.Point(6, 226);
            this.rb207.Name = "rb207";
            this.rb207.Size = new System.Drawing.Size(102, 17);
            this.rb207.TabIndex = 10;
            this.rb207.TabStop = true;
            this.rb207.Text = "07 Public Safety";
            this.rb207.UseVisualStyleBackColor = true;
            this.rb207.CheckedChanged += new System.EventHandler(this.rb207_CheckedChanged);
            // 
            // rb206
            // 
            this.rb206.AutoSize = true;
            this.rb206.Location = new System.Drawing.Point(6, 203);
            this.rb206.Name = "rb206";
            this.rb206.Size = new System.Drawing.Size(83, 17);
            this.rb206.TabIndex = 9;
            this.rb206.TabStop = true;
            this.rb206.Text = "06 Religious";
            this.rb206.UseVisualStyleBackColor = true;
            this.rb206.CheckedChanged += new System.EventHandler(this.rb206_CheckedChanged);
            // 
            // rb205
            // 
            this.rb205.AutoSize = true;
            this.rb205.Location = new System.Drawing.Point(6, 180);
            this.rb205.Name = "rb205";
            this.rb205.Size = new System.Drawing.Size(96, 17);
            this.rb205.TabIndex = 8;
            this.rb205.TabStop = true;
            this.rb205.Text = "05 Educational";
            this.rb205.UseVisualStyleBackColor = true;
            this.rb205.CheckedChanged += new System.EventHandler(this.rb205_CheckedChanged);
            // 
            // rb204
            // 
            this.rb204.AutoSize = true;
            this.rb204.Location = new System.Drawing.Point(6, 157);
            this.rb204.Name = "rb204";
            this.rb204.Size = new System.Drawing.Size(96, 17);
            this.rb204.TabIndex = 7;
            this.rb204.TabStop = true;
            this.rb204.Text = "04 Health Care";
            this.rb204.UseVisualStyleBackColor = true;
            this.rb204.CheckedChanged += new System.EventHandler(this.rb204_CheckedChanged);
            // 
            // rb203
            // 
            this.rb203.AutoSize = true;
            this.rb203.Location = new System.Drawing.Point(6, 134);
            this.rb203.Name = "rb203";
            this.rb203.Size = new System.Drawing.Size(94, 17);
            this.rb203.TabIndex = 6;
            this.rb203.TabStop = true;
            this.rb203.Text = "03 Commercial";
            this.rb203.UseVisualStyleBackColor = true;
            this.rb203.CheckedChanged += new System.EventHandler(this.rb203_CheckedChanged);
            // 
            // rb202
            // 
            this.rb202.AutoSize = true;
            this.rb202.Location = new System.Drawing.Point(6, 111);
            this.rb202.Name = "rb202";
            this.rb202.Size = new System.Drawing.Size(68, 17);
            this.rb202.TabIndex = 5;
            this.rb202.TabStop = true;
            this.rb202.Text = "02 Office";
            this.rb202.UseVisualStyleBackColor = true;
            this.rb202.CheckedChanged += new System.EventHandler(this.rb202_CheckedChanged);
            // 
            // rb201
            // 
            this.rb201.AutoSize = true;
            this.rb201.Location = new System.Drawing.Point(6, 88);
            this.rb201.Name = "rb201";
            this.rb201.Size = new System.Drawing.Size(78, 17);
            this.rb201.TabIndex = 4;
            this.rb201.TabStop = true;
            this.rb201.Text = "01 Lodging";
            this.rb201.UseVisualStyleBackColor = true;
            this.rb201.CheckedChanged += new System.EventHandler(this.rb201_CheckedChanged);
            // 
            // rb2NR
            // 
            this.rb2NR.AutoSize = true;
            this.rb2NR.Location = new System.Drawing.Point(6, 65);
            this.rb2NR.Name = "rb2NR";
            this.rb2NR.Size = new System.Drawing.Size(119, 17);
            this.rb2NR.TabIndex = 3;
            this.rb2NR.TabStop = true;
            this.rb2NR.Text = "NR Non Residential";
            this.rb2NR.UseVisualStyleBackColor = true;
            this.rb2NR.CheckedChanged += new System.EventHandler(this.rb2NR_CheckedChanged);
            // 
            // rb200
            // 
            this.rb200.AutoSize = true;
            this.rb200.Location = new System.Drawing.Point(6, 42);
            this.rb200.Name = "rb200";
            this.rb200.Size = new System.Drawing.Size(92, 17);
            this.rb200.TabIndex = 2;
            this.rb200.TabStop = true;
            this.rb200.Text = "00 Residential";
            this.rb200.UseVisualStyleBackColor = true;
            this.rb200.CheckedChanged += new System.EventHandler(this.rb200_CheckedChanged);
            // 
            // rb2XX
            // 
            this.rb2XX.AutoSize = true;
            this.rb2XX.Location = new System.Drawing.Point(6, 19);
            this.rb2XX.Name = "rb2XX";
            this.rb2XX.Size = new System.Drawing.Size(66, 17);
            this.rb2XX.TabIndex = 1;
            this.rb2XX.TabStop = true;
            this.rb2XX.Text = "XX Total";
            this.rb2XX.UseVisualStyleBackColor = true;
            this.rb2XX.CheckedChanged += new System.EventHandler(this.rb2XX_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbUtilities);
            this.groupBox2.Controls.Add(this.rb1Federal);
            this.groupBox2.Controls.Add(this.rb1State);
            this.groupBox2.Controls.Add(this.rb1Public);
            this.groupBox2.Controls.Add(this.rb1Private);
            this.groupBox2.Controls.Add(this.rb1Total);
            this.groupBox2.Location = new System.Drawing.Point(15, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(121, 219);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Survey";
            // 
            // rbUtilities
            // 
            this.rbUtilities.AutoSize = true;
            this.rbUtilities.Location = new System.Drawing.Point(6, 134);
            this.rbUtilities.Name = "rbUtilities";
            this.rbUtilities.Size = new System.Drawing.Size(58, 17);
            this.rbUtilities.TabIndex = 5;
            this.rbUtilities.Text = "Utilities";
            this.rbUtilities.UseVisualStyleBackColor = true;
            this.rbUtilities.CheckedChanged += new System.EventHandler(this.rbUtilities_CheckedChanged);
            // 
            // rb1Federal
            // 
            this.rb1Federal.AutoSize = true;
            this.rb1Federal.Location = new System.Drawing.Point(6, 111);
            this.rb1Federal.Name = "rb1Federal";
            this.rb1Federal.Size = new System.Drawing.Size(60, 17);
            this.rb1Federal.TabIndex = 4;
            this.rb1Federal.Text = "Federal";
            this.rb1Federal.UseVisualStyleBackColor = true;
            this.rb1Federal.CheckedChanged += new System.EventHandler(this.rb1Federal_CheckedChanged);
            // 
            // rb1State
            // 
            this.rb1State.AutoSize = true;
            this.rb1State.Location = new System.Drawing.Point(6, 88);
            this.rb1State.Name = "rb1State";
            this.rb1State.Size = new System.Drawing.Size(100, 17);
            this.rb1State.TabIndex = 3;
            this.rb1State.Text = "State and Local";
            this.rb1State.UseVisualStyleBackColor = true;
            this.rb1State.CheckedChanged += new System.EventHandler(this.rb1State_CheckedChanged);
            // 
            // rb1Public
            // 
            this.rb1Public.AutoSize = true;
            this.rb1Public.Location = new System.Drawing.Point(6, 65);
            this.rb1Public.Name = "rb1Public";
            this.rb1Public.Size = new System.Drawing.Size(54, 17);
            this.rb1Public.TabIndex = 2;
            this.rb1Public.Text = "Public";
            this.rb1Public.UseVisualStyleBackColor = true;
            this.rb1Public.CheckedChanged += new System.EventHandler(this.rb1Public_CheckedChanged);
            // 
            // rb1Private
            // 
            this.rb1Private.AutoSize = true;
            this.rb1Private.Location = new System.Drawing.Point(6, 42);
            this.rb1Private.Name = "rb1Private";
            this.rb1Private.Size = new System.Drawing.Size(58, 17);
            this.rb1Private.TabIndex = 1;
            this.rb1Private.Text = "Private";
            this.rb1Private.UseVisualStyleBackColor = true;
            this.rb1Private.CheckedChanged += new System.EventHandler(this.rb1Private_CheckedChanged);
            // 
            // rb1Total
            // 
            this.rb1Total.AutoSize = true;
            this.rb1Total.Location = new System.Drawing.Point(6, 19);
            this.rb1Total.Name = "rb1Total";
            this.rb1Total.Size = new System.Drawing.Size(49, 17);
            this.rb1Total.TabIndex = 0;
            this.rb1Total.Text = "Total";
            this.rb1Total.UseVisualStyleBackColor = true;
            this.rb1Total.CheckedChanged += new System.EventHandler(this.rb1Total_CheckedChanged);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeColumns = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgData.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Location = new System.Drawing.Point(551, 109);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.Size = new System.Drawing.Size(133, 491);
            this.dgData.TabIndex = 3;
            this.dgData.DoubleClick += new System.EventHandler(this.dgData_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(246, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Select Tsar Series";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(524, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Or Enter Series:";
            // 
            // txtSeries
            // 
            this.txtSeries.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSeries.Location = new System.Drawing.Point(551, 77);
            this.txtSeries.MaxLength = 8;
            this.txtSeries.Name = "txtSeries";
            this.txtSeries.Size = new System.Drawing.Size(133, 20);
            this.txtSeries.TabIndex = 13;
            this.txtSeries.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeries_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(293, 634);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 27);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmTsarSeriesSelectionPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 681);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSeries);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTsarSeriesSelectionPopup";
            this.Text = "Select Tsar Series";
            this.Load += new System.EventHandler(this.frmTsarSeriesSelectionPopup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rb44;
        private System.Windows.Forms.RadioButton rb43;
        private System.Windows.Forms.RadioButton rb42;
        private System.Windows.Forms.RadioButton rb3Seasonal;
        private System.Windows.Forms.RadioButton rb3Adjusted;
        private System.Windows.Forms.RadioButton rb3Unadjusted;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb1Federal;
        private System.Windows.Forms.RadioButton rb1State;
        private System.Windows.Forms.RadioButton rb1Public;
        private System.Windows.Forms.RadioButton rb1Private;
        private System.Windows.Forms.RadioButton rb1Total;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rb215;
        private System.Windows.Forms.RadioButton rb214;
        private System.Windows.Forms.RadioButton rb213;
        private System.Windows.Forms.RadioButton rb212;
        private System.Windows.Forms.RadioButton rb211;
        private System.Windows.Forms.RadioButton rb210;
        private System.Windows.Forms.RadioButton rb209;
        private System.Windows.Forms.RadioButton rb208;
        private System.Windows.Forms.RadioButton rb207;
        private System.Windows.Forms.RadioButton rb206;
        private System.Windows.Forms.RadioButton rb205;
        private System.Windows.Forms.RadioButton rb204;
        private System.Windows.Forms.RadioButton rb203;
        private System.Windows.Forms.RadioButton rb202;
        private System.Windows.Forms.RadioButton rb201;
        private System.Windows.Forms.RadioButton rb2NR;
        private System.Windows.Forms.RadioButton rb200;
        private System.Windows.Forms.RadioButton rb2XX;
        private System.Windows.Forms.RadioButton rb220;
        private System.Windows.Forms.RadioButton rb219;
        private System.Windows.Forms.RadioButton rb216;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeries;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.RadioButton rbUtilities;
    }
}