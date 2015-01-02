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
    public partial class frmReplaceCurve : Form
    {
        private List<IParameter> mAllParams;
        private List<IParameter> mExistParams;

        public frmReplaceCurve(List<IParameter> allParams, List<IParameter> existParams)
        {
            InitializeComponent();
            this.mAllParams = allParams;
            this.mExistParams = existParams;
        }

        public IParameter NewParameter { get; private set; }
        public IParameter OldParameter { get; private set; }

        private void frmReplaceCurve_Load(object sender, EventArgs e)
        {
            this.cboxNewName.DataSource = this.mAllParams;
            this.cboxNewName.DisplayMember = "Caption";
            this.cboxOldName.DataSource = this.mExistParams;
            this.cboxOldName.DisplayMember = "Caption";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.NewParameter = this.cboxNewName.SelectedItem as IParameter;
            this.OldParameter = this.cboxOldName.SelectedItem as IParameter;
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
