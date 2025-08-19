using System;
using System.Collections.Generic;
using System.Linq;

namespace Linear_Programming_Algorithms
{
   
    public class StepResult
    {
        public List<string> Lines { get; } = new List<string>();
        public bool IsFinished { get; set; } = false;
    }

    
    public class KnapsackResult
    {
        public double MaxProfit { get; set; }
        public bool[] Taken { get; set; }
    }


    public class KnapsackStepper
    {
        
        public bool IsInitialized { get; private set; }
        public bool IsFinished { get; private set; }

        // read-only after Initialize
        private Item[] _items; 
        private int _n;
        private double _capacity;

        // B&B internal state
        private List<Node> _queue;
        private double _bestProfit;
        private bool[] _bestTakenOriginal;
        private int _nodeCounter;

        
        private double _rootBound;

       
        public void Initialize(double[] profits, double[] weights, double capacity)
        {
            if (profits == null) throw new ArgumentNullException(nameof(profits));
            if (weights == null) throw new ArgumentNullException(nameof(weights));
            if (profits.Length != weights.Length) throw new ArgumentException("profits and weights must match.");
            if (capacity < 0) throw new ArgumentException("capacity must be non-negative.");

            _n = profits.Length;
            _capacity = capacity;

            _items = Enumerable.Range(0, _n)
                .Select(i => new Item
                {
                    OriginalIndex = i,
                    Profit = profits[i],
                    Weight = weights[i],
                    Ratio = weights[i] == 0 ? double.PositiveInfinity : profits[i] / weights[i]
                })
                .OrderByDescending(it => it.Ratio)
                .ToArray();

            // Prepare root and queue
            var root = new Node(_n) { Level = -1, Profit = 0.0, Weight = 0.0 };
            root.Bound = Bound(root);
            _rootBound = root.Bound;

            _queue = new List<Node> { root };
            _bestProfit = 0.0;
            _bestTakenOriginal = new bool[_n];
            _nodeCounter = 0;
            IsInitialized = true;
            IsFinished = false;
        }

        
        public (int sortedIndex, int origIndex, double weight, double profit, double ratio)[] GetSortedItemsInfo()
        {
            if (!IsInitialized) throw new InvalidOperationException("Initialize first.");
            return Enumerable.Range(0, _items.Length)
                .Select(i => (sortedIndex: i,
                              origIndex: _items[i].OriginalIndex,
                              weight: _items[i].Weight,
                              profit: _items[i].Profit,
                              ratio: _items[i].Ratio))
                .ToArray();
        }


       
        public void Initialize(LPData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Objective.Type != ProblemType.Max) throw new ArgumentException("Objective must be max.");
            if (data.Constraints == null || data.Constraints.Count != 1) throw new ArgumentException("Expect exactly 1 constraint.");
            var c = data.Constraints[0];
            if (c.Relation != Relation.LessOrEqual) throw new ArgumentException("Constraint must be <=");

            for (int i = 0; i < data.VariableCount; i++)
                if (data.SignRestrictions[i] != SignRestriction.Binary)
                    throw new ArgumentException($"Variable {i + 1} must be 'bin'.");

            var profits = data.Objective.Coefficients;
            var weights = c.Coefficients;
            Initialize(profits, weights, c.Rhs);
        }

        
        public void Reset()
        {
            IsInitialized = false;
            IsFinished = false;
            _items = null;
            _queue = null;
            _bestProfit = 0.0;
            _bestTakenOriginal = null;
            _nodeCounter = 0;
            _n = 0;
            _capacity = 0.0;
        }

       
        public IEnumerable<string> GetInitializationLines()
        {
            if (!IsInitialized) throw new InvalidOperationException("Call Initialize(...) first.");

            yield return "=== Items (sorted by value/weight) ===";
            yield return "Sorted  Orig  Profit  Weight  Ratio";
            for (int i = 0; i < _items.Length; i++)
            {
                // Orig printed 1-based for readability
                yield return string.Format("{0,6}  {1,4}    {2,6:0.##}   {3,6:0.##}   {4,6:0.##}",
                    i, _items[i].OriginalIndex + 1, _items[i].Profit, _items[i].Weight, _items[i].Ratio);
            }
            yield return new string('-', 60);
            yield return $"Capacity: {_capacity:0.##}   RootBound: {_rootBound:0.##}";
            yield return new string('-', 60);
        }

