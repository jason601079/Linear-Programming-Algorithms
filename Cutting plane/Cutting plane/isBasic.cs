using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.Cutting_plane
{
    internal class isBasic
    {
        public bool IsBasicVar(List<double> columns) //possible type object for each column
        {
            for (int i = 0; i < columns.Count; i++) {
                if (columns[i] == 0 || columns[i] == 1)
                { return true; }
                else
                {
                    return false;
                }

        }
    }
}
