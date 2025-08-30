using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.BNB
{
    internal sealed class VarBound
    {
        public int Index;        
        public double? Lower;    
        public double? Upper;    
        public VarBound(int index, double? lower = null, double? upper = null)
        {
            Index = index; Lower = lower; Upper = upper;
        }
        public VarBound Clone() => new VarBound(Index, Lower, Upper);
    }

    internal enum NodeStatus { Unknown, Infeasible, Fractional, IntegerFeasible, Pruned }

    internal sealed class BnBNode
    {
        public int Id;
        public int Depth;
        public List<VarBound> Bounds = new List<VarBound>();
        public double[] X;           
        public double Z;           
        public NodeStatus Status = NodeStatus.Unknown;
        public int BranchedVar = -1;  

        public BnBNode(int id, int depth)
        {
            Id = id; Depth = depth;
        }

        public BnBNode CloneChild(int newId)
        {
            var c = new BnBNode(newId, Depth + 1);
            foreach (var b in Bounds) c.Bounds.Add(b.Clone());
            return c;
        }

        public override string ToString()
            => $"Node {Id} (depth {Depth}) z={Z:F3} status={Status}";
    }
}
