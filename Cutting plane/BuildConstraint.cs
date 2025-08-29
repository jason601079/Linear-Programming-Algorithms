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

            for (int i = 0; i < numRows; i++)
            {
                double rhs = _primal.TableauPublic[i, numCols - 1];
                double fracPart = rhs - Math.Floor(rhs);

                if (fracPart > 1e-6)
                {
                    var cutCoeffs = new List<double>();
                    for (int j = 0; j < numOriginalVars; j++)
                    {
                        double val = _primal.TableauPublic[i, j];
                        double coeffFrac = val - Math.Floor(val);
                        cutCoeffs.Add(coeffFrac);
                    }

                    return (cutCoeffs, "<=", fracPart);
                }
            }

            return (null, null, 0);
        }



    }
}
