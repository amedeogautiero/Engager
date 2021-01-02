using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engager.Events
{
    internal delegate void LapCompleteEventHandler(GearBase gear);
    public delegate void CompleteEventHandler(Engager engager);
}
