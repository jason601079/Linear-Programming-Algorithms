using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.Cutting_plane
{
    public  interface IConstraint
    {
        bool Satisifes(int candidateValue);


    }
            //public list<double> coefficients = new list<double>();
        //public double RHS;


    }
