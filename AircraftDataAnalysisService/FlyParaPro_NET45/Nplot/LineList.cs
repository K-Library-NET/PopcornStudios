using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace NPlot
{
    public class LineList
    {
        private const int POINTS_PERSECOND = 1;
        public LineList()
        {
        }

        public LineList(IParameter param, List<float> data)
        {
            if (data == null) return;

            this.PointList = new List<PointF>();
            this.LineName = param.Caption;
            this.LineUnit = param.Unit;
            this.X = new List<float>();
            this.Y = new List<float>();
            float px = 0;
            //float py = 0;
            float interval = 1f / POINTS_PERSECOND;

            #region 旧代码
            //if (param.Frequence >= POINTS_PERSECOND)
            //{
            //    int j = 0, step = param.Frequence / POINTS_PERSECOND;
            //    while (j < data.Count)
            //    {
            //        int index = j * step;
            //        if (index >= data.Count) break;

            //        X.Add(px);
            //        Y.Add(data[index]);
            //        //PointF pointF = new PointF(px, data[index]);
            //        //this.PointList.Add(pointF);
            //        px += interval;
            //        j++;
            //    }
            //}
            //else
            //{
            //    foreach (float f in data)
            //    {
            //        for (int i = 0; i < POINTS_PERSECOND; i++)
            //        {
            //            //PointF pointF = new PointF(px, f);
            //            //this.PointList.Add(pointF);
            //            X.Add(px);
            //            Y.Add(f);
            //            px += interval;
            //        }
            //    }
            //}
            #endregion

            foreach (float f in data)
            {
                    X.Add(px);
                    Y.Add(f);
                    px += interval;
            }

            this.PointCount = this.PointList.Count;
            this.EndNum = this.PointCount - 1;
        }

        //曲线点坐标数组
        public List<PointF> PointList { get; set; }

        public List<float> X { get; set; }
        public List<float> Y { get; set; }

        //曲线点数量
        public int PointCount { get; set; }

        //截曲线的起始点
        public int StartNum { get; set; }

        //截曲线的终点
        public int EndNum { get; set; }

        //曲线名称
        public string LineName { get; set; }

        //曲线单位
        public string LineUnit { get; set; }
    }
}
