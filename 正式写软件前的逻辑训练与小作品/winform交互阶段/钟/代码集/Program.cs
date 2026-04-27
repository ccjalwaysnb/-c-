using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 新番茄钟
{
    public class Myform : Form1
    {
        private System.Windows.Forms.Timer timer;
        Label timeshowthing;
        Label pd;
        Button btn;
        char model = '0';//判断模式
        bool stsp = true;//是否暂停
        int hour = 0;
        int minute = 0;
        int second = 0;
        string[] timec = { "00", "00" };
        Button ctn_;
        Label[] ctn__ = new Label[3];
        NumericUpDown[] ctn = new NumericUpDown[3];//声明独属于倒计时的控件
        public Myform()//总运行方法
        {
            buttonmake();
            labelmake();
        }
        void buttonmake()//集合了所有创建按钮的方法
        {
            stspbutton();
            rebutton();
            mobutton();
        }
        void labelmake()//集合了所以创建显示器的方法
        {
            Label lbl = conmake(600, 50, 100, 0, "多功能时钟", "微软雅黑", 28);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            pd = conmake(350, 50, 180, 170, "当前并未选择启用的模式", "微软雅黑light", 10);
            pd.TextAlign = ContentAlignment.MiddleCenter;
            timelabel();
        }
/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////////////以下是按键创建方法
/// </summary>

        public Button conmake(int a, int b, int c, int d, string e, object f)//按件创造模板
        {
            Button atn = new Button();
            atn.Size = new Size(a, b);
            atn.Location = new Point(c, d);
            atn.Text = e;
            atn.Tag = f;
            this.Controls.Add(atn);
            return atn;
        }
        public void stspbutton()//创建暂停启动按钮
        {
            btn = conmake(50, 50, 590, 70, "启动", null);
            btn.Click += (s, e) =>
            {
                if (model == '0')//假如未选择模型
                {
                    MessageBox.Show("当前并未选择启动的模式，所以无法启动");
                }
                else
                {
                    if (stsp)
                    {
                        stsp = false;
                        btn.Text = "暂停";
                        timer.Start();
                    }
                    else
                    {
                        stsp = true;
                        btn.Text = "启动";
                        timer.Stop();
                    }
                }
            };
        }

        public void rebutton()//创建重置按钮
        {
            Button re=conmake(50, 50, 590, 120, "重置", null);
            re.Click += relogic;
        }

        public void mobutton()//创建模式切换按钮,
        {
            string[] moname = { "计时", "倒计时", "番茄钟" };
            for(int i=0;i<3;i++)
            {
                Button modelthing = conmake(50, 50, 100, 70 + i * 50, moname[i], null);
                modelthing.Click += relogic;
                switch (i)
                {
                    case 0:
                        modelthing.Click += (s, e) =>
                        {
                                model = '1';
                                pd.Text = "计时模式";
                            timelogic();
                        };
                        break;
                    case 1:
                        modelthing.Click += (s, e) =>
                        {
                                model = '2';
                                pd.Text = "请通过下方控件录入倒计时时长";
                            retimeif();
                            timelogic();
                        };
                        break;
                    case 2:
                        modelthing.Click += (s, e) =>
                        {
                                model = '3';
                                pd.Text = "番茄钟模式";
                                timeshowthing.Text = "0：25：00";
                            timelogic();
                        };
                        break;
                }
            }
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////以下是显示器创建方法
        /// </summary>
         Label conmake(int a, int b, int c, int d, string e, string f,int g)//显示器创建模板
        {
            Label lbl = new Label();
            lbl.Size = new Size(a, b);
            lbl.Location = new Point(c, d);
            lbl.Text = e;
            lbl.Font=new Font(f, g, FontStyle.Bold);
            this.Controls.Add(lbl);
            return lbl;
        }
        public void timelabel ()//时间显示创建
        {
            timeshowthing = conmake(350, 50, 220, 96, "0：00：00", "微软雅黑light", 30);
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////特殊界面
        /// </summary>
        public void retimeif()//独属于倒计时的特殊界面
        {

            for (int i = 0; i <3; i++)
            {
                ctn[i]=new NumericUpDown();
                ctn[i].Size=new Size(75,100);
                ctn[i].Location = new Point(180+125*i, 250);
                ctn[i].Font=new Font("微软雅黑light",30, FontStyle.Bold);
                ctn[i].Tag=i.ToString();
                Controls.Add(ctn[i]);
                switch(i)
                {
                    case 0: ctn[i].Maximum = 9; ctn[i].Minimum = 0; ctn[i].DecimalPlaces = 0; break;
                    case 1: ctn[i].Maximum = 59;ctn[i].Minimum = 0; ctn[i].DecimalPlaces = 0; break;
                    case 2: ctn[i].Maximum = 59; ctn[i].Minimum = 0; ctn[i].DecimalPlaces = 0; break;
                }
                ctn__[i] = conmake(40, 40, 260+i*125, 260, "：", "微软雅黑light", 20);
            }
            ctn_=conmake(40, 40, 305, 300, "录入", null);
            ctn_.Click += (s, e) =>
            {
                second = (int)ctn[2].Value;
                minute = (int)ctn[1].Value;
                hour = (int)ctn[0].Value;
                timeshowlogic(second, minute);
            };
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////以下是逻辑方法
        /// </summary>
        public void timelogic()//时间显示逻辑，通过model区分
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            switch (model)
            {
                case '1':timer.Tick+=notimelogic; break;
                case '2':timer.Tick+=retimelogic; break;
                case '3':timer.Tick+=potimelogic; break;
            }
        }

        void notimelogic(object sender, EventArgs e)//计时器模式
        {
                second++;
                if (second == 60)
                {
                    second = 0;
                    minute++;
                    if (minute == 60)
                    {
                        minute = 0;
                        hour++;
                        if (hour == 10)
                        {
                            hour = 0;
                            timer.Stop();
                            MessageBox.Show("计时器已达到显示上限，请重新设定并使用");
                        }
                    }
                }
                timeshowlogic(second, minute);
        }
        void retimelogic(object sender, EventArgs e)//倒计时模式
        {
            second--;
            if (second == -1)
            {
                minute--;
                second = 59;
                if(minute ==-1)
                {
                    hour--;
                    minute = 59;
                    if(hour==-1)
                    {
                        second = 0;
                        minute = 0;
                        hour = 0;
                        timer.Stop();
                        MessageBox.Show("倒计时结束");
                        btn.Text = "启动";
                        stsp = true;
                        timeshowlogic(0,0);
                    }
                }
            }
            timeshowlogic(second, minute);
        }
        void potimelogic(object sender, EventArgs e)//暂定为番茄钟模式
        {
            second++;
            int c = 1500 - second;
            int a = c / 60;//分钟
            int b = c%60;//秒
            if(c==0)
            {
                timer.Stop();
                MessageBox.Show("时间到，该休息五分钟了");
            }
            timeshowlogic(b, a);
        }
        void relogic(object sender, EventArgs e)//重置逻辑
        {
                model = '0';//让模型判断为零，避免启动带来的问题
                timer.Stop();//暂停tick
                timer.Dispose();//删除
                hour = 0;
                minute = 0;
                second = 0;//这三个与显示逻辑与计算逻辑强相关，归零之后逻辑即可代表完全重置
                timeshowthing.Text = "0：00：00";
                pd.Text = "当前并未选择启用的模式";//恢复表面显示
            stsp = true;
            btn.Text = "启动";
            if (ctn_ != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    Controls.Remove(ctn[i]);
                    ctn[i].Dispose();
                    ctn[i] = null;
                    Controls.Remove(ctn__[i]);
                    ctn__[i].Dispose();
                    ctn__[i] = null;
                }
                Controls.Remove(ctn_);
                ctn_.Dispose();
                ctn_ = null;

            }
        }
        public void timeshowlogic(int a,int b)//时间展示逻辑，a为秒，b为分
        {
            if (a < 10)
            {
                timec[0] = "0" + a.ToString();
            }
            else
            {
                timec[0] = a.ToString();
            }
            if (b < 10)
            {
                timec[1] = "0" +b.ToString();
            }
            else
            {
                timec[1] = b.ToString();
            }
            timeshowthing.Text = hour.ToString() + "：" + timec[1] + "：" + timec[0];
        }
    }
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Myform());
        }
    }
}
