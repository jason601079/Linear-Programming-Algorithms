using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.Cutting_plane
{
    public class DataToColumn
    {
     
        public List<double> column { get; set; } = new List<double>();

       
        public DataToColumn() { }

        public DataToColumn(IEnumerable<double> values)
        {
            column = new List<double>(values);
        }

        public double this[int index]
        {
            get => column[index];
            set => column[index] = value;
        }
    }
}
