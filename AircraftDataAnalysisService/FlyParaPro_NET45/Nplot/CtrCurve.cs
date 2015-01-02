using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPlot
{
    public delegate void CtrCurveClickEvent(CtrCurve sender,double x,double y);
    public partial class CtrCurve : UserControl
    {
        public event CtrCurveClickEvent CtrCurveClick;
        public event NPlot.Windows.PlotSurface2D.InteractionHandler CurveInteractionOccured;
        public event EventHandler RestoreStatusClick;

        public NPlot.Windows.PlotSurface2D PlotSurface2DObject
        {
            get { return this.plotSurface2D1; }
        }

        public CtrCurve()
        {
            InitializeComponent();

            this.Parameters = new List<IParameter>();
            this.VerticalLines = new List<VerticalLine>();

            this.plotSurface2D1.PlotSursface2DClick += new NPlot.Windows.PlotSursface2DClickEvent(plotSurface2D1_PlotSursface2DClick);
            this.plotSurface2D1.InteractionOccured += new NPlot.Windows.PlotSurface2D.InteractionHandler(plotSurface2D1_InteractionOccured);
            this.plotSurface2D1.RestoreStatus += new EventHandler(plotSurface2D1_RestoreStatus);
        }

        void plotSurface2D1_RestoreStatus(object sender, EventArgs e)
        {
            if (this.RestoreStatusClick != null)
                this.RestoreStatusClick(this, e);
        }

        void plotSurface2D1_InteractionOccured(object sender)
        {
            if (this.CurveInteractionOccured != null)
                this.CurveInteractionOccured(this);
        }

        void plotSurface2D1_PlotSursface2DClick(double x, double y)
        {
            if (this.CtrCurveClick != null)
                this.CtrCurveClick(this, x, y);
        }

        public void InitCurve()
        {
            plotSurface2D1.Clear();
       
            NPlot.Grid mygrid = new NPlot.Grid();//新建网格
            mygrid.VerticalGridType = NPlot.Grid.GridType.Coarse;//横轴方向网格类型Coarse为粗糙型，Fine为细密型，None没有网格
            Pen majorGridPen = new Pen(Color.LightGray);//网格画笔（定义颜色）
            float[] pattern = { 2.0f, 1.0f };//画笔短划线和空白区域的数组
            majorGridPen.DashPattern = pattern;
            mygrid.MajorGridPen = majorGridPen;//MajorGridPen为Coarse对应画笔，MinorGridPen为Fine对应画笔
            plotSurface2D1.Add(mygrid);

            plotSurface2D1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());

            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());

            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(false));

            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());

            plotSurface2D1.RightMenu = NPlot.Windows.PlotSurface2D.DefaultContextMenu;

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("-"));

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("更换曲线", mnuReplaceCurve_Click));

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("增加曲线", mnuAddCurve_Click));

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("删除曲线", mnuDeleteCurve_Click));

            plotSurface2D1.PlotBackColor = Color.White;

            NPlot.Legend legend = new NPlot.Legend();
            legend.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Left);
            legend.VerticalEdgePlacement = NPlot.Legend.Placement.Inside;
            legend.HorizontalEdgePlacement = NPlot.Legend.Placement.Inside;
            legend.YOffset = 8;
            legend.XOffset = 8;
            legend.BorderStyle = LegendBase.BorderType.Line;

            plotSurface2D1.Legend = legend;
            plotSurface2D1.LegendZOrder = 0;
        }

        public void InitAction()
        {
            plotSurface2D1.Clear();

            NPlot.Grid mygrid = new NPlot.Grid();//新建网格
            mygrid.VerticalGridType = NPlot.Grid.GridType.Coarse;//横轴方向网格类型Coarse为粗糙型，Fine为细密型，None没有网格
            Pen majorGridPen = new Pen(Color.LightGray);//网格画笔（定义颜色）
            float[] pattern = { 2.0f, 1.0f };//画笔短划线和空白区域的数组
            majorGridPen.DashPattern = pattern;
            mygrid.MajorGridPen = majorGridPen;//MajorGridPen为Coarse对应画笔，MinorGridPen为Fine对应画笔
            plotSurface2D1.Add(mygrid);

            // 启动默认的右键菜单功能
            plotSurface2D1.RightMenu = NPlot.Windows.PlotSurface2D.DefaultContextMenu;

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("-"));

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("更换曲线", mnuReplaceCurve_Click));

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("增加曲线", mnuAddCurve_Click));

            plotSurface2D1.RightMenu.Menu.MenuItems.Add(new MenuItem("删除曲线", mnuDeleteCurve_Click));

            //背景颜色
            plotSurface2D1.PlotBackColor = Color.White;

            NPlot.Legend legend = new NPlot.Legend();//图例
            legend.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Right);//图例位置
            legend.VerticalEdgePlacement = NPlot.Legend.Placement.Inside;
            legend.HorizontalEdgePlacement = NPlot.Legend.Placement.Inside;
            legend.YOffset = 8;
            legend.XOffset = -8;
            legend.BorderStyle = LegendBase.BorderType.Line;

            plotSurface2D1.Legend = legend;
            plotSurface2D1.LegendZOrder = 0; // default zorder for adding idrawables is 0, so this puts legend on top.
        }

        public List<LineList> LineListSets = new List<LineList>();

        public List<VerticalLine> VerticalLines { get; private set; }

        public List<IParameter> Parameters { get; private set; }

        public int startNum;

        public int endNum;

        public int lineCount;

        public void AddVerticalLines(List<VerticalLine> lines)
        {
            this.VerticalLines = lines;
            foreach (VerticalLine vl in lines)
            {
                this.PlotSurface2DObject.Add(vl);
            }
        }

        public void DrawCurve(LineList lineList, int startNum, int endNum)
        {
            this.LineListSets.Add(lineList);

            this.startNum = startNum;
            this.endNum = endNum;

            string YAxisLable = string.Empty;

            LinePlot linePlot = new LinePlot();

            linePlot.OrdinateData = lineList.Y;//高度-时间曲线纵坐标
            linePlot.AbscissaData = lineList.X;//高度-时间曲线横坐标
            linePlot.Label = lineList.LineName;

            switch (this.LineListSets.Count)
            {
                case 1:
                    linePlot.Color = Color.Blue;
                    break;
                case 2:
                    linePlot.Color = Color.Black;
                    break;
                case 3:
                    linePlot.Color = Color.Purple;
                    break;
                case 4:
                    linePlot.Color = Color.Green;
                    break;
                default:
                    linePlot.Color = Color.Brown;
                    break;
            }

            linePlot.Pen.Width = 2.0f;
            linePlot.Shadow = true;//画线时加阴影

            //YAxisLable = (lineList.LineName + "(" + lineList.LineUnit + ")");

            plotSurface2D1.Add(linePlot);

            plotSurface2D1.XAxis1.Color = Color.Black;

            plotSurface2D1.YAxis1.Color = Color.Black;

            plotSurface2D1.XAxis1.Label = "绝对时间(s)";
            plotSurface2D1.XAxis1.LabelOffset = 0;//轴注释与轴间距

            plotSurface2D1.YAxis1.LabelOffset = 0;
            plotSurface2D1.YAxis1.Label += YAxisLable;

            this.lineCount = this.LineListSets.Count;
        }

        public void ReDrawCurve()
        {
            this.InitCurve();
            string YAxisLable = string.Empty;
            int lineCount = 1;//主要用来改变颜色
            foreach (LineList lineList in this.LineListSets)
            {
                LinePlot linePlot = new LinePlot();

                linePlot.OrdinateData = lineList.Y;//高度-时间曲线纵坐标
                linePlot.AbscissaData = lineList.X;//高度-时间曲线横坐标
                linePlot.Label = lineList.LineName;

                //if (this.LineListSets.Count == 1)
                //    linePlot.Color = Color.Blue;
                //else
                //    linePlot.Color = Color.Black;

                switch (lineCount)
                {
                    case 1:
                        linePlot.Color = Color.Blue;
                        break;
                    case 2:
                        linePlot.Color = Color.Black;
                        break;
                    case 3:
                        linePlot.Color = Color.Purple;
                        break;
                    case 4:
                        linePlot.Color = Color.Cyan;
                        break;
                    default:
                        linePlot.Color = Color.Brown;
                        break;
                }
                lineCount++;

                linePlot.Pen.Width = 2.0f;
                linePlot.Shadow = true;//画线时加阴影

                //YAxisLable = (lineList.LineName + "(" + lineList.LineUnit + ")");

                plotSurface2D1.Add(linePlot);

                plotSurface2D1.XAxis1.Color = Color.Black;

                plotSurface2D1.YAxis1.Color = Color.Black;

                plotSurface2D1.XAxis1.Label = "绝对时间(s)";
                plotSurface2D1.XAxis1.LabelOffset = 0;//轴注释与轴间距

                plotSurface2D1.YAxis1.LabelOffset = 0;
                plotSurface2D1.YAxis1.Label += YAxisLable;
            }
            this.lineCount = this.LineListSets.Count;

            AddVerticalLines(this.VerticalLines);

            this.Refresh();
        }

        private void mnuReplaceCurve_Click(object sender, EventArgs e)
        {
            frmReplaceCurve frm = new frmReplaceCurve(this.GetParameterObject.GetParameterDef(), this.Parameters);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (GetParameterObject != null)
                {
                    //删除要替换的曲线
                    int index = this.Parameters.IndexOf(frm.OldParameter);
                    this.Parameters.RemoveAt(index);
                    this.LineListSets.RemoveAt(index);

                    //添加新曲线
                    List<float> data = GetParameterObject.GetParameterData(frm.NewParameter);
                    LineList lineList = new LineList(frm.NewParameter, data);
                    lineList.LineUnit = frm.NewParameter.Unit;
                    lineList.LineName = frm.NewParameter.Caption;
                    this.LineListSets.Add(lineList);
                    this.Parameters.Add(frm.NewParameter);

                    ReDrawCurve();
                }
            }
        }

        private void mnuAddCurve_Click(object sender, EventArgs e)
        {
            List<IParameter> parameters = GetParameterObject.GetParameterDef();
            frmAddCurve frmaddCurve = new frmAddCurve(parameters);
            
            DialogResult dr = frmaddCurve.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (GetParameterObject != null)
                {
                    this.Parameters.Add(frmaddCurve.SelectedParameter);
                    List<float> data = GetParameterObject.GetParameterData(frmaddCurve.SelectedParameter);
                    LineList lineList = new LineList(frmaddCurve.SelectedParameter, data);
                    lineList.LineUnit = frmaddCurve.SelectedParameter.Unit;
                    lineList.LineName = frmaddCurve.SelectedParameter.Caption;
                    DrawCurve(lineList, startNum, endNum);
                    this.Refresh();
                }
            }
            
        }

        private void mnuDeleteCurve_Click(object sender, EventArgs e)
        {
            if (this.LineListSets.Count == 1)
            {
                MessageBox.Show("只有一条曲线，不允许删除！");
                return;
            }
            frmAddCurve frmaddCurve = new frmAddCurve(this.Parameters);

            DialogResult dr = frmaddCurve.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (GetParameterObject != null)
                {
                    int index = this.Parameters.IndexOf(frmaddCurve.SelectedParameter);
                    this.Parameters.RemoveAt(index);
                    this.LineListSets.RemoveAt(index);
                    ReDrawCurve();
                }
            }
        }

        public IGetParameter GetParameterObject { get; set; }
    }
}
