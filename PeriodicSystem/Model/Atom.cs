using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Atom : NotifyBase
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public String Abbrevation{ get { return (Protons > 0 && Protons < abbrevations.Length ) ? abbrevations[Protons] : "XX"; } }

        private double x = 0;
        private double y = 0;

        public double X {
            get { return x; }
            set {
                x = (value < 0) ? 0 : value;
                CenterX = x+(Width/2);
                NotifyPropertyChanged();
            }
        }

        public double Y {
            get { return y; }
            set {
                y = (value < 0) ? 0 : value;
                CenterY = y + (Height/2);
                NotifyPropertyChanged();
            }
        }

        private double centerX = 0;
        private double centerY = 0;

        public double CenterX { get { return centerX; } private set { centerX = value; NotifyPropertyChanged(); } }
        public double CenterY { get { return centerY; } private set { centerY = value; NotifyPropertyChanged(); } }

        public int Protons { get; set; }

        public Atom()
        {
        }

        public Atom(int protons)
        {
            X = 220;
            Y = 220;
            Protons = protons;
            Width = 100;
            Height = 100;
        }

        public Atom(int protons, int x, int y)
        {
            X = x;
            Y = y;
            Protons = protons;
            Width = 100;
            Height = 100;
        }


        static private String[] abbrevations =
        {
            "Undefined",
            "H",
            "He",
            "Li",
            "Be",
            "B",
            "C",
            "N",
            "O",
            "F",
            "Ne",
            "Na",
            "Mg",
            "Al",
            "Si",
            "P",
            "S",
            "Cl",
            "Ar",
            "K",
            "Ca",
            "Sc",
            "Ti",
            "V",
            "Cr",
            "Mn",
            "Fe",
            "Co",
            "Ni",
            "Cu",
            "Zn",
            "Ga",
            "Ge",
            "As",
            "Se",
            "Br",
            "Kr",
            "Rb",
            "Sr",
            "Y",
            "Zr",
            "Nb",
            "Mo",
            "Tc",
            "Ru",
            "Rh",
            "Pd",
            "Ag",
            "Cd",
            "In",
            "Sn",
            "Sb",
            "Te",
            "I",
            "Xe",
            "Cs",
            "Ba",
            "La",
            "Ce",
            "Pr",
            "Nd",
            "Pm",
            "Sm",
            "Eu",
            "Gd",
            "Tb",
            "Dy",
            "Ho",
            "Er",
            "Tm",
            "Yb",
            "Lu",
            "Hf",
            "Ta",
            "W",
            "Re",
            "Os",
            "Ir",
            "Pt",
            "Au",
            "Hg",
            "Tl",
            "Pb",
            "Bi",
            "Po",
            "At",
            "Rn",
            "Fr",
            "Ra",
            "Ac",
            "Th",
            "Pa",
            "U",
            "Np",
            "Pu",
            "Am",
            "Cm",
            "Bk",
            "Cf",
            "Es",
            "Fm",
            "Md",
            "No",
            "Lr",
            "Rf",
            "Db",
            "Sg",
            "Bh",
            "Hs",
            "Mt",
            "Ds",
            "Rg",
            "Cn",
            "Uut",
            "Fl",
            "Uup",
            "Lv",
            "Uus",
            "Uuo"
        };
    }
}
