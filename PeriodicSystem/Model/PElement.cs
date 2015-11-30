using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using Figures;

namespace Model
{
    public class PElement : NotifyBase
    {

        public String name { get; set; }
        public String symbol { get; set; }
        public int number { get; set; }
        public double weight { get; set; }
        public int[] shells { get; set; }

        public PElement()
        {
            name = "name";
            symbol = "sym";
            number = 0;
            weight = 0;
            shells = new int[7];

        }

        public PElement(String name, String symbol, int number, double weight, int[] shells)
        {
            this.name = name;
            this.symbol = symbol;
            this.number = number;
            this.weight = weight;
            this.shells = shells;

        }

    }
}
