using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model;
using PeriodicSystem.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace PeriodicSystem.ViewModel
{
    class ViewModel : ViewModelBase
    {
        public ObservableCollection<Atom> Atoms{ get; set; }
        public ObservableCollection<Binding> Bindings { get; set; }

        private Point initialMousePosition;
        private Point initialAtomPosition;

        private bool isAddingBindings;
        private Atom bindingFrom;
        private Atom bindingTo;

        private Object selectedModel;


        public ICommand LoadFromXMLCommand { get; }
        public ICommand SaveToXMLCommand { get; }
        public ICommand AddAtomCommand { get; }
        public ICommand AddAtomsCommand{ get; }
        public ICommand RemoveAtomCommand { get; }
        public ICommand AddBindingCommand { get; }
        public ICommand RemoveBindingCommand { get; }
        public ICommand MoveAtomCommand { get; }
        public ICommand MoveMoleculeCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand SaveDrawingCommand { get; }
        public ICommand LoadDrawingCommand { get; }
        public ICommand ExportBitmapCommand { get; }
        public ICommand AddMoleculeCommand { get; }
        public ICommand NewDrawingCommand { get; }

        public ICommand MouseDownAtomCommand { get; }
        public ICommand MouseMoveAtomCommand { get; }
        public ICommand MouseUpAtomCommand { get; }
        
        public ICommand MouseDownBindingCommand {get;}

        private UndoRedoController undoRedoController;
        

        public ViewModel()
        {
            Atoms = new ObservableCollection<Atom>();
            Bindings = new ObservableCollection<Binding>();

            undoRedoController = new UndoRedoController();
            
            LoadFromXMLCommand = new RelayCommand(loadFromXML);
            SaveToXMLCommand = new RelayCommand(saveToXML); 
            AddAtomCommand = new RelayCommand<int>(addAtom);
            AddAtomsCommand = new RelayCommand(addAtoms);
            RemoveAtomCommand = new RelayCommand<Atom>(removeAtom);
            AddBindingCommand = new RelayCommand(addBinding);
            RemoveBindingCommand = new RelayCommand(removeBinding);
            MoveAtomCommand = new RelayCommand(moveAtom);
            MoveMoleculeCommand = new RelayCommand(moveMolecule);
            UndoCommand = new RelayCommand(undo);
            RedoCommand = new RelayCommand(redo);
            SaveDrawingCommand = new RelayCommand(saveDrawing);
            LoadDrawingCommand = new RelayCommand(loadDrawing);
            ExportBitmapCommand = new RelayCommand(exportBitmap);
            AddMoleculeCommand = new RelayCommand(addMolecule);
            NewDrawingCommand = new RelayCommand(newDrawing);

            MouseDownAtomCommand = new RelayCommand<MouseEventArgs>(mouseDownAtom);
            MouseMoveAtomCommand = new RelayCommand<MouseEventArgs>(MouseMoveAtom);
            MouseUpAtomCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpAtom);

            MouseDownBindingCommand = new RelayCommand<MouseEventArgs>(mouseDownBinding);

            //test
        }
        
        private Atom TargetAtom(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // From the shapes visual element, the Shape object which is the DataContext is retrieved.
            return (Atom)shapeVisualElement.DataContext;

        }

        private Point RelativeMousePosition(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // The canvas holding the shapes visual element, is found by searching up the tree of visual elements.
            var canvas = FindParentOfType<Canvas>(shapeVisualElement);
            // The mouse position relative to the canvas is gotten here.
            return Mouse.GetPosition(canvas);
        }

        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }

        private void mouseDownAtom(MouseEventArgs e)
        {
            if (!isAddingBindings) { 
                //moving atom
                var atom = TargetAtom(e);
                var mousePosition = RelativeMousePosition(e);

                initialMousePosition = mousePosition;
                initialAtomPosition = new Point(atom.X, atom.Y);

                e.MouseDevice.Target.CaptureMouse();
            }

        }

        private void MouseMoveAtom(MouseEventArgs e)
        {
            if (Mouse.Captured != null && !isAddingBindings)
            {
                Console.WriteLine("rykker");

                var shape = TargetAtom(e);
                var mousePosition = RelativeMousePosition(e);
                
                shape.X = initialAtomPosition.X + (mousePosition.X - initialMousePosition.X);
                shape.Y = initialAtomPosition.Y + (mousePosition.Y - initialMousePosition.Y);
            }
        }

        private void MouseUpAtom(MouseButtonEventArgs e)
        {
            if (isAddingBindings)
            {
                var atom = TargetAtom(e);

                if (bindingFrom == null)
                {
                    bindingFrom = atom;
                }
                else
                {
                    bindingTo = atom;

                    if (bindingTo != bindingFrom)
                    {
                        undoRedoController.addAndExecute(new AddBindingCommand(Bindings, new Binding(bindingFrom, atom)));
                        bindingFrom = null;
                        bindingTo = null;
                        isAddingBindings = false;
                    }
                }
            }
            else {
                //moving atom
                var atom = TargetAtom(e);
                var mousePosition = RelativeMousePosition(e);

                atom.X = initialAtomPosition.X;
                atom.Y = initialAtomPosition.Y;

                undoRedoController.addAndExecute(new MoveAtomCommand(atom, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                e.MouseDevice.Target.ReleaseMouseCapture();

            }
        }

        private Binding TargetBinding(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // From the shapes visual element, the Shape object which is the DataContext is retrieved.
            return (Binding)shapeVisualElement.DataContext;
        }

        private void mouseDownBinding(MouseEventArgs e)
        {
            Binding binding = TargetBinding(e);

            selectedModel = binding;
        }

        private void addAtom(int protons)
        {
            undoRedoController.addAndExecute(new AddAtomCommand(Atoms, new Atom(protons)));
        }

        private void addAtoms()
        {
            Console.WriteLine("test");
        }

        private void removeAtom(Atom atom)
        {
            undoRedoController.addAndExecute(new RemoveAtomCommand(atom, Atoms, Bindings));
        }

        private void saveToXML()
        {
            try
            {
                var xEle = new XElement("Atoms",
                            from Atom in Atoms
                            select new XElement("Atom",
                                         new XElement("X", Atom.X),
                                           new XElement("Y", Atom.Y),
                                           new XElement("Protons", Atom.Protons)
                                       ));
                xEle.Save("C:\\Test\\Atoms.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Save To XML");
        }

        private void loadFromXML()
        {
            Console.WriteLine("Loading Atoms from XML");

            XDocument xmlDoc = XDocument.Load("C:\\Test\\Atoms.xml");
            Atoms = new ObservableCollection<Atom>(xmlDoc.Descendants("Atom")
                                       .Select(x => new Atom
                                       {
                                           Protons = Int32.Parse(x.Element("Protons").Value),
                                           X = Double.Parse(x.Element("X").Value),
                                           Y = Double.Parse(x.Element("Y").Value)
                                       })
                                       .ToList());
        }

        private void addBinding()
        {
            isAddingBindings = true;
            
        }

        private void removeBinding()
        {
            if (selectedModel != null) {
                undoRedoController.addAndExecute(new RemoveBindingCommand((Binding)selectedModel));
            }
        }

        private void moveAtom()
        {

        }

        private void moveMolecule()
        {

        }
        
        private void undo()
        {
            undoRedoController.undo();
        }

        private void redo()
        {
            undoRedoController.redo();
        }

        private void saveDrawing()
        {

        }

        private void loadDrawing()
        {

        }

        private void exportBitmap()
        {

        }

        private void addMolecule()
        {

        }

        private void newDrawing()
        {

        }
    }
}
