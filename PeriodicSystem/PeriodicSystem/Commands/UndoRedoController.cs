using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicSystem.Commands
{
    class UndoRedoController
    {
        private Stack<IUndoRedoCommand> undoStack;
        private Stack<IUndoRedoCommand> redoStack;

        public UndoRedoController()
        {
            undoStack = new Stack<IUndoRedoCommand>();
            redoStack = new Stack<IUndoRedoCommand>();
        }

        public void addAndExecute(IUndoRedoCommand command)
        {
            undoStack.Push(command);
            command.execute();
        }

        public void undo()
        {

        }

        public void redo()
        {

        }
    }
}
