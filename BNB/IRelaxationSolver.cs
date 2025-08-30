using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.BNB
{
    internal interface IRelaxationSolver
    {
        RelaxationResult Solve(
           double[,] A, double[] b, double[] c,
           IReadOnlyList<VarBound> extraBounds, // add x_i >= L, x_i <= U
           bool isMax);
    }
}
