using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model
{
    public class PSystem : UserControl
    {
        public ObservableCollection<PElement> elements { get; set; }

        public PSystem()
        {
            elements = new ObservableCollection<PElement>();
            for(int i=1; i<104; i++)
            {
                elements.Add(new PElement("name", "s", i, 0.0, new int[7]));
            }
        }
        public PSystem(PElement[] initElements)
        {
            if(initElements != null)
            {
            }
            foreach(PElement e in initElements)
            {
                elements.Add(e);
            }
        }

        public static PSystem createFromFile(String filePath)
        {
            System.IO.StreamReader file;
            try {
                file = new System.IO.StreamReader(filePath);
            }
            catch (Exception e)
            {
                return null;
            }

            String text = file.ReadToEnd();
            file.Close();
            String[] lines;
            if(text != null)
            {
                lines = text.Split(Environment.NewLine.ToArray()); //splits the lines in to seperate strings in the array.

                int numberOfEntries = lines.Length / 6 - 1;
                PElement[] elements = new PElement[numberOfEntries];
                String name,
                       symbol;
                int number;
                double weight;
                int[] shells = new int[7];
                String[] shellBuffer;

                for (int l=6; l<lines.Length; l += 6)
                {
                    name = lines[l];
                    symbol = lines[l + 1];
                    number = Int32.Parse(lines[l + 2]);
                    weight = Double.Parse(lines[l + 3]);
                    shellBuffer = lines[1 + 4].Split(' ');
                    for(int i=0;i<shellBuffer.Length;i++)
                    {
                        shells[i] = Int32.Parse(shellBuffer[i]);
                    }
                    elements[l / 6 - 1] = new PElement(name, symbol, number, weight, shells);
                }

                return new PSystem(elements);
            }
            return null;
        }

    }
}
