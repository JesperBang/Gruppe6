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

		public static void resetCounter()
		{
			counter = 0;
		}

		private bool hitTestVisible=true;
		public bool HitTestVisible { get { return hitTestVisible; } set { hitTestVisible = value; NotifyPropertyChanged(); } }

		//[XmlIgnore]
		//public double Width { get; set; }
		//[XmlIgnore]
		//public double Height { get; set; }
		private double size;
		public double Size { get { return size; } set { size = value; NotifyPropertyChanged(); } }

        private double x = 0;
        private double y = 0;

        public double X
        {
            get { return x; }
            set
            {
                x = (value < 0) ? 0 : value;
                CenterX = x - (size / 2);
                NotifyPropertyChanged();
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = (value < 0) ? 0 : value;
                CenterY = y - (size / 2);
                NotifyPropertyChanged();
            }
        }

        private double centerX;
        private double centerY;

        [XmlIgnore]
        public double CenterX { get { return centerX; } private set { centerX = value; NotifyPropertyChanged(); } }
        [XmlIgnore]
        public double CenterY { get { return centerY; } private set { centerY = value; NotifyPropertyChanged(); } }

        private bool isSelected = false;
        [XmlIgnore]
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }

        public Brush SelectedColor => isSelected ? Brushes.SeaGreen : Brushes.Red;

		public String Name { get; set; }
		public String Symbol { get; set; }
		public int Number { get; set; }
		public double Weight { get; set; }
		public int[] Shells { get; set; }

        public Atom()
        {
			++counter;
        }
        
    }
}