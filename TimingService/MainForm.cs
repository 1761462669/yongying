using Business;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimingService
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        List<GetCallTarget> timeConfig = new List<GetCallTarget>();
        DateTime? time = null;
        private void MainForm_Load(object sender, EventArgs e)
        {
            StartService();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            foreach (GetCallTarget item in timeConfig)
            {
                time = Convert.ToDateTime(item.time);
                String mark = item.mark;
                if (!time.HasValue)
                {
                    StopService();
                    return;
                }
                if (DateTime.Now.Hour.Equals(time.Value.Hour) && DateTime.Now.Minute.Equals(time.Value.Minute))
                {
                    int split = DateTime.Now.Second - time.Value.Second;
                    if (split >= 0 && split < 5)
                    {
                        _CallService cs = new _CallService();
                        try
                        {
                            cs.GetService(mark);
                            //此处替换需要调用的代码
                           //MessageBox.Show("定时执行调用了："+mark);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }

        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
            StopService();
        }

        private void bt_start_Click(object sender, EventArgs e)
        {
            StartService();
        }
        private void StartService()
        {
            _GetCallTarget gtc = new _GetCallTarget();
            try
            {
                gtc.GetCall(out timeConfig);
            }
            catch (Exception)
            {
                StopService();
                MessageBox.Show("正确检查配置文件，并重新启动服务！");
            }

            timer1.Interval = 5000;
            this.Text = "定时服务，运行中";
            timer1.Start();
        }
        private void StopService()
        {
            this.Text = "定时服务，已停止";
            //this.textBox1.Text = "";
            timer1.Stop();
        }
    }
}
