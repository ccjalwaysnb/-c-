using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 小测试
{
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
            Application.Run(new Form1());
        }
    }
    class Calculate
    {
        public static double calculate(double x, double y,int z)
        {
            switch (z)
            {
                case 1: return x + y;
                case 2: return x - y;
                case 3: return x * y;
                case 4: return x / y;
            }
        }
    }
    public class Myform:Form
    {
        public button()
    }
}
