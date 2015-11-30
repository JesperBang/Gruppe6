using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Media;
using Figures;

namespace Model
{
    public class Atom : NotifyBase
    {
        private static int counter = 0;

        public int Id { get; set; } = ++counter;

        [XmlIgnore]
        public double Width { get; set; }
        [XmlIgnore]
        public double Height { get; set; }

        [XmlIgnore]
        public String Abbrevation { get { return (Protons > 0 && Protons < abbrevations.Length) ? abbrevations[Protons] : "XX"; } }

        private double x = 0;
        private double y = 0;

        public double X
        {
            get { return x; }
            set
            {
                x = (value < 0) ? 0 : value;
                CenterX = x + (Width / 2);
                NotifyPropertyChanged();
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = (value < 0) ? 0 : value;
                CenterY = y + (Height / 2);
                NotifyPropertyChanged();
            }
        }

        private double centerX;
        private double centerY;

        [XmlIgnore]
        public double CenterX { get { return centerX; } private set { centerX = value; NotifyPropertyChanged(); } }
        [XmlIgnore]
        public double CenterY { get { return centerY; } private set { centerY = value; NotifyPropertyChanged(); } }

        public int Protons { get; set; }

        private bool isSelected = false;
        [XmlIgnore]
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }

        public Brush SelectedColor => isSelected ? Brushes.SeaGreen : Brushes.Red;

        public Atom()
        {
        }
        
    }
}