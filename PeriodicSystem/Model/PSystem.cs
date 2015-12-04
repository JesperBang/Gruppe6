using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Figures;

namespace Model
{
    public class PSystem : NotifyBase
    {
        public ObservableCollection<PElement> elements { get; set; } = new ObservableCollection<PElement>();
		public ObservableCollection<Label> elementSymbols { get; set; } = new ObservableCollection<Label>();
		public ObservableCollection<Grid> elementGrid { get; set; } = new ObservableCollection<Grid>();

		public ObservableCollection<PElement> currentSelection { get; set; } = new ObservableCollection<PElement>();
		public delegate void MouseButtonEventHandler(object sender, MouseButtonEventArgs e);

		public PSystem()
        {

			PElement[] initElements = createFromFile("periodic_table.txt");

			if (initElements != null)
            {

				//PElement testE = initElements[0];
				for(int i=0; i<initElements.Length; i++)
				{
					elements.Add(initElements[i]);

					Label lab = new Label();
					lab.Content = initElements[i].symbol;
					lab.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
					lab.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    elementSymbols.Add(lab);
				}

			setupGrid();
			currentSelection.Add(elements[0]); //element 79 gold is on index 64
            }
			else
			{
				//throw new NullPointerException();
			}
        }
        public PSystem(PElement[] initElements)
        {
            

		}

		private void elementSelected(Object sender, MouseButtonEventArgs e)
		{

			//currentSelection = new ObservableCollection<PElement>();
			//currentSelection.Add(elements[69]);
			currentSelection[0] = elements[69];
		}

		public PElement findElement(int number)
		{
			foreach(PElement e in elements)
			{
				if (e.number == number) return e;
			}
			return elements[0];
		}

		private void setupGrid()
		{

			Grid grid = new Grid();
			grid.ShowGridLines = true;
			grid.Background = System.Windows.Media.Brushes.LemonChiffon;
			for (int i = 0; i < 9; i++)
			{
				grid.RowDefinitions.Add(new RowDefinition());
			}
			for (int i = 0; i < 18; i++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition());
			}
			//first row 0 - 1
			//grid.Height = 300;
			//grid.Width = 400;
			grid.Children.Add(elementSymbols[0]);
			Grid.SetColumn(elementSymbols[1], 17);
			grid.Children.Add(elementSymbols[1]);

			//second row 2 -9
			Grid.SetRow(elementSymbols[2], 1);
			grid.Children.Add(elementSymbols[2]);
			Grid.SetRow(elementSymbols[3], 1);
			Grid.SetColumn(elementSymbols[3], 1);
			grid.Children.Add(elementSymbols[3]);
			for (int i = 0; i < 6; i++)
			{
				Grid.SetColumn(elementSymbols[4 + i], 12 + i);
				Grid.SetRow(elementSymbols[4 + i], 1);
				grid.Children.Add(elementSymbols[4 + i]);
			}

			//third row 10 - 17
			Grid.SetRow(elementSymbols[10], 2);
			grid.Children.Add(elementSymbols[10]);
			Grid.SetRow(elementSymbols[11], 2);
			Grid.SetColumn(elementSymbols[11], 1);
			grid.Children.Add(elementSymbols[11]);
			for (int i = 0; i < 6; i++)
			{
				Grid.SetColumn(elementSymbols[12 + i], 12 + i);
				Grid.SetRow(elementSymbols[12 + i], 2);
				grid.Children.Add(elementSymbols[12 + i]);
			}

			//fourth fifth and sixth row 18 - 71
			for (int j = 0; j < 3; j++)
			{
				for (int i = 0; i < 18; i++)
				{
					Grid.SetRow(elementSymbols[18 * j + 18 + i], 3 + j);
					Grid.SetColumn(elementSymbols[18 * j + 18 + i], i);
					grid.Children.Add(elementSymbols[18 * j + 18 + i]);
				}
			}

			//seventh row 72 - 83
			for (int i = 0; i < 12; i++)
			{
				Grid.SetRow(elementSymbols[72 + i], 6);
				Grid.SetColumn(elementSymbols[72 + i], i);
				grid.Children.Add(elementSymbols[72 + i]);
			}

			//eight and ninth row 84 - 
			for (int j = 0; j < 2; j++)
			{
				for (int i = 0; i < 14; i++)
				{
					Grid.SetRow(elementSymbols[14 * j + 84 + i], 7 + j);
					Grid.SetColumn(elementSymbols[14 * j + 84 + i], i + 2);
					grid.Children.Add(elementSymbols[14 * j + 84 + i]);
				}
			}
			elementGrid.Add(grid);
			//elementSymbols[0]


		}

        private PElement[] createFromFile(String filePath)
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
				lines = Regex.Split(text, Environment.NewLine); //splits the lines in to seperate strings in the array.
				//lines = text.Split('\n');
				String testLine1 = lines[0];
				String testLine2 = lines[1];
				String testLine3 = lines[2];
				String testLine4 = lines[3];
				String testLine5 = lines[4];
				String testLine6 = lines[5];

				int numberOfEntries = lines.Length / 6 - 1;
                PElement[] elements = new PElement[numberOfEntries+1];
                String name,
                       symbol;
                int number;
                double weight;
                int[] shells = new int[7];
                String[] shellBuffer;

                for (int l=6; l<lines.Length; l += 6)
				{
					
					//String testName = lines[l];
					name = lines[l];
					//String testSymbol = lines[l+1];
					symbol = lines[l + 1];
					//String testNumber = lines[l+2];
					number = Int32.Parse(lines[l + 2]);
					//String testWeight = lines[l+3];
					System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
					weight = Double.Parse(lines[l + 3], nfi);
					//String testShell = lines[l+4];
					//Regex reg = new Regex(@"\s");
					if (lines[l + 4].Contains(" "))
					{
						shellBuffer = lines[l + 4].Split(' ');
					}else
					{
						shellBuffer = new string[1];
						shellBuffer[0] = lines[l + 4];
					}
					//String testShell2 = Regex.Split(lines[1 + 4], " ")[0];
					//try { String testShell3 = Regex.Split(lines[1 + 4], " ")[1]; }catch(Exception e) { }

					//if (shellBuffer.Length < 2)
					//{
					//shellBuffer = new String[]{ lines[l+4] };
					//}

					for (int i = 0; i < shellBuffer.Length; i++)
					{
						int shellLength = shellBuffer.Length;
						shells[i] = Int32.Parse(shellBuffer[i]);

						//if (number == 79)
						//{
						//	String shellbufferTest = shellBuffer[i];
						//	int shellValueTest = Int32.Parse(shellBuffer[i]);
						//}
					}
					elements[l / 6 - 1] = new PElement(name, symbol, number, weight, shells);
					
					//String testNameElement = elements[l / 6 - 1].name;

				}

				return elements;
            }
            return null;
        }

    }
}
