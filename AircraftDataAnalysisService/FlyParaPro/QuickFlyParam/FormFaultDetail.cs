using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using FlyParamBusiness.Fault;
using FlyParamBusiness;
using NPlot;

namespace QuickFlyParam
{
    public partial class FormFaultDetail : Form, NPlot.IGetParameter
    {
        private FaultInfo mFaultInfo;
        private BackgroundWorker mWorker;

        private int gpsTime;

        public FormFaultDetail()
        {
            InitializeComponent();
        }

      
        public FormFaultDetail(FaultInfo faultInfo, DataTable dt, int gpsStartTime)
        {
            InitializeComponent();

            this.mDataDT = dt;
            this.mFaultInfo = faultInfo;
            this.Load += new EventHandler(FormFaultDetail_Load);
            Initial();

            this.mWorker = new BackgroundWorker();
            this.mWorker.DoWork += new DoWorkEventHandler(mWorker_DoWork);
            this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mWorker_RunWorkerCompleted);

            this.dgvData.ReadOnly = true;
            this.dgvData.Visible = false;

            this.gpsTime = gpsStartTime;

            ShowDataBySecond(0);
        }

        void FormFaultDetail_Load(object sender, EventArgs e)
        {
            //this.mWorker.RunWorkerAsync();
        }

        void mWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.dgvData.DataSource = this.mDataDT;
            this.dgvData.Visible = true;

            foreach (DataGridViewColumn dvc in this.dgvData.Columns)
                dvc.SortMode = DataGridViewColumnSortMode.NotSortable;

