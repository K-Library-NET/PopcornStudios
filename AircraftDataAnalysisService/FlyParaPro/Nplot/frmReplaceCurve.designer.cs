namespace NPlot
{
    partial class frmReplaceCurve
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboxNewName = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxOldName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择更换参数：";
            // 
            // cboxNewName
            // 
            this.cboxNewName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxNewName.FormattingEnabled = true;
            this.cboxNewName.Location = new System.Drawing.Point(124, 68);
            this.cboxNewName.Name = "cboxNewName";
            this.cboxNewName.Size = new System.Drawing.Size(144, 20);
            this.cboxNewName.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(38, 114);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(163, 114);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择被更换参数：";
            // 
            // cboxOldName
            // 
            this.cboxOldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxOldName.FormattingEnabled = true;
            this.cboxOldName.Location = new System.Drawing.Point(124, 27);
            this.cboxOldName.Name = "cboxOldName";
            this.cboxOldName.Size = new System.Drawing.Size(144, 20);
            this.cboxOldName.TabIndex = 5;
            // 
            // frmReplaceCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 166);
            this.Controls.Add(this.cboxOldName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboxNewName);
            this.Controls.Add(this.label1);
            this.Name = "frmReplaceCurve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更换曲线";
            this.Load += new System.EventHandler(this.frmReplaceCurve_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboxNewName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxOldName;
    }
}