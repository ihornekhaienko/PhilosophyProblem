using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosophy
{
    public class Fork
    {
        private static int count = 0;
        public string Name { get; private set; }

        public Fork()
        {
            Name = "fork " + ++count;
        }
    }
}