            this.Refresh();
        }

        private DataTable mDataDT;
        void mWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //构造记录集的列头
            this.mDataDT = new DataTable();
            foreach (KeyValuePair<string, FlyParameter> fp in FlyParameter.Parameters)
            {
                DataColumn dc = new DataColumn(fp.Value.Caption);
                mDataDT.Columns.Add(dc);
            }
            //生成记录集的记录。
            for (int i = 0; i < this.mFaultInfo.PHYHeader.FlySeconds ; i++)
            {
                DataRow dr = mDataDT.NewRow();
                foreach (KeyValuePair<string, FlyParameter> kvp in FlyParameter.Parameters)
                {
                    dr[kvp.Value.Caption] = this.mFaultInfo.ParamValues[kvp.Key][i];
                }
                mDataDT.Rows.Add(dr);
            }
        }

        private void Initial()
        {
            if (this.mFaultInfo == null) return;

            foreach (ParameterCondition pc in this.mFaultInfo.Fault.ParamConditions)
            {
                AddCurve(pc.Parameter
                    , this.mFaultInfo.ParamValues[pc.Parameter.ID]
                    ,this.mFaultInfo.HappenTimes
                    ,(int)this.mFaultInfo.Fault.Duration);
            }

            this.lblFault.Text = this.mFaultInfo.Fault.Title;
            this.lblHandle.Text = this.mFaultInfo.Fault.EventHandling;
            this.lblLevel.Text = this.mFaultInfo.Fault.EventLevel.ToString();
            this.lblRemark.Text = this.mFaultInfo.Fault.Remark;
        }

        //为了拖拉的时候同步它们的坐标，要记录添加的图表控件
        private List<CtrCurve> mCtrCurves = new List<CtrCurve>();
        /// <summary>
        /// 动态增加图表控件
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="data"></param>
        private void AddCurve(FlyParameter parameter, List<float> data, List<int> happenTimes,int duration)
        {
            CtrCurve curve = new CtrCurve();
            curve.GetParameterObject = this;
            curve.Height = 200;
            curve.Dock = DockStyle.Top;
            NPlot.LineList lineList = new NPlot.LineList(parameter, data);
            curve.SuspendLayout();
            curve.InitCurve();
            curve.DrawCurve(lineList, 0, lineList.EndNum);
            curve.Parameters.Add(parameter);

            //添加红线（故障的发生时间）
            List<VerticalLine> vlines = GetVerticalLines(happenTimes, duration);
            if (vlines != null)
            {
                curve.AddVerticalLines(vlines);
            }

            this.pnlContainer.Controls.Add(curve);
            curve.ResumeLayout();
            curve.BringToFront();

            curve.CtrCurveClick += new CtrCurveClickEvent(curve_CtrCurveClick);
            //curve.CurveInteractionOccured += new NPlot.Windows.PlotSurface2D.InteractionHandler(curve_CurveInteractionOccured);
            curve.RestoreStatusClick += new EventHandler(curve_RestoreStatusClick);

            this.mCtrCurves.Add(curve);
        }

        private List<VerticalLine> GetVerticalLines(List<int> happenTimes, int duration)
        {
            if (happenTimes == null || happenTimes.Count == 0) return null;

            List<VerticalLine> vlines = new List<VerticalLine>();
            int starttime = happenTimes[0];
            foreach (int time in happenTimes)
            {
                if (time == starttime 
                    || ((time - starttime) > (duration * 1000)))
                {
                    VerticalLine vl = new VerticalLine((time - this.mFaultInfo.PHYHeader.GPSStartTime) / 1000, Color.Red);
                    vl.Pen.Width = 2;
                    vlines.Add(vl);
                    starttime = time;
                }
            }
            return vlines;
        }

        private bool mNeedInteraction = true;
        void curve_RestoreStatusClick(object sender, EventArgs e)
        {
            this.mNeedInteraction = false;
            CtrCurve currentCurve = sender as CtrCurve;
            if (currentCurve != null)
            {
                //this.pnlContainer.SuspendLayout();
                foreach (CtrCurve curve in this.mCtrCurves)
                {
                    if (curve.Equals(currentCurve) == false)
                    {
                        curve.PlotSurface2DObject.OriginalDimensions();
                    }
                }
                //this.pnlContainer.ResumeLayout();
                //this.pnlContainer.Refresh();
            }
            this.mNeedInteraction = true;
        }

        void curve_CurveInteractionOccured(object sender)
        {
            //if (mNeedInteraction == false) return;

            CtrCurve currentCurve = sender as CtrCurve;
            if (currentCurve != null)
            {
                foreach (CtrCurve curve in this.mCtrCurves)
                {
                    if (curve.Equals(currentCurve) == false)
                    {
                        curve.PlotSurface2DObject.XAxis1 = currentCurve.PlotSurface2DObject.XAxis1;
                        curve.PlotSurface2DObject.Refresh();
                    }
                }
            }
        }
        /// <summary>
        /// 处理图表点击事件，根据X值定位表格里的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void curve_CtrCurveClick(CtrCurve sender, double x, double y)
        {
            IParameter param = sender.Parameters[0];
            
            int index = (int)x;

            if ((index > 0) && (index < this.mDataDT.Rows.Count - 1))
            {
                ShowDataBySecond(index);
            }
            else
                ShowDataBySecond(0);
        }

        private void ShowDataBySecond(int second)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("参数名称"));
            //dt.Columns.Add(new DataColumn("参数值",typeof(float)));
            dt.Columns.Add(new DataColumn("参数值"));

            DataRow dr1 = dt.NewRow();
            dr1["参数名称"] = "时间";
            dr1["参数值"] = BaseFunction.MSecondToTimeStringNew(this.gpsTime + (second - 1) * 1000);
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["参数名称"] = "秒值";
            dr2["参数值"] = second.ToString();
            dt.Rows.Add(dr2);

            foreach (DataColumn dc in this.mDataDT.Columns)
            {
                if (dc.Ordinal > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr["参数名称"] = dc.ColumnName;
                    dr["参数值"] = mDataDT.Rows[second][dc];
                    dt.Rows.Add(dr);
                }
            }
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.DataSource = dt;
            this.dgvData.Visible = true;
            this.dgvData.Columns[0].Frozen = true;
            this.dgvData.Columns[0].DefaultCellStyle = this.dgvData.RowHeadersDefaultCellStyle;

            foreach (DataGridViewColumn dvc in this.dgvData.Columns)
            {
                dvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                //dvc.FillWeight = 50;
                dvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dvc.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dvc.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            }
            //this.dgvData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.dgvData.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        #region NPlot.IGetParameter接口实现
        public List<float> GetParameterData(IParameter param)
        {
            return this.mFaultInfo.ParamValues[param.ID];
        }
        public List<IParameter> GetParameterDef()
        {
            List<IParameter> list = new List<IParameter>();
            list.AddRange(FlyParameter.Parameters.Values.ToArray());
            return list;
        }
        #endregion

    }
}
