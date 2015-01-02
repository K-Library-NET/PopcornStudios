using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPlot
{
    public partial class frmAddCurve : Form
    {
        public frmAddCurve()
        {
            InitializeComponent();
        }

        public frmAddCurve(List<IParameter> list)
        {
             InitializeComponent();

             this.cboxDataName.DataSource = list;
             this.cboxDataName.DisplayMember = "Caption";
        }

        public IParameter SelectedParameter { get; private set; }

        private void frmAddCurve_Load(object sender, EventArgs e)
        {
            //DataCon dataCon = new DataCon();
            //dataCon.BindCbox("select PCaption from tb_PDataInfo", "tb_PDataInfo", "PCaption", cboxDataName);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SelectedParameter = this.cboxDataName.SelectedItem as IParameter;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
