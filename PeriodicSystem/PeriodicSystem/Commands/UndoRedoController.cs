using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicSystem.Commands
{
    public class UndoRedoController
    {
		public static UndoRedoController Instance { get; } = new UndoRedoController();
		// Keeps track of the Undo/Redo commands.
		// This is a Singleton, which ensures there will only ever be one instance of the class.
		// There should never be more than one, otherwise problems could arise.

		// Regions can be used to make code foldable (minus/plus sign to the left).
		#region Fields

		// The Undo stack, holding the Undo/Redo commands that have been executed.
		private readonly Stack<IUndoRedoCommand> undoStack = new Stack<IUndoRedoCommand>();
            // The Redo stack, holding the Undo/Redo commands that have been executed and then unexecuted (undone).
            private readonly Stack<IUndoRedoCommand> redoStack = new Stack<IUndoRedoCommand>();

            #endregion

            #region Properties

            // Part of the Singleton design pattern.
            // This holds the only instance of the class that will ever exist.
            // See (http://en.wikipedia.org/wiki/Singleton_pattern) for a description of the Singleton design pattern, 
            //  look under eager initialization for this version of the design pattern.
            // This is used by other objects to retrieve a reference to the single 'UndoRedoController' object.
            // Java:
            //  private static UndoRedoController instance = new UndoRedoController();
            //
            //  public static UndoRedoController GetInstance()
            //  {
            //    return instance;
            //  }

            #endregion

            #region Constructor

            // Part of the Singleton design pattern.
            // This ensures that only the 'UndoRedoController' can instantiate itself.
            private UndoRedoController() { }

            #endregion

            #region Methods

            // Used for adding the Undo/Redo command to the 'undoStack' and at the same time executing it.
            public void AddAndExecute(IUndoRedoCommand command)
            {
                undoStack.Push(command);
                redoStack.Clear();
                command.Execute();
            }

            // This informs the View (GUI) when the Undo command can be used.
            // Lambda expression to check that the 'undoStack' collection is not empty.
            // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
            // Java:
            //  public bool CanUndo()
            //  {
            //    return undoStack.Count > 0;
            //  }
            public bool CanUndo() => undoStack.Any();

            // Undoes the Undo/Redo command that was last executed, if possible.
            public void Undo()
            {
                if (!undoStack.Any()) throw new InvalidOperationException();
                // This uses 'var' which is an implicit type variable (https://msdn.microsoft.com/en-us/library/bb383973.aspx).
                var command = undoStack.Pop();
                redoStack.Push(command);
                command.UnExecute();
            }

            // This informs the View (GUI) when the Redo command can be used.
            // Lambda expression to check that the 'redoStack' collection is not empty.
            // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
            // Java:
            //  public bool CanUndo()
            //  {
            //    return redoStack.Count > 0;
            //  }
            public bool CanRedo() => redoStack.Any();

            // Redoes the Undo/Redo command that was last unexecuted (undone), if possible.
            public void Redo()
            {
                if (!redoStack.Any()) throw new InvalidOperationException();
                // This uses 'var' which is an implicit type variable (https://msdn.microsoft.com/en-us/library/bb383973.aspx).
                var command = redoStack.Pop();
                undoStack.Push(command);
                command.Execute();
            }

            #endregion
        }
    
}
