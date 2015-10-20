using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicSystem.Commands
{
    class UndoRedoCommand
    {
    // Custom interface for implementing Undo/Redo commands.
    public interface UndoRedoCommand
        {
            // Regions can be used to make code foldable (minus/plus sign to the left).
            #region Methods (that has to be implemented)

            // For doing and redoing the command.
            void Execute();
            // For undoing the command.
            void UnExecute();

            #endregion
        }
    }
}
