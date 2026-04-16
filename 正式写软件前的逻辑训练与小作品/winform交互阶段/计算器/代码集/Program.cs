using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace 计算器
{
    public class Myform : Form
    {
        Label caption1;//定义字幕
        string showthing="";//定义运行后应该显示的东西
        int logic2=0;//第二个数字，初始化为零
        int logic1=0;//第一个数字，初始化为零
        int logic3 = 0;//第三个数字初始化建立用于简便连续运算
        string Fuhao=null;//符号，初始化为null
        string fh=null;//用于简便连续运算
        char[] fuhao = { '+', '-', '×', '÷' };//数组，用来建立符号按键，这是直接显示text的
        bool judge1 = false;//！judge1代表第二个数为空
        bool judge2 = false;//!judge2代表符号为空
        bool judge3=false;//用于简便连续运算的后面几次正常运行
        void huifu(object sender, EventArgs e)//ac键
        {
            judge1 = false;//！judge1代表第二个数为空
            judge2 = false;//!judge2代表符号为空
            judge3 = false;//用于简便连续运算的后面几次正常运行
            logic2 = 0;//第二个数字，初始化为零
            logic1 = 0;//第一个数字，初始化为零
            logic3 = 0;//第三个数字初始化建立用于简便连续运算
            Fuhao = null;//符号，初始化为null
            fh = null;//用于简便连续运算
            showthing = "";//定义运行后应该显示的东西
            caption1.Text = "请通过按钮输入内容";
        }
        public void buttoncre(char[] d)
        {
            for (int i = 0;i<9; i++)//九宫格数字按键
            {
                int a = i % 3;
                int b = i / 3;
                Button c= new Button();
                c.Size = new Size(50, 50);
                c.Location = new Point(100+50*a, 50*b+100);
                c.Text=(i+1).ToString();
                c.Tag=i+1;
                c.Click += showingg;
                this.Controls.Add(c);
            }
            for (int i = 0; i<fuhao.Length; i++)//符号按键
            {
                Button c= new Button();
                c.Size=new Size(50, 50);
                c.Location = new Point(100 + 50 * i,50);
                c.Text = fuhao[i].ToString();
                switch (c.Text)                 //符号的tag是char形式
                {
                    case "+": c.Tag = '+'; break;
                    case "-": c.Tag="-";break;
                    case "×":c.Tag = "*";break;
                    case "÷":c.Tag= "/";break;
                }
                c.Click += showingg;
                this.Controls.Add(c);
            }
            Button hufu = new Button();
            hufu.Size = new Size(50, 50);
            hufu.Location = new Point(300, 50);
            hufu.Text = "AC";
            hufu.Click += huifu;
            this.Controls.Add(hufu);
            Button ling = new Button();
            ling.Size = new Size(50, 50);
            ling.Location = new Point(150, 250);
            ling.Text = "0";
            ling.Tag = "0";
            ling.Click += showingg;
            this.Controls.Add(ling);//加入0按键
        }
        void showingg(object sender, EventArgs e)
        {
            Button x = (Button)sender;
            int num = 0;
            if (!judge1)//第二个数还没变过
            {
                if (!judge2)//并且符号也没有输入过
                {
                    if (int.TryParse(x.Text, out num))//假如用户输入的按键为数字
                    {
                        logic1 = logic1 * 10 + num;//那么算作对第一个数的修改,这是在计算逻辑里面的修改
                        showthing = logic1.ToString();//显示栏直接显示第一个数字
                    }
                    else//假如用户输入的是符号
                    {
                        if (showthing == "")//判断第一个数是否存在
                        {
                            MessageBox.Show("违法输入");//如果不存在直接禁止输入
                        }
                        else                         //如果存在
                        {
                            Fuhao = x.Tag.ToString();//在计算逻辑里面添入符号
                            showthing += x.Text;//显示器增添符号
                            judge2 = true;//现在符号存在了
                        }
                    }

                }
                else//但是符号存在
                {
                    if (int.TryParse(x.Text, out num))//如果输入的是数字
                    {
                        logic2 = logic2 * 10 + num;//逻辑里面开始增添第二个数字的改动
                        showthing += logic2.ToString();//直接吧改动后的第二个数字增添进显示器
                        judge1 = true;//现在数字二改过了
                    }
                    else                              //如果输入的是符号
                    {
                        Fuhao = x.Tag.ToString();//直接覆盖，更新为最新输入的符号
                        showthing = logic1 + x.Text;//麻痹的可算是找到bug了，直接把第二个数字全去掉只显示原本的数字一和修改后的符号
                    }
                }
            }
            else//假如第二个数字修改过了
            {
                if (int.TryParse(x.Text, out num))//如果输入的是数字
                {
                    if (logic2 == 0)
                    {
                        logic2 = logic2 * 10 + num;//直接修改数字二，在逻辑里面
                        showthing =logic1.ToString()+Fuhao+ x.Text;//显示器修改环节
                    }
                    else
                    {
                        logic2 = logic2 * 10 + num;//直接修改数字二，在逻辑里面
                        showthing += x.Text;//显示器修改环节
                    }
                }
                else//如果输入的是符号
                {
                    jisuan();//直接当作连续运算，直接把一开始的算式解出答案，并且让数字一为结果
                    logic2 = 0;//把逻辑里面的数字二重新初始化
                    showthing = logic1.ToString() + x.Text;//运算之后的结果自动设定为数字一，所以直接显示数字一和加上去的符号
                    Fuhao = x.Tag.ToString();//逻辑里面的符号更新为新输入的
                    judge1 = false;//现在数字二没改过了
                    judge2 = true;
                }
            }
            judge3 = false;//中断连续计算进程
            caption1.Text = showthing;//让显示起作用
        }
        void conting(object sender, KeyEventArgs e)//点击键盘按键之后的逻辑
        {
            if (e.KeyCode == Keys.O)//如果是o键
            {
                if (!judge2||!judge1)//假如符号不存在或者数字二不存在
                {
                    showthing = logic1.ToString();//直接展示数字一就行了，不进行运算
                    Fuhao = null;
                    judge2 = false;//直接消除符号
                }
                else//假如符号存在
                {
                    if (Fuhao == "/" && logic2 == 0)//假如符号为除号，并且数字二为零
                    {
                        MessageBox.Show("禁止除0");
                    }
                    else //符号不为除号的话直接当作合法算式计算就行了
                    {
                        jisuan();//进行计算
                        showthing = logic1.ToString();//显示运算结果
                    }
                }
                judge3 = false;//中断连续计算进程
            }
            else if (e.KeyCode == Keys.Back)//如果是回退键
            {
                if (!judge1)//数字二为空时
                {
                    if (!judge2)//并且符号也没有
                    {
                            logic1 = logic1 / 10;//通过int的特性，逻辑里面也实现回退一位
                            if (logic1 == 0)//假如数字一为零
                            {
                                showthing = logic1.ToString();
                            }
                            else
                            {
                                showthing = showthing.Substring(0, showthing.Length - 1);//展示回退一位
                            }
                    }
                    else//如果有符号
                    {
                        Fuhao = null;//逻辑里面直接消除符号
                        showthing = logic1.ToString();//为了避免数字二真的为零而不是初始化，直接强制只显示数字一
                        judge2 = false;//初始化符号判断
                    }
                }
                else//如果数字二存在
                {
                    logic2 = logic2 / 10;//走特性，逻辑
                    showthing = showthing.Substring(0, showthing.Length - 1);//展示一下回退
                    if (logic2 == 0)//假如数字二扣完之后为零了
                    {
                        judge1 = false;//直接判断数字二不存在
                    }
                }
                judge3= false;//中断连续计算进程
            }
            else if (e.KeyCode == Keys.L)//设L键为简便连续计算键
            {
                if (judge3)//如果进行过一次连续运算了
                {
                    continuous();
                    showthing = logic1.ToString();//直接展示数字一就行了，不进行运算
                }
                else//假如没进行过，则开始正常运算一次
                {
                    if (!judge2||!judge1)//假如符号不存在或者数字二不存在
                    {
                        showthing = logic1.ToString();//直接展示数字一就行了，不进行运算
                        Fuhao=null;
                        judge2=false;//直接消除符号
                    }
                    else//假如符号存在
                    {
                        if (Fuhao == "/" && logic2 == 0)//假如符号为除号，并且数字二为零
                        {
                            MessageBox.Show("禁止除0");
                        }
                        else //符号不为除号的话直接当作合法算式计算就行了
                        {
                            continuous();//进行计算
                            showthing = logic1.ToString();//显示运算结果
                        }
                    }
                }
            }
            caption1.Text = showthing;//让显示起作用
        }
        void jisuan()//计算逻辑
        {
            switch (Fuhao)//通过符号来判断运算种类
            {
            case "+": logic1 = logic1 + logic2; break;
            case "-": logic1 = logic1 - logic2; break;
            case "*": logic1 = logic1 * logic2; break;
            case "/":logic1 = logic1 / logic2; break;
            }
        logic2 = 0;//因为运算答案为数字一以方便继续使用，所以初始化数字二
        Fuhao = null;//初始化符号
        judge1=false;
        judge2 =false;//两个判断恢复
        }
        void continuous()//连续计算逻辑
        {
            if (!judge3)//假如是第一次进行连续运算，则初始化所有用于正常计算的东西，随后记录为已经连续运算过一次了
            {
                fh = Fuhao;
                logic3 = logic2;
                judge3 = true;
                logic2 = 0;//因为运算答案为数字一以方便继续使用，所以初始化数字二
                Fuhao = null;//初始化符号
                judge1 = false;
                judge2 = false;//两个判断恢复
            }
            switch (fh)//通过符号来判断运算种类
            {
                case "+": logic1 = logic1 + logic3; break;
                case "-": logic1 = logic1 - logic3; break;
                case "*": logic1 = logic1 * logic3; break;
                case "/": logic1 = logic1 / logic3; break;
            }

        }
        public Myform()
        {
            Size = new Size(600,400);
            buttoncre(fuhao);
            caption1 = new Label();
            caption1.Size = new Size(400,50);
            caption1.Location = new Point(100, 0);
            caption1.Text = "请通过按钮输入内容";
            this.Controls.Add(caption1);
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(conting);
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
