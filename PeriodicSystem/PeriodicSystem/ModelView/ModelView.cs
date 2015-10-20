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

namespace PeriodicSystem.ModelView
{

        class ViewModel
        {
        // A reference to the Undo/Redo controller.
        private UndoRedoController undoRedoController = UndoRedoController.Instance;

        // Keeps track of the state, depending on whether a line is being added or not.
        private bool isAddingLine;
        // Used for saving the shape that a line is drawn from, while it is being drawn.
        private Shape addingLineFrom;
        // Saves the initial point that the mouse has during a move operation.
        private Point initialMousePosition;
        // Saves the initial point that the shape has during a move operation.
        private Point initialShapePosition;
        // Used for making the shapes transparent when a new line is being added.
        // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
        public double ModeOpacity => isAddingLine ? 0.4 : 1.0;

        public ObservableCollection<Atom> Atoms { get; set; }
            public ObservableCollection<Binding> Bindings { get; set; }

            public ICommand addAtomCommand { get; }
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

            public ViewModel()
            {
                Atoms = new ObservableCollection<Atom>();
                Bindings = new ObservableCollection<Binding>();


                Atoms.Add(new Atom());
            }

            private void addAtom()
            {

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

            }

            private void redo()
            {

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