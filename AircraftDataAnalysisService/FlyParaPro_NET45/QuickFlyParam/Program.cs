using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace QuickFlyParam
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyAppContext());
        }
    }

    // The class that handles the creation of the application windows
    class MyAppContext : ApplicationContext
    {
        private FormLogin Form1;
        private FormMain Form2;
        private int formCount;
        private Timer loginTimer = new Timer();

        public MyAppContext()
        {
            formCount = 0;

            // Create both application forms and handle the Closed event
            // to know when both forms are closed.
            Form1 = new FormLogin();
            Form1.Closed += new EventHandler(OnFormClosed);
            formCount++;

            //Form2 = new FormMain();
            //Form2.Closed += new EventHandler(OnFormClosed);            
            //formCount++;

            loginTimer.Tick += new EventHandler(loginTimer_Tick);
            loginTimer.Interval = 2000;
            loginTimer.Enabled = true;

            Form1.Show();
            //Form2.Hide();
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            // When a form is closed, decrement the count of open forms.

            // When the count gets to 0, exit the app by calling
            // ExitThread().
            formCount--;
            if (formCount == 0)
            {
                ExitThread();
            }
        }

        void loginTimer_Tick(object sender, EventArgs e)
        {
            loginTimer.Enabled = false;
            loginTimer.Dispose();


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "";
            openFileDialog.Filter = "飞参数据文件|*.phy|所有文件|*.*";
            openFileDialog.Title = "请选择飞参文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //this.MainForm = Form2;
                Form2 = new FormMain();
                Form2.Closed += new EventHandler(OnFormClosed);
                formCount++;
                Form2.fileName = openFileDialog.FileName;
                Form2.Reflash();
                Form2.Show();

            }
            else
            {
                Application.Exit();
            }
            Form1.Close();
        }
    }
}
