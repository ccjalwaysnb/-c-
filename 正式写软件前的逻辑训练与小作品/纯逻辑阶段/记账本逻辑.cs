using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 逻辑训练3_记账本
{
    class Function
    {
        public static int choose(string a,int b,int c)
        {
            int d;
            if (int.TryParse(a, out d))
            {
                if (d <= c && d >= b)
                    return d;
                else
                {
                    Console.WriteLine("请输入正确的数字");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("请输入正确的数字");
                return 0;
            }

        }
        public static void inrecord(List<decimal> money, List<string> reason)
        {
            Console.WriteLine("请输入本次入账金额");
            decimal a;
            while (!decimal.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("请重新输入正常数字");
            }
            money.Add(a);
            Console.WriteLine("请输入此次入账的原因");
            reason.Add(Console.ReadLine());
        }
        public static void outrecord(List<decimal> money, List<string> reason)
        {
            Console.WriteLine("请输入本次消费金额");
            decimal a;
            while (!decimal.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("请重新输入正常数字");
            }
            money.Add(a);
            Console.WriteLine("请输入此次消费的原因");
            reason.Add(Console.ReadLine());
        }
        public static void summarize(List<decimal> inmoney, List<decimal> outmoney, List<string> inreason, List<string> outreason)
        {
            decimal x = 0;
            decimal y = 0;
            int i = inmoney.Count + outmoney.Count;
            foreach (decimal a in inmoney)
            {
                x += a;
            }
            foreach (decimal b in outmoney)
            {
                y += b;
            }
            decimal m = x - y;
            Console.WriteLine("当前总收入为" + x + "元");
            Console.WriteLine("当前总指出为" + y + "元");
            Console.WriteLine("当前总收支为" + m + "元");
            Console.ReadKey();
            Console.WriteLine("请问是否要查询已产生的每笔收支明细");
            Console.WriteLine("按下数字1并回车即可查询，或者按下数字2并回车直接结束本次小结");
            int pd = Function.choose(Console.ReadLine(), 1, 2);
            switch (pd)
            {
                case 0:
                    pd = Function.choose(Console.ReadLine(), 1, 2);
                    break;
                case 1:
                    {
                        Console.WriteLine("当前总计产生" + i + "笔收支项目，内容如下(先列收入再列支出)");
                        for (int e = 0; e < inmoney.Count; e++)
                        {
                            int a = e + 1;
                            Console.WriteLine("第" + a + "笔；" + inmoney[e]);
                            Console.WriteLine("原因：" + inreason[e]);
                        }
                        for (int e = 0; e < outmoney.Count; e++)
                        {
                            int a = e + 1;
                            Console.WriteLine("第" + a + "笔；" + outmoney[e]);
                            Console.WriteLine("原因：" + outreason[e]);
                        }
                        Console.WriteLine("按下回车结束本次小结");
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    break;
            }
        }
        public static void day(List<decimal> inmoney, List<decimal> outmoney, List<string> inreason, List<string> outreason)
        {
            decimal x = 0;
            decimal y = 0;
            int i=inmoney.Count+outmoney.Count;
            foreach(decimal a in inmoney)
            {
                x += a;
            }
            foreach(decimal b in outmoney)
            {
                y += b;
            }
            decimal m = x - y;
            Console.WriteLine("今日总收入为" + x + "元");
            Console.WriteLine("今日总指出为" + y + "元");
            Console.WriteLine("今日总收支为" + m + "元");
            Console.ReadKey();
            Console.WriteLine("今日总计产生" + i + "笔收支项目，内容如下(先列收入再列支出)");
            for (int e = 0; e < inmoney.Count; e++)
            {
                int a = e + 1;
                Console.WriteLine("第" + a + "笔；" + inmoney[e]);
                Console.WriteLine("原因：" + inreason[e]);
            }
            for (int e = 0; e < outmoney.Count; e++)
            {
                int a = e + 1;
                Console.WriteLine("第" + a + "笔；" + outmoney[e]);
                Console.WriteLine("原因：" + outreason[e]);
            }
            Console.ReadKey();
            Console.WriteLine("按下回车结束今日总结并关闭程序");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<decimal> inmoney = new List<decimal>();
            List<decimal> outmoney = new List<decimal>();
            List<string> inreason = new List<string>();
            List<string> outreason = new List<string>();
            while (true)
            {
                Console.WriteLine("您是想记录账单，还是立刻总结当前的花销，又或是直接开始总结今日的收支？");
                Console.WriteLine("输入数字1并按下回车键，即可记录当前账单");
                Console.WriteLine("输入数字2并按下回车键，即可立刻总结当前花销");
                Console.WriteLine("输入数字3并按下回车键，即可直接开始总结今日的收支");
                int pd = Function.choose(Console.ReadLine(), 1, 3);
                switch(pd)
                {
                    case 0:
                        {
                            pd = Function.choose(Console.ReadLine(), 1, 3);
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("请问你想记录入账还是花销");
                            Console.WriteLine("记录入账请输入数字1并回车，记录花销请输入数字2并回车");
                            int ch=Function.choose(Console.ReadLine(),1, 2);
                            switch(ch)
                            {
                                case 0:
                                    {
                                        ch = Function.choose(Console.ReadLine(), 1, 2);
                                        break;
                                    }
                                case 1:
                                    {
                                        Function.inrecord(inmoney, inreason);
                                        break;
                                    }
                                case 2:
                                    {
                                        Function.outrecord(outmoney, outreason);
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            Function.summarize(inmoney,outmoney,inreason,outreason);
                            break ;
                        }
                    case 3:
                        {
                            Console.WriteLine("正在总结中");
                            Thread.Sleep(3000);
                            break;
                        }

                }
                Function.day(inmoney, outmoney, inreason, outreason);
            }
        }
    }
}