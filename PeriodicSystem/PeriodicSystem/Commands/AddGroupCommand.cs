using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PeriodicSystem.Commands
{
	class AddGroupCommand : IUndoRedoCommand
	{
		
		public List<List<Atom>> groups = new List<List<Atom>>();
		List<Atom> temp = new List<Atom>();
		ObservableCollection<Atom> selectedAtoms = new ObservableCollection<Atom>();

		public AddGroupCommand(List<List<Atom>> _groups, ObservableCollection<Atom> _selectedAtoms)
		{
			groups = _groups;
			selectedAtoms = _selectedAtoms;
		}

		public void Execute()
		{
			foreach(Atom a in selectedAtoms)
			{
				temp.Add(a);
				//CurrColor = Brushes.Beige;
			}
			groups.Add(temp);
		}
		public void UnExecute()
		{
			groups.Remove(temp);
		}

	}
}
