using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PeriodicSystem.Commands;
using System.Windows.Shapes;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using Model;
using GalaSoft.MvvmLight.CommandWpf;
using System.Text.RegularExpressions;

namespace PeriodicSystem.ViewModel
{

	class ViewModel : ViewModelBase
	{
		// A reference to the Undo/Redo controller.
		private UndoRedoController undoRedoController = UndoRedoController.Instance;

		private bool isAddingAtom;

		// Keeps track of the state, depending on whether a line is being added or not.
		private bool isAddingLine;
		// Used for saving the shape that a line is drawn from, while it is being drawn.
		private Atom addingLineFrom;
		// Saves the initial point that the mouse has during a move operation.
		private Point initialMousePosition;
		// Saves the initial point that the shape has during a move operation.
		private Point initialAtomPosition;
		// Used for making the shapes transparent when a new line is being added.
		// This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
		public double ModeOpacity => isAddingLine ? 0.4 : 1.0;

		public ObservableCollection<PSystem> system { get; set; } = new ObservableCollection<PSystem>();
		public ObservableCollection<PElement> selectedElement { get; set; }
		public ObservableCollection<Grid> elementGrid { get; set; }


		public ObservableCollection<Atom> Atoms { get; set; }
		public ObservableCollection<Binding> Bindings { get; set; }

		public ICommand mouseUpCanvasCommand { get; }
		public ICommand addAtomCommand { get; }
		public ICommand mouseDownAddAtomCommand { get; }
		public ICommand addAtomsCommand { get; }
		public ICommand removeAtomCommand { get; }
		public ICommand addBindingCommand { get; }
		public ICommand removeBindingCommand { get; }
		public ICommand moveAtomsCommand { get; }
		public ICommand moveMoleculeCommand { get; }
		public ICommand undoCommand { get; }
		public ICommand redoCommand { get; }
		public ICommand saveDrawingCommand { get; }
		public ICommand loadDrawingCommand { get; }
		public ICommand exportBitmapCommand { get; }
		public ICommand addMoleculeCommand { get; }
		public ICommand newDrawingCommand { get; }
		public ICommand elementSelectedCommand { get; }

		public ViewModel()
		{
			Atoms = new ObservableCollection<Atom>();
			Bindings = new ObservableCollection<Binding>();
			system.Add(new PSystem { });
			elementGrid = system[0].elementGrid;

			//addAtom(new Atom() { X=50, Y=50, Size = 50, Symbol="H"});
			//addAtom(new Atom() { X = 100, Y = 100, Size = 100, Symbol="O"});

			elementSelectedCommand = new RelayCommand<MouseButtonEventArgs>(elementSelected);
			addAtomCommand = new RelayCommand<Atom>(addAtom);
			mouseDownAddAtomCommand = new RelayCommand<MouseButtonEventArgs>(mouseDownAddAtom);
			mouseUpCanvasCommand = new RelayCommand<MouseButtonEventArgs>(mouseUpCanvas);
			saveDrawingCommand = new RelayCommand(saveDrawing);
			loadDrawingCommand = new RelayCommand(loadDrawing);
			newDrawingCommand = new RelayCommand(newDrawing);

			undoCommand = new RelayCommand(undo);
			redoCommand = new RelayCommand(redo);

			selectedElement = system[0].currentSelection;
			//elements.Add(system.currentSelection);
		}

		private static T FindParentOfType<T>(DependencyObject o)
		{
			try {
				dynamic parent = VisualTreeHelper.GetParent(o);
				return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
			}
			catch (Exception e) {
				return default(T);
			}
		}

