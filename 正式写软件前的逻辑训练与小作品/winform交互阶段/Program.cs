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
        Label caption1;
        string showthing;
        int logic1=0;
        int logic2=0;
        string Fuhao;
        char[] fuhao = { '+', '-', '×', '÷' };
        public void buttoncre(char[] d)
        {
            for (int i = 0;i<9; i++)
            {
                int a = i % 3;
                int b = i / 3;
                Button c= new Button();
                c.Size = new Size(50, 50);
                c.Location = new Point(100+50*a, 50*b+100);
                c.Text=(i+1).ToString();
                c.Tag=i+1;
                c.Click += showing;
                this.Controls.Add(c);
            }
            for (int i = 0; i<fuhao.Length; i++)
            {
                Button c= new Button();
                c.Size=new Size(50, 50);
                c.Location = new Point(100 + 50 * i,50);
                c.Text = fuhao[i].ToString();
                switch (c.Text)
                {
                    case "+": c.Tag = '+'; break;
                    case "-": c.Tag="-";break;
                    case "×":c.Tag = "*";break;
                    case "÷":c.Tag= "/";break;
                }
                c.Click += showing;
                this.Controls.Add(c);
            }
        }
        void showing(object sender, EventArgs e)
        {
            Button x=(Button)sender;
            showthing += x.Text;
            int num = 0;
            if (Fuhao == null)
            {
                if (int.TryParse(x.Text, out num))
                {
                    logic1 = logic1 * 10 + num;
                }
                else
                { 
                    Fuhao = x.Tag.ToString();
                }
            }
            else
            {
                if (int.TryParse(x.Text, out num))
                {
                    logic2 = logic2 * 10 + num;
                }
                else
                {
                    Fuhao= x.Tag.ToString();
                }
            }
            caption1.Text = showthing;
            this.ActiveControl = null;

        }
        void conting(object sender,KeyEventArgs e)
        {

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
            KeyDown += conting;
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
