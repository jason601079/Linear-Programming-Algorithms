using System;
using System.Collections.Generic;
using System.Linq;
using Linear_Programming_Algorithms.Cutting_plane;

namespace Linear_Programming_Algorithms
{
    public class CuttingPlane
    {
        private readonly Primal _primal;
        private readonly Dual _dual;
        private readonly BuildConstraint _builder;

        public CuttingPlane(Primal primal, Dual dual, BuildConstraint builder)
        {
            _primal = primal;
            _dual = dual;
            _builder = builder;
        }

        public void SolveWithCuts()
        {
            bool hasFractional;

            do
            {
                _primal.Solve();

                var (cutCoeffs, inequality, rhsFrac) = _builder.BuildGomoryCut();

                hasFractional = cutCoeffs != null;

                if (hasFractional)
                {

                    _primal.AddGomoryCut(cutCoeffs, rhsFrac, inequality);
                    if (inequality == ">=")
                    {
                        _dual.Solve();
                    }
                    else
                    {
                        _primal.Solve();
                    }
                }

            } while (hasFractional);

            Console.WriteLine("All variables are integer. Cutting plane finished.");
        }
    }
}
