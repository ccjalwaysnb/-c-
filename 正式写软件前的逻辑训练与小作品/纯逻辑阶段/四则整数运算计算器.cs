using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 超级计算器
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("超级计算机");
            Console.WriteLine("请先注册");
            Console.WriteLine("你的名字是");
            string name = Console.ReadLine();
            if (name == "陈凯")
            {
                name = "ccj的好大儿";
            }
            else
            {
                name = "尊敬的" + name;
            }
            Console.WriteLine("注册成功");
            Console.WriteLine("你的名字是" + name);
            for (int I = 0; I < 1; )
            {
                for (int i = 0; i < 1;)
                {
                    Console.WriteLine("请选择需要进行的四则运算");
                    Console.WriteLine("通过输入+ - * /来进行选择,记得按回车保存选择");
                    string fuhao = Console.ReadLine();
                    if (fuhao == "/")
                    {
                        chufa();
                        i = 1;
                    }
                    else if (fuhao == "*")
                    {
                        chengfa();
                        i = 1;
                    }
                    else if (fuhao == "+")
                    {
                        jiafa();
                        i = 1;
                    }
                    else if (fuhao == "-")
                    {
                        jianfa();
                        i = 1;
                    }
                    else
                    {
                        Console.WriteLine("请重新选择符号");
                        i = 0;
                    }
                }
                ganxie(name);
                string p=Console.ReadLine();
                if(p =="是")
                {
                    I = 0;
                }
                else
                {   
                    I = 1;
                }

                
            }
        }
        
        static void chufa()
        {
            Console.WriteLine("本除法计算机会自动四舍五入取整");
            Console.WriteLine("请输入除数然后按下回车保存");
            string n = Console.ReadLine();
            Console.WriteLine("请输入被除数然后按下回车键进行运算");
            string m = Console.ReadLine();
            int daan = 0;
            daan = int.Parse(n) / int.Parse(m);
            Console.WriteLine("答案是" + daan);
        }
        static void chengfa()
        {
            Console.WriteLine("请输入乘数然后按下回车保存");
            string n = Console.ReadLine();
            Console.WriteLine("请输入另一个乘数然后按下回车键进行运算");
            string m = Console.ReadLine();
            int daan = 0;
            daan = int.Parse(n) * int.Parse(m);
            Console.WriteLine("答案是" + daan);
        }
        static void jiafa()
        {
            Console.WriteLine("请输入加数然后按下回车保存");
            string n = Console.ReadLine();
            Console.WriteLine("请输入另一个加数然后按下回车键进行运算");
            string m = Console.ReadLine();
            int daan = 0;
            daan = int.Parse(n) + int.Parse(m);
            Console.WriteLine("答案是" + daan);
        }
        static void jianfa()
        {
            Console.WriteLine("请输入被减数然后按下回车保存");
            string n = Console.ReadLine();
            Console.WriteLine("请输入减数然后按下回车键进行运算");
            string m = Console.ReadLine();
            int daan = 0;
            daan = int.Parse(n) - int.Parse(m);
            Console.WriteLine("答案是" + daan);
        }
        static void ganxie(string name)
        {
            Console.WriteLine("感谢" + name + "使用本计算机");
            Console.WriteLine("是否还想进行一次计算机的使用？");
        }
    }
}
 