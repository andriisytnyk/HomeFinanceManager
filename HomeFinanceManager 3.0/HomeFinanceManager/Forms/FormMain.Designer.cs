namespace HomeFinanceManager.Forms
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpAdd = new System.Windows.Forms.TabPage();
            this.gbAdd = new System.Windows.Forms.GroupBox();
            this.lbName = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.tbAddAccountSum = new System.Windows.Forms.TextBox();
            this.cbAddDate = new System.Windows.Forms.ComboBox();
            this.tbAddName = new System.Windows.Forms.TextBox();
            this.cbAddAccounts = new System.Windows.Forms.ComboBox();
            this.tbAddCost = new System.Windows.Forms.TextBox();
            this.lbCost = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbAddCount = new System.Windows.Forms.TextBox();
            this.lbImportanceAdd = new System.Windows.Forms.Label();
            this.cbAddType = new System.Windows.Forms.ComboBox();
            this.lbTypeAdd = new System.Windows.Forms.Label();
            this.cbAddImportance = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbSearchAdd = new System.Windows.Forms.TextBox();
            this.lbSearchAdd = new System.Windows.Forms.Label();
            this.dgvRight = new System.Windows.Forms.DataGridView();
            this.clRightName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRightPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRightCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRightSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRightType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRightImportance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRightAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRightDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLeft = new System.Windows.Forms.DataGridView();
            this.clLeftName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clLeftPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clLeftCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clLeftSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clLeftType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clLeftImportance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clLeftAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.gbChange = new System.Windows.Forms.GroupBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.tbSumForTransfer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAccountForTransferSum = new System.Windows.Forms.TextBox();
            this.tbCurrentAccountSum = new System.Windows.Forms.TextBox();
            this.lbAccountSum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCurrentAccount = new System.Windows.Forms.Label();
            this.cbCurrentAccount = new System.Windows.Forms.ComboBox();
            this.cbAccountForTransfer = new System.Windows.Forms.ComboBox();
            this.lbAccountForTransfer = new System.Windows.Forms.Label();
            this.tbCurrentSum = new System.Windows.Forms.TextBox();
            this.lbCurrentSum = new System.Windows.Forms.Label();
            this.btnChangeSum = new System.Windows.Forms.Button();
            this.lbChangeSum1 = new System.Windows.Forms.Label();
            this.tbChangeSum = new System.Windows.Forms.TextBox();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.lbFilter = new System.Windows.Forms.Label();
            this.gbAccount = new System.Windows.Forms.GroupBox();
            this.chboxAccountInfo = new System.Windows.Forms.CheckBox();
            this.clbAccountInfo = new System.Windows.Forms.CheckedListBox();
            this.gbDate = new System.Windows.Forms.GroupBox();
            this.lbLastTime = new System.Windows.Forms.Label();
            this.cbLastTime = new System.Windows.Forms.ComboBox();
            this.lbDay = new System.Windows.Forms.Label();
            this.lbMonth = new System.Windows.Forms.Label();
            this.lbYear = new System.Windows.Forms.Label();
            this.cbDay = new System.Windows.Forms.ComboBox();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.gbImportance = new System.Windows.Forms.GroupBox();
            this.chboxImportanceInfo = new System.Windows.Forms.CheckBox();
            this.clbImportanceInfo = new System.Windows.Forms.CheckedListBox();
            this.gbSum = new System.Windows.Forms.GroupBox();
            this.lbMax = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.nudMax = new System.Windows.Forms.NumericUpDown();
            this.gbType = new System.Windows.Forms.GroupBox();
            this.chboxTypeInfo = new System.Windows.Forms.CheckBox();
            this.clbTypeInfo = new System.Windows.Forms.CheckedListBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnCancelInfo = new System.Windows.Forms.Button();
            this.listbFilter = new System.Windows.Forms.ListBox();
            this.lbSearchInfo = new System.Windows.Forms.Label();
            this.tbSearchInfo = new System.Windows.Forms.TextBox();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.clShowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clShowPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clShowCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clShowSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clShowType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clShowImportance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clShowAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clShowDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpGraphic = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkedListBox3 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.cmsDgv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDgvEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDgvDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.Hint = new System.Windows.Forms.ToolTip(this.components);
            this.IconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl.SuspendLayout();
            this.tpAdd.SuspendLayout();
            this.gbAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).BeginInit();
            this.gbChange.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.gbFilters.SuspendLayout();
            this.gbAccount.SuspendLayout();
            this.gbDate.SuspendLayout();
            this.gbImportance.SuspendLayout();
            this.gbSum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).BeginInit();
            this.gbType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            this.tpGraphic.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.cmsDgv.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tpAdd);
            this.tabControl.Controls.Add(this.tpInfo);
            this.tabControl.Controls.Add(this.tpGraphic);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1354, 693);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabStop = false;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tpAdd
            // 
            this.tpAdd.BackColor = System.Drawing.Color.Lavender;
            this.tpAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpAdd.Controls.Add(this.gbAdd);
            this.tpAdd.Controls.Add(this.tbSearchAdd);
            this.tpAdd.Controls.Add(this.lbSearchAdd);
            this.tpAdd.Controls.Add(this.dgvRight);
            this.tpAdd.Controls.Add(this.dgvLeft);
            this.tpAdd.Controls.Add(this.btnLeft);
            this.tpAdd.Controls.Add(this.btnRight);
            this.tpAdd.Controls.Add(this.gbChange);
            this.tpAdd.Location = new System.Drawing.Point(4, 22);
            this.tpAdd.Name = "tpAdd";
            this.tpAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdd.Size = new System.Drawing.Size(1346, 667);
            this.tpAdd.TabIndex = 0;
            this.tpAdd.Text = "Добавление/изменение";
            // 
            // gbAdd
            // 
            this.gbAdd.Controls.Add(this.lbName);
            this.gbAdd.Controls.Add(this.lbDate);
            this.gbAdd.Controls.Add(this.tbAddAccountSum);
            this.gbAdd.Controls.Add(this.cbAddDate);
            this.gbAdd.Controls.Add(this.tbAddName);
            this.gbAdd.Controls.Add(this.cbAddAccounts);
            this.gbAdd.Controls.Add(this.tbAddCost);
            this.gbAdd.Controls.Add(this.lbCost);
            this.gbAdd.Controls.Add(this.lbCount);
            this.gbAdd.Controls.Add(this.btnSave);
            this.gbAdd.Controls.Add(this.tbAddCount);
            this.gbAdd.Controls.Add(this.lbImportanceAdd);
            this.gbAdd.Controls.Add(this.cbAddType);
            this.gbAdd.Controls.Add(this.lbTypeAdd);
            this.gbAdd.Controls.Add(this.cbAddImportance);
            this.gbAdd.Controls.Add(this.btnAdd);
            this.gbAdd.Location = new System.Drawing.Point(637, -1);
            this.gbAdd.Name = "gbAdd";
            this.gbAdd.Size = new System.Drawing.Size(705, 137);
            this.gbAdd.TabIndex = 67;
            this.gbAdd.TabStop = false;
            this.gbAdd.Text = "Добавление";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(151, 20);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(57, 13);
            this.lbName.TabIndex = 48;
            this.lbName.Text = "Название";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Location = new System.Drawing.Point(521, 62);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(33, 13);
            this.lbDate.TabIndex = 65;
            this.lbDate.Text = "Дата";
            // 
            // tbAddAccountSum
            // 
            this.tbAddAccountSum.Enabled = false;
            this.tbAddAccountSum.Location = new System.Drawing.Point(182, 83);
            this.tbAddAccountSum.Name = "tbAddAccountSum";
            this.tbAddAccountSum.Size = new System.Drawing.Size(55, 20);
            this.tbAddAccountSum.TabIndex = 66;
            this.tbAddAccountSum.TabStop = false;
            // 
            // cbAddDate
            // 
            this.cbAddDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddDate.FormattingEnabled = true;
            this.cbAddDate.IntegralHeight = false;
            this.cbAddDate.Location = new System.Drawing.Point(357, 58);
            this.cbAddDate.Name = "cbAddDate";
            this.cbAddDate.Size = new System.Drawing.Size(160, 21);
            this.cbAddDate.TabIndex = 52;
            // 
            // tbAddName
            // 
            this.tbAddName.Location = new System.Drawing.Point(210, 16);
            this.tbAddName.Name = "tbAddName";
            this.tbAddName.Size = new System.Drawing.Size(140, 20);
            this.tbAddName.TabIndex = 45;
            // 
            // cbAddAccounts
            // 
            this.cbAddAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddAccounts.FormattingEnabled = true;
            this.cbAddAccounts.Location = new System.Drawing.Point(240, 82);
            this.cbAddAccounts.Name = "cbAddAccounts";
            this.cbAddAccounts.Size = new System.Drawing.Size(110, 21);
            this.cbAddAccounts.TabIndex = 54;
            this.cbAddAccounts.SelectedIndexChanged += new System.EventHandler(this.cbAccountsAdd_SelectedIndexChanged);
            // 
            // tbAddCost
            // 
            this.tbAddCost.Location = new System.Drawing.Point(210, 37);
            this.tbAddCost.Name = "tbAddCost";
            this.tbAddCost.Size = new System.Drawing.Size(140, 20);
            this.tbAddCost.TabIndex = 46;
            this.tbAddCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCost_KeyPress);
            // 
            // lbCost
            // 
            this.lbCost.AutoSize = true;
            this.lbCost.Location = new System.Drawing.Point(84, 41);
            this.lbCost.Name = "lbCost";
            this.lbCost.Size = new System.Drawing.Size(124, 13);
            this.lbCost.TabIndex = 50;
            this.lbCost.Text = "Стоимость (за 1 шт/кг)";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(104, 62);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(104, 13);
            this.lbCount.TabIndex = 64;
            this.lbCount.Text = "Количество (шт/кг)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(304, 109);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 59;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbAddCount
            // 
            this.tbAddCount.Location = new System.Drawing.Point(210, 58);
            this.tbAddCount.Name = "tbAddCount";
            this.tbAddCount.Size = new System.Drawing.Size(140, 20);
            this.tbAddCount.TabIndex = 47;
            this.tbAddCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCount_KeyPress);
            // 
            // lbImportanceAdd
            // 
            this.lbImportanceAdd.AutoSize = true;
            this.lbImportanceAdd.Location = new System.Drawing.Point(521, 41);
            this.lbImportanceAdd.Name = "lbImportanceAdd";
            this.lbImportanceAdd.Size = new System.Drawing.Size(101, 13);
            this.lbImportanceAdd.TabIndex = 55;
            this.lbImportanceAdd.Text = "Степень важности";
            // 
            // cbAddType
            // 
            this.cbAddType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddType.FormattingEnabled = true;
            this.cbAddType.IntegralHeight = false;
            this.cbAddType.Items.AddRange(new object[] {
            "Продукты",
            "Хоз.товары",
            "Услуги",
            "(Добавить новый тип)"});
            this.cbAddType.Location = new System.Drawing.Point(357, 16);
            this.cbAddType.Name = "cbAddType";
            this.cbAddType.Size = new System.Drawing.Size(160, 21);
            this.cbAddType.TabIndex = 49;
            this.cbAddType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // lbTypeAdd
            // 
            this.lbTypeAdd.AutoSize = true;
            this.lbTypeAdd.Location = new System.Drawing.Point(521, 20);
            this.lbTypeAdd.Name = "lbTypeAdd";
            this.lbTypeAdd.Size = new System.Drawing.Size(26, 13);
            this.lbTypeAdd.TabIndex = 53;
            this.lbTypeAdd.Text = "Тип";
            // 
            // cbAddImportance
            // 
            this.cbAddImportance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddImportance.FormattingEnabled = true;
            this.cbAddImportance.IntegralHeight = false;
            this.cbAddImportance.Items.AddRange(new object[] {
            "Первостепенной важности",
            "Средней важности",
            "Малой важности",
            "Неважно"});
            this.cbAddImportance.Location = new System.Drawing.Point(357, 37);
            this.cbAddImportance.Name = "cbAddImportance";
            this.cbAddImportance.Size = new System.Drawing.Size(160, 21);
            this.cbAddImportance.TabIndex = 51;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(379, 81);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 22);
            this.btnAdd.TabIndex = 56;
            this.btnAdd.Text = "Добавить в список";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbSearchAdd
            // 
            this.tbSearchAdd.Location = new System.Drawing.Point(42, 116);
            this.tbSearchAdd.Name = "tbSearchAdd";
            this.tbSearchAdd.Size = new System.Drawing.Size(138, 20);
            this.tbSearchAdd.TabIndex = 63;
            this.tbSearchAdd.TabStop = false;
            // 
            // lbSearchAdd
            // 
            this.lbSearchAdd.AutoSize = true;
            this.lbSearchAdd.Location = new System.Drawing.Point(6, 119);
            this.lbSearchAdd.Name = "lbSearchAdd";
            this.lbSearchAdd.Size = new System.Drawing.Size(39, 13);
            this.lbSearchAdd.TabIndex = 62;
            this.lbSearchAdd.Text = "Поиск";
            // 
            // dgvRight
            // 
            this.dgvRight.AllowUserToAddRows = false;
            this.dgvRight.AllowUserToDeleteRows = false;
            this.dgvRight.AllowUserToResizeRows = false;
            this.dgvRight.BackgroundColor = System.Drawing.Color.White;
            this.dgvRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRight.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clRightName,
            this.clRightPrice,
            this.clRightCount,
            this.clRightSum,
            this.clRightType,
            this.clRightImportance,
            this.clRightAccount,
            this.clRightDate});
            this.dgvRight.Location = new System.Drawing.Point(637, 137);
            this.dgvRight.MultiSelect = false;
            this.dgvRight.Name = "dgvRight";
            this.dgvRight.ReadOnly = true;
            this.dgvRight.RowHeadersVisible = false;
            this.dgvRight.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRight.Size = new System.Drawing.Size(705, 526);
            this.dgvRight.TabIndex = 61;
            this.dgvRight.TabStop = false;
            this.dgvRight.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRight_CellMouseClick);
            this.dgvRight.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvRight_RowsAdded);
            // 
            // clRightName
            // 
            this.clRightName.HeaderText = "Название";
            this.clRightName.Name = "clRightName";
            this.clRightName.ReadOnly = true;
            this.clRightName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clRightName.Width = 110;
            // 
            // clRightPrice
            // 
            this.clRightPrice.HeaderText = "Цена";
            this.clRightPrice.Name = "clRightPrice";
            this.clRightPrice.ReadOnly = true;
            this.clRightPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clRightPrice.Width = 50;
            // 
            // clRightCount
            // 
            dataGridViewCellStyle1.NullValue = null;
            this.clRightCount.DefaultCellStyle = dataGridViewCellStyle1;
            this.clRightCount.HeaderText = "Шт/Кг";
            this.clRightCount.Name = "clRightCount";
            this.clRightCount.ReadOnly = true;
            this.clRightCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clRightCount.Width = 45;
            // 
            // clRightSum
            // 
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.clRightSum.DefaultCellStyle = dataGridViewCellStyle2;
            this.clRightSum.HeaderText = "Сумма";
            this.clRightSum.Name = "clRightSum";
            this.clRightSum.ReadOnly = true;
            this.clRightSum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clRightSum.Width = 50;
            // 
            // clRightType
            // 
            this.clRightType.HeaderText = "Тип";
            this.clRightType.Name = "clRightType";
            this.clRightType.ReadOnly = true;
            this.clRightType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clRightType.Width = 70;
            // 
            // clRightImportance
            // 
            this.clRightImportance.HeaderText = "Важность";
            this.clRightImportance.Name = "clRightImportance";
            this.clRightImportance.ReadOnly = true;
            this.clRightImportance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clRightImportance.Width = 145;
            // 
            // clRightAccount
            // 
            this.clRightAccount.HeaderText = "Счёт";
            this.clRightAccount.Name = "clRightAccount";
            this.clRightAccount.ReadOnly = true;
            // 
            // clRightDate
            // 
            dataGridViewCellStyle3.Format = "D";
            dataGridViewCellStyle3.NullValue = null;
            this.clRightDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.clRightDate.HeaderText = "Дата";
            this.clRightDate.Name = "clRightDate";
            this.clRightDate.ReadOnly = true;
            this.clRightDate.Width = 115;
            // 
            // dgvLeft
            // 
            this.dgvLeft.AllowUserToAddRows = false;
            this.dgvLeft.AllowUserToDeleteRows = false;
            this.dgvLeft.AllowUserToResizeRows = false;
            this.dgvLeft.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeft.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clLeftName,
            this.clLeftPrice,
            this.clLeftCount,
            this.clLeftSum,
            this.clLeftType,
            this.clLeftImportance,
            this.clLeftAccount});
            this.dgvLeft.Location = new System.Drawing.Point(2, 137);
            this.dgvLeft.MultiSelect = false;
            this.dgvLeft.Name = "dgvLeft";
            this.dgvLeft.ReadOnly = true;
            this.dgvLeft.RowHeadersVisible = false;
            this.dgvLeft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeft.Size = new System.Drawing.Size(590, 526);
            this.dgvLeft.TabIndex = 60;
            this.dgvLeft.TabStop = false;
            this.dgvLeft.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLeft_CellMouseClick);
            this.dgvLeft.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvLeft_RowsAdded);
            // 
            // clLeftName
            // 
            this.clLeftName.HeaderText = "Название";
            this.clLeftName.Name = "clLeftName";
            this.clLeftName.ReadOnly = true;
            this.clLeftName.Width = 110;
            // 
            // clLeftPrice
            // 
            this.clLeftPrice.HeaderText = "Цена";
            this.clLeftPrice.Name = "clLeftPrice";
            this.clLeftPrice.ReadOnly = true;
            this.clLeftPrice.Width = 50;
            // 
            // clLeftCount
            // 
            dataGridViewCellStyle4.NullValue = null;
            this.clLeftCount.DefaultCellStyle = dataGridViewCellStyle4;
            this.clLeftCount.HeaderText = "Шт/Кг";
            this.clLeftCount.Name = "clLeftCount";
            this.clLeftCount.ReadOnly = true;
            this.clLeftCount.Width = 45;
            // 
            // clLeftSum
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.clLeftSum.DefaultCellStyle = dataGridViewCellStyle5;
            this.clLeftSum.HeaderText = "Сумма";
            this.clLeftSum.Name = "clLeftSum";
            this.clLeftSum.ReadOnly = true;
            this.clLeftSum.Width = 50;
            // 
            // clLeftType
            // 
            this.clLeftType.HeaderText = "Тип";
            this.clLeftType.Name = "clLeftType";
            this.clLeftType.ReadOnly = true;
            this.clLeftType.Width = 70;
            // 
            // clLeftImportance
            // 
            this.clLeftImportance.HeaderText = "Важность";
            this.clLeftImportance.Name = "clLeftImportance";
            this.clLeftImportance.ReadOnly = true;
            this.clLeftImportance.Width = 145;
            // 
            // clLeftAccount
            // 
            this.clLeftAccount.HeaderText = "Счёт";
            this.clLeftAccount.Name = "clLeftAccount";
            this.clLeftAccount.ReadOnly = true;
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(598, 404);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(34, 23);
            this.btnLeft.TabIndex = 58;
            this.btnLeft.TabStop = false;
            this.btnLeft.Text = "< -";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(598, 374);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(34, 23);
            this.btnRight.TabIndex = 57;
            this.btnRight.TabStop = false;
            this.btnRight.Text = "- >";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // gbChange
            // 
            this.gbChange.Controls.Add(this.btnTransfer);
            this.gbChange.Controls.Add(this.tbSumForTransfer);
            this.gbChange.Controls.Add(this.label2);
            this.gbChange.Controls.Add(this.tbAccountForTransferSum);
            this.gbChange.Controls.Add(this.tbCurrentAccountSum);
            this.gbChange.Controls.Add(this.lbAccountSum);
            this.gbChange.Controls.Add(this.label1);
            this.gbChange.Controls.Add(this.lbCurrentAccount);
            this.gbChange.Controls.Add(this.cbCurrentAccount);
            this.gbChange.Controls.Add(this.cbAccountForTransfer);
            this.gbChange.Controls.Add(this.lbAccountForTransfer);
            this.gbChange.Controls.Add(this.tbCurrentSum);
            this.gbChange.Controls.Add(this.lbCurrentSum);
            this.gbChange.Controls.Add(this.btnChangeSum);
            this.gbChange.Controls.Add(this.lbChangeSum1);
            this.gbChange.Controls.Add(this.tbChangeSum);
            this.gbChange.Location = new System.Drawing.Point(2, -1);
            this.gbChange.Name = "gbChange";
            this.gbChange.Size = new System.Drawing.Size(590, 115);
            this.gbChange.TabIndex = 5;
            this.gbChange.TabStop = false;
            this.gbChange.Text = "Редактирование счетов";
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(464, 80);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(86, 23);
            this.btnTransfer.TabIndex = 31;
            this.btnTransfer.TabStop = false;
            this.btnTransfer.Text = "Перевести";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // tbSumForTransfer
            // 
            this.tbSumForTransfer.Location = new System.Drawing.Point(343, 82);
            this.tbSumForTransfer.Name = "tbSumForTransfer";
            this.tbSumForTransfer.Size = new System.Drawing.Size(115, 20);
            this.tbSumForTransfer.TabIndex = 30;
            this.tbSumForTransfer.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Сумма для перевода";
            // 
            // tbAccountForTransferSum
            // 
            this.tbAccountForTransferSum.Enabled = false;
            this.tbAccountForTransferSum.Location = new System.Drawing.Point(464, 32);
            this.tbAccountForTransferSum.Name = "tbAccountForTransferSum";
            this.tbAccountForTransferSum.Size = new System.Drawing.Size(86, 20);
            this.tbAccountForTransferSum.TabIndex = 27;
            // 
            // tbCurrentAccountSum
            // 
            this.tbCurrentAccountSum.Enabled = false;
            this.tbCurrentAccountSum.Location = new System.Drawing.Point(223, 33);
            this.tbCurrentAccountSum.Name = "tbCurrentAccountSum";
            this.tbCurrentAccountSum.Size = new System.Drawing.Size(86, 20);
            this.tbCurrentAccountSum.TabIndex = 23;
            // 
            // lbAccountSum
            // 
            this.lbAccountSum.AutoSize = true;
            this.lbAccountSum.Location = new System.Drawing.Point(223, 20);
            this.lbAccountSum.Name = "lbAccountSum";
            this.lbAccountSum.Size = new System.Drawing.Size(86, 13);
            this.lbAccountSum.TabIndex = 23;
            this.lbAccountSum.Text = "Сумма на счету";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Сумма на счету";
            // 
            // lbCurrentAccount
            // 
            this.lbCurrentAccount.AutoSize = true;
            this.lbCurrentAccount.Location = new System.Drawing.Point(102, 18);
            this.lbCurrentAccount.Name = "lbCurrentAccount";
            this.lbCurrentAccount.Size = new System.Drawing.Size(77, 13);
            this.lbCurrentAccount.TabIndex = 23;
            this.lbCurrentAccount.Text = "Текущий счёт";
            // 
            // cbCurrentAccount
            // 
            this.cbCurrentAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentAccount.FormattingEnabled = true;
            this.cbCurrentAccount.Items.AddRange(new object[] {
            "(Добавить счёт)"});
            this.cbCurrentAccount.Location = new System.Drawing.Point(102, 33);
            this.cbCurrentAccount.Name = "cbCurrentAccount";
            this.cbCurrentAccount.Size = new System.Drawing.Size(115, 21);
            this.cbCurrentAccount.TabIndex = 23;
            this.cbCurrentAccount.SelectedIndexChanged += new System.EventHandler(this.cbCurrentAccount_SelectedIndexChanged);
            // 
            // cbAccountForTransfer
            // 
            this.cbAccountForTransfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAccountForTransfer.FormattingEnabled = true;
            this.cbAccountForTransfer.Location = new System.Drawing.Point(343, 32);
            this.cbAccountForTransfer.Name = "cbAccountForTransfer";
            this.cbAccountForTransfer.Size = new System.Drawing.Size(115, 21);
            this.cbAccountForTransfer.TabIndex = 25;
            this.cbAccountForTransfer.SelectedIndexChanged += new System.EventHandler(this.cbAccountForTransfer_SelectedIndexChanged);
            // 
            // lbAccountForTransfer
            // 
            this.lbAccountForTransfer.AutoSize = true;
            this.lbAccountForTransfer.Location = new System.Drawing.Point(343, 17);
            this.lbAccountForTransfer.Name = "lbAccountForTransfer";
            this.lbAccountForTransfer.Size = new System.Drawing.Size(102, 13);
            this.lbAccountForTransfer.TabIndex = 24;
            this.lbAccountForTransfer.Text = "Счёт для перевода";
            // 
            // tbCurrentSum
            // 
            this.tbCurrentSum.Enabled = false;
            this.tbCurrentSum.Location = new System.Drawing.Point(5, 58);
            this.tbCurrentSum.Name = "tbCurrentSum";
            this.tbCurrentSum.Size = new System.Drawing.Size(89, 20);
            this.tbCurrentSum.TabIndex = 5;
            this.tbCurrentSum.TabStop = false;
            this.tbCurrentSum.Text = "0";
            // 
            // lbCurrentSum
            // 
            this.lbCurrentSum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrentSum.AutoSize = true;
            this.lbCurrentSum.Location = new System.Drawing.Point(6, 43);
            this.lbCurrentSum.Name = "lbCurrentSum";
            this.lbCurrentSum.Size = new System.Drawing.Size(78, 13);
            this.lbCurrentSum.TabIndex = 4;
            this.lbCurrentSum.Text = "Общая сумма";
            // 
            // btnChangeSum
            // 
            this.btnChangeSum.Location = new System.Drawing.Point(223, 80);
            this.btnChangeSum.Name = "btnChangeSum";
            this.btnChangeSum.Size = new System.Drawing.Size(86, 23);
            this.btnChangeSum.TabIndex = 3;
            this.btnChangeSum.TabStop = false;
            this.btnChangeSum.Text = "Изменить";
            this.btnChangeSum.UseVisualStyleBackColor = true;
            this.btnChangeSum.Click += new System.EventHandler(this.btnChangeSum_Click);
            // 
            // lbChangeSum1
            // 
            this.lbChangeSum1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbChangeSum1.AutoSize = true;
            this.lbChangeSum1.Location = new System.Drawing.Point(102, 69);
            this.lbChangeSum1.Name = "lbChangeSum1";
            this.lbChangeSum1.Size = new System.Drawing.Size(103, 13);
            this.lbChangeSum1.TabIndex = 2;
            this.lbChangeSum1.Text = "Изменение суммы";
            // 
            // tbChangeSum
            // 
            this.tbChangeSum.Location = new System.Drawing.Point(102, 82);
            this.tbChangeSum.Name = "tbChangeSum";
            this.tbChangeSum.Size = new System.Drawing.Size(115, 20);
            this.tbChangeSum.TabIndex = 1;
            this.tbChangeSum.TabStop = false;
            this.tbChangeSum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbChangeSum_KeyPress);
            // 
            // tpInfo
            // 
            this.tpInfo.BackColor = System.Drawing.Color.Lavender;
            this.tpInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpInfo.Controls.Add(this.gbFilters);
            this.tpInfo.Controls.Add(this.lbSearchInfo);
            this.tpInfo.Controls.Add(this.tbSearchInfo);
            this.tpInfo.Controls.Add(this.tbSum);
            this.tpInfo.Controls.Add(this.dgvShow);
            this.tpInfo.Location = new System.Drawing.Point(4, 22);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(1346, 667);
            this.tpInfo.TabIndex = 1;
            this.tpInfo.Text = "Информация";
            // 
            // gbFilters
            // 
            this.gbFilters.Controls.Add(this.lbFilter);
            this.gbFilters.Controls.Add(this.gbAccount);
            this.gbFilters.Controls.Add(this.gbDate);
            this.gbFilters.Controls.Add(this.gbImportance);
            this.gbFilters.Controls.Add(this.gbSum);
            this.gbFilters.Controls.Add(this.gbType);
            this.gbFilters.Controls.Add(this.btnFilter);
            this.gbFilters.Controls.Add(this.btnCancelInfo);
            this.gbFilters.Controls.Add(this.listbFilter);
            this.gbFilters.Location = new System.Drawing.Point(2, 3);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(537, 410);
            this.gbFilters.TabIndex = 38;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "Фильтры";
            // 
            // lbFilter
            // 
            this.lbFilter.AutoSize = true;
            this.lbFilter.Location = new System.Drawing.Point(50, 16);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(135, 13);
            this.lbFilter.TabIndex = 34;
            this.lbFilter.Text = "Используемые фильтры:";
            // 
            // gbAccount
            // 
            this.gbAccount.Controls.Add(this.chboxAccountInfo);
            this.gbAccount.Controls.Add(this.clbAccountInfo);
            this.gbAccount.Location = new System.Drawing.Point(48, 288);
            this.gbAccount.Name = "gbAccount";
            this.gbAccount.Size = new System.Drawing.Size(217, 119);
            this.gbAccount.TabIndex = 37;
            this.gbAccount.TabStop = false;
            this.gbAccount.Text = "По счёту:";
            // 
            // chboxAccountInfo
            // 
            this.chboxAccountInfo.AutoSize = true;
            this.chboxAccountInfo.Location = new System.Drawing.Point(9, 18);
            this.chboxAccountInfo.Name = "chboxAccountInfo";
            this.chboxAccountInfo.Size = new System.Drawing.Size(45, 17);
            this.chboxAccountInfo.TabIndex = 1;
            this.chboxAccountInfo.Text = "Все";
            this.chboxAccountInfo.UseVisualStyleBackColor = true;
            this.chboxAccountInfo.CheckedChanged += new System.EventHandler(this.chboxAccountInfo_CheckedChanged);
            // 
            // clbAccountInfo
            // 
            this.clbAccountInfo.CheckOnClick = true;
            this.clbAccountInfo.FormattingEnabled = true;
            this.clbAccountInfo.Location = new System.Drawing.Point(6, 35);
            this.clbAccountInfo.Name = "clbAccountInfo";
            this.clbAccountInfo.Size = new System.Drawing.Size(201, 79);
            this.clbAccountInfo.TabIndex = 0;
            this.clbAccountInfo.TabStop = false;
            this.clbAccountInfo.SelectedIndexChanged += new System.EventHandler(this.clbAccountInfo_SelectedIndexChanged);
            // 
            // gbDate
            // 
            this.gbDate.Controls.Add(this.lbLastTime);
            this.gbDate.Controls.Add(this.cbLastTime);
            this.gbDate.Controls.Add(this.lbDay);
            this.gbDate.Controls.Add(this.lbMonth);
            this.gbDate.Controls.Add(this.lbYear);
            this.gbDate.Controls.Add(this.cbDay);
            this.gbDate.Controls.Add(this.cbMonth);
            this.gbDate.Controls.Add(this.cbYear);
            this.gbDate.Location = new System.Drawing.Point(48, 114);
            this.gbDate.Name = "gbDate";
            this.gbDate.Size = new System.Drawing.Size(441, 56);
            this.gbDate.TabIndex = 25;
            this.gbDate.TabStop = false;
            this.gbDate.Text = "По дате";
            // 
            // lbLastTime
            // 
            this.lbLastTime.AutoSize = true;
            this.lbLastTime.Location = new System.Drawing.Point(42, 14);
            this.lbLastTime.Name = "lbLastTime";
            this.lbLastTime.Size = new System.Drawing.Size(43, 13);
            this.lbLastTime.TabIndex = 62;
            this.lbLastTime.Text = "Время:";
            // 
            // cbLastTime
            // 
            this.cbLastTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLastTime.FormattingEnabled = true;
            this.cbLastTime.Items.AddRange(new object[] {
            "За всё время",
            "Вчера",
            "Последняя неделя",
            "Последний месяц",
            "Последние 3 месяца",
            "Последние 6 месяцев",
            "Последний год",
            "(задать интервал)"});
            this.cbLastTime.Location = new System.Drawing.Point(39, 29);
            this.cbLastTime.Name = "cbLastTime";
            this.cbLastTime.Size = new System.Drawing.Size(125, 21);
            this.cbLastTime.TabIndex = 61;
            this.cbLastTime.TabStop = false;
            this.cbLastTime.SelectedIndexChanged += new System.EventHandler(this.cbLastTime_SelectedIndexChanged);
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Location = new System.Drawing.Point(350, 14);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(34, 13);
            this.lbDay.TabIndex = 60;
            this.lbDay.Text = "День";
            // 
            // lbMonth
            // 
            this.lbMonth.AutoSize = true;
            this.lbMonth.Location = new System.Drawing.Point(258, 14);
            this.lbMonth.Name = "lbMonth";
            this.lbMonth.Size = new System.Drawing.Size(40, 13);
            this.lbMonth.TabIndex = 59;
            this.lbMonth.Text = "Месяц";
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.Location = new System.Drawing.Point(188, 14);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(25, 13);
            this.lbYear.TabIndex = 58;
            this.lbYear.Text = "Год";
            // 
            // cbDay
            // 
            this.cbDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDay.FormattingEnabled = true;
            this.cbDay.IntegralHeight = false;
            this.cbDay.Items.AddRange(new object[] {
            " ",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.cbDay.Location = new System.Drawing.Point(347, 29);
            this.cbDay.Name = "cbDay";
            this.cbDay.Size = new System.Drawing.Size(40, 21);
            this.cbDay.TabIndex = 57;
            this.cbDay.TabStop = false;
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.IntegralHeight = false;
            this.cbMonth.Items.AddRange(new object[] {
            " ",
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.cbMonth.Location = new System.Drawing.Point(255, 29);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(75, 21);
            this.cbMonth.TabIndex = 56;
            this.cbMonth.TabStop = false;
            this.cbMonth.SelectedIndexChanged += new System.EventHandler(this.cbMonth_SelectedIndexChanged);
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.IntegralHeight = false;
            this.cbYear.Items.AddRange(new object[] {
            " "});
            this.cbYear.Location = new System.Drawing.Point(185, 29);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(50, 21);
            this.cbYear.TabIndex = 55;
            this.cbYear.TabStop = false;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
            // 
            // gbImportance
            // 
            this.gbImportance.Controls.Add(this.chboxImportanceInfo);
            this.gbImportance.Controls.Add(this.clbImportanceInfo);
            this.gbImportance.Location = new System.Drawing.Point(48, 170);
            this.gbImportance.Name = "gbImportance";
            this.gbImportance.Size = new System.Drawing.Size(217, 118);
            this.gbImportance.TabIndex = 26;
            this.gbImportance.TabStop = false;
            this.gbImportance.Text = "По важности";
            // 
            // chboxImportanceInfo
            // 
            this.chboxImportanceInfo.AutoSize = true;
            this.chboxImportanceInfo.Location = new System.Drawing.Point(10, 18);
            this.chboxImportanceInfo.Name = "chboxImportanceInfo";
            this.chboxImportanceInfo.Size = new System.Drawing.Size(45, 17);
            this.chboxImportanceInfo.TabIndex = 1;
            this.chboxImportanceInfo.TabStop = false;
            this.chboxImportanceInfo.Text = "Все";
            this.chboxImportanceInfo.UseVisualStyleBackColor = true;
            this.chboxImportanceInfo.CheckedChanged += new System.EventHandler(this.chboxImportanceInfo_CheckedChanged);
            // 
            // clbImportanceInfo
            // 
            this.clbImportanceInfo.CheckOnClick = true;
            this.clbImportanceInfo.FormattingEnabled = true;
            this.clbImportanceInfo.Items.AddRange(new object[] {
            "Первостепенной важности",
            "Средней важности",
            "Малой важности",
            "Неважно"});
            this.clbImportanceInfo.Location = new System.Drawing.Point(7, 35);
            this.clbImportanceInfo.Name = "clbImportanceInfo";
            this.clbImportanceInfo.Size = new System.Drawing.Size(200, 79);
            this.clbImportanceInfo.TabIndex = 0;
            this.clbImportanceInfo.TabStop = false;
            this.clbImportanceInfo.SelectedIndexChanged += new System.EventHandler(this.clbImportanceInfo_SelectedIndexChanged);
            // 
            // gbSum
            // 
            this.gbSum.Controls.Add(this.lbMax);
            this.gbSum.Controls.Add(this.lbMin);
            this.gbSum.Controls.Add(this.nudMin);
            this.gbSum.Controls.Add(this.nudMax);
            this.gbSum.Location = new System.Drawing.Point(269, 288);
            this.gbSum.Name = "gbSum";
            this.gbSum.Size = new System.Drawing.Size(220, 61);
            this.gbSum.TabIndex = 28;
            this.gbSum.TabStop = false;
            this.gbSum.Text = "По сумме";
            // 
            // lbMax
            // 
            this.lbMax.AutoSize = true;
            this.lbMax.Location = new System.Drawing.Point(121, 16);
            this.lbMax.Name = "lbMax";
            this.lbMax.Size = new System.Drawing.Size(61, 13);
            this.lbMax.TabIndex = 23;
            this.lbMax.Text = "Максимум";
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.Location = new System.Drawing.Point(31, 16);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(55, 13);
            this.lbMin.TabIndex = 22;
            this.lbMin.Text = "Минимум";
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(31, 35);
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(70, 20);
            this.nudMin.TabIndex = 20;
            this.nudMin.TabStop = false;
            this.nudMin.ValueChanged += new System.EventHandler(this.nudMin_ValueChanged);
            // 
            // nudMax
            // 
            this.nudMax.Location = new System.Drawing.Point(121, 35);
            this.nudMax.Name = "nudMax";
            this.nudMax.Size = new System.Drawing.Size(70, 20);
            this.nudMax.TabIndex = 21;
            this.nudMax.TabStop = false;
            this.nudMax.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMax.ValueChanged += new System.EventHandler(this.nudMax_ValueChanged);
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.chboxTypeInfo);
            this.gbType.Controls.Add(this.clbTypeInfo);
            this.gbType.Location = new System.Drawing.Point(269, 170);
            this.gbType.Name = "gbType";
            this.gbType.Size = new System.Drawing.Size(220, 118);
            this.gbType.TabIndex = 27;
            this.gbType.TabStop = false;
            this.gbType.Text = "По типу";
            // 
            // chboxTypeInfo
            // 
            this.chboxTypeInfo.AutoSize = true;
            this.chboxTypeInfo.Location = new System.Drawing.Point(10, 18);
            this.chboxTypeInfo.Name = "chboxTypeInfo";
            this.chboxTypeInfo.Size = new System.Drawing.Size(45, 17);
            this.chboxTypeInfo.TabIndex = 2;
            this.chboxTypeInfo.TabStop = false;
            this.chboxTypeInfo.Text = "Все";
            this.chboxTypeInfo.UseVisualStyleBackColor = true;
            this.chboxTypeInfo.CheckedChanged += new System.EventHandler(this.chboxTypeInfo_CheckedChanged);
            // 
            // clbTypeInfo
            // 
            this.clbTypeInfo.CheckOnClick = true;
            this.clbTypeInfo.FormattingEnabled = true;
            this.clbTypeInfo.Items.AddRange(new object[] {
            "Продукты",
            "Хоз.товары",
            "Услуги"});
            this.clbTypeInfo.Location = new System.Drawing.Point(7, 35);
            this.clbTypeInfo.Name = "clbTypeInfo";
            this.clbTypeInfo.Size = new System.Drawing.Size(205, 79);
            this.clbTypeInfo.TabIndex = 0;
            this.clbTypeInfo.TabStop = false;
            this.clbTypeInfo.SelectedIndexChanged += new System.EventHandler(this.clbTypeInfo_SelectedIndexChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(269, 367);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(101, 23);
            this.btnFilter.TabIndex = 29;
            this.btnFilter.TabStop = false;
            this.btnFilter.Text = "Фильтровать";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnCancelInfo
            // 
            this.btnCancelInfo.Location = new System.Drawing.Point(379, 367);
            this.btnCancelInfo.Name = "btnCancelInfo";
            this.btnCancelInfo.Size = new System.Drawing.Size(110, 23);
            this.btnCancelInfo.TabIndex = 30;
            this.btnCancelInfo.TabStop = false;
            this.btnCancelInfo.Text = "Очистить фильтры";
            this.btnCancelInfo.UseVisualStyleBackColor = true;
            this.btnCancelInfo.Click += new System.EventHandler(this.btnCancelInfo_Click);
            // 
            // listbFilter
            // 
            this.listbFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listbFilter.FormattingEnabled = true;
            this.listbFilter.HorizontalScrollbar = true;
            this.listbFilter.Location = new System.Drawing.Point(48, 32);
            this.listbFilter.Name = "listbFilter";
            this.listbFilter.Size = new System.Drawing.Size(441, 82);
            this.listbFilter.TabIndex = 33;
            this.listbFilter.TabStop = false;
            this.listbFilter.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listbFilter_DrawItem);
            this.listbFilter.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listbFilter_MeasureItem);
            // 
            // lbSearchInfo
            // 
            this.lbSearchInfo.AutoSize = true;
            this.lbSearchInfo.Location = new System.Drawing.Point(545, 3);
            this.lbSearchInfo.Name = "lbSearchInfo";
            this.lbSearchInfo.Size = new System.Drawing.Size(39, 13);
            this.lbSearchInfo.TabIndex = 36;
            this.lbSearchInfo.Text = "Поиск";
            // 
            // tbSearchInfo
            // 
            this.tbSearchInfo.Location = new System.Drawing.Point(585, 0);
            this.tbSearchInfo.Name = "tbSearchInfo";
            this.tbSearchInfo.Size = new System.Drawing.Size(169, 20);
            this.tbSearchInfo.TabIndex = 35;
            this.tbSearchInfo.TabStop = false;
            this.tbSearchInfo.TextChanged += new System.EventHandler(this.tbSearchInfo_TextChanged);
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(793, 0);
            this.tbSum.Name = "tbSum";
            this.tbSum.ReadOnly = true;
            this.tbSum.Size = new System.Drawing.Size(71, 20);
            this.tbSum.TabIndex = 31;
            this.tbSum.TabStop = false;
            this.Hint.SetToolTip(this.tbSum, "Общая сумма");
            this.tbSum.MouseEnter += new System.EventHandler(this.tbSum_MouseEnter);
            this.tbSum.MouseLeave += new System.EventHandler(this.tbSum_MouseLeave);
            // 
            // dgvShow
            // 
            this.dgvShow.AllowUserToAddRows = false;
            this.dgvShow.AllowUserToDeleteRows = false;
            this.dgvShow.AllowUserToResizeRows = false;
            this.dgvShow.BackgroundColor = System.Drawing.Color.White;
            this.dgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clShowName,
            this.clShowPrice,
            this.clShowCount,
            this.clShowSum,
            this.clShowType,
            this.clShowImportance,
            this.clShowAccount,
            this.clShowDate});
            this.dgvShow.Location = new System.Drawing.Point(542, 21);
            this.dgvShow.Name = "dgvShow";
            this.dgvShow.ReadOnly = true;
            this.dgvShow.RowHeadersVisible = false;
            this.dgvShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShow.Size = new System.Drawing.Size(800, 642);
            this.dgvShow.TabIndex = 16;
            this.dgvShow.TabStop = false;
            this.dgvShow.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvShow_CellMouseClick);
            this.dgvShow.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvShow_RowsAdded);
            // 
            // clShowName
            // 
            this.clShowName.HeaderText = "Название";
            this.clShowName.Name = "clShowName";
            this.clShowName.ReadOnly = true;
            this.clShowName.Width = 140;
            // 
            // clShowPrice
            // 
            this.clShowPrice.HeaderText = "Цена";
            this.clShowPrice.Name = "clShowPrice";
            this.clShowPrice.ReadOnly = true;
            this.clShowPrice.Width = 70;
            // 
            // clShowCount
            // 
            this.clShowCount.HeaderText = "Шт";
            this.clShowCount.Name = "clShowCount";
            this.clShowCount.ReadOnly = true;
            this.clShowCount.Width = 40;
            // 
            // clShowSum
            // 
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.clShowSum.DefaultCellStyle = dataGridViewCellStyle6;
            this.clShowSum.HeaderText = "Сумма";
            this.clShowSum.Name = "clShowSum";
            this.clShowSum.ReadOnly = true;
            this.clShowSum.Width = 70;
            // 
            // clShowType
            // 
            this.clShowType.HeaderText = "Тип";
            this.clShowType.Name = "clShowType";
            this.clShowType.ReadOnly = true;
            // 
            // clShowImportance
            // 
            this.clShowImportance.HeaderText = "Важность";
            this.clShowImportance.Name = "clShowImportance";
            this.clShowImportance.ReadOnly = true;
            this.clShowImportance.Width = 145;
            // 
            // clShowAccount
            // 
            this.clShowAccount.HeaderText = "Счёт";
            this.clShowAccount.Name = "clShowAccount";
            this.clShowAccount.ReadOnly = true;
            // 
            // clShowDate
            // 
            dataGridViewCellStyle7.Format = "D";
            dataGridViewCellStyle7.NullValue = null;
            this.clShowDate.DefaultCellStyle = dataGridViewCellStyle7;
            this.clShowDate.HeaderText = "Дата";
            this.clShowDate.Name = "clShowDate";
            this.clShowDate.ReadOnly = true;
            this.clShowDate.Width = 115;
            // 
            // tpGraphic
            // 
            this.tpGraphic.BackColor = System.Drawing.Color.Lavender;
            this.tpGraphic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpGraphic.Controls.Add(this.groupBox7);
            this.tpGraphic.Controls.Add(this.chart1);
            this.tpGraphic.Controls.Add(this.groupBox1);
            this.tpGraphic.Location = new System.Drawing.Point(4, 22);
            this.tpGraphic.Name = "tpGraphic";
            this.tpGraphic.Size = new System.Drawing.Size(1346, 667);
            this.tpGraphic.TabIndex = 2;
            this.tpGraphic.Text = "Графики";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radioButton2);
            this.groupBox7.Controls.Add(this.radioButton1);
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(231, 48);
            this.groupBox7.TabIndex = 38;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Приоритет";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(148, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(64, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "По типу";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(19, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(91, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "По важности";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Lavender;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(240, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.MarkerColor = System.Drawing.Color.Red;
            series1.MarkerSize = 10;
            series1.Name = "sImportance1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.MarkerColor = System.Drawing.Color.Orange;
            series2.MarkerSize = 10;
            series2.Name = "sImportance2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.MarkerColor = System.Drawing.Color.Yellow;
            series3.MarkerSize = 10;
            series3.Name = "sImportance3";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.MarkerColor = System.Drawing.Color.Green;
            series4.MarkerSize = 10;
            series4.Name = "sImportance4";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(1103, 661);
            this.chart1.TabIndex = 40;
            this.chart1.Text = "chartInfo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(3, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 607);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтры";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.checkedListBox1);
            this.groupBox2.Location = new System.Drawing.Point(6, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 119);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "По счёту:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(45, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Все";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 35);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(201, 79);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.comboBox3);
            this.groupBox3.Controls.Add(this.comboBox4);
            this.groupBox3.Location = new System.Drawing.Point(6, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 122);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "По дате";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(7, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(204, 20);
            this.textBox1.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Время:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "За всё время",
            "Вчера",
            "Последняя неделя",
            "Последний месяц",
            "Последние 3 месяца",
            "Последние 6 месяцев",
            "Последний год",
            "(задать интервал)"});
            this.comboBox1.Location = new System.Drawing.Point(39, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(125, 21);
            this.comboBox1.TabIndex = 61;
            this.comboBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "День";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "Месяц";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Год";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.IntegralHeight = false;
            this.comboBox2.Items.AddRange(new object[] {
            " ",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.comboBox2.Location = new System.Drawing.Point(169, 95);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(40, 21);
            this.comboBox2.TabIndex = 57;
            this.comboBox2.TabStop = false;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.IntegralHeight = false;
            this.comboBox3.Items.AddRange(new object[] {
            " ",
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.comboBox3.Location = new System.Drawing.Point(77, 95);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(75, 21);
            this.comboBox3.TabIndex = 56;
            this.comboBox3.TabStop = false;
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.IntegralHeight = false;
            this.comboBox4.Items.AddRange(new object[] {
            " "});
            this.comboBox4.Location = new System.Drawing.Point(7, 95);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(50, 21);
            this.comboBox4.TabIndex = 55;
            this.comboBox4.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.checkedListBox2);
            this.groupBox4.Location = new System.Drawing.Point(6, 137);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(217, 118);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "По важности";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(10, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(45, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.TabStop = false;
            this.checkBox2.Text = "Все";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.CheckOnClick = true;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Items.AddRange(new object[] {
            "Первостепенной важности",
            "Средней важности",
            "Малой важности",
            "Неважно"});
            this.checkedListBox2.Location = new System.Drawing.Point(7, 35);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(200, 79);
            this.checkedListBox2.TabIndex = 0;
            this.checkedListBox2.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.numericUpDown1);
            this.groupBox5.Controls.Add(this.numericUpDown2);
            this.groupBox5.Location = new System.Drawing.Point(6, 493);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(220, 61);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "По сумме";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(121, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Максимум";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Минимум";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(31, 35);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown1.TabIndex = 20;
            this.numericUpDown1.TabStop = false;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(121, 35);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown2.TabIndex = 21;
            this.numericUpDown2.TabStop = false;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBox3);
            this.groupBox6.Controls.Add(this.checkedListBox3);
            this.groupBox6.Location = new System.Drawing.Point(6, 375);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(217, 118);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "По типу";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(10, 18);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(45, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.TabStop = false;
            this.checkBox3.Text = "Все";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkedListBox3
            // 
            this.checkedListBox3.CheckOnClick = true;
            this.checkedListBox3.FormattingEnabled = true;
            this.checkedListBox3.Items.AddRange(new object[] {
            "Продукты",
            "Хоз.товары",
            "Услуги"});
            this.checkedListBox3.Location = new System.Drawing.Point(7, 35);
            this.checkedListBox3.Name = "checkedListBox3";
            this.checkedListBox3.Size = new System.Drawing.Size(205, 79);
            this.checkedListBox3.TabIndex = 0;
            this.checkedListBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 569);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 29;
            this.button1.TabStop = false;
            this.button1.Text = "Фильтровать";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 569);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 30;
            this.button2.TabStop = false;
            this.button2.Text = "Очистить фильтры";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(1226, 694);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 35);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.TabStop = false;
            this.btnLogout.Text = "Сменить пользователя";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // cmsDgv
            // 
            this.cmsDgv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDgvEdit,
            this.tsmiDgvDelete});
            this.cmsDgv.Name = "contextMenuStrip1";
            this.cmsDgv.Size = new System.Drawing.Size(129, 48);
            this.cmsDgv.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsDgv_ItemClicked);
            // 
            // tsmiDgvEdit
            // 
            this.tsmiDgvEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDgvEdit.Image")));
            this.tsmiDgvEdit.Name = "tsmiDgvEdit";
            this.tsmiDgvEdit.Size = new System.Drawing.Size(128, 22);
            this.tsmiDgvEdit.Text = "Изменить";
            // 
            // tsmiDgvDelete
            // 
            this.tsmiDgvDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDgvDelete.Image")));
            this.tsmiDgvDelete.Name = "tsmiDgvDelete";
            this.tsmiDgvDelete.Size = new System.Drawing.Size(128, 22);
            this.tsmiDgvDelete.Text = "Удалить";
            // 
            // Hint
            // 
            this.Hint.Active = false;
            this.Hint.AutoPopDelay = 5000;
            this.Hint.InitialDelay = 100;
            this.Hint.ReshowDelay = 100;
            // 
            // IconTray
            // 
            this.IconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("IconTray.Icon")));
            this.IconTray.Text = "HomeFinanceManager";
            this.IconTray.Visible = true;
            this.IconTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Icon_MouseDoubleClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomeFinanceManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl.ResumeLayout(false);
            this.tpAdd.ResumeLayout(false);
            this.tpAdd.PerformLayout();
            this.gbAdd.ResumeLayout(false);
            this.gbAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).EndInit();
            this.gbChange.ResumeLayout(false);
            this.gbChange.PerformLayout();
            this.tpInfo.ResumeLayout(false);
            this.tpInfo.PerformLayout();
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            this.gbAccount.ResumeLayout(false);
            this.gbAccount.PerformLayout();
            this.gbDate.ResumeLayout(false);
            this.gbDate.PerformLayout();
            this.gbImportance.ResumeLayout(false);
            this.gbImportance.PerformLayout();
            this.gbSum.ResumeLayout(false);
            this.gbSum.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).EndInit();
            this.gbType.ResumeLayout(false);
            this.gbType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            this.tpGraphic.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.cmsDgv.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpAdd;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnChangeSum;
        private System.Windows.Forms.Label lbChangeSum1;
        private System.Windows.Forms.TextBox tbChangeSum;
        private System.Windows.Forms.GroupBox gbChange;
        private System.Windows.Forms.Label lbCurrentSum;
        private System.Windows.Forms.TextBox tbCurrentSum;
        private System.Windows.Forms.DataGridView dgvShow;
        private System.Windows.Forms.ContextMenuStrip cmsDgv;
        private System.Windows.Forms.ToolStripMenuItem tsmiDgvEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiDgvDelete;
        private System.Windows.Forms.GroupBox gbSum;
        private System.Windows.Forms.Label lbMax;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.NumericUpDown nudMax;
        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.GroupBox gbImportance;
        private System.Windows.Forms.GroupBox gbDate;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCancelInfo;
        private System.Windows.Forms.ListBox listbFilter;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Label lbFilter;
        private System.Windows.Forms.ToolTip Hint;
        private System.Windows.Forms.Label lbSearchInfo;
        private System.Windows.Forms.TextBox tbSearchInfo;
        private System.Windows.Forms.CheckedListBox clbTypeInfo;
        private System.Windows.Forms.CheckedListBox clbImportanceInfo;
        private System.Windows.Forms.CheckBox chboxTypeInfo;
        private System.Windows.Forms.CheckBox chboxImportanceInfo;
        private System.Windows.Forms.NotifyIcon IconTray;
        private System.Windows.Forms.TextBox tbCurrentAccountSum;
        private System.Windows.Forms.Label lbAccountSum;
        private System.Windows.Forms.Label lbCurrentAccount;
        private System.Windows.Forms.ComboBox cbCurrentAccount;
        private System.Windows.Forms.Label lbLastTime;
        private System.Windows.Forms.ComboBox cbLastTime;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbMonth;
        private System.Windows.Forms.Label lbYear;
        private System.Windows.Forms.ComboBox cbDay;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.GroupBox gbAccount;
        private System.Windows.Forms.CheckBox chboxAccountInfo;
        private System.Windows.Forms.CheckedListBox clbAccountInfo;
        private System.Windows.Forms.TextBox tbAddAccountSum;
        private System.Windows.Forms.ComboBox cbAddAccounts;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.ComboBox cbAddDate;
        private System.Windows.Forms.TextBox tbAddCount;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.TextBox tbSearchAdd;
        private System.Windows.Forms.Label lbSearchAdd;
        private System.Windows.Forms.DataGridView dgvRight;
        private System.Windows.Forms.DataGridView dgvLeft;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Label lbImportanceAdd;
        private System.Windows.Forms.Label lbTypeAdd;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lbCost;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbAddCost;
        private System.Windows.Forms.TextBox tbAddName;
        private System.Windows.Forms.ComboBox cbAddImportance;
        private System.Windows.Forms.ComboBox cbAddType;
        private System.Windows.Forms.GroupBox gbAdd;
        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowImportance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clShowDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLeftName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLeftPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLeftCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLeftSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLeftType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLeftImportance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clLeftAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightImportance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRightDate;
        private System.Windows.Forms.Label lbAccountForTransfer;
        private System.Windows.Forms.TextBox tbAccountForTransferSum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAccountForTransfer;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.TextBox tbSumForTransfer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tpGraphic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckedListBox checkedListBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}