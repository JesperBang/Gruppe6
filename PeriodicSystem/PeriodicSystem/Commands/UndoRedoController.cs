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
            if(undoStack.Count > 0)
            {
                IUndoRedoCommand command = undoStack.Pop();
                redoStack.Push(command);
                command.unexecute();
            }
        }

        public void redo()
        {
            if (redoStack.Count > 0) {
                IUndoRedoCommand command = redoStack.Pop();
                undoStack.Push(command);
                command.execute();
            }
        }

        public void clearStacks()
        {
            undoStack.Clear();
            redoStack.Clear();
        }

        public bool canUndo()
        {
            return undoStack.Count > 0;
        }

        public bool canRedo()
        {
            return redoStack.Count > 0;
        }
    }
}
