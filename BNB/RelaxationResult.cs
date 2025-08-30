using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.BNB
{
    internal sealed class RelaxationResult
    {
        public bool Feasible;
        public double[] X;
        public double Z;
        public double[,] OptimalTableau;      
        public System.Collections.Generic.List<double[,]> TableauList; 
    }
}
