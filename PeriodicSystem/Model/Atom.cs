using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Atom
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Protons { get; set; }

        public Atom(int protons)
        {
            X = 100;
            Y = 100;
            Protons = protons;
        }

        public Atom(int protons, int x, int y)
        {
            X = x;
            Y = y;
            Protons = protons;
        }
    }
}
