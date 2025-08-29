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
        private bool _hasFractional = true;
        private int _iteration = 1;

        public bool IsFinished => !_hasFractional;
        public int CurrentIteration => _iteration;

        public CuttingPlane(Primal primal)
        {
            _primal = primal ?? throw new ArgumentNullException(nameof(primal));
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

            int fracRow = _primal.FindFractionalRow();
            if (fracRow != -1)
            {
                _hasFractional = true;
                log.Items.Add($"Adding Gomory cut from row {fracRow}.");
                _primal.AddGomoryCut(fracRow);

                _primal.Solve();
            }
            else
            {
                _hasFractional = false;
                log.Items.Add("Integer-feasible solution found!");
            }

            _iteration++;
        }


        public (double[] x, double z) GetSolution() => _primal.GetSolution();
    }
   
    }