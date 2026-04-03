using System;
namespace RectangleApplication
{
    class Model
    {
        public static bool judgement(int x, int y)
        {
            return x == y;
        }
        public static int trans()
        {
            int c;
            while (!int.TryParse(Console.ReadLine(), out c))
            {
                Console.WriteLine("请重新输入一个自然数");
            }
            return c;
        }

    }
    class program
    {
        static void Main()
        {
            Random a = new Random();
            Console.WriteLine("欢迎来到逻辑测训练项目——猜数字");
            while(true)
            {
                int b=a.Next(1,3);
                Console.WriteLine("请输入你猜测的自然数");
                int c;
                while (true)
                {
                    c = Model.trans();
                    if (Model.judgement(b, c))
                    {
                        Console.WriteLine("真棒，你猜对了");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("猜错了，请重新再来");
                    }
                }
                Console.WriteLine("请问是否还想再来一遍？");
                Console.WriteLine("输入数字1并按下回车即可重置本程序运行，或直接按下回车键退出本程序");
                if(Console.ReadLine() == "1")
                {
                    Console.WriteLine("正在重置随机数，按下回车键即可重新开始");
                    Console.ReadKey();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
