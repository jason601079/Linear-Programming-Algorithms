using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms
{
    internal class NonLinear
    {
        //hardcoded values to be used for now
        private double x = 5.0;
        private double learningRate = 0.1;
        private double tolerance = 1e-6;
        private int maxIterations = 1000;
        public bool maximize = true;

        //used to store the final results to display on form
        public double FinalX { get; private set; }
        public double FinalFx { get; private set; }
        public int Iterations { get; private set; }
        public List<string> IterationLog { get; private set; } = new List<string>();

        // function being used is f(x) = -x² + 4x + 10
        private double Function(double x)
        {
            return -x * x + 4 * x + 10;
        }

        //Approximates derivitive
        private double NumericalDerivative(double x, double h = 1e-6)
        {
            return (Function(x + h) - Function(x - h)) / (2 * h);
        }

        public void Solve() {
            int iteration = 0;
            double currentX = x;

            IterationLog.Clear();

            IterationLog.Add($"Initial guess: x = {currentX}, f(x) = {Function(currentX)}");

            while (iteration < maxIterations)
            {
                double grad = NumericalDerivative(currentX);
                double newX = maximize ? currentX + learningRate * grad : currentX - learningRate * grad;// if it is a max problem add learning rate to x 
                //if it is a min problem subtract learning rate from x

                // adds the iteration count , new calculated x and new value for the function to a List  
                IterationLog.Add($"Iteration {iteration + 1}: x = {newX:F6}, f(x) = {Function(newX):F6}");

                // checks that the answer is close enough to an optimum and itering again will not make any changes
                if (Math.Abs(newX - currentX) < tolerance)
                {
                    currentX = newX;
                    break;
                }

                currentX = newX;
                iteration++;
            }

            FinalX = currentX;
            FinalFx = Function(currentX);
            Iterations = iteration;

        }

    }
}
