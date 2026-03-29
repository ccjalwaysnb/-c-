using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace 再度复习
{
    class monster
    {
        public string name;
        public float hp;
        public float atk;
        public float df;
        public monster(string name, float hp, float atk, float df)
        {
            this.name = name;
            this.hp = hp;
            this.atk = atk;
            this.df = df;
        }
    }
    class hero
    {
        public string name;
        public float hp;
        public float atk;
        public float df;
        public int career;
        public hero(string a,float b,float c,float d,int e)
        {
            name = a;
            hp = b;
            atk = c;
            df = d;
            this.career= e;
        }
    }
    class Attackion
    {
        public void Madenew(hero a, monster b)
        {
            hero x = new hero("1", 1, 1, 1, 1);
            x = a;
            monster y = new monster("1", 1, 1, 1);
            y = b;
        }
        public float damage(hero x, monster y)
        {
            if (x.atk - y.df < 0)
            {
                return 0;
            }
            return x.atk - y.df;
        }
        public float redamage(hero x, monster y)
        {
            if (y.atk - x.df < 0)
            {
                return 0;
            }
            return y.atk - x.df;
        }
        public int nattacking(hero x, monster y)//普通攻击
        {
            y.hp -= damage(x, y);
            x.hp -= redamage(x, y);
            if (x.hp <= 0 && y.hp <= 0)
            {
                Console.WriteLine("you had killed him by" + damage(x, y) + ",but it killed you too by" + redamage(x, y));
                Console.ReadKey();
                Console.WriteLine("loser");
                Console.ReadKey();
                return 2;
            }
            else if (x.hp > 0 && y.hp <= 0)
            {
                Console.WriteLine("you had killed him by" + damage(x, y) + ",but it hurt you before it daed by" + redamage(x, y));
                Console.ReadKey();
                Console.WriteLine("winner");
                Console.ReadKey();
                return 1;
            }
            else if (x.hp <= 0 && y.hp > 0)
            {
                Console.WriteLine("you had done your best,hurt him by" + damage(x, y) + ",but that was useless,it killed you by" + redamage(x, y));
                Console.ReadKey();
                Console.WriteLine("loser");
                Console.ReadKey();
                return 2;
            }
            else
            {
                Console.WriteLine("you had hurted him by" + damage(x, y) + ",and it hurt you too by" + redamage(x, y));
                Console.ReadKey();
                Console.WriteLine("keep on");
                Console.ReadKey();
                return 3;
            }

        }
        public int wskill(hero x, monster y)
        {
            x.atk = x.atk / 2;
            x.df = x.df * 2;
            y.hp -= damage(x, y);
            x.hp -= redamage(x, y);
            Console.WriteLine("you decide keep your attanion on defense more than before.");
            Console.ReadKey();
            if (damage(x, y) == 0 && redamage(x, y) > 0)
            {
                Console.WriteLine("you may can not hurt him");
                Console.ReadKey();
                Console.WriteLine("总座高见--");
                Console.ReadKey();
            }
            else if (redamage(x, y) == 0 && damage(x, y) > 0)
            {
                Console.WriteLine("that is so good,it can not hurt you"); Console.ReadKey();
            }
            else
            {
                Console.WriteLine("You hunched up your shell."); Console.ReadKey();
            }
            if (x.hp <= 0 && y.hp <= 0)
            {
                Console.WriteLine("you had killed him by" + damage(x, y) + ",but it killed you too by" + redamage(x, y)); Console.ReadKey();
                Console.WriteLine("loser"); Console.ReadKey();
                x.atk = x.atk * 2;
                x.df = x.df / 2;
                return 2;
            }
            else if (x.hp > 0 && y.hp <= 0)
            {
                Console.WriteLine("you had killed him by" + damage(x, y) + ",but it hurt you before it daed by" + redamage(x, y)); Console.ReadKey();
                Console.WriteLine("winner"); Console.ReadKey();
                x.atk = x.atk * 2;
                x.df = x.df / 2;
                return 1;
            }
            else if (x.hp <= 0 && y.hp > 0)
            {
                Console.WriteLine("you had done your best,hurt him by" + damage(x, y) + ",but that was useless,it killed you by" + redamage(x, y));
                Console.ReadKey();
                Console.WriteLine("loser"); Console.ReadKey();
                x.atk = x.atk * 2;
                x.df = x.df / 2;
                return 2;
            }
            else
            {
                Console.WriteLine("you had hurted him by" + damage(x, y) + ",and it hurt you too by" + redamage(x, y));
                Console.ReadKey();
                Console.WriteLine("keep on"); Console.ReadKey();
                x.atk = x.atk * 2;
                x.df = x.df / 2;
                return 3;
            }
        }
        public int askill(hero x, monster y)
        {
            x.atk = 2 * x.atk;
            x.hp -= redamage(x, y);
            Console.WriteLine("you decide keep attanion on attact");
            Console.ReadKey();
            if (x.hp <= 0)
            {
                Console.WriteLine("you had made a worst decision,you are dead by your stupid.");
                Console.ReadKey();
                Console.WriteLine("catch the best time next time.");
                Console.ReadKey();
                Console.WriteLine("loser");
                Console.ReadKey();
                return 2;
            }
            else
            {
                Console.WriteLine("it had hurted you by" + redamage(x, y) + "and you beared");
                Console.ReadKey();
                if (x.hp > 0 && damage(x, y) > y.hp)
                {
                    Console.WriteLine("catch your time");
                    Console.ReadKey();
                    Console.WriteLine("keep on");
                    Console.ReadKey();
                }
                return 3;
            }
            //remenber when archer had chooses nattacking,you should reset archer's atk
        }

    }

    class program
    {
        static void Main()
        {
            monster GPT = new monster("GPT", 100, 10, 5);
            Console.WriteLine("测试");
            Console.WriteLine("名字输入");
            string name = Console.ReadLine();
            Console.WriteLine("选择职业，1：战士     2：射手");
            hero player = new hero(name,0,0,0,0);
            while (true)
            {
                string choses = Console.ReadLine();
                if (choses != "1" && choses != "2")
                {
                    Console.WriteLine("重新输入");
                }
                else if (choses == "1")
                {
                    player =new hero(name, 100, 15, 10,1);
                    break;
                }
                else if (choses == "2")
                {
                    player=new hero(name, 100, 10, 5,2);
                    break;
                }
            }


        }
    }
}

