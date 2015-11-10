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
        private List<Binding> bindingsToRemove;

        public RemoveAtomCommand(Atom atom, ObservableCollection<Atom> atoms, ObservableCollection<Binding> bindings)
        {
            this.atom = atom;
            this.atoms = atoms;

            this.bindings = bindings;

            bindingsToRemove = new List<Binding>();
            foreach(Binding b in bindings)
            {
                if(b.BindingPoint1 == atom || b.BindingPoint2 == atom)
                {
                    bindingsToRemove.Add(b);
                }
            }
        }

        public void execute()
        {
            atoms.Remove(atom);
            foreach (Binding b in bindingsToRemove)
            {
                bindings.Remove(b);
            }
        }

        public void unexecute()
        {
            atoms.Add(atom);
            foreach (Binding b in bindingsToRemove)
            {
                bindings.Add(b);
            }
        }
    }
}
