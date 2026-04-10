using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace 记账本
{
    public class Myform : Form 
    {
        Label subtitle1;
        Label subtitle2;
        string cont;
        public string show;
        string[] an = {"+", "-", "×", "÷"};
        public void buttoncreat(string[] a)
        {
            for(int i=0; i<a.Length; i++)
            {
                Button b=new Button();
                b.Size = new Size(60,60);
                b.Location = new Point(100 + i * 60, 100);
                b.Text = a[i];
                b.Click += xianshi;
                switch(i)
                {
                    case 0:
                        b.Tag = "+";
                        break;
                        case 1: b.Tag = "-";
                        break;
                        case 2: b.Tag = "*";
                        break;
                        case 3: b.Tag = "/";
                        break;
                }
                this.Controls.Add(b);
            }
            for(int i=1;i<10;i++)
            {
                Button b= new Button();
                b.Size=new Size(60,60);
                int heng = (i-1) % 3;
                int shu=(i-1)/ 3;
                b.Location = new Point(100+heng*60,170+shu*60);
                b.Text=i.ToString();
                b.Tag = i;
                b.Click +=xianshi;
                this.Controls.Add(b);
            }//建立九宫格数字按钮
            Button d = new Button();
            d.Size = new Size(60, 60);
            d.Location = new Point(160, 350);
            d.Text = "0";
            d.Tag = "0";
            d.Click += xianshi;
            this.Controls.Add(d);//建立0按钮
        }
        void xianshi(object sender, EventArgs e)//控制显示和内部逻辑的方法
        {
            Button b = (Button)sender;
            show += b.Text;
            cont += b.Tag;
            subtitle2.Text = show;
            this.ActiveControl = null;
        }
        void calculate(object sender,KeyEventArgs e)//回车和回退的应用
        {
            if (e.KeyCode == Keys.Enter)
            {
                char op = '+';
                if (cont.Contains("+")) op = '+';
                else if (cont.Contains("-")) op = '-';
                else if (cont.Contains("*")) op = '*';
                else if (cont.Contains("/")) op = '/';
                string[] parts = cont.Split(op);
                int i=0;
                switch (op)
                {
                    case '+': i = int.Parse(parts[0]) + int.Parse(parts[1]); break;
                    case '-': i = int.Parse(parts[0]) - int.Parse(parts[1]); break;
                    case '*': i = int.Parse(parts[0]) * int.Parse(parts[1]); break;
                    case '/':
                        {
                            if (int.Parse(parts[1]) == 0)
                            {
                                MessageBox.Show("找茬是吧，不能除零");
                                this.Close();
                                break;
                            }
                            else
                            {
                                i = int.Parse(parts[0]) / int.Parse(parts[1]); break;
                            }
                        }
                }
                show=i.ToString();
                cont=i.ToString();
                subtitle2.Text = show;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (show.Length == 0)
                {
                    MessageBox.Show("找茬是吧，上哪给你删字符？");
                    this.Close();
                }
                else
                {
                    show=show.Substring(0, show.Length - 1);
                    cont=cont.Substring(0, cont.Length - 1);
                    subtitle2.Text = show;
                }
            }
        }



        public Myform()
        {
            this.Size = new Size(800, 600);
            this.KeyPreview = true;
            KeyDown += calculate;//键盘的使用
            buttoncreat(an);
            subtitle1 = new Label();//字幕
            subtitle1.Text = "超级计算器,先输入两边的数字再进行运算";
            subtitle1.Size = new Size(700, 50);
            subtitle1.Location = new Point(50,10);
            subtitle1.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(subtitle1);
            subtitle2 = new Label();//显示
            subtitle2.Text ="";
            subtitle2.Size = new Size(700, 50);
            subtitle2.Location = new Point(50, 410);
            subtitle2.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(subtitle2);
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        class program
        {
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Myform());
            }
        }
    }
}
