using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PeriodicSystem.Commands
{
    class RemoveBindingCommand : IUndoRedoCommand
    {
        private Binding binding;

        public RemoveBindingCommand(Binding binding)
        {
            this.binding = binding;
        }

        public void execute()
        {
            throw new NotImplementedException();
        }

        public void unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
