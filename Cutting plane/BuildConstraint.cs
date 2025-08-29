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

            for (int i = 0; i < numRows; i++)
            {
                double rhs = _primal.TableauPublic[i, numCols - 1];
                double fracPart = rhs - Math.Floor(rhs);

                if (fracPart > 1e-6) // fractional RHS
                {
                    var cutCoeffs = new List<double>();
                    for (int j = 0; j < numCols - 1; j++)
                    {
                        double val = _primal.TableauPublic[i, j];
                        double coeffFrac = val - Math.Floor(val);
                        cutCoeffs.Add(coeffFrac);
                    }

                    string inequality;


                    // If the sum of fractional parts > 0.5, treat as >= else <=
                    double sumFrac = 0;
                    foreach (var c in cutCoeffs) sumFrac += c;
                    inequality = (sumFrac > 0.5) ? ">=" : "<=";

                    return (cutCoeffs, inequality, fracPart);
                }
            }

            return (null, null, 0);
        }
    }
}
