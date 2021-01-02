using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InfiniteLoop
{
    class Program
    {

        static void Main(string[] args)
        {
            //vuoto();

            //vuoto2();

            //one();

            //two();

            //range();

            words();

            Console.ReadKey();
            return;

            Motore m = new Motore();
            Ingranaggio<int> ii = new Ingranaggio<int>();
            
            IngranaggioRange ir = new IngranaggioRange(5, 9);
            //m.Ingrana<int>(1, 2, 3, 4, 5,6,7);
            //m.Ingrana<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);

            string template = "01dqwertyuiopasdfghjklzxcvbnm";

            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());

            StringBuilder sb = new StringBuilder();
            Console.ReadLine();

            //string url = "http://localhost:57496/auth/login?ReturnUrl=%2fHome%2fIndex";
            //string postData = "This is a test that posts this string to a Web server.";
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //WebRequest request = WebRequest.Create(url);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = byteArray.Length;


            //Stream dataStream = request.GetRequestStream();
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //dataStream.Close();
            //WebResponse response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            //Stream data = response.GetResponseStream();

            //bool ret1 = post1("aeo");
            //bool ret2 = post1("deo");

            Console.WriteLine(DateTime.Now);
            long nLinee = 0;

            //foreach (var d in m)
            //{
            //    char[] chars = d.Select(dd => (char)dd.Valore).ToArray();
            //    string word = new string(chars);
            //    nLinee ++;
            //    //Console.WriteLine(word);
            //    sb.AppendLine(word);
            //}

            Parallel.ForEach<Dente[]>(m, (item, state) => 
            {
                char[] chars = item.Select(dd => (char)dd.Valore).ToArray();
                string word = new string(chars);

                //bool ret = post1(word);
                bool ret = false;
                if (ret)
                {
                    state.Break();
                }
            });


            Console.WriteLine(DateTime.Now);
            Console.WriteLine(nLinee);


            //Console.WriteLine(sb.ToString());


            Console.ReadLine();

            //System.IO.File.WriteAllText(@"c:\temp\passwprds.txt", sb.ToString());

            //var ret = from t in m
            //          let denti = t.Cast<Dente<int>>()
            //          let array = denti.Select(d => d.Valore)
            //          where array.Average() == array.Count()
            //          select t.Cast<Dente<int>>();

            //foreach (var d in ret)
            //{
            //    //Console.WriteLine(string.Format("{0}+{1}={2}", d.ElementAt(0).Valore, d.ElementAt(1).Valore, (d.ElementAt(0).Valore + d.ElementAt(1).Valore)));
            //    Console.WriteLine(string.Format("{0}+{1}", d.ElementAt(0).Valore, d.ElementAt(1).Valore));
            //}
        }

        static void vuoto()
        {
            Console.WriteLine(string.Format("-----vuoto-----"));

            Motore m = new Motore();


            foreach (var d in m)
            {
                Console.WriteLine(string.Format("{0}", d[0].Valore));
            }

            Console.WriteLine(string.Format("---------------"));
            Console.WriteLine(string.Format("---------------"));
        }

        static void vuoto2()
        {
            Console.WriteLine(string.Format("-----vuoto2----"));

            Motore m = new Motore();

            Ingranaggio<int> i = new Ingranaggio<int>();
            m.AggiungiIngranaggio(i);

            foreach (var d in m)
            {
                Console.WriteLine(string.Format("{0}", d[0].Valore));
            }

            Console.WriteLine(string.Format("---------------"));
            Console.WriteLine(string.Format("---------------"));
        }

        static void one()
        {
            Console.WriteLine(string.Format("------one------"));

            Motore m = new Motore();

            m.Ingrana<int>(1, 2, 3);

            foreach (var d in m)
            {
                Console.WriteLine(string.Format("{0}", d[0].Valore));
            }

            Console.WriteLine(string.Format("---------------"));
            Console.WriteLine(string.Format("---------------"));
        }

        static void two()
        {
            Console.WriteLine(string.Format("------two------"));

            Motore m = new Motore();

            m.Ingrana<int>(1, 2, 3, 4, 5, 6, 7);
            m.Ingrana<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);

            foreach (var d in m)
            {
                Console.WriteLine(string.Format("{0} - {1}", d[0].Valore, d[1].Valore));
            }

            Console.WriteLine(string.Format("---------------"));
            Console.WriteLine(string.Format("---------------"));
        }

        static void range()
        {
            Console.WriteLine(string.Format("-----range-----"));
            Motore m = new Motore();

            IngranaggioRange ir = new IngranaggioRange(5, 9);

            m.AggiungiIngranaggio(ir);

            foreach (var d in m)
            {
                Console.WriteLine(string.Format("{0}", d[0].Valore));
            }
            Console.WriteLine(string.Format("---------------"));
            Console.WriteLine(string.Format("---------------"));
        }

        static void brute_force()
        {
            HttpClient client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });

            Func<string, bool> post = delegate(string word)
            {
                string url = "http://localhost:57496/auth/login?ReturnUrl=%2fHome%2fIndex";
                var values = new Dictionary<string, string>
                {
                    { "userName", "ame" },
                    { "password", word }
                };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync(url, content);
                response.Wait();

                if (response.Result.StatusCode == HttpStatusCode.Found
                    && response.Result.Headers.Location.ToString() == "/Home/Index")
                {
                    return true;
                }

                //var responseString = response.Content.ReadAsStringAsync();
                return false;
            };

            Motore m = new Motore();

            string template = "01dqwertyuiopasdfghjklzxcvbnm";

            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());

            StringBuilder sb = new StringBuilder();
            Console.ReadLine();

            Console.WriteLine(DateTime.Now);
            long nLinee = 0;

            //foreach (var d in m)
            //{
            //    char[] chars = d.Select(dd => (char)dd.Valore).ToArray();
            //    string word = new string(chars);
            //    nLinee ++;
            //    //Console.WriteLine(word);
            //    sb.AppendLine(word);
            //}

            Parallel.ForEach<Dente[]>(m, (item, state) =>
            {
                char[] chars = item.Select(dd => (char)dd.Valore).ToArray();
                string word = new string(chars);

                bool ret = post(word);
                if (ret)
                {
                    state.Break();
                }
            });


            Console.WriteLine(DateTime.Now);
            Console.WriteLine(nLinee);


            //Console.WriteLine(sb.ToString());


            Console.ReadLine();
        }

        static void words()
        {
            Motore m = new Motore();
            string template = "1dqwertyuiopasdfghjklzxcvbnm";

            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());
            m.Ingrana<char>(template.ToCharArray());

            Console.WriteLine(DateTime.Now);
            long nLinee = 0;
            StringBuilder sb = new StringBuilder();
            //foreach (var d in m)
            //{
            //    char[] chars = d.Select(dd => (char)dd.Valore).ToArray();
            //    string word = new string(chars);
            //    nLinee++;
            //    //Console.WriteLine(word);
            //    sb.AppendLine(word);
            //}
            //MemoryStream ms = new MemoryStream();
            //using (MemoryStream ms = new MemoryStream())
            MemoryStream ms = null;
            using (ms = new MemoryStream())
            {
                //using (StreamWriter file = new StreamWriter(@"c:\temp\password.txt"))
                using (StreamWriter file = new StreamWriter(ms))
                {
                    foreach (var d in m)
                    {
                        char[] chars = d.Select(dd => (char)dd.Valore).ToArray();
                        string word = new string(chars);
                        nLinee++;
                        //Console.WriteLine(word);
                        file.WriteLine(word);
                    }
                }
                ms.Close();
            }
            ms = null;

            //sb = null;
            //ms.Close();
            //ms.Dispose();
            //ms = null;
            //System.GC.SuppressFinalize(sb);
            //System.GC.Collect();
            System.GC.Collect();
            //System.GC.WaitForPendingFinalizers();

            Console.WriteLine(DateTime.Now);
            Console.WriteLine(nLinee);

        }
    }
}
