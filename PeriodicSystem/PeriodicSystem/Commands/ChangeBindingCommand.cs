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
                    binding.Enbinding = 10;
                    binding.Tobinding = 0;
                    binding.Trebinding = 0;
                    break;

                case Binding.TypeOfBinding.Double:
                    binding.BindingState = Binding.TypeOfBinding.Triple;
                    binding.Enbinding = 0;
                    binding.Tobinding = 10;
                    binding.Trebinding = 30;
                    break;

                case Binding.TypeOfBinding.Triple:
                    binding.BindingState = Binding.TypeOfBinding.Single;
                    binding.Enbinding = 10;
                    binding.Tobinding = 30;
                    binding.Trebinding = 50;
                    break;
            }
        }

        public void unexecute()
        {
            switch (binding.BindingState)
            {
                case Binding.TypeOfBinding.Single:
                    binding.BindingState = Binding.TypeOfBinding.Triple;
                    binding.Enbinding = 0;
                    binding.Tobinding = 10;
                    binding.Trebinding = 30;
                    break;

                case Binding.TypeOfBinding.Double:
                    binding.BindingState = Binding.TypeOfBinding.Single;
                    binding.Enbinding = 10;
                    binding.Tobinding = 30;
                    binding.Trebinding = 50;
                    break;

                case Binding.TypeOfBinding.Triple:
                    binding.BindingState = Binding.TypeOfBinding.Double;
                    binding.Enbinding = 10;
                    binding.Tobinding = 0;
                    binding.Trebinding = 0;
                    break;
            }
        }
    }
}
