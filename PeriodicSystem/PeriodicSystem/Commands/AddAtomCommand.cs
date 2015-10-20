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
    class AddAtomCommand : IUndoRedoCommand
    {
        private ObservableCollection<Atom> atoms;
        private Atom atom;

        public AddAtomCommand(ObservableCollection<Atom> atoms, Atom atom)
        {
            this.atoms = atoms;
            this.atom = atom;
        }

        public void execute()
        {
            atoms.Add(atom);
        }

        public void unexecute()
        {
            atoms.Remove(atom);
        }
    }
}
