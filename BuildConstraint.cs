using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.Cutting_plane
{
    public class BuildConstraint
    {
        public List<double> GomoryCut(Primal primal)
        {
            var tableau = primal.TableauPublic;
            int numRows = tableau.GetLength(0)-1;
            int numCols = tableau.GetLength(1);
            List<double> cut = new List<double>();

            for (int i = 0; i < numRows; i++)
            {
                double rhs = tableau[i, numCols - 1];
                if (rhs%1 != 0) //check if rhs value is NOT integer
                {
                    for (int j = 0; j < numCols - 1 ; j++)
                    {
                        double fracPart = tableau[i, j] - Math.Floor(tableau[i, j]);
                        cut.Add(fracPart);
                    }

                    cut.Add(rhs - Math.Floor(rhs));
                }

            }
            return cut;

        }
        
    }
        


}
