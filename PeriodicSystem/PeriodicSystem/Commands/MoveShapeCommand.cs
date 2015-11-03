using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PeriodicSystem.Commands
{
    class MoveShapeCommand
    {
        // Undo/Redo command for moving a Shape.
        public class MoveShapeCommand : IUndoRedoCommand
        {
            // Regions can be used to make code foldable (minus/plus sign to the left).
            #region Fields

            // The 'shape' field holds an existing shape, 
            //  and the reference points to the same object, 
            //  as one of the objects in the MainViewModels 'Shapes' ObservableCollection.
            // This shape is moved by changing its coordinates (X and Y), 
            //  and if undone the coordinates are changed back to the original coordinates.
            private Shape shape;

            // The 'offsetX' field holds the offset (difference) between the original and final X coordinate.
            private double offsetX;
            // The 'offsetY' field holds the offset (difference) between the original and final Y coordinate.
            private double offsetY;

            #endregion

            #region Constructor

            // For changing the current state of the diagram.
            public MoveShapeCommand(Shape _shape, double _offsetX, double _offsetY)
            {
                shape = _shape;
                offsetX = _offsetX;
                offsetY = _offsetY;
            }

            #endregion

            #region Methods

            // For doing and redoing the command.
            public void Execute()
            {
                shape.CanvasCenterX += offsetX;
                shape.CanvasCenterY += offsetY;
            }

            // For undoing the command.
            public void UnExecute()
            {
                shape.CanvasCenterX -= offsetX;
                shape.CanvasCenterY -= offsetY;
            }

            #endregion
        }
    }
}
