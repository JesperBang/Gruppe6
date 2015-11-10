using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PeriodicSystem.Commands
{
    class RemoveAtomCommand : IUndoRedoCommand
    {
        private ObservableCollection<Atom> atoms;
        private ObservableCollection<Binding> bindings;
        private Atom atom;

        public RemoveAtomCommand(Atom atom, ObservableCollection<Atom> atoms, ObservableCollection<Binding> bindings)
        {
            this.atom = atom;
            this.atoms = atoms;
            this.bindings = bindings;
        }

        public void execute()
        {
            atoms.Remove(atom);
        }

        public void unexecute()
        {
            atoms.Add(atom);
        }
    }
}
