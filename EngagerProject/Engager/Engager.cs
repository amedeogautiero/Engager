using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engager.Events;
using System.Collections;

namespace Engager
{
    public class Engager : IEnumerable<object[]>
    {
        public event CompleteEventHandler Completed;

        List<GearBase> gears = new List<GearBase>();
        public bool Turn()
        {
            isCurrentable = true;

            if (this.gears.Count == 0)
                throw new EngagerException("No Gears");
            
            return this.gears[0].Turn();
        }

        public void Reset()
        {
            this.isCurrentable = true;
            for (int x = 0; x < this.gears.Count; x++)
            {
                this.gears[0].Reset();
            }
        }

        public void Add(GearBase gear)
        {
            gear.LapCompleted += new Events.LapCompleteEventHandler(gear_LapCompleted);
            this.gears.Add(gear);
        }

        public void Gear<T>(params T[] valori)
        {
            Gear<T> gear = new Gear<T>();
            gear.AddCogs(valori);
            gear.LapCompleted += new Events.LapCompleteEventHandler(gear_LapCompleted);
            this.gears.Add(gear);
        }

        private bool isCurrentable = true;
        public object[] Current
        {
            get
            {
                if (!isCurrentable) return null;

                object[] rets = new object[this.gears.Count];
                for (int x = 0; x < this.gears.Count; x++)
                {
                    rets[x] = this.gears[x].CurrentCog;
                }

                return rets;
            }
        }

        void gear_LapCompleted(GearBase gear)
        {
            int index = this.gears.IndexOf(gear);

            index++;

            if (index == this.gears.Count)//non ci sono altri ingranaggi
            {
                isCurrentable = false;
                if (this.Completed != null)
                    this.Completed(this);
            }
            else
            {
                this.gears[index].Turn();
            }
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            while (this.Current != null)
            {
                yield return this.Current;
                this.Turn();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
