using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotore
{
    public abstract class Ingranaggio : IEnumerator<Dente>
    {
        //private ListaDenti denti = null;
        private IListaDenti denti = null;
        protected int IndiceDenteCorrente = 0;

        internal delegate void GiroCompletatoEventHandler(Ingranaggio ingranaggio);
        internal event GiroCompletatoEventHandler GiroCompletato;

        protected Ingranaggio()
        {
            this.denti = new ListaDenti();
            this.IndiceDenteCorrente = -1;
            this.m_Current = null;
        }

        protected Ingranaggio(IListaDenti denti):this()
        {
            this.denti = denti;
        }

        private Dente m_Current = null;

        public virtual Dente Current
        {
            get
            {
                //if (this.denti == null)
                //    return null;

                //return this.denti[this.IndiceDenteCorrente];
                return m_Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                //return this.denti[this.IndiceDenteCorrente];
                return m_Current;
            }
        }

        public virtual bool MoveNext()
        {
            //if (this.denti == null)
            //    return false;

            this.IndiceDenteCorrente++;

            if (this.IndiceDenteCorrente == this.denti.Count)
            {
                this.Reset();

                if (this.GiroCompletato != null)
                    this.GiroCompletato(this);
            }
            else
            {
                this.m_Current = this.denti[this.IndiceDenteCorrente];
            }

            return true;
        }

        public virtual void Reset()
        {
            this.IndiceDenteCorrente = -1;
            this.m_Current = null;

            if (this.denti.Count > 0)
            {
                this.IndiceDenteCorrente = 0;
                this.m_Current = this.denti[0];
            }
        }

        public void Dispose()
        {
            this.denti = null;
            this.m_Current = null;
        }

        protected bool AggiungiDente(Dente dente)
        {
            if (dente != null && dente.Valore != null)
            {
                this.denti.Add(dente);
                this.IndiceDenteCorrente = 0;
                this.m_Current = this.denti[0];
                return true;
            }
            return false;
        }

        protected void AggiungiDente(object valore)
        {
            //this.denti.Add(new Dente() { Valore = valore });
            this.AggiungiDente(new Dente() { Valore = valore });
        }

        protected void AggiungiDenti(object[] valori)
        {
            foreach (object valore in valori)
            {
                AggiungiDente(valore);
            }
        }

        protected void AggiungiDenti(Dente[] denti)
        {
            foreach (Dente dente in denti)
            {
                //this.denti.Add(dente);
                this.AggiungiDente(dente);
            }
        }
    }

    public class Ingranaggio<T> : Ingranaggio
    {
        public void AggiungiDente(T valore)
        {
            //base.denti.Add(new Dente<T>() { Valore = valore });
            base.AggiungiDente(valore);
        }

        public void AggiungiDenti(T[] valori)
        {
            foreach (T valore in valori)
            {
                AggiungiDente(valore);
            }
        }

        public void AggiungiDente(Dente<T> dente)
        {
            //base.denti.Add(valore);
            base.AggiungiDente(dente);
        }

        public void AggiungiDenti(Dente<T>[] denti)
        {
            base.AggiungiDenti(denti);
        }
    }

    public class IngranaggioRange : Ingranaggio
    {
        public class ListaDentiRange : IListaDenti
        {
            internal ListaDentiRange(int minValue, int maxValue)
            {
                this.MinValue = minValue;
                this.MaxValue = maxValue;
            }

            public int MinValue { get; private set; }
            public int MaxValue { get; private set; }

            public int Count
            {
                get
                {
                    return (this.MaxValue - this.MinValue) + 1;
                }
            }

            public Dente this[int index]
            {
                get
                {
                    int somma = index + this.MinValue;
                    if (somma <= this.MaxValue)
                    {
                        return new Dente<int>() { Valore = somma };
                    }

                    throw new IndexOutOfRangeException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public void Add(Dente dente)
            {
                throw new NotImplementedException();
            }
        }

        public IngranaggioRange(int minValue, int maxValue):base(new ListaDentiRange(minValue,maxValue))
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            //base.IndiceDenteCorrente = 0;
            //this.m_Current = calcola();
        }

        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        //private Dente m_Current;
        //public override Dente Current
        //{
        //    get
        //    {
        //        //if (base.IndiceDenteCorrente + this.MinValue <= this.MaxValue)
        //        //    return new Dente<int>() { Valore = (base.IndiceDenteCorrente + this.MinValue) };

        //        //return null;
        //        return m_Current;
        //    }
        //}

        //public override bool MoveNext()
        //{
        //    return base.MoveNext();

        //    this.m_Current = null;
        //    base.IndiceDenteCorrente++;
        //    //int somma = base.IndiceDenteCorrente + this.MinValue;
        //    //if (somma <= this.MaxValue)
        //    //{
        //    //    this.m_Current = new Dente<int>() { Valore = somma };
        //    //    return true;
        //    //}
        //    this.m_Current = calcola();
        //    if (this.m_Current != null)
        //        return true;

        //    return false;
        //}

        private Dente calcola()
        {
            int somma = base.IndiceDenteCorrente + this.MinValue;
            if (somma <= this.MaxValue)
            {
                return new Dente<int>() { Valore = somma };
            }

            return null;
        }
    }
}
