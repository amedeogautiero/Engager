using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestEngager
{
    class Program
    {
        static private void wordx2(string mask, string[] range, string tabella = "parole")
        {
            Func<string, int, string> repeat = delegate (string strBase, int volte)
            {
                string output = "";
                for (uint i = 0; i < volte; i++)
                {
                    output += strBase;
                }
                return output; 
            };

            string _range = "[" + string.Join("", range) + "]";
            Engager.Engager pre = new Engager.Engager();

            string[] _masks = mask.Split('_');

            Engager.Gear<string> g1 = null;
            
            

            int rangeToRepeat = 0;
            int slotEmpty = 0; //0 = init value, -1 = no empty 1 = empty
            string precMark = "."; // . = init value
            foreach (string _mask in _masks)
            {
                if (_mask != precMark)
                {
                    g1 = new Engager.Gear<string>();
                    pre.Add(g1);
                }

                if (string.IsNullOrEmpty(_mask))
                {
                    slotEmpty = 1;

                    if (g1.TotalCogs == 0) g1.AddCog("");
                    string rangeRepeated = repeat(_range, g1.TotalCogs);
                    g1.AddCog(rangeRepeated);
                }
                else
                {
                    slotEmpty = -1;
                    g1.AddCog(_mask);
                }


                precMark = _mask;

            }
            

            var rets = from a in pre
                       select a;


            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("select length(parola),  * from" + Environment.NewLine);
            sb.Append("(" + Environment.NewLine);

            foreach (var ret in rets)
            {
                string _pre = string.Join("", ret);
                Console.WriteLine("{0}", _pre);

                sb.Append(string.Format("select * from {2} where parola glob '{0}' {1}", _pre, Environment.NewLine, tabella));
                sb.Append("union" + Environment.NewLine);
                
            }
            sb.Append("select '' " + Environment.NewLine);
            sb.Append(")" + Environment.NewLine);
            sb.Append("order by 1 desc;" + Environment.NewLine);

            System.IO.File.WriteAllText(@"c:\temp\quesy.sql", sb.ToString());
        }

        static private void wordx()
        {
            string[] lettere = new string[] {"", "q", "f", "o", "a", "o", "s" };
            Engager.Engager pre = new Engager.Engager();
            Engager.Gear<string> g1 = new Engager.Gear<string>();
            g1.AddCogs(lettere);
            Engager.Gear<string> g2 = new Engager.Gear<string>();
            g2.AddCogs(lettere);
            Engager.Gear<string> g3 = new Engager.Gear<string>();
            g3.AddCogs(lettere);
            pre.Add(g1);
            pre.Add(g2);
            pre.Add(g3);

            Engager.Engager post = new Engager.Engager();
            Engager.Gear<string> g4 = new Engager.Gear<string>();
            g4.AddCogs(lettere);
            Engager.Gear<string> g5 = new Engager.Gear<string>();
            g5.AddCogs(lettere);
            Engager.Gear<string> g6 = new Engager.Gear<string>();
            g6.AddCogs(lettere);
            post.Add(g4);
            post.Add(g5);
            post.Add(g6);

            post.Completed += delegate(Engager.Engager engager)
            {
                //engager.Reset();
                int a = 0;
            };

            pre.Completed += delegate(Engager.Engager engager)
            {
                post.Reset();
            };

            var rets = from a in pre
                       select a;

            foreach (var ret in rets)
            {
                string _pre = string.Join("", ret);
                string _post = string.Join("", post.Current);
                Console.WriteLine("{0}bio{1}", _pre, _post);
            }
        }

        static private void sum() 
        { 
            Engager.Engager sum = new Engager.Engager();
            sum.Gear<int>(1, 2, 3);

            Engager.Gear<int> g1 = new Engager.Gear<int>();
            g1.AddCogs(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });

            Engager.Gear<int> g2 = new Engager.Gear<int>();
            g2.AddCogs(new int[] { 7, 2, 1, 3, 5, 6, 11, 8, 9, -1, -2, -3 });

            Engager.Gear<int> g3 = new Engager.Gear<int>();
            g3.AddCogs(new int[] { 7, 8, 0 });

            sum.Add(g1);
            sum.Add(g2);
            sum.Add(g3);


            var ret = from c in sum
                      where c.Cast<int>().Sum() == 11
                      select c;

            var def = Console.ForegroundColor;

            /*
            foreach (var c in ret)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}|{1}|{2}",c[0],c[1], c[2] );
            }
            */

            foreach (var c in sum)
            {
                if (c.Cast<int>().Sum() == 11)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = def;
                }
                Console.WriteLine("{0} + {1} + {2} = 11", c[0], c[1], c[2]);
            }

        }

        static void Main(string[] args)
        {
            sum();

            return;
            wordx2("_zio", new string[]{ "q","f","o","a","o","s" }, "italungo");

            //wordx2("____bio__", new string[] { "q", "f", "o", "a", "o", "s" });

            Engager.Engager e = new Engager.Engager();
            Engager.Gear<string> g1 = new Engager.Gear<string>();
            g1.AddCog("/Mainline/");
            g1.AddCog("/Test/");
            g1.AddCog("/Preproduzione/");
            Engager.Gear<string> g2 = new Engager.Gear<string>();
            g2.AddCog("/Mainline/");
            g2.AddCog("/Test/");
            g2.AddCog("/Preproduzione/");

            e.Add(g1);
            e.Add(g2);

            
            
            var rets = from a in e
            //           where a.Rank = a.Rank
            //           && a.All(aa2 => ((int)aa2) <= 3)
            //           && a.All(aa2 => ((int)aa2) > 0)
                        //where a != null
                        //&& (a is object[])
                        //&& (a as object[]).Length == 2
                        //&& (a as object[])[0] != null
                        //&& (a as object[])[1] != null
                        //&& (a as object[])[0] != (a as object[])[1]
                       select a;
            
            foreach (var ret in rets)
            {
            //    //Console.Write("--> ");
            //    //foreach (object retN in ret) Console.Write("-[{0},{1}]-", retN, valore((int)retN));
            //    //Console.Write(" costa {0}", ret.Sum(aa => valore((int)aa)));                
            //    //Console.WriteLine("");
            //    ret.Select(a => new {valore(0), valore(1)} );
            }

            //var comb = rets.Select(a => new {V1 = valore(0),V2 = valore(1)} );
        }

        static void Main2(string[] args)
        {
            string s = Engager.RegexHelper.ExpandCharClass(@"[\-a-zA-F1 5-9]");
            string s2 = Engager.RegexHelper.ExpandCharClass(@"^[\w\-\.]*[\w\.]\@[\w\.]*[\w\-\.]+[\w\-]+[\w]\.+[\w]+[\w $]");
            //s = Engager.RegexHelper.ExpandCharClass(@"[10-900]{3}");


            Engager.Engager e = new Engager.Engager();

            Engager.Gear<int> g1 = new Engager.Gear<int>();
            g1.AddCog(0);
            g1.AddCog(1);
            g1.AddCog(2);
            g1.AddCog(3);
            g1.AddCog(4);
            g1.AddCog(5);
            g1.AddCog(6);
            g1.AddCog(7);
            g1.AddCog(8);


            Engager.Gear<int> g2 = new Engager.Gear<int>();
            g2.AddCog(0);
            g2.AddCog(1);
            g2.AddCog(2);
            g2.AddCog(3);
            g2.AddCog(4);
            g2.AddCog(5);
            g2.AddCog(6);
            g2.AddCog(7);
            g2.AddCog(8);

            e.Add(g1);
            e.Add(g1.Clone());
            e.Add(g1.Clone());
            e.Add(g1.Clone());
            e.Completed += delegate(Engager.Engager engager)
            {
                //engager.Reset();
            };

            //while (e.Current != null)
            //{
            //    Console.WriteLine("--> {0}, {1}, {2}", e.Current[0], e.Current[1], (int)e.Current[1] + (int)e.Current[0]);
            //    e.Turn();
            //}

            //var rets = e.Where(a => (int)a[0] + (int)a[1] == 8);
            var rets = from a in e
                       //where (int)a[0] + (int)a[1] == 8
                       where a.Sum(aa => (int)aa) == 8
                       && a.All(aa2 => ((int)aa2) <=3)
                       && a.All(aa2 => ((int)aa2) > 0)
                       select a;

            rets = from a2 in rets
                   select a2.OrderByDescending(aa => (int)aa).ToArray();

            rets = from a3 in rets
                   orderby (int)a3[0] descending, (int)a3[1] descending, (int)a3[2], (int)a3[3] descending 
                   select a3;

            foreach (var ret in rets)
            {
                Console.Write("--> ");
                foreach (object retN in ret) Console.Write("-[{0},{1}]-", retN, valore((int)retN));
                Console.Write(" costa {0}", ret.Sum(aa => valore((int)aa)));                
                Console.WriteLine("");
            }

        }

        static int valore(int posti)
        {
            switch (posti)
            {
                case 3:
                    return 90;
                    break;
                case 2:
                    return 70;
                    break;
                case 1:
                    return 40;
                    break;
            }

            return 0;
        }
    }
}
