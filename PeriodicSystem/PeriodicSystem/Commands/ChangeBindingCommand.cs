using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicSystem.Commands
{
    class ChangeBindingCommand : IUndoRedoCommand
    {
        Binding binding;

        public ChangeBindingCommand(Binding binding)
        {
            this.binding = binding;
        }

        public void execute()
        {
            switch (binding.BindingState)
            {
                case Binding.TypeOfBinding.Single:
                    binding.BindingState = Binding.TypeOfBinding.Double;
                    break;

                case Binding.TypeOfBinding.Double:
                    binding.BindingState = Binding.TypeOfBinding.Triple;
                    break;

                case Binding.TypeOfBinding.Triple:
                    binding.BindingState = Binding.TypeOfBinding.Single;
                    break;
            }
        }

        public void unexecute()
        {
            switch (binding.BindingState)
            {
                case Binding.TypeOfBinding.Single:
                    binding.BindingState = Binding.TypeOfBinding.Triple;
                    break;

                case Binding.TypeOfBinding.Double:
                    binding.BindingState = Binding.TypeOfBinding.Single;
                    break;

                case Binding.TypeOfBinding.Triple:
                    binding.BindingState = Binding.TypeOfBinding.Double;
                    break;
            }
        }
    }
}
