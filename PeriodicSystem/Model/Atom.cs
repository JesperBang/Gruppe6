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

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
