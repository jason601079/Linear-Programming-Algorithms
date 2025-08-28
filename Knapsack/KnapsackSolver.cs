using System;
using System.Collections.Generic;
using System.Linq;

namespace Linear_Programming_Algorithms
{
    public class KnapsackSolver
    {
       
        public (double maxProfit, bool[] taken) Solve(double[] profits, double[] weights, double capacity)
        {
            if (profits == null) throw new ArgumentNullException(nameof(profits));
            if (weights == null) throw new ArgumentNullException(nameof(weights));
            if (profits.Length != weights.Length) throw new ArgumentException("profits and weights must have same length");
            if (capacity < 0) throw new ArgumentException("capacity must be non-negative");

            int n = profits.Length;

            // Build items and sort by profit/weight ratio descending 
            var items = Enumerable.Range(0, n)
                .Select(i => new Item
                {
                    OriginalIndex = i,
                    Profit = profits[i],
                    Weight = weights[i],
                    Ratio = weights[i] == 0 ? double.PositiveInfinity : profits[i] / weights[i]
                })
                .OrderByDescending(it => it.Ratio)
                .ToArray();

            // Upper bound via fractional knapsack on remaining items
            double Bound(Node node)
            {
                if (node.Weight >= capacity) return 0.0;
                double bound = node.Profit;
                double totalW = node.Weight;
                int idx = node.Level + 1;
                while (idx < n && totalW + items[idx].Weight <= capacity)
                {
                    totalW += items[idx].Weight;
                    bound += items[idx].Profit;
                    idx++;
                }

                if (idx < n && items[idx].Weight > 0)
                {
                    double remain = capacity - totalW;
                    bound += items[idx].Ratio * remain;
                }

                return bound;
            }

            // Root
            var root = new Node(n) { Level = -1, Profit = 0.0, Weight = 0.0 };
            root.Bound = Bound(root);

            double bestProfit = 0.0;
            bool[] bestTakenOriginal = new bool[n];

            
            var queue = new List<Node> { root };

            while (queue.Count > 0)
            {
                // extract node with highest bound
                int bestIdx = 0;
                for (int i = 1; i < queue.Count; i++)
                    if (queue[i].Bound > queue[bestIdx].Bound) bestIdx = i;

                var node = queue[bestIdx];
                queue.RemoveAt(bestIdx);

                if (node.Bound <= bestProfit) continue; // cannot improve

                int nextLevel = node.Level + 1;
                if (nextLevel >= n) continue;

                // Branch: take next item
                var take = node.Clone();
                take.Level = nextLevel;
                take.Weight += items[nextLevel].Weight;
                take.Profit += items[nextLevel].Profit;
                take.Taken[nextLevel] = true;

                if (take.Weight <= capacity)
                {
                    // feasible solution
                    if (take.Profit > bestProfit)
                    {
                        bestProfit = take.Profit;
                        bestTakenOriginal = MapTakenToOriginalOrder(take.Taken, items, n);
                    }

                    take.Bound = Bound(take);
                    if (take.Bound > bestProfit) queue.Add(take);
                }

                // Branch: do NOT take next item
                var notTake = node.Clone();
                notTake.Level = nextLevel;
                notTake.Taken[nextLevel] = false;
                notTake.Bound = Bound(notTake);
                if (notTake.Bound > bestProfit) queue.Add(notTake);
            }

            return (bestProfit, bestTakenOriginal);
        }

        
        public (double maxProfit, bool[] taken) Solve(LPData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Objective.Type != ProblemType.Max)
                throw new ArgumentException("Knapsack solver requires a maximization objective (max).");

            if (data.Constraints == null || data.Constraints.Count != 1)
                throw new ArgumentException("Knapsack solver expects exactly one constraint (capacity).");

            var constr = data.Constraints[0];
            if (constr.Relation != Relation.LessOrEqual)
                throw new ArgumentException("Knapsack solver expects the single constraint to be '<=' (LessOrEqual).");

            int n = data.VariableCount;

            // require binary variables
            for (int i = 0; i < n; i++)
            {
                if (data.SignRestrictions[i] != SignRestriction.Binary)
                    throw new ArgumentException($"Variable #{i + 1} must be declared 'bin' (binary) for 0/1 knapsack. Found: {data.SignRestrictions[i]}");
            }

            double[] profits = new double[n];
            double[] weights = new double[n];

            // Objective coefficients -> profits
            for (int i = 0; i < n; i++) profits[i] = data.Objective.Coefficients[i];

            // Constraint coefficients -> weights (single constraint)
            for (int i = 0; i < n; i++) weights[i] = constr.Coefficients[i];

            double capacity = constr.Rhs;

            return Solve(profits, weights, capacity);
        }

        // Parse file path using Data.Parse and solve
        public (double maxProfit, bool[] taken) SolveFromFile(string path)
        {
            var data = LPData.Parse(path);
            return Solve(data);
        }

        
        private static bool[] MapTakenToOriginalOrder(bool[] takenSorted, Item[] sortedItems, int n)
        {
            var result = new bool[n];
            for (int i = 0; i < takenSorted.Length; i++)
            {
                if (takenSorted[i])
                    result[sortedItems[i].OriginalIndex] = true;
            }
            return result;
        }

        private class Item
        {
            public int OriginalIndex;
            public double Profit;
            public double Weight;
            public double Ratio;
        }

        private class Node
        {
            public int Level;      
            public double Profit;
            public double Weight;
            public double Bound;
            public bool[] Taken;

            public Node(int n) { Taken = new bool[n]; }

            public Node Clone()
            {
                var c = new Node(Taken.Length)
                {
                    Level = this.Level,
                    Profit = this.Profit,
                    Weight = this.Weight,
                    Bound = this.Bound
                };
                Array.Copy(this.Taken, c.Taken, Taken.Length);
                return c;
            }
        }
    }
}
