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
        
        public ICommand addAtomCommand { get; }
        public ICommand addAtomsCommand{ get; }
        public ICommand removeAtomCommand { get; }
        public ICommand addBindingCommand { get; }
        public ICommand removeBindingCommand { get; }
        public ICommand moveAtomCommand { get; }
        public ICommand moveMoleculeCommand { get; }
        public ICommand undoCommand { get; }
        public ICommand redoCommand { get; }
        public ICommand saveDrawingCommand { get; }
        public ICommand loadDrawingCommand { get; }
        public ICommand exportBitmapCommand { get; }
        public ICommand addMoleculeCommand { get; }
        public ICommand newDrawingCommand { get; }
        
        private UndoRedoController undoRedoController;

        public ViewModel()
        {
            Atoms = new ObservableCollection<Atom>();
            Bindings = new ObservableCollection<Binding>();

            undoRedoController = new UndoRedoController();
            
            addAtomCommand = new RelayCommand(addAtom);
            addAtomsCommand = new RelayCommand(addAtoms);
            removeAtomCommand = new RelayCommand(removeAtom);
            addBindingCommand = new RelayCommand(addBinding);
            removeBindingCommand = new RelayCommand(removeBinding);
            moveAtomCommand = new RelayCommand(moveAtom);
            moveMoleculeCommand = new RelayCommand(moveMolecule);
            undoCommand = new RelayCommand(undo);
            redoCommand = new RelayCommand(redo);
            saveDrawingCommand = new RelayCommand(saveDrawing);
            loadDrawingCommand = new RelayCommand(loadDrawing);
            exportBitmapCommand = new RelayCommand(exportBitmap);
            addMoleculeCommand = new RelayCommand(addMolecule);
            newDrawingCommand = new RelayCommand(newDrawing);
        }

        private void addAtom()
        {
            undoRedoController.addAndExecute(new AddAtomCommand(Atoms, new Atom()));
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
