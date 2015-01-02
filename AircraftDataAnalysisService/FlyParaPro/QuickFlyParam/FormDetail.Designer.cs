namespace QuickFlyParam
{
    partial class FormDetail
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
            this.ctrCurve1 = new NPlot.CtrCurve();
            this.SuspendLayout();
            // 
            // ctrCurve1
            // 
            this.ctrCurve1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrCurve1.GetParameterObject = null;
            this.ctrCurve1.Location = new System.Drawing.Point(0, 0);
            this.ctrCurve1.Name = "ctrCurve1";
            this.ctrCurve1.Size = new System.Drawing.Size(784, 201);
            this.ctrCurve1.TabIndex = 0;
            // 
            // FormDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 201);
            this.Controls.Add(this.ctrCurve1);
            this.Name = "FormDetail";
            this.Text = "FormDetail";
            this.ResumeLayout(false);

        }

        #endregion

        private NPlot.CtrCurve ctrCurve1;
    }
}