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
        string[] fuhao = {"+", "-", "×", "÷"};
        public void buttoncre(string[] d)
        {
            for (int i = 0;i<9; i++)
            {
                int a = i % 3;
                int b = i / 3;
                Button c= new Button();
                c.Size = new Size(50, 50);
                c.Location = new Point(100+50*a, 50*b+100);
                c.Text=(i+1).ToString();
                c.Tag=(i+1).ToString();
                c.Click += showing;
                this.Controls.Add(c);
            }
            for (int i = 0; i<fuhao.Length; i++)
            {
                Button c= new Button();
                c.Size=new Size(50, 50);
                c.Location = new Point(100 + 50 * i,50);
                c.Text = fuhao[i];
                c.Tag=fuhao[i];
                c.Click += showing;
                this.Controls.Add(c);
            }
        }
        void showing(object sender, EventArgs e)
        {

        }
        public Myform()
        {
            Size = new Size(600,400);
            buttoncre(fuhao);
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
