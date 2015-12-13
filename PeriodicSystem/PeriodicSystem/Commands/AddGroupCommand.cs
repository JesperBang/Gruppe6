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
		List<Atom> tempGroup = new List<Atom>();
		ObservableCollection<Atom> selectedAtoms = new ObservableCollection<Atom>();
		List<Atom> tempAtoms = new List<Atom>();

		public AddGroupCommand(List<List<Atom>> _groups, ObservableCollection<Atom> _selectedAtoms)
		{
			groups = _groups;
			selectedAtoms = _selectedAtoms;

			foreach(Atom a in selectedAtoms)
			{
				tempAtoms.Add(a);
			}

		}

		public void Execute()
		{
			foreach(Atom a in tempAtoms)
			{
				tempGroup.Add(a);
				//CurrColor = Brushes.Beige;
			}
			groups.Add(tempGroup);
		}
		public void UnExecute()
		{
			groups.Remove(tempGroup);
			foreach(Atom a in tempGroup)
			{
				a.IsSelected = false;
			}
		}

	}
}
