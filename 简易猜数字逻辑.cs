using System;
namespace RectangleApplication
{
    class program
    {
        static void Main()
        {
            while (true)
            {
                Random a = new Random();
                int b = a.Next(1, 3);
                while (true)
                {
                    Console.WriteLine("请输入你猜测的数字");
                    int c;
                    while (!int.TryParse(Console.ReadLine(), out c))
                    {
                        Console.WriteLine("请重新输入一个自然数");
                    }
                    if(c==b)
                    {
                        Console.WriteLine("真棒，你猜对了");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("真可惜，你猜错了，重新再来吧");
                    }
                }
                Console.WriteLine("请问还想再来一次吗");
                Console.WriteLine("按下数字1并回车即可重新开始，或者随机键退出");
                string choose =Console.ReadLine();
                if (choose =="1")
                {
                    Console.WriteLine("正在重新生成数字，按下回车键继续");
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
