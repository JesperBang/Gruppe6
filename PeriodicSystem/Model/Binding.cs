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

        private bool isSelected = false;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); } }

        //styrer tykkelsen på bindingen, så der kan repræsenteres enkelt dobelt og tripel
        private int enbinding = 10;
        public int Enbinding { get { return enbinding; } set { enbinding = value; NotifyPropertyChanged(); } }
        private int tobinding = 0;
        public int Tobinding { get { return tobinding; } set { tobinding = value; NotifyPropertyChanged(); } }
        private int trebinding = 0;
        public int Trebinding { get { return trebinding; } set { trebinding = value; NotifyPropertyChanged(); } }
        
        private Atom bindingPoint1 = new Atom(1);
        public Atom BindingPoint1 { get { return bindingPoint1; } set { bindingPoint1 = value; NotifyPropertyChanged(); }}

        private Atom bindingPoint2 = new Atom(1);
        public Atom BindingPoint2 { get { return bindingPoint2; } set { bindingPoint2 = value; NotifyPropertyChanged(); } }
        
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

        public static void resetIds()
        {
            counter = 0;
        }
    }
}
