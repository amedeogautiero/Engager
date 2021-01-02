using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteLoop
{
    public class Motore : IEnumerable<Dente[]>
    {
        List<Ingranaggio> ingranaggi = null;
        Ingranaggio primoIngranaggi = null;

        private bool altro = true;

        public Motore()
        {
            ingranaggi = new List<Ingranaggio>();
        }

        private Dente[] m_Current = null;
        public Dente[] Current
        {
            get
            {
                if (m_Current != null) return m_Current;

                if (!altro) return null;

                if (this.ingranaggi.Count == 0) return null;

                //Dente[] rets = null;

                //rets = new Dente[this.ingranaggi.Count];
                //for (int x = 0; x < this.ingranaggi.Count; x++)
                //{
                //    rets[x] = this.ingranaggi[x].Current;
                //}

                bool isOk = false;

                m_Current = new Dente[this.ingranaggi.Count];
                for (int x = 0; x < this.ingranaggi.Count; x++)
                {
                    Dente Current = this.ingranaggi[x].Current;
                    if (Current != null)
                    {
                        m_Current[x] = Current;
                        isOk = true;
                    }
                }

                //m_Current = (from ingranaggio in this.ingranaggi
                //            //where ingranaggio != null && ingranaggio.Current != null
                //             where ingranaggio.Current != null
                //             select ingranaggio.Current).ToArray();

                if (!isOk)
                    return null;

                if (m_Current.Count() == 0)
                    return null;

                return m_Current;
            }
        }

        public bool Gira()
        {
            this.m_Current = null;
            if (ingranaggi.Count > 0)
            {
                //return ingranaggi[0].Gira();
                //return this.ingranaggi[0].MoveNext();
                return this.primoIngranaggi.MoveNext();
            }

            
            return false;
        }

        public bool AggiungiIngranaggio(Ingranaggio ingranaggio)
        {
            if (ingranaggio != null)
            {
                ingranaggio.GiroCompletato += ingranaggio_GiroCompletato;
                this.ingranaggi.Add(ingranaggio);
                if (this.ingranaggi.Count == 1)
                {
                    primoIngranaggi = ingranaggio;
                }
                return true;
            }
            return false;
        }

        private void ingranaggio_GiroCompletato(Ingranaggio ingranaggio)
        {
            int index = this.ingranaggi.IndexOf(ingranaggio);

            index++;

            if (index == this.ingranaggi.Count)//non ci sono altri ingranaggi
            {
                altro = false;
                //if (this.Completed != null)
                //    this.Completed(this);
            }
            else
            {
                //this.ingranaggi[index].Gira();
                this.ingranaggi[index].MoveNext();
            }
        }

        public IEnumerator<Dente[]> GetEnumerator()
        {
            Func<bool> gira = delegate ()
            {
                this.m_Current = null;
                //if (ingranaggi.Count > 0)
                //{
                    //return ingranaggi[0].Gira();
                    //return this.ingranaggi[0].MoveNext();
                    return this.primoIngranaggi.MoveNext();
                //}

                //return false;
            };

            if (this.ingranaggi != null)
            {
                this.ingranaggi.ForEach(i => i.Reset());
            }

            while (this.Current != null)
            {
                yield return this.m_Current;
                //this.Gira();
                gira();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Ingrana<T>(params T[] valori)
        {
            if (valori.Length > 0)
            {
                Ingranaggio<T> ingranaggio = new Ingranaggio<T>();
                ingranaggio.AggiungiDenti(valori);
                return this.AggiungiIngranaggio(ingranaggio);
            }
            return false;
        }

        public int NumeroIngranaggi
        {
            get
            {
                return this.ingranaggi.Count;
            }
        }
    }
}
