using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PeriodicSystem.Commands
{
    class MoveAtomCommand : IUndoRedoCommand
    {
        Atom atom;
        double oldX;
        double oldY;
        double newX;
        double newY;

        public MoveAtomCommand(Atom atom, double newX, double newY)
        {
            this.atom = atom;
            this.newX = newX;
            this.newY = newY;

            oldX = atom.X;
            oldY = atom.Y;
        }

        public void execute()
        {
            atom.X += newX;
            atom.Y += newY;
        }

        public void unexecute()
        {
            atom.X = oldX;
            atom.Y = oldY;
        }
    }
}
