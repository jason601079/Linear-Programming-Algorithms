using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linear_Programming_Algorithms;

namespace Linear_Programming_Algorithms.Cutting_plane
{

    public class DataToColumn
    {
        private Primal _primal;
        public List<double> column = new List<double>();

        public DataToColumn(Primal primal)
        {
            _primal = primal;
        }

        public List<double> GetColumn(int colIndex) //transform tableau rows into columns
        {
            
            var tableau = _primal.TableauPublic;

            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                column.Add(tableau[i, colIndex]);
            }

            return column;
        }
    }

}
