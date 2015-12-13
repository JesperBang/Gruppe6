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
	class RemoveGroupCommand : IUndoRedoCommand
	{
		
		public List<List<Atom>> groups = new List<List<Atom>>();
		List<Atom> temp = new List<Atom>();
		ObservableCollection<Atom> selectedAtoms = new ObservableCollection<Atom>();
		List<Atom> group;

		public RemoveGroupCommand(List<List<Atom>> _groups, ObservableCollection<Atom> _selectedAtoms, ref int _selectedGroup)
		{
			groups = _groups;
			selectedAtoms = _selectedAtoms;
			_selectedGroup = 0;
		}

		public void Execute()
		{
			if (groups.Count > 0)
			{
			
				 foreach (Atom a in selectedAtoms)
				{
					foreach(List<Atom> g in groups)
					{
						if (g.Contains(a))
						{
							group = g;

							foreach(Atom b in group)
							{
								b.IsSelected = false;
								if (selectedAtoms.Contains(b))
								{
									selectedAtoms.Remove(b);
								}
							}

							goto loop;
						}
					}
				}
			loop: { }

				groups.Remove(group);
				
			}
		}
		public void UnExecute()
		{
			groups.Add(group);
		}

	}
}
