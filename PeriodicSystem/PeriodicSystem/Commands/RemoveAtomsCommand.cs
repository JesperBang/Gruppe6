using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace PeriodicSystem.Commands
{
	public class RemoveAtomsCommand : IUndoRedoCommand
	{
		#region Fields
		private ObservableCollection<Atom> atoms;
		private List<Atom> atomsToRemove;

		#endregion

		#region Constructor

		// For the current state of the diagram.
		public RemoveAtomsCommand(ObservableCollection<Atom> _atoms,List<Atom> _atomsToRemove)
		{
			atoms = _atoms;
			//lines = _lines;
			atomsToRemove = _atomsToRemove;

			foreach(Atom a in atoms)
			{
				a.IsSelected = false;
			}
		}

		#endregion

		#region Methods

		// For doing and redoing the command.
		public void Execute()
		{
			atomsToRemove.ForEach(x => atoms.Remove(x));
		}

		// For undoing the command.
		public void UnExecute()
		{
			atomsToRemove.ForEach(x => atoms.Add(x));
		}

		#endregion
	}

}
