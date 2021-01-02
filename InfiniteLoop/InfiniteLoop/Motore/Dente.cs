using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotore
{
    public class Dente
    {
        internal Dente()
        { }

        public object Valore { get; set; }
    }

    public class Dente<T> : Dente
    {
        public new T Valore
        {
            get
            {
                return (T)base.Valore;
            }
            set
            {
                base.Valore = value;
            }
        }
    }
}
