using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.Cutting_plane
{
    internal class Variable
    {

        private string name;
        private double coefficient;
        private VariableSignRestriction restriction;
    }
}
