using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.ObjectModel;

namespace PeriodicSystem.Commands
{
	public class MoveSelectedAtomsCommand : IUndoRedoCommand
	{
		#region Fields
		
		private ObservableCollection<Atom> atoms;

		// The 'offsetX' field holds the offset (difference) between the original and final X coordinate.
		private double offsetX;
		// The 'offsetY' field holds the offset (difference) between the original and final Y coordinate.
		private double offsetY;

		#endregion

		#region Constructor

		// For changing the current state of the diagram.
		public MoveSelectedAtomsCommand(ObservableCollection<Atom> _atoms, double _offsetX, double _offsetY)
		{
			atoms = _atoms;
			offsetX = _offsetX;
			offsetY = _offsetY;
		}

		#endregion

		#region Methods

		// For doing and redoing the command.
		public void Execute()
		{
			foreach(Atom a in atoms)
			{

				a.X += offsetX;
				a.Y += offsetY;

			}
		}

		// For undoing the command.
		public void UnExecute()
		{

			foreach (Atom a in atoms)
			{

				a.X -= offsetX;
				a.Y -= offsetY;

			}

		}

			#endregion
	}
	
}
