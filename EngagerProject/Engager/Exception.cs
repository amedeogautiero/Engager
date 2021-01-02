using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engager
{
    public class EngagerException : ApplicationException
    {

        public EngagerException(string message)
            : base(message)
        { }
    }
}
