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
    class RemoveBindingCommand : IUndoRedoCommand
    {
        private Binding binding;
        private ObservableCollection<Binding> bindings;

        public RemoveBindingCommand(Binding binding, ObservableCollection<Binding> bindings)
        {
            this.binding = binding;
            this.bindings = bindings;
        }

        public void execute()
        {
            bindings.Remove(binding);
        }

        public void unexecute()
        {
            bindings.Add(binding);
        }
    }
}
