using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Binding
    {
        public enum TypeOfBinding
        {
            Single, Double, Triple
        };

        public Atom BindingPoint1 { get; set; }
        public Atom BindingPoint2 { get; set; }

        public int X1 { get { return BindingPoint1.X; } }
        public int Y1 { get { return BindingPoint1.Y; } }

        public int X2 { get { return BindingPoint2.X; } }
        public int Y2 { get { return BindingPoint2.Y; } }

        public TypeOfBinding BindingState { get; set; }

        public Binding(Atom atom1, Atom atom2)
        {
            BindingPoint1 = atom1;
            BindingPoint2 = atom2;
        }

        public Binding(Atom atom1, Atom atom2, TypeOfBinding state)
        {
            BindingPoint1 = atom1;
            BindingPoint2 = atom2;
            BindingState = state;
        }
    }
}
