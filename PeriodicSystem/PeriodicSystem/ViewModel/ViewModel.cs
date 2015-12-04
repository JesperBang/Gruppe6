using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PeriodicSystem.Commands;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
		private ObservableCollection<Point> initialAtomPositions = new ObservableCollection<Point>();
		// Used for making the shapes transparent when a new line is being added.
		// This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
		public double ModeOpacity => isAddingLine ? 0.4 : 1.0;

		public ObservableCollection<PSystem> system { get; set; } = new ObservableCollection<PSystem>();
		public ObservableCollection<PElement> selectedElement { get; set; }
		public ObservableCollection<Grid> elementGrid { get; set; }


		public ObservableCollection<Atom> Atoms { get; set; }
		public ObservableCollection<Atom> selectedAtoms { get; set; } = new ObservableCollection<Atom>();
		public ObservableCollection<Binding> Bindings { get; set; }

		public ICommand selectAllAtomsCommand { get; }
		public ICommand mouseUpCanvasCommand { get; }
		public ICommand moveSelectedAtomsCommand { get; }
		public ICommand mouseUpAtomCommand { get; }
		public ICommand addAtomCommand { get; }
		public ICommand mouseDownAddAtomCommand { get; }
		public ICommand mouseDownAtomCommand { get; }
		public ICommand addAtomsCommand { get; }
		public ICommand removeAtomsCommand { get; }
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
			mouseDownAtomCommand = new RelayCommand<MouseButtonEventArgs>(mouseDownAtom);
			moveSelectedAtomsCommand = new RelayCommand<MouseEventArgs>(moveSelectedAtoms);
			mouseUpAtomCommand = new RelayCommand<MouseButtonEventArgs>(mouseUpAtom);
			saveDrawingCommand = new RelayCommand(saveDrawing);
			loadDrawingCommand = new RelayCommand(loadDrawing);
			newDrawingCommand = new RelayCommand(newDrawing);
			removeAtomsCommand = new RelayCommand(removeAtoms);
			selectAllAtomsCommand = new RelayCommand(selectAllAtoms);

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

		private Atom TargetAtom(MouseEventArgs e)
		{
			var sve = (FrameworkElement)e.Device.Target;
			return (Atom)sve.DataContext;
		}

		private Point RelativeMousePosition(MouseEventArgs e)
		{
			var sve = (FrameworkElement) e.MouseDevice.Target;
			var canvas = (Canvas) FindParentOfName(sve, "canvas");
			return Mouse.GetPosition(canvas);
		}

		private static FrameworkElement FindParentOfName(FrameworkElement o, String name)
		{
			try
			{
				if (o.Name.Equals(name)) return o;

				dynamic parent = VisualTreeHelper.GetParent(o);

				return parent.name!=null && parent.name.equals(name) ? parent : FindParentOfName(parent,name);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		private void selectAllAtoms()
		{
			foreach(Atom a in Atoms)
			{
				selectedAtoms.Add(a);
				a.IsSelected = true;
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
						Y=e.GetPosition(FindParentOfName((FrameworkElement)e.Device.Target,"canvas")).Y,
						HitTestVisible=!isAddingAtom});

			} 
		}

		private void mouseDownAddAtom(MouseButtonEventArgs e)
		{
			if (isAddingAtom)
			{
				isAddingAtom = false;
				selectedElement[0].IsSelected = false;
				foreach (Atom a in Atoms)
					a.HitTestVisible = true;
			}
			else
			{
				isAddingAtom = true;
				selectedElement[0].IsSelected = true;
				foreach (Atom a in Atoms)
					a.HitTestVisible = false;
			}
		}

		private void mouseDownAtom(MouseButtonEventArgs e)
			{
			if (selectedAtoms.Count > 0 && Keyboard.IsKeyDown(Key.LeftShift))
			{
				var targetAtom = TargetAtom(e);
				if (targetAtom.IsSelected)
				{
					var mousePos = RelativeMousePosition(e);
					initialMousePosition = mousePos;
					foreach (Atom a in selectedAtoms)
					{
						initialAtomPositions.Add(new Point(a.X, a.Y));
					}
					e.MouseDevice.Target.CaptureMouse();
				}
			}
			else
			{
				if (selectedAtoms.Count > 0 && !Keyboard.IsKeyDown(Key.LeftCtrl))
				{
					foreach (Atom a in selectedAtoms)
					{
						a.IsSelected = false;
					}
					selectedAtoms.Clear();
				}

				var sve = (FrameworkElement)e.Device.Target;
				var atom = (Atom)sve.DataContext;

				if (atom.IsSelected)
				{
					atom.IsSelected = false; //atom.IsSelected = atom.IsSelected? false :true; <-- could be used for a toggle.
					selectedAtoms.Remove(atom);
				}
				else
				{
					atom.IsSelected = true;
					selectedAtoms.Add(atom);
				}

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

		private void moveSelectedAtoms(MouseEventArgs e)
		{

			if(Mouse.Captured != null)
			{
				var targetAtom = TargetAtom(e);
				var mousePos = RelativeMousePosition(e);

				foreach(Atom a in selectedAtoms)
				{
					a.X = initialAtomPositions[selectedAtoms.IndexOf(a)].X + (mousePos.X - initialMousePosition.X);
					a.Y = initialAtomPositions[selectedAtoms.IndexOf(a)].Y + (mousePos.Y - initialMousePosition.Y);
				}
			}

		}

		private void mouseUpAtom(MouseButtonEventArgs e)
		{
			if(Mouse.Captured != null)
			{
				var targetAtom = TargetAtom(e);
				var mousePos = RelativeMousePosition(e);

				foreach(Atom a in selectedAtoms)
				{
					a.X = initialAtomPositions[selectedAtoms.IndexOf(a)].X;
					a.Y = initialAtomPositions[selectedAtoms.IndexOf(a)].Y;
				}
				initialAtomPositions.Clear();
				undoRedoController.AddAndExecute(new MoveSelectedAtomsCommand(selectedAtoms, (mousePos.X - initialMousePosition.X), (mousePos.Y - initialMousePosition.Y)));
				e.MouseDevice.Target.ReleaseMouseCapture();

			}
		}

		private void addAtoms()
		{

		}

		private void removeAtoms()
		{
			List<Atom> tempAtoms = new List<Atom>();
			foreach (Atom a in selectedAtoms)
			{
				tempAtoms.Add(a);
			}
			foreach (Atom a in tempAtoms)
			{
				a.IsSelected = false;
				selectedAtoms.Remove(a);
			}
			undoRedoController.AddAndExecute(new RemoveAtomsCommand(Atoms, tempAtoms));
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