		private static FrameworkElement FindParentOfName(FrameworkElement o, String name)
		{
			try
			{
				if (o.Name.Equals(name)) return o;

				dynamic parent = VisualTreeHelper.GetParent(o);

				return parent.name.equals(name) ? parent : FindParentOfName(parent,name);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		private void mouseUpCanvas(MouseButtonEventArgs e)
		{
			if (selectedElement[0].IsSelected == true)
			{
				addAtom(new Atom() {Name=selectedElement[0].name,
						Symbol=selectedElement[0].symbol,
						Number=selectedElement[0].number,
						Weight=selectedElement[0].weight,
						Shells=selectedElement[0].shells,
						Size = 30 +selectedElement[0].weight/2,
						X=e.GetPosition(FindParentOfName((FrameworkElement)e.Device.Target,"canvas")).X,
						Y=e.GetPosition(FindParentOfName((FrameworkElement)e.Device.Target,"canvas")).Y});

			} 
		}

		private void mouseDownAddAtom(MouseButtonEventArgs e)
		{
			if (isAddingAtom)
			{
				isAddingAtom = false;
				selectedElement[0].IsSelected = false;
			}
			else
			{
				isAddingAtom = true;
				selectedElement[0].IsSelected = true;
			}
		}

		private void elementSelected(MouseButtonEventArgs e)
		{
			try {
				var sve = (FrameworkElement)e.MouseDevice.Target;
				var label = FindParentOfType<Label>(sve);
				system[0].currentSelection[0].IsSelected = false;
				system[0].currentSelection[0] = system[0].elements[system[0].elementSymbols.IndexOf(label)];
				if (isAddingAtom) system[0].currentSelection[0].IsSelected = true;
				
				//selectedElement = system[0].currentSelection;
			}catch(Exception excep) { }
			
			//system[0].currentSelection[0] = system[0].elements[69];
		}

		private void addAtom(Atom atom)
		{

			undoRedoController.AddAndExecute(new AddAtomCommand(Atoms, atom));

		}

		private void addAtoms()
		{

		}

		private void removeAtom()
		{

		}

		private void addBinding()
		{

		}

		private void removeBinding()
		{

		}

		private void moveAtom()
		{

		}

		private void moveMolecule()
		{

		}

		private void undo()
		{
			if (undoRedoController.CanUndo())
			{
				undoRedoController.Undo();
			}
		}

		private void redo()
		{
			if (undoRedoController.CanRedo())
			{
				undoRedoController.Redo();
			}
		}

		private void saveDrawing()
		{
			String filepath = "save.txt";
            System.IO.StreamWriter file;
			String savefile = "";
			String n = "\r\n";

			//foreach (Atom a in Atoms){
			//	savefile += a.X + n + a.Y + n + a.Size + n + a.Number + n;
			//}
			for(int i = 0; i < Atoms.Count; i++)
			{
				savefile += Atoms[i].X + n + Atoms[i].Y + n + Atoms[i].Size + n + Atoms[i].Number + n;
			}

			try
			{
				file = new System.IO.StreamWriter(filepath);
				file.Write(savefile);
				file.Close();
			}
			catch
			{
				return;
			}

		}

		private void loadDrawing()
		{
			String filepath = "save.txt";
			System.IO.StreamReader file;
			String[] text;

			try
			{
				file = new System.IO.StreamReader(filepath);
				text = Regex.Split(file.ReadToEnd(), "\r\n");
				Atoms.Clear();
				undoRedoController.clear();
				Atom.resetCounter();

				for (int i = 0; i < text.Length; i += 4)
				{
					double x = double.Parse(text[0 + i]);
					double y = double.Parse(text[1 + i]);
					double s = double.Parse(text[2 + i]);
					int num = Int32.Parse(text[3 + i]);
					PElement e = system[0].findElement(num);

					addAtom(new Atom() { Size = s, X = x, Y = y, Name = e.name, Number = num, Symbol = e.symbol, Weight = e.weight, Shells = e.shells });

				}

			}
			catch
			{
				return;
			}

		}

		private void exportBitmap()
		{

		}

		private void addMolecule()
		{

		}

		private void newDrawing()
		{
			Atoms.Clear();
			undoRedoController.clear();
			Atom.resetCounter();
		}
	}
}