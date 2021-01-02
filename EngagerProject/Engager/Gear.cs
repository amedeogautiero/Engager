using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engager.Events;

namespace Engager
{
    public class GearBase
    {
        internal event LapCompleteEventHandler LapCompleted;

        public GearBase()
        {
            this.CurrentCogIndex = -1;
        }

        public int CurrentCogIndex { get; set; }

        public int TotalCogs 
        {
            get
            {
                return this.cogs.Count;
            }
        }

        internal List<object> cogs = new List<object>();

        public bool Turn()
        {
            this.CurrentCogIndex++;

            if (this.CurrentCogIndex == cogs.Count)
            {
                this.CurrentCogIndex = 0;
                if (this.LapCompleted != null)
                    this.LapCompleted(this);
            }

            return true;
        }

        public void Reset()
        {
            if (cogs.Count == 0)
                this.CurrentCogIndex = -1;
            else
                this.CurrentCogIndex = 0;
        }

        public object CurrentCog
        {
            get
            {
                return cogs[this.CurrentCogIndex];
            }
        }
    }

    public class Gear<T> : GearBase 
    {
        public void AddCog(T cog)
        {
            CurrentCogIndex = 0;
            cogs.Add(cog);
        }

        public void AddCogs(T[] cogs)
        {
            foreach (T cog in cogs)
                AddCog(cog);
        }

        public Gear<T> Clone()
        {
            Gear<T> r = new Gear<T>();
            foreach (T cog in cogs) r.AddCog(cog);
            return r;
        }

        public T CurrentCog
        {
            get
            {
                return (T)cogs[this.CurrentCogIndex];
            }
        }
    }
}
