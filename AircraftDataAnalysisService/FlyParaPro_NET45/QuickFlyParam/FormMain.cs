using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using FlyParamBusiness.PHYFile;
using FlyParamBusiness.Fault;
using FlyParamBusiness;
using System.Threading;

namespace QuickFlyParam
{
    public partial class FormMain : Form
    {
        public string fileName;
        private DataTable mDataDT;
        private DataTable mExtremumTable;
        private BackgroundWorker mWorker;
        private BackgroundWorker mWorkerFault;

        bool isReadEnd;

        public FormMain()
        {
            InitializeComponent();

            this.Load += new EventHandler(FormMain_Load);
            this.Shown += new EventHandler(FormMain_Shown);
        }

        void FormMain_Shown(object sender, EventArgs e)
        {
            this.mWorker = new BackgroundWorker();

            this.mWorker.DoWork += new DoWorkEventHandler(mWorker_DoWork);
            this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mWorker_RunWorkerCompleted);

            //this.lblReadPHYFile.Visible = true;
            this.mWorkerFault = new BackgroundWorker();
            this.mWorkerFault.WorkerReportsProgress = true;
            this.mWorkerFault.DoWork += new DoWorkEventHandler(mWorkerFault_DoWork);
            this.mWorkerFault.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mWorkerFault_RunWorkerCompleted);
            this.mWorkerFault.ProgressChanged += new ProgressChangedEventHandler(mWorkerFault_ProgressChanged);
            OpenPHYFile();
        }

        void mWorkerFault_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is List<int>)
                ReportItemFinished(e.UserState as List<int>);
            else
                ReportItemStart(e.UserState);
        }

        void mWorkerFault_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pnlProcess.Visible = false;
            Application.DoEvents();
            MessageBox.Show("故障状态判读完毕！");
            this.menuStrip1.Enabled = true;
            this.tabControl1.Enabled = true;
        }

        void mWorkerFault_DoWork(object sender, DoWorkEventArgs e)
        {
            Scan(this.fileName);
        }

        void OpenPHYFile()
        {
            this.menuStrip1.Enabled = false;
            this.tabControl1.Enabled = false;
            this.pnlProcess.Visible = true;
            this.tabControl1.SelectedIndex = 0;
            this.lblInfo.Text = "正在读取飞参数据，请稍候...";
            this.mWorker.RunWorkerAsync();
        }

        void FormMain_Load(object sender, EventArgs e)
        {
            //this.mWorker = new BackgroundWorker();

            //isReadEnd = false;

            //this.toolStripMenuItem1.Enabled = false;

            //this.mWorker.DoWork += new DoWorkEventHandler(mWorker_DoWork);
            //this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mWorker_RunWorkerCompleted);

            //this.lblReadPHYFile.Visible = true;

            //this.mWorker.RunWorkerAsync();
        }

        void mWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowFlyParameter(this.mDataDT);
            ShowFlyParameterExtremum(this.mExtremumTable);
            isReadEnd = true;
            //this.lblReadPHYFile.Visible = false;
            this.menuStrip1.Enabled = true;
            this.tabControl1.Enabled = true;
            this.pnlProcess.Visible = false;
            Application.DoEvents();
            MessageBox.Show("飞参数据读取完毕！");
        }

        void mWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ShowFlyParamToDataGrid(this.fileName);
        }

        private void ShowFlyParamToDataGrid(string phyFile)
        {
            FileStream fs = new FileStream(phyFile, FileMode.Open, FileAccess.Read);
            BinaryReader binReader = new BinaryReader(fs);

            //读文件头（包括飞参记录时长）
            PHYHeader header = PHYHelper.ReadPHYHeader(binReader);
            int secondCount = PHYHelper.GetFlyParamSeconds(header);

            try
            {
                this.mDataDT = FlyParameter.CreateTable();
                Dictionary<string, DataRow> dictExtremum = this.CreateExtremumTable();

                for (int i = 1; i <= secondCount; i++)
                {
                    DataRow row = this.mDataDT.NewRow();
                    row["时间"] = BaseFunction.MSecondToTimeStringNew(header.GPSStartTime + (i - 1) * 1000);
                    row["秒值"] = i - 1;

                    //读取当前秒的飞参值
                    foreach (FlyParameter fp in FlyParameter.Parameters.Values.ToList())
                    {
                        float[] values = PHYHelper.ReadFlyParameter(binReader, i, header, fp);

                        row[fp.Caption] = values[0];// 一秒一个数据，只取第一个

                        if (dictExtremum.ContainsKey(fp.Caption))
                        {
                            DataRow drExtremum = dictExtremum[fp.Caption];
                            if (values[0] > Convert.ToSingle(drExtremum["最大值"]))
                            {
                                drExtremum["最大值"] = values[0];
                                drExtremum["时间(最大值)"] = row["时间"];
                                drExtremum["秒值(最大值)"] = row["秒值"];
                            }
                            if (values[0] < Convert.ToSingle(drExtremum["最小值"]))
                            {
                                drExtremum["最小值"] = values[0];
                                drExtremum["时间(最小值)"] = row["时间"];
                                drExtremum["秒值(最小值)"] = row["秒值"];
                            }
                        }
                    }
                    this.mDataDT.Rows.Add(row);
                }
                dictExtremum = null;
            }

            catch (PHYException pex)
            {
                MessageBox.Show("飞参文件数据异常，请检查。");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                binReader.Close();
                fs.Close();
            }
        }

        private void Scan(string phyFile)
        {
            //this.lblReadPHYFile.Visible = true;
            //this.lblInfo.Text = "正在判读飞参数据，请稍候...";
            //this.pnlProcess.Visible = true;

            FileStream fs = new FileStream(phyFile, FileMode.Open, FileAccess.Read);
            BinaryReader binReader = new BinaryReader(fs);

            PHYHeader header = PHYHelper.ReadPHYHeader(binReader);
            int secondCount = PHYHelper.GetFlyParamSeconds(header);

            try
            {
                List<AircraftFault> faults = AircraftFault.GetFaults();

                //记录每个故障的持续时间，初始为0
                Dictionary<AircraftFault, int> checkResult = new Dictionary<AircraftFault, int>();
                foreach (AircraftFault f in faults)
                    checkResult.Add(f, 0);

                //获取需要读取的飞参列表
                List<FlyParameter> needReadParams = FlyParameter.Parameters.Values.ToList();// FaultHelper.GetFlyParamNoRepetion(faults);

                //定义记录故障涉及的飞参的所有值
                Dictionary<string, List<float>> paramValues = new Dictionary<string, List<float>>();
                foreach (FlyParameter fp in needReadParams)
                    paramValues.Add(fp.ID, new List<float>());

                for (int i = 1; i <= secondCount; i++)
                {
                    //读取当前秒的飞参值
                    foreach (FlyParameter fp in needReadParams)
                    {
                        float[] values = PHYHelper.ReadFlyParameter(binReader, i, header, fp);
                        paramValues[fp.ID].AddRange(values);
                    }
                }
                //this.lblReadPHYFile.Visible = false;

                foreach (AircraftFault fault in faults)
                {
                    int gps_startTime = header.GPSStartTime;

                    this.mWorkerFault.ReportProgress(1, new FaultInfo { Fault = fault, ParamValues = paramValues, PHYHeader = header });

                    List<int> faultHappenTimes = new List<int>();
                    int duration = 0;//记录持续时间
                    for (int i = 1; i <= secondCount; i++)
                    {
                        if (FaultHelper.CheckFault(fault, paramValues, i))
                        {
                            duration++;//增加持续时间

                            if (duration == fault.Duration)
                                faultHappenTimes.Add(gps_startTime);
                            //ReportFaultInfo(new FaultInfo { HappenTime = fp_Time, Fault = fault, ParamValues = paramValues });//报告故障
                        }
                        else
                        {
                            duration = 0;//持续时间清零
                        }

                        gps_startTime += 1000;
                    }
                    this.mWorkerFault.ReportProgress(1, faultHappenTimes);
                }

                //把各个飞参数据调整相同频率，每秒1个。
                foreach (KeyValuePair<string, FlyParameter> kvp in FlyParameter.Parameters)
                {
                    paramValues[kvp.Key] = FlyParameter.GetData4Frequence(paramValues[kvp.Key], kvp.Value.Frequence);
                }
            }
            catch (PHYException pex)
            {
                MessageBox.Show("飞参文件数据异常，请检查！");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //this.lblReadPHYFile.Visible = false;
                binReader.Close();
                fs.Close();
                //this.pnlProcess.Visible = false;
            }

            isReadEnd = true;
        }

        private void ShowFlyParameter(DataTable dt)
        {
            this.dgvData.SuspendLayout();
            this.dgvData.ReadOnly = true;
            //this.dgvData.AllowUserToOrderColumns = false;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.DataSource = dt;//显示所有飞参数据
            this.dgvData.Columns[0].Frozen = true;
            foreach (DataGridViewColumn dvc in this.dgvData.Columns)
            {
                dvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dvc.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dvc.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }
            this.dgvData.Columns[0].DefaultCellStyle = this.dgvData.RowHeadersDefaultCellStyle;
            //this.dgvData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.dgvData.ResumeLayout();
        }

        private void ShowFlyParameterExtremum(DataTable dt)
        {
            this.dgvExtremum.ReadOnly = true;
            this.dgvExtremum.RowHeadersVisible = false;
            this.dgvExtremum.DataSource = dt;
            this.dgvExtremum.Columns[0].Frozen = true;

            foreach (DataGridViewColumn dvc in this.dgvExtremum.Columns)
            {
                dvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //dvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dvc.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //dvc.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dvc.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }

            //this.dgvExtremum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //this.dgvExtremum.Columns[0].FillWeight = 23.8f;
            //this.dgvExtremum.Columns[1].FillWeight = 12.7f;
            //this.dgvExtremum.Columns[2].FillWeight = 12.7f;
            //this.dgvExtremum.Columns[3].FillWeight = 12.7f;
            //this.dgvExtremum.Columns[4].FillWeight = 12.7f;
            //this.dgvExtremum.Columns[5].FillWeight = 12.7f;
            //this.dgvExtremum.Columns[6].FillWeight = 12.7f;
            //this.dgvExtremum.RowHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            //this.dgvExtremum.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dgvExtremum.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            //this.dgvExtremum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvExtremum.Columns[0].DefaultCellStyle = this.dgvExtremum.RowHeadersDefaultCellStyle;
            this.dgvExtremum.Columns["时间(最大值)"].HeaderText = "时间";
            this.dgvExtremum.Columns["秒值(最大值)"].HeaderText = "秒值";
            this.dgvExtremum.Columns["时间(最小值)"].HeaderText = "时间";
            this.dgvExtremum.Columns["秒值(最小值)"].HeaderText = "秒值";
            //this.dgvExtremum.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.dgvExtremum.Refresh();
        }


        private Dictionary<string, DataRow> CreateExtremumTable()
        {
            Dictionary<string, DataRow> dict = new Dictionary<string, DataRow>();
            mExtremumTable = new DataTable();
            mExtremumTable.Columns.Add(new DataColumn("参数名称"));
            mExtremumTable.Columns.Add(new DataColumn("最大值", typeof(float)));
            mExtremumTable.Columns.Add(new DataColumn { ColumnName = "时间(最大值)" });
            mExtremumTable.Columns.Add(new DataColumn { ColumnName = "秒值(最大值)" });
            mExtremumTable.Columns.Add(new DataColumn("最小值", typeof(float)));
            mExtremumTable.Columns.Add(new DataColumn { ColumnName = "时间(最小值)" });
            mExtremumTable.Columns.Add(new DataColumn { ColumnName = "秒值(最小值)" });

            foreach (KeyValuePair<string, FlyParameter> kvp in FlyParameter.Parameters)
            {
                if (kvp.Value.SubIndex == -1)
                {
                    DataRow dr = mExtremumTable.NewRow();
                    dr["参数名称"] = kvp.Value.Caption;
                    dr["最大值"] = float.MinValue;
                    dr["最小值"] = float.MaxValue;
                    mExtremumTable.Rows.Add(dr);
                    dict.Add(kvp.Value.Caption, dr);
                }
            }
            return dict;
        }

        private void ReportItemStart(object item)
        {
            ListViewItem lvi = new ListViewItem(new string[] { item.ToString(), "处理中......", string.Empty });
            lvi.Tag = item;
            this.lvCheck.Items.Add(lvi);
        }

        private void ReportItemFinished(List<int> list)
        {
            ListViewItem lvi = this.lvCheck.Items[this.lvCheck.Items.Count - 1];
            if (list == null || list.Count == 0)
            {
                lvi.SubItems[1].Text = "正常";
            }
            else
            {
                lvi.SubItems[1].Text = "异常";
                FaultInfo fi = lvi.Tag as FaultInfo;
                if (fi != null)
                {
                    StringBuilder sb = new StringBuilder();
                    fi.HappenTimes = list;
                    int starttime = list[0];
                    foreach (int time in list)
                    {
                        if (time == starttime || ((time - starttime) > (fi.Fault.Duration * 1000)))
                        {
                            sb.Append(string.Format("{0} ", BaseFunction.MSecondToTimeStringNew(time)));
                        }
                    }
                    lvi.SubItems[2].Text = sb.ToString();
                }
            }
            Application.DoEvents();
        }

        public void Reflash()
        {
            FileStream fs = new FileStream(this.fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binReader = new BinaryReader(fs);

            PHYHeader header = PHYHelper.ReadPHYHeader(binReader);

            this.toolStripStatusLabel8.Text = this.fileName;

            this.toolStripStatusLabel4.Text = header.AircrfName;
            this.toolStripStatusLabel6.Text = header.AircrfNum;
            DateTime time = header.BTime;
            this.toolStripStatusLabel1.Text = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString() + " "
                + BaseFunction.MSecondToTimeStringNew(header.GPSStartTime) + "-" + BaseFunction.MSecondToTimeStringNew(header.GPSEndTime);

            ShowFlyParameter();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            this.btnScan.Enabled = false;
            this.lvCheck.Items.Clear();
            Scan(this.fileName);
            this.btnScan.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormFaultDetail f = new FormFaultDetail();
            f.ShowDialog();
        }

        private void lvCheck_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FileStream fs = new FileStream(this.fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binReader = new BinaryReader(fs);

            PHYHeader header = PHYHelper.ReadPHYHeader(binReader);
            if (this.lvCheck.SelectedItems.Count > 0)
            {
                FaultInfo ff = this.lvCheck.SelectedItems[0].Tag as FaultInfo;
                if (ff != null)
                {
                    FormFaultDetail f = new FormFaultDetail(ff, this.mDataDT, header.GPSStartTime);
                    f.ShowDialog();
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.fileName = openFileDialog1.FileName;
                this.Reflash();
                //this.lblReadPHYFile.Visible = true;
                this.ClearInfo();
                OpenPHYFile();
            }
        }

        private void ClearInfo()
        {
            this.lvCheck.Items.Clear();
            this.dgvData.DataSource = null;
            this.dgvExtremum.DataSource = null;
            this.mDataDT = null;
            this.mExtremumTable = null;
            this.故障诊断ToolStripMenuItem.Visible = true;
            this.故障信息ToolStripMenuItem.Visible = false;
            this.mAllListViewItems = null;
        }

        private void 故障诊断ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.故障诊断ToolStripMenuItem.Enabled = false;
            this.menuStrip1.Enabled = false;
            this.tabControl1.Enabled = false;
            this.lvCheck.Items.Clear();
            this.mAllListViewItems = null;
            this.tabControl1.SelectedIndex = 2;

            isReadEnd = false;

            this.lblInfo.Text = "正在判读飞参数据，请稍候...";
            this.pnlProcess.Visible = true;

            this.mWorkerFault.RunWorkerAsync();
            this.故障诊断ToolStripMenuItem.Enabled = true;
            this.故障诊断ToolStripMenuItem.Visible = false;
            this.故障信息ToolStripMenuItem.Visible = true;

        }

        void ShowFlyParameter()
        {
            DataTable mDataDT = new DataTable();

            FileStream fs = new FileStream(this.fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binReader = new BinaryReader(fs);

            PHYHeader header = PHYHelper.ReadPHYHeader(binReader);

            //构造记录集的列头
            foreach (KeyValuePair<string, FlyParameter> fp in FlyParameter.Parameters)
            {
                string columnName;
                if (fp.Value.Unit == "")
                {
                    columnName = fp.Value.Caption;
                }
                else
                {
                    columnName = fp.Value.Caption + "(" + fp.Value.Unit + ")";
                }
                DataColumn dc = new DataColumn(columnName);
                mDataDT.Columns.Add(dc);
            }

            ////生成记录集的记录。
            //for (int i = 0; i < header.FlySeconds * 2; i++)
            //{
            //    DataRow dr = mDataDT.NewRow();
            //    foreach (KeyValuePair<string, FlyParameter> kvp in FlyParameter.Parameters)
            //    {
            //        dr[kvp.Value.Caption] = this.mFaultInfo.ParamValues[kvp.Key][i];
            //    }
            //    mDataDT.Rows.Add(dr);
            //}
        }

        private List<ListViewItem> mAllListViewItems = null;
        private void lvCheck_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 1)
            {
                if (mAllListViewItems == null)
                {
                    mAllListViewItems = new List<ListViewItem>();
                    foreach (ListViewItem item in this.lvCheck.Items)
                    {
                        mAllListViewItems.Add(item);
                        FaultInfo fi = item.Tag as FaultInfo;
                        if (fi == null || fi.HappenTimes == null || fi.HappenTimes.Count == 0)
                            item.Remove();
                    }
                }
                else
                {
                    this.lvCheck.Items.Clear();
                    foreach (ListViewItem item in this.mAllListViewItems)
                    {
                        this.lvCheck.Items.Add(item);
                    }
                    this.mAllListViewItems = null;
                }
            }
        }

        private void 表格显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }

        private void 极值列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }

        private void 故障信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }

        private void OnCellDoubleClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex == -1 && e.ColumnIndex > 1)
            {
                var col = this.mDataDT.Columns[e.ColumnIndex];
                var caption = col.ColumnName;
                if (FlyParameter.CaptionParameters.ContainsKey(caption))
                {
                    var parameter = FlyParameter.CaptionParameters[caption];

                    FormDetail detail = new FormDetail();
                    List<float> mdata = this.GenerateSubTableData(this.mDataDT, col);

                    detail.AddCurve(parameter, mdata);

                    detail.Text = caption + " 曲线";
                    detail.Show();
                }
            }
        }

        private List<float> GenerateSubTableData(DataTable dataTable, DataColumn col)
        {
            List<float> datas = new List<float>();

            if (dataTable != null && col != null && !string.IsNullOrEmpty(col.ColumnName)
                && dataTable.Columns.Contains(col.ColumnName))
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var row = dataTable.Rows[i];
                    var item = row[col];
                    float dt = 0F;
                    float.TryParse(item.ToString(), out dt);
                    datas.Add(dt);
                }
            }

            return datas;
        }

        private DataTable GenerateSubTable(DataTable dataTable, DataColumn col)
        {
            throw new NotImplementedException();
        }

    }

    public class FaultInfo
    {
        public AircraftFault Fault { get; set; }
        public Dictionary<string, List<float>> ParamValues { get; set; }
        public List<int> HappenTimes { get; set; }
        public PHYHeader PHYHeader { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", this.Fault.Title);
        }
    }
}
