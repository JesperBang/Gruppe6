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
    class AddBindingCommand : IUndoRedoCommand
    {
        ObservableCollection<Binding> bindings;
        Binding binding;

        public AddBindingCommand(ObservableCollection<Binding> bindings, Binding binding)
        {
            this.bindings = bindings;
            this.binding = binding;
        }

        public void execute()
        {
            bindings.Add(binding);
        }

        public void unexecute()
        {
            bindings.Remove(binding);
        }
    }
}
