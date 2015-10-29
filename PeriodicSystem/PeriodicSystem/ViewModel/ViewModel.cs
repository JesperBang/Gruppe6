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
using System.Windows.Input;

namespace PeriodicSystem.ViewModel
{
    class ViewModel : ViewModelBase
    {
        public ObservableCollection<Atom> Atoms{ get; set; }
        public ObservableCollection<Binding> Bindings { get; set; }
        
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
        
        private UndoRedoController undoRedoController;

        public ViewModel()
        {
            Atoms = new ObservableCollection<Atom>();
            Bindings = new ObservableCollection<Binding>();

            undoRedoController = new UndoRedoController();
            
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

            //test
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
