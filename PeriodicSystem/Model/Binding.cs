using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Binding : NotifyBase
    {
        private static int counter = 0;
        public int Id { get; set; } = ++counter;

        public enum TypeOfBinding
        {
            Single, Double, Triple
        };
        
        private Atom bindingPoint1 = new Atom(1);
        public Atom BindingPoint1 { get { return bindingPoint1; } set { bindingPoint1 = value; NotifyPropertyChanged(); }}

        private Atom bindingPoint2 = new Atom(1);
        public Atom BindingPoint2 { get { return bindingPoint2; } set { bindingPoint2 = value; NotifyPropertyChanged(); } }

        //does not notify UI of changes in atoms...
        //public double X1 { get { return BindingPoint1.CenterX; } }
        //public double Y1 { get { return BindingPoint1.CenterY; } }

        //public double X2 { get { return BindingPoint2.CenterX; } }
        //public double Y2 { get { return BindingPoint2.CenterY; } }

        public TypeOfBinding BindingState { get; set; }

        public Binding(Atom atom1, Atom atom2)
        {
            BindingPoint1 = atom1;
            BindingPoint2 = atom2;
            BindingState = TypeOfBinding.Single;
        }

        public Binding(Atom atom1, Atom atom2, TypeOfBinding state)
        {
            BindingPoint1 = atom1;
            BindingPoint2 = atom2;
            BindingState = state;
        }

        public void changeBinding()
        {
            switch (BindingState)
            {
                case TypeOfBinding.Single:
                    BindingState = TypeOfBinding.Double;
                    break;

                case TypeOfBinding.Double:
                    BindingState = TypeOfBinding.Triple;
                    break;

                case TypeOfBinding.Triple:
                    BindingState = TypeOfBinding.Single;
                    break;
            }
        }
    }
}
