using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Diagram
    {
        public List<Atom> Atom { get; set; }
        public List<Atom> Atoms { get; set; }
        public List<Binding> Binding { get; set; }
        public List<Binding> Bindings { get; set; }
    }
}
