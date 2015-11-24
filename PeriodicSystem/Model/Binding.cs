using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Model
{
    public class Binding : NotifyBase
    {
        private static int counter = 0;
        [XmlIgnore]
        public int Id { get; set; } = ++counter;

        public enum TypeOfBinding
        {
            Single, Double, Triple
        };

        private bool isSelected = false;
        [XmlIgnore]
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }

        public Brush SelectedColor => isSelected ? Brushes.SeaGreen : Brushes.Blue;

        //styrer tykkelsen på bindingen, så der kan repræsenteres enkelt dobelt og tripel
        private int enbinding = 10;
        [XmlIgnore]
        public int Enbinding { get { return enbinding; } set { enbinding = value; NotifyPropertyChanged(); } }
        private int tobinding = 0;
        [XmlIgnore]
        public int Tobinding { get { return tobinding; } set { tobinding = value; NotifyPropertyChanged(); } }
        private int trebinding = 0;
        [XmlIgnore]
        public int Trebinding { get { return trebinding; } set { trebinding = value; NotifyPropertyChanged(); } }
        
        private Atom bindingPoint1 = new Atom(1);
        [XmlIgnore]
        public Atom BindingPoint1 {
            get {
                return bindingPoint1;
            }
            set {
                bindingPoint1 = value;
                BPID1 = bindingPoint1.Id;
                NotifyPropertyChanged();
            }
        }

        public int BPID1{get; set; } //bindingpoint id 1

        private Atom bindingPoint2 = new Atom(1);
        [XmlIgnore]
        public Atom BindingPoint2 {
            get {
                return bindingPoint2;
            }
            set{
                bindingPoint2 = value;
                BPID2 = bindingPoint2.Id;
                NotifyPropertyChanged();
            }
        }

        public int BPID2 { get; set; }//bindingpoint id 2

        private TypeOfBinding bindingState;
        public TypeOfBinding BindingState {
            get { return bindingState; }
            set
            {
                bindingState = value;
                switch (bindingState)
                {
                    case Binding.TypeOfBinding.Single:
                        Enbinding = 10;
                        Tobinding = 0;
                        Trebinding = 0;
                        break;

                    case Binding.TypeOfBinding.Double:
                        Enbinding = 0;
                        Tobinding = 10;
                        Trebinding = 30;
                        break;

                    case Binding.TypeOfBinding.Triple:
                        Enbinding = 10;
                        Tobinding = 30;
                        Trebinding = 50;
                        break;
                }
            }
        }
        
        public Binding() { }

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
