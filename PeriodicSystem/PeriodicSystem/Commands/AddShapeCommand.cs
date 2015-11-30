using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PeriodicSystem.Commands
{
		// Undo/Redo command for adding a Shape.
		public class AddShapeCommand : IUndoRedoCommand
		{
			// Regions can be used to make code foldable (minus/plus sign to the left).
			#region Fields

			// The 'shapes' field holds the current collection of shapes, 
			//  and the reference points to the same collection as the one the MainViewModel point to, 
			//  therefore when this collection is changed in a object of this class, 
			//  it also changes the collection that the MainViewModel uses.
			// For a description of an ObservableCollection see the MainViewModel class.
			private ObservableCollection<Shape> shapes;
			// The 'shape' field holds a new shape, that is added to the 'shapes' collection, 
			//  and if undone, it is removed from the collection.
			private Shape shape;

			#endregion

			#region Constructor

			// For changing the current state of the diagram.
			public AddShapeCommand(ObservableCollection<Shape> _shapes, Shape _shape)
			{
				shapes = _shapes;
				shape = _shape;
			}

			#endregion

			#region Methods

			// For doing and redoing the command.
			public void Execute()
			{
				shapes.Add(shape);
			}

			// For undoing the command.
			public void UnExecute()
			{
				shapes.Remove(shape);
			}

			#endregion
		}
	
}
