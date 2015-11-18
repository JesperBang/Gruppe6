using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicSystem.Commands
{
    class RemoveSelectionCommand : IUndoRedoCommand
    {
        private ObservableCollection<Atom> atoms;
        private ObservableCollection<Binding> bindings;
        private List<Atom> atomsToRemove;
        private List<Binding> bindingsToRemove;

        public RemoveSelectionCommand(List<Atom> atomsToRemove, List<Binding> bindingsToRemove, ObservableCollection<Atom> atoms, ObservableCollection<Binding> bindings)
        {
            this.atomsToRemove = atomsToRemove;
            this.atoms = atoms;
            this.bindings = bindings;

            List<Binding> additionalBindingsToRemove = new List<Binding>();
            foreach (Binding b in bindings)
            {
                if (atomsToRemove.Contains(b.BindingPoint1) || atomsToRemove.Contains(b.BindingPoint2))
                {
                    additionalBindingsToRemove.Add(b);
                }
            }
            
            foreach (Binding b in additionalBindingsToRemove)
            {
                if (!bindingsToRemove.Contains(b))
                {
                    bindingsToRemove.Add(b);
                }
            }

            this.bindingsToRemove = bindingsToRemove;
    }

        public void execute()
        {
            foreach (Binding b in bindingsToRemove)
            {
                bindings.Remove(b);
            }

            foreach (Atom a in atomsToRemove)
            {
                atoms.Remove(a);
            }
        }

        public void unexecute()
        {
            foreach (Atom a in atomsToRemove)
            {
                atoms.Add(a);
            }
            foreach (Binding b in bindingsToRemove)
            {
                bindings.Add(b);
            }
        }
    }
}
