﻿using GalaSoft.MvvmLight.CommandWpf;
using Model;
using PeriodicSystem.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PeriodicSystem.ViewModel
{
    class ViewModel
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
        

        public ViewModel()
        {
            Atoms = new ObservableCollection<Atom>();
            Bindings = new ObservableCollection<Binding>();


            Atoms.Add(new Atom());
            Atoms.Add(new Atom());
            Atoms.Add(new Atom());

            addAtomCommand = new RelayCommand(addAtom);
            addAtomsCommand = new RelayCommand(addAtom);
            removeAtomCommand = new RelayCommand(addAtom);
            addBindingCommand = new RelayCommand(addAtom);
            removeBindingCommand = new RelayCommand(addAtom);
            moveAtomCommand = new RelayCommand(addAtom);
            moveMoleculeCommand = new RelayCommand(addAtom);

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