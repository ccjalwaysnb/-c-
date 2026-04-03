using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace 逻辑训练3_记账本
{
    class Function
    {
        public static void record(List<decimal> money, List<string> reason)
        {
            Console.WriteLine("请输入金额，入账为正数，花销为复数");
            decimal a;
            while (!decimal.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("请重新输入正常的数字，无论如何不要带除了负号以外的非数字字符");
            }
            money.Add(a);
            Console.WriteLine("请输入此次花出或入账的原因");
            reason.Add(Console.ReadLine());
        }
        public static void summarize(List<decimal> money, List<string> reason)
        {
            int i = 0;
            decimal m = 0;
            while (i <= money.Count)
            {
                m += money[i];
                i++;
            }
            Console.WriteLine("当前总收支为" + m + "元");
            Console.WriteLine("输入数字1并回车即可查看详细账单，或者直接回车跳过当前总结");
            if (Console.ReadLine() == "1")
            {
                Console.WriteLine("当前已产生" + money.Count + "笔收支项目，内容如下");
                for (int e = 0; e < money.Count; e++)
                {
                    int a = e + 1;
                    Console.WriteLine("第" + a + "笔；" + money[e]);
                    Console.WriteLine("原因：" + reason[e]);
                }
                Console.ReadKey();
                Console.WriteLine("按下回车结束本次总结");
            }
            else;
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                List<decimal> money = new List<decimal>();
                List<string> reason = new List<string>();
                while (true)
                {
                    Console.WriteLine("您是想记录账单，还是立刻总结当前的花销，又或是直接开始总结今日的收支？");
                    Console.WriteLine("输入数字1并按下回车键，即可记录当前账单");
                    Console.WriteLine("输入数字2并按下回车键，即可立刻总结当前花销");
                    Console.WriteLine("直接按下回车键，即可直接开始总结今日的收支");
                    int chooses;
                    while (!int.TryParse(Console.ReadLine(), out chooses))
                    {
                        Console.WriteLine("请输入正确的数字选项");
                    }
                    if (chooses == 1)
                    {
                        Function.record(money, reason);
                    }
                    else if (chooses == 2)
                    {
                        Function.summarize(money, reason);
                    }
                    else
                    {

                        break;
                    }
                }
            }
        }
    }
}
