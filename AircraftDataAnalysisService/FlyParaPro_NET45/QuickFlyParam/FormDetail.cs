using FlyParamBusiness.Fault;
using NPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickFlyParam
{
    public partial class FormDetail : Form, IGetParameter
    {
        private List<float> mdata;
        public FormDetail()
        {
            InitializeComponent();
        }

        public FlyParameter Parameter
        {
            get;
            set;
        }

        public void AddCurve(FlyParameter parameter, List<float> data)
        {
            this.Parameter = parameter;
            this.mdata = data;

            CtrCurve curve = this.ctrCurve1;
            curve.GetParameterObject = this;
            curve.Height = 200;
            curve.Dock = DockStyle.Top;
            NPlot.LineList lineList = new NPlot.LineList(parameter, data);
            curve.SuspendLayout();
            curve.InitCurve();
            curve.DrawCurve(lineList, 0, lineList.EndNum);
            curve.Parameters.Add(parameter);
            curve.ResumeLayout();
            curve.BringToFront();
        }

        public List<float> GetParameterData(IParameter param)
        {
            if (param != null && this.Parameter != null && param.Caption == this.Parameter.Caption)
            {
                return this.mdata;
            }

            return new List<float>();
        }

        public List<IParameter> GetParameterDef()
        {
            var list = new List<IParameter>();
            if (this.Parameter == null)
            {
                var iparameter = this.Parameter as NPlot.IParameter;

                list.Add(iparameter);
            }
            return list;
        }
    }
}