        // Step: perform one loop iteration (pop one node and branch it).
        // When finished (queue empty) returns IsFinished==true with final summary.
        public StepResult Step()
        {
            if (!IsInitialized) throw new InvalidOperationException("Stepper not initialized. Call Initialize(...) first.");
            if (IsFinished)
            {
                var finishedAlready = new StepResult();
                finishedAlready.IsFinished = true;
                finishedAlready.Lines.Add("Stepper already finished.");
                finishedAlready.Lines.Add($"Best profit = {_bestProfit:0.##}");
                finishedAlready.Lines.Add("Taken (original indices): " + (_bestTakenOriginal == null ? "(none)" : string.Join(", ", IndicesFromBoolArray(_bestTakenOriginal))));
                return finishedAlready;
            }

            var res = new StepResult();

            if (_queue == null || _queue.Count == 0)
            {
                // finished
                IsFinished = true;
                res.IsFinished = true;
                res.Lines.Add("No more nodes. Search finished.");
                res.Lines.Add($"Best profit = {_bestProfit:0.##}");
                res.Lines.Add("Taken (original indices): " + (_bestTakenOriginal == null ? "(none)" : string.Join(", ", IndicesFromBoolArray(_bestTakenOriginal))));
                return res;
            }

            // Pop best-bound node
            int bestIdx = 0;
            for (int i = 1; i < _queue.Count; i++)
                if (_queue[i].Bound > _queue[bestIdx].Bound) bestIdx = i;

            var node = _queue[bestIdx];
            _queue.RemoveAt(bestIdx);
            _nodeCounter++;

           
            res.Lines.Add($"[{_nodeCounter}] Node level={node.Level}  —  Profit={node.Profit:0.##}, Weight={node.Weight:0.##}, Bound={node.Bound:0.##}");

            // compute deciding item (subproblem)
            int nextLevel = node.Level + 1;
            if (nextLevel >= _n)
            {
                res.Lines.Add("    Subproblem: no more items to decide at this node.");
                res.Lines.Add($"    Queue size: {_queue.Count}  |  Current best: {_bestProfit:0.##}");
                res.Lines.Add(new string('-', 60));
                return res;
            }

            var deciding = _items[nextLevel];
            res.Lines.Add($"    Subproblem: decide item -> sorted={nextLevel}, orig={deciding.OriginalIndex + 1}, ratio={deciding.Ratio:0.##}");

            // Branch: TAKE
            var take = node.Clone();
            take.Level = nextLevel;
            take.Weight += deciding.Weight;
            take.Profit += deciding.Profit;
            take.Taken[nextLevel] = true;

            // Evaluate TAKE branch
            if (take.Weight <= _capacity)
            {
                // feasible
                bool isNewBest = false;
                if (take.Profit > _bestProfit)
                {
                    _bestProfit = take.Profit;
                    _bestTakenOriginal = MapTakenToOriginalOrder(take.Taken, _items, _n);
                    isNewBest = true;
                }

                take.Bound = Bound(take);
                bool enqueued = take.Bound > _bestProfit;
                string takeAction = isNewBest ? "NEW BEST" : (enqueued ? "enqueued" : "pruned");

                res.Lines.Add($"    [TAKE] Profit={take.Profit:0.##}, Weight={take.Weight:0.##} — feasible — Bound={take.Bound:0.##} — {takeAction}");
                if (enqueued) _queue.Add(take);
            }
            else
            {
                // infeasible
                res.Lines.Add($"    [TAKE] Profit={take.Profit:0.##}, Weight={take.Weight:0.##} — INFEASIBLE (exceeds capacity) — pruned");
            }

            // Branch: SKIP
            var notTake = node.Clone();
            notTake.Level = nextLevel;
            notTake.Taken[nextLevel] = false;
            notTake.Bound = Bound(notTake);
            bool skipEnqueued = notTake.Bound > _bestProfit;
            string skipAction = skipEnqueued ? "enqueued" : "pruned";
            res.Lines.Add($"    [SKIP] Bound={notTake.Bound:0.##} — {skipAction}");
            if (skipEnqueued) _queue.Add(notTake);

            // Summary
            res.Lines.Add($"    Queue size after branching: {_queue.Count}  |  Current best: {_bestProfit:0.##}  |  Taken: " +
                          (_bestTakenOriginal == null ? "(none)" : string.Join(", ", _bestTakenOriginal.Select((t, i) => t ? (i + 1).ToString() : null).Where(x => x != null))));

            res.Lines.Add(new string('-', 60));
            return res;
        }

       
        public KnapsackResult RunAll()
        {
            if (!IsInitialized) throw new InvalidOperationException("Stepper not initialized. Call Initialize(...) first.");

           
            var savedQueue = _queue.Select(n => n.Clone()).ToList();
            var savedBest = _bestProfit;
            var savedBestTaken = _bestTakenOriginal == null ? null : (bool[])_bestTakenOriginal.Clone();
            var savedNodeCounter = _nodeCounter;

            
            while (_queue.Count > 0)
            {
                // pop best
                int bestIdx = 0;
                for (int i = 1; i < _queue.Count; i++)
                    if (_queue[i].Bound > _queue[bestIdx].Bound) bestIdx = i;

                var node = _queue[bestIdx];
                _queue.RemoveAt(bestIdx);

                if (node.Bound <= _bestProfit) continue;

                int nextLevel = node.Level + 1;
                if (nextLevel >= _n) continue;

                // TAKE
                var take = node.Clone();
                take.Level = nextLevel;
                take.Weight += _items[nextLevel].Weight;
                take.Profit += _items[nextLevel].Profit;
                take.Taken[nextLevel] = true;
                if (take.Weight <= _capacity)
                {
                    if (take.Profit > _bestProfit)
                    {
                        _bestProfit = take.Profit;
                        _bestTakenOriginal = MapTakenToOriginalOrder(take.Taken, _items, _n);
                    }
                    take.Bound = Bound(take);
                    if (take.Bound > _bestProfit) _queue.Add(take);
                }

                // SKIP
                var notTake = node.Clone();
                notTake.Level = nextLevel;
                notTake.Taken[nextLevel] = false;
                notTake.Bound = Bound(notTake);
                if (notTake.Bound > _bestProfit) _queue.Add(notTake);
            }

            var result = new KnapsackResult { MaxProfit = _bestProfit, Taken = _bestTakenOriginal };

            // restore saved state so stepping can continue where it left off
            _queue = savedQueue;
            _bestProfit = savedBest;
            _bestTakenOriginal = savedBestTaken;
            _nodeCounter = savedNodeCounter;

            return result;
        }


        private double Bound(Node node)
        {
            if (node.Weight >= _capacity) return 0.0;
            double bound = node.Profit;
            double totalW = node.Weight;
            int idx = node.Level + 1;
            while (idx < _n && totalW + _items[idx].Weight <= _capacity)
            {
                totalW += _items[idx].Weight;
                bound += _items[idx].Profit;
                idx++;
            }
            if (idx < _n && _items[idx].Weight > 0)
            {
                double remain = _capacity - totalW;
                bound += _items[idx].Ratio * remain;
            }
            return bound;
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

        private static IEnumerable<int> IndicesFromBoolArray(bool[] arr)
        {
            if (arr == null) yield break;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i]) yield return i + 1; 
        }

        // Internal types
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

            public Node(int n) { Taken = new bool[n]; Level = -1; }

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
