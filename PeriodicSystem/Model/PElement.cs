using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;

namespace Model
{
    class PElement : Panel{ //skal måske bruge System.windows.forms.panel i stedet.
        Label[] labels;
        String name { set { name = value; } }
        String symbol { set { symbol = value; } }
        int number { set { number = value; } }
        double weight { set { weight = value; } }
        int[] shells { set { shells = value; } }

        public PElement(String name, String symbol, int number, double weight, int[] shells){

            this.symbol = symbol;
            this.number = number;
            this.weight = weight;
            this.shells = shells;

            labels[0] = new Label();
            labels[0].Content = name;
            this.AddVisualChild(labels[0]);

        }
        
    }
}
