using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Model;

namespace PeriodicSystem.Commands
{
		// Undo/Redo command for adding a Shape.
		public class AddAtomCommand : IUndoRedoCommand
		{
			#region Fields
			private ObservableCollection<Atom> atoms;
			private Atom atom;

			#endregion

			#region Constructor
			public AddAtomCommand(ObservableCollection<Atom> _atoms, Atom _atom)
			{
				atoms = _atoms;
				atom = _atom;
			}

			#endregion

			#region Methods

			// For doing and redoing the command.
			public void Execute()
			{
				atoms.Add(atom);
			}

			// For undoing the command.
			public void UnExecute()
			{
				atoms.Remove(atom);
			}

			#endregion
		}
	
}
