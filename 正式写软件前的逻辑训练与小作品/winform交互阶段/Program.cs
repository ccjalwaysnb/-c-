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
        Label zimu1;
        Label zimu2;
        string op;
        string[] an = {"+", "-", "x", "/","="};
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
            }
            Button d = new Button();
            d.Size = new Size(60, 60);
            d.Location = new Point(160, 350);
            d.Text = "0";
            d.Tag = "0";
            d.Click += xianshi;
            this.Controls.Add(d);
            Button c = new Button();
            c.Size = new Size(60, 60);
            c.Location = new Point(340, 100);
            c.Text = "等于";
            c.Tag = "=";
            c.Click += xianshi;
            this.Controls.Add(c);
        }
        void xianshi(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string show=b.Text.ToString();
            op= b.Tag.ToString();
            zimu2.Text += show;
        }
        

        public Myform()
        {
            this.Size = new Size(800, 600);
            buttoncreat(an);
            zimu1 = new Label();//字幕
            zimu1.Text = "超级计算器,先输入两边的数字再进行运算";
            zimu1.Size = new Size(700, 50);
            zimu1.Location = new Point(50,10);
            zimu1.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(zimu1);
            zimu2 = new Label();//显示
            zimu2.Text = "";
            zimu2.Size = new Size(700, 50);
            zimu2.Location = new Point(50, 410);
            zimu2.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(zimu2);
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
