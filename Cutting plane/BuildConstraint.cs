using System;
using System.Collections.Generic;


namespace Linear_Programming_Algorithms.Cutting_plane
{
    public class BuildConstraint
    {
        private Primal _primal;

        public BuildConstraint(Primal primal)
        {
            _primal = primal;
        }

        public (List<double> cutCoeffs, string inequality, double rhsFrac) BuildGomoryCut()
        {
            int numRows = _primal.TableauPublic.GetLength(0) - 1; 
            int numCols = _primal.TableauPublic.GetLength(1);
            int numOriginalVars = _primal.NumVariables;

            double maxFrac = 0;
            int fracRow = -1;

            for (int i = 0; i < numRows; i++)
            {
                double rhs = _primal.TableauPublic[i, numCols - 1];
                double frac = rhs - Math.Floor(rhs);
                if (frac > 0.01 && frac < 0.99)
                {
                    maxFrac = frac;
                    fracRow = i;
                }
            }

            if (fracRow == -1)
                return (null, null, 0); // all integer

            var cutCoeffs = new List<double>();
            for (int j = 0; j < numOriginalVars; j++)
            {
                double val = _primal.TableauPublic[fracRow, j];
                double coeffFrac = val - Math.Floor(val);
                cutCoeffs.Add(coeffFrac);
            }

            return (cutCoeffs, "<=", maxFrac);
        }
    }
}


