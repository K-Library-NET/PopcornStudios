namespace QuickFlyParam
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnScan = new System.Windows.Forms.Button();
            this.lvCheck = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.故障诊断ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.表格显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.极值列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.故障信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvExtremum = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlProcess = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblInfo = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtremum)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.pnlProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScan.Location = new System.Drawing.Point(865, 19);
            this.btnScan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(112, 36);
            this.btnScan.TabIndex = 5;
            this.btnScan.Text = "开始扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lvCheck
            // 
            this.lvCheck.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCheck.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvCheck.FullRowSelect = true;
            this.lvCheck.GridLines = true;
            this.lvCheck.Location = new System.Drawing.Point(4, 5);
            this.lvCheck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvCheck.MultiSelect = false;
            this.lvCheck.Name = "lvCheck";
            this.lvCheck.Size = new System.Drawing.Size(778, 471);
            this.lvCheck.TabIndex = 7;
            this.lvCheck.UseCompatibleStateImageBehavior = false;
            this.lvCheck.View = System.Windows.Forms.View.Details;
            this.lvCheck.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvCheck_ColumnClick);
            this.lvCheck.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvCheck_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "检查项";
            this.columnHeader1.Width = 338;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "状态";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "故障发生时间";
            this.columnHeader3.Width = 319;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 546);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(794, 26);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.DarkGray;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(44, 21);
            this.toolStripStatusLabel2.Text = "文件名";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStripStatusLabel8.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(135, 21);
            this.toolStripStatusLabel8.Text = "toolStripStatusLabel8";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.DarkGray;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(32, 21);
            this.toolStripStatusLabel3.Text = "机型";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStripStatusLabel4.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(135, 21);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BackColor = System.Drawing.Color.DarkGray;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(32, 21);
            this.toolStripStatusLabel5.Text = "机号";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStripStatusLabel6.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(135, 21);
            this.toolStripStatusLabel6.Text = "toolStripStatusLabel6";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.BackColor = System.Drawing.Color.DarkGray;
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(56, 21);
            this.toolStripStatusLabel7.Text = "记录时段";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(135, 21);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.故障诊断ToolStripMenuItem,
            this.表格显示ToolStripMenuItem,
            this.极值列表ToolStripMenuItem,
            this.故障信息ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(794, 31);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(102, 25);
            this.toolStripMenuItem1.Text = "选择文件";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 故障诊断ToolStripMenuItem
            // 
            this.故障诊断ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.故障诊断ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("故障诊断ToolStripMenuItem.Image")));
            this.故障诊断ToolStripMenuItem.Name = "故障诊断ToolStripMenuItem";
            this.故障诊断ToolStripMenuItem.Size = new System.Drawing.Size(102, 25);
            this.故障诊断ToolStripMenuItem.Text = "故障诊断";
            this.故障诊断ToolStripMenuItem.Click += new System.EventHandler(this.故障诊断ToolStripMenuItem_Click);
            // 
            // 表格显示ToolStripMenuItem
            // 
            this.表格显示ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("表格显示ToolStripMenuItem.Image")));
            this.表格显示ToolStripMenuItem.Name = "表格显示ToolStripMenuItem";
            this.表格显示ToolStripMenuItem.Size = new System.Drawing.Size(102, 25);
            this.表格显示ToolStripMenuItem.Text = "参数报表";
            this.表格显示ToolStripMenuItem.Click += new System.EventHandler(this.表格显示ToolStripMenuItem_Click);
            // 
            // 极值列表ToolStripMenuItem
            // 
            this.极值列表ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("极值列表ToolStripMenuItem.Image")));
            this.极值列表ToolStripMenuItem.Name = "极值列表ToolStripMenuItem";
            this.极值列表ToolStripMenuItem.Size = new System.Drawing.Size(102, 25);
            this.极值列表ToolStripMenuItem.Text = "极值报表";
            this.极值列表ToolStripMenuItem.Click += new System.EventHandler(this.极值列表ToolStripMenuItem_Click);
            // 
            // 故障信息ToolStripMenuItem
            // 
            this.故障信息ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("故障信息ToolStripMenuItem.Image")));
            this.故障信息ToolStripMenuItem.Name = "故障信息ToolStripMenuItem";
            this.故障信息ToolStripMenuItem.Size = new System.Drawing.Size(102, 25);
            this.故障信息ToolStripMenuItem.Text = "故障信息";
            this.故障信息ToolStripMenuItem.Visible = false;
            this.故障信息ToolStripMenuItem.Click += new System.EventHandler(this.故障信息ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "飞参数据文件|*.phy|所有文件|*.*";
            this.openFileDialog1.Title = "请选择飞参文件";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 31);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(794, 515);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvData);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(786, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "参数报表";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(4, 5);
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(778, 471);
            this.dgvData.TabIndex = 1;
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellDoubleClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvExtremum);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(786, 481);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "极值报表";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvExtremum
            // 
            this.dgvExtremum.AllowUserToAddRows = false;
            this.dgvExtremum.AllowUserToDeleteRows = false;
            this.dgvExtremum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExtremum.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExtremum.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvExtremum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExtremum.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvExtremum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtremum.Location = new System.Drawing.Point(0, 0);
            this.dgvExtremum.Name = "dgvExtremum";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExtremum.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExtremum.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvExtremum.RowTemplate.Height = 23;
            this.dgvExtremum.Size = new System.Drawing.Size(786, 481);
            this.dgvExtremum.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvCheck);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(786, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "故障信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlProcess
            // 
            this.pnlProcess.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlProcess.Controls.Add(this.progressBar1);
            this.pnlProcess.Controls.Add(this.lblInfo);
            this.pnlProcess.Location = new System.Drawing.Point(169, 218);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(453, 74);
            this.pnlProcess.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Location = new System.Drawing.Point(15, 39);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(423, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.Location = new System.Drawing.Point(11, 12);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(129, 20);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "正在进行。。。。";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.pnlProcess);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnScan);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "飞参快速判读";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtremum)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.pnlProcess.ResumeLayout(false);
            this.pnlProcess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListView lvCheck;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 表格显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 极值列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 故障诊断ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.DataGridView dgvExtremum;
        private System.Windows.Forms.ToolStripMenuItem 故障信息ToolStripMenuItem;
        private System.Windows.Forms.Panel pnlProcess;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblInfo;
    }
}

