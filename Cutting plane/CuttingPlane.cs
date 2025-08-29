using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Linear_Programming_Algorithms.Cutting_plane;
using Org.BouncyCastle.Asn1.X509;

namespace Linear_Programming_Algorithms
{
    public class CuttingPlane
    {
        private readonly Primal _primal;
        private readonly Dual _dual;
        private readonly BuildConstraint _builder;
        private bool _hasFractional = true;
        private int _iteration = 1;

        public bool IsFinished => !_hasFractional;
        public int CurrentIteration => _iteration;

        public CuttingPlane(Primal primal, Dual dual, BuildConstraint builder)
        {
            _primal = primal;
            _dual = dual;
            _builder = builder;
        }

        
        public void Step(ListBox log)
        {
            if (!_hasFractional)
            {
                log.Items.Add("All variables are integer. Cutting plane finished.");
                return;
            }

            _primal.Solve();

            log.Items.Add($"Iteration {_iteration}: Current Tableau:");
            var tableau = _primal.TableauPublic;
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < tableau.GetLength(1); j++)
                    row += tableau[i, j].ToString("F3").PadLeft(10);
                log.Items.Add(row);
            }

            // Build Gomory cut
            var (cutCoeffs, inequality, rhsFrac) = _builder.BuildGomoryCut();
            _hasFractional = cutCoeffs != null;

            if (_hasFractional)
            {
                log.Items.Add("Adding Gomory Cut:");
                string cut = string.Join(" + ", cutCoeffs.Select((v, idx) => $"{v:F3}*x{idx + 1}"));
                log.Items.Add($"{cut} {inequality} {rhsFrac:F3}");

                _primal.AddGomoryCut(cutCoeffs, rhsFrac);

                if (inequality == ">=")
                    _dual.Solve();
                else
                    _primal.Solve();
            }

            _iteration++;
        }

        public (double[] x, double z) GetSolution() => _primal.GetSolution();
    }

}
