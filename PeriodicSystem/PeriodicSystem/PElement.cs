using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;

namespace PeriodicSystem
{
    class PElement : UserControl{
        Label[] labels;
        String symbol { set { symbol = value; } }
        int number { set { number = value; } }
        double weight { set { weight = value; } }
        int[] shells { set { shells = value; } }

        public PElement(String symbol, int number, double weight, int[] shells){

            this.symbol = symbol;
            this.number = number;
            this.weight = weight;
            this.shells = shells;

            

        }

    }
}
