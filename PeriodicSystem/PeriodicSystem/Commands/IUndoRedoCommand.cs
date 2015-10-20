using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicSystem.Commands
{
    interface IUndoRedoCommand
    {
        void execute();
        void unexecute();
    }
}
