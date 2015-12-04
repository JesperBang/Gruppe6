using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicSystem.Commands
{
	class duplicateSelectedCommand : IUndoRedoCommand
	{

		private ObservableCollection<Atom> atoms;
		private List<Atom> selectedAtoms;
		private List<Atom> templist = new List<Atom>();

		public duplicateSelectedCommand(ObservableCollection<Atom> _atoms, List<Atom> _selectedAtoms)
		{

			atoms = _atoms;
			selectedAtoms = _selectedAtoms;

		}

		public void Execute()
		{
			foreach(Atom a in selectedAtoms)
			{
				Atom temp = new Atom() { Name = a.Name, Symbol = a.Symbol, Number = a.Number, Weight = a.Weight, Size = a.Size, HitTestVisible = a.HitTestVisible, IsSelected = false, Shells = (int[])a.Shells.Clone(), X = a.X, Y = a.Y };
				//atoms.Add(temp);
				atoms.Insert(0,temp);
				templist.Add(temp);
			}
		}

		public void UnExecute()
		{
			foreach(Atom a in templist)
			{
				atoms.Remove(a);
			}
		}
	}
}
