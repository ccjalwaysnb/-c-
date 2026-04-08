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
        Button anniu1;
        Button anniu2;
        Button anniu3;
        Button anniu4;
        TextBox shuru1;
        TextBox shuru2;
        int a;
        public Myform()
        {
            this.Size = new Size(800, 600);
            zimu1 = new Label();//字幕
            zimu1.Text = "超级计算器,先输入两边的数字再进行运算";
            zimu1.Size = new Size(700, 50);
            zimu1.Location = new Point(50,10);
            zimu1.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(zimu1);
            anniu1 = new Button();//加
            anniu1.Text = "加";
            anniu1.Size = new Size(50, 50);
            anniu1.Location = new Point(375, 60);
            anniu1.Click += dianji1;
            this.Controls.Add(anniu1);
            anniu2 = new Button();//减
            anniu2.Text = "减";
            anniu2.Size = new Size(50, 50);
            anniu2.Location = new Point(375, 120);
            anniu2.Click += dianji2;
            this.Controls.Add(anniu2);
            anniu3 = new Button();//乘
            anniu3.Text = "乘";
            anniu3.Size = new Size(50, 50);
            anniu3.Location = new Point(375, 180);
            anniu3.Click += dianji3;
            this.Controls.Add(anniu3);
            anniu4 = new Button();//除
            anniu4.Text = "除";
            anniu4.Size = new Size(50, 50);
            anniu4.Location = new Point(375, 240);
            anniu4.Click += dianji4;
            this.Controls.Add(anniu4);
            shuru1 = new TextBox();//输入框
            shuru1.Size = new Size(50, 50);
            shuru1.Location = new Point(200, 235);
            this.Controls.Add(shuru1);
            shuru2 = new TextBox();//输入框
            shuru2.Size = new Size(50, 50);
            shuru2.Location = new Point(600, 235);
            this.Controls.Add(shuru2);
        }
        void dianji1(object sender, EventArgs e)
        {
            a = int.Parse(shuru1.Text) + int.Parse(shuru2.Text); 
            MessageBox.Show(a.ToString());
        }
        void dianji2(object sender, EventArgs e)
        {
            a = int.Parse(shuru1.Text) - int.Parse(shuru2.Text);
            MessageBox.Show(a.ToString());
        }
        void dianji3(object sender, EventArgs e)
        {
            a = int.Parse(shuru1.Text) * int.Parse(shuru2.Text);
            MessageBox.Show(a.ToString());
        }
        void dianji4(object sender, EventArgs e)
        {
            a = int.Parse(shuru1.Text) / int.Parse(shuru2.Text);
            MessageBox.Show(a.ToString());
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
