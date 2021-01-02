using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteLoop
{
    public interface IListaDenti
    {
        Dente this[int index] { get; set; }

        int Count { get; }

        void Add(Dente dente);
    }

    public class ListaDenti : IListaDenti
    {
        List<Dente> denti = null;

        public ListaDenti()
        {
            denti = new List<Dente>();
        }

        public Dente this[int index]
        {
            get
            {
                return denti[index];
            }

            set
            {
                denti[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return denti.Count;
            }
        }

        public void Add(Dente dente)
        {
            denti.Add(dente);
        }
    }
}
