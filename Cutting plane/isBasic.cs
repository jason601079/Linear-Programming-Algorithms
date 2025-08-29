using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.Cutting_plane
{
    internal class isBasic
    {
        public bool IsBasicVar(DataToColumn columns) //possible type object for each column
        {
            int onesCount = 0;
            foreach (var coefficient in columns.column) 
            {
                if (coefficient == 1)
                {
                    onesCount++;
                }
                else if (coefficient != 0) 
                {
                    return false;
                }

            }
        
            return onesCount == 1;
        }

    }
}
