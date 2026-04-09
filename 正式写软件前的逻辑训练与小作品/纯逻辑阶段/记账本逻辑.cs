using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 逻辑训练3_记账本
{
    class Date//数据录入
    {
        public decimal a;
        public string b;
        public Date(decimal a, string b)
        {
            this.a = a;
            this.b = b;
        }
    }
    class Function
    {
        public static void inrecord(List<Date> z)//记录账单
        {
            Console.WriteLine("本次是记录入账还是花销？");

            decimal a;
            string b;
            while (true)
            {
                Console.WriteLine("1：入账     2：花销");
                string choose = Console.ReadLine();
                if (choose == "1")
                {
                    Console.WriteLine("请输入具体金额");
                    while (!decimal.TryParse(Console.ReadLine(), out a))
                    {
                        Console.WriteLine("请重新输入正常数字");
                    }
                    Console.WriteLine("备注");
                    b = Console.ReadLine();
                    break;
                }
                else if (choose == "2")
                {
                    Console.WriteLine("请输入具体金额");
                    while (!decimal.TryParse(Console.ReadLine(), out a))
                    {
                        Console.WriteLine("请重新输入正常数字");
                    }
                    a = -a;
                    Console.WriteLine("备注");
                    b = Console.ReadLine();
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入正确的选择");
                }
            }
            z.Add(new Date(a, b));
        }
        public static void summarize(List<Date> money)//当前总结
        {
            decimal x = 0;
            decimal y = 0;
            foreach (var item in money)
            {
                if (item.a < 0)
                    y += item.a;
                else
                    x += item.a;
            }
            decimal m = x + y;
            Console.WriteLine("当前总收入为" + x + "元");
            Console.WriteLine("当前总支出为" + y + "元");
            Console.WriteLine("当前总收支为" + m + "元");
            Console.ReadKey();
            Console.WriteLine("请问是否要查询已产生的每笔收支明细");
            Console.WriteLine("按下数字1并回车即可查询，或者按下回车直接结束本次小结");
            string choose = Console.ReadLine();
            if (choose == "1")
            {
                Console.WriteLine("当前总计产生" + money.Count + "笔收支项目，内容如下");
                for (int e = 0; e < money.Count; e++)
                {
                    int a = e + 1;
                    Console.WriteLine("第" + a + "笔；" + money[e].a);
                    Console.WriteLine("备注：" + money[e].b);
                }
            }
            Console.WriteLine("按下回车结束本次小结");
            Console.ReadKey();
        }
        public static void day(List<Date> money)//每日总结
        {
            decimal x = 0;
            decimal y = 0;
            int i = money.Count;
            foreach (var item in money)
            {
                if (item.a < 0)
                    y += item.a;
                else
                    x += item.a;
            }
            decimal m = x + y;
            Console.WriteLine("今日总收入为" + x + "元");
            Console.WriteLine("今日总指出为" + y + "元");
            Console.WriteLine("今日总收支为" + m + "元");
            Console.ReadKey();
            Console.WriteLine("今日总计产生" + i + "笔收支项目，内容如下");
            for (int e = 0; e < money.Count; e++)
            {
                int a = e + 1;
                Console.WriteLine("第" + a + "笔；" + money[e].a);
                Console.WriteLine("备注：" + money[e].b);
            }
            Console.ReadKey();
            Console.WriteLine("按下回车结束今日总结并关闭程序");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Date> money = new List<Date>();
            while (true)
            {
                Console.WriteLine("您是想记录账单，还是立刻总结当前的花销，又或是直接开始总结今日的收支？");
                Console.WriteLine("输入数字1并按下回车键，即可记录当前账单");
                Console.WriteLine("输入数字2并按下回车键，即可立刻总结当前花销");
                Console.WriteLine("直接按下回车键，即可直接开始总结今日的收支");
                string choose = Console.ReadLine();
                if (choose == "1")
                {
                    Function.inrecord(money);
                }
                else if (choose == "2")
                {
                    Function.summarize(money);
                }
                else
                {
                    Console.WriteLine("正在总结中");
                    Thread.Sleep(3000);
                    break;
                }
            }
            Function.day(money);
        }
    }
}
