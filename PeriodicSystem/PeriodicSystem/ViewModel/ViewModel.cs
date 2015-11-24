using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model;
using PeriodicSystem.Commands;
using PeriodicSystem.Serialize;
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
using System.Xml.Serialization;

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

        private bool clickHandled = false;

        private List<Atom> selectedAtoms;
        private List<Binding> selectedBindings;

        public ICommand ClearBindingStateCommand { get; }
        public ICommand ClearSelectionCommand { get; }
        public ICommand SelectAllCommand { get; }
        public ICommand RemoveModelCommand { get; }
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
        public ICommand ChangeBindingCommand { get; }

        private UndoRedoController undoRedoController;
        

        public ViewModel()
        {
            Atoms = new ObservableCollection<Atom>();
            Bindings = new ObservableCollection<Binding>();

            undoRedoController = new UndoRedoController();

            ClearBindingStateCommand = new RelayCommand(clearBindingState, () => isAddingBindings);
            ClearSelectionCommand = new RelayCommand(clearSelections);
            SelectAllCommand = new RelayCommand(selectAll);
            RemoveModelCommand = new RelayCommand(removeModel, canRemoveModel);
            LoadFromXMLCommand = new RelayCommand(loadFromXML);
            SaveToXMLCommand = new RelayCommand(saveToXML); 
            AddAtomCommand = new RelayCommand<int>(addAtom);
            AddAtomsCommand = new RelayCommand(addAtoms);
            AddBindingCommand = new RelayCommand(addBinding);
            MoveMoleculeCommand = new RelayCommand(moveMolecule);
            UndoCommand = new RelayCommand(undo, undoRedoController.canUndo);
            RedoCommand = new RelayCommand(redo, undoRedoController.canRedo);
            ExportBitmapCommand = new RelayCommand(exportBitmap);
            AddMoleculeCommand = new RelayCommand(addMolecule);
            NewDrawingCommand = new RelayCommand(newDrawing);


            MouseDownAtomCommand = new RelayCommand<MouseEventArgs>(mouseDownAtom);
            MouseMoveAtomCommand = new RelayCommand<MouseEventArgs>(MouseMoveAtom);
            MouseUpAtomCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpAtom);

            MouseDownBindingCommand = new RelayCommand<MouseEventArgs>(mouseDownBinding);
            ChangeBindingCommand = new RelayCommand<int>(changeBinding);

            initStateVariables();
        }

        private void initStateVariables()
        {
            clickHandled = false;

            clearBindingState();
            clearSelections();

            Atom.resetIds();
            Binding.resetIds();
        }

        private void clearBindingState()
        {
            isAddingBindings = false;
            bindingFrom = null;
            bindingTo = null;
        }

        private void clearSelections()
        {
            //workaround for multiple leftclick bindings triggering
            if (clickHandled)
            {
                clickHandled = false;
                return;
            }

            if (selectedAtoms != null) {
                foreach (Atom a in selectedAtoms)
                {
                    a.IsSelected = false;
                }
            }

            if (selectedBindings != null) {
                foreach (Binding b in selectedBindings)
                {
                    b.IsSelected = false;
                }
            }

            selectedAtoms = new List<Atom>();
            selectedBindings = new List<Binding>();
        }

        public void selectAll()
        {
            clearSelections();

            foreach (Atom a in Atoms)
            {
                selectedAtoms.Add(a);
                a.IsSelected = true;
            }

            foreach (Binding b in Bindings)
            {
                selectedBindings.Add(b);
                b.IsSelected = true;
            }
        }

        public void changeBinding(int id)
        {
            undoRedoController.addAndExecute(new ChangeBindingCommand(Bindings.Where(x => x.Id == id).First()));
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

                //clear selection unless ctrl is pressed
                if (!(Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl)))
                {
                    clearSelections();
                }
                clickHandled = true;
                selectedAtoms.Add(atom);
                atom.IsSelected = true;


                e.MouseDevice.Target.CaptureMouse();
            }

        }

        private void MouseMoveAtom(MouseEventArgs e)
        {
            if (Mouse.Captured != null && !isAddingBindings)
            {
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

                //only move if change is significant
                if (atom.X != initialAtomPosition.X && atom.Y != initialAtomPosition.Y) {

                    atom.X = initialAtomPosition.X;
                    atom.Y = initialAtomPosition.Y;

                    undoRedoController.addAndExecute(new MoveAtomCommand(atom, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                }
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

            //clear selection unless ctrl is pressed
            if (!(Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl)))
            {
                clearSelections();
            }
            clickHandled = true;
            selectedBindings.Add(binding);
            binding.IsSelected = true;
        }

        private void addAtom(int protons)
        {
            undoRedoController.addAndExecute(new AddAtomCommand(Atoms, new Atom(protons)));
        }

        private void addAtoms()
        {
            
        }

        public bool canRemoveModel()
        {
            return (selectedAtoms.Count + selectedBindings.Count > 0);
        }

        private void removeModel()
        {
            undoRedoController.addAndExecute(new RemoveSelectionCommand(selectedAtoms, selectedBindings, Atoms, Bindings));
            clearSelections();
        }

        private void saveToXML()
        {
            SerializeXML serializer = SerializeXML.Instance;
            Diagram diagram = new Diagram();
            diagram.Atoms = Atoms.ToList();
            diagram.Bindings = Bindings.ToList();

            System.Windows.Forms.SaveFileDialog saveXMLDialog = new System.Windows.Forms.SaveFileDialog();
            saveXMLDialog.Filter = "XML-File | *.xml";
            saveXMLDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            if (saveXMLDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                serializer.save(diagram, saveXMLDialog.FileName);
            }
        }

        private void loadFromXML()
        {
            SerializeXML serializer = SerializeXML.Instance;

            //serializer.load(path);
            /*
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
                                       */
        }

        private void addBinding()
        {
            clearSelections();
            isAddingBindings = true;
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

        private void exportBitmap()
        {

        }

        private void addMolecule()
        {

        }

        private void newDrawing()
        {
            MessageBoxResult result = (Atoms.Count == 0) ? MessageBoxResult.No : MessageBox.Show("Would you like to save changes before proceeding?", "New Drawing", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    //TODO add file saved confirmation
                    saveToXML();
                    break;
                case MessageBoxResult.Cancel:
                    return;
            }

            Atoms.Clear();
            Bindings.Clear();
            undoRedoController.clearStacks();
            initStateVariables();
        }
    }
}